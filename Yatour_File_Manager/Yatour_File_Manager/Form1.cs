using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yatour_File_Manager
{
    public partial class Form1 : Form
    {
        ListBox[] listBoxes = new ListBox[100];

        public Form1()
        {
            this.AllowDrop = true;
            InitializeComponent();

            textBox1.Text = ReadDatafromTXT();
            folderBrowserDialog1.SelectedPath = textBox1.Text;
            ReadFromTarget();
        }

        private void ReadFromTarget()
        {
            if (Directory.Exists(folderBrowserDialog1.SelectedPath))
            {
                foreach (String dir in Directory.GetDirectories(folderBrowserDialog1.SelectedPath))
                {
                    DirectoryInfo di = new DirectoryInfo(dir);
                    if (di.Name.Contains("CD"))
                    {
                        CreateTabAndCDList(di.Name);

                        foreach (String f in Directory.GetFiles(dir))
                        {
                            FileInfo fi = new FileInfo(f);
                            MyFile mf = new MyFile(f, File.GetCreationTime(f), fi.Length);

                            MyFile.MyFileLists[Convert.ToInt32(new String(di.Name.Where(Char.IsDigit).ToArray()))].Add(mf);
                        }
                    }
                }
                int i = 0;

                foreach (List<MyFile> list in MyFile.MyFileLists)
                {
                    if (list != null)
                    {
                        list.Sort((x, y) => DateTime.Compare(x.CreationTime, y.CreationTime));
                        foreach (MyFile mf in list)
                        {
                            listBoxes[i].Items.Add(mf.Name);
                        }
                    }
                    i++;
                }
            }
            else
            {
                MessageBox.Show("Choose a valid Directory!\nBefor you have to create some Sub-Directories and name them \"CD1\", \"CD2\" up to \"CD99\".", "Info!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Button_target_Click(new object(), new EventArgs());
            }
            
        }

        private void CreateTabAndCDList (String dirName)
        {
            string numerics = new String(dirName.Where(Char.IsDigit).ToArray());
            int i = Convert.ToInt32(numerics);
            
            MyFile.MyFileLists[i] = new List<MyFile>();

            listBoxes[i] = new ListBox();
            helpProvider1.SetHelpString(listBoxes[i], "Drag & Drop your Music-Files or Directories here.\nBut only .mp3- and .wma-Files!");
            listBoxes[i].Name = dirName;

            listBoxes[i].AllowDrop = true;
            listBoxes[i].FormattingEnabled = true;
            listBoxes[i].ItemHeight = 16;
            listBoxes[i].Location = new System.Drawing.Point(6, 10);

            listBoxes[i].BackColor = Color.WhiteSmoke;
            
            listBoxes[i].Height = 340;
            listBoxes[i].Width = 696;
            listBoxes[i].TabIndex = i;
            listBoxes[i].DragDrop += new System.Windows.Forms.DragEventHandler(this.ListBox_CDx_DragDrop);
            listBoxes[i].DragEnter += new System.Windows.Forms.DragEventHandler(this.ListBox_CDx_DragEnter);

            TabPage tp = new TabPage(dirName);

            tp.Controls.Add(listBoxes[i]);
            tp.Location = new System.Drawing.Point(4, 25);
            tp.Name = dirName;
            tp.Padding = new System.Windows.Forms.Padding(0);
            tp.TabIndex = i;
            tp.Text = dirName;
            tp.UseVisualStyleBackColor = true;

            tabControl1.Controls.Add(tp);
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            progressBar_Copy.Value = 0;
            backgroundWorker_Copy.RunWorkerAsync();
            button_Save.Enabled = false;

        }

        private void backgroundWorker_Progress_DoWork(object sender, DoWorkEventArgs e)
        {
            long size = 0;
            long maxSize = 0;

            foreach(List<MyFile> list in MyFile.MyFileLists)
            {
                if (list != null)
                {
                    foreach (MyFile mf in list)
                    {
                        maxSize += mf.Size;
                    }
                }
            }

            int rounds = 0;
            long reportedSize = 0;
            while (size < maxSize)
            {
                size = 0;
                foreach (String subDir in Directory.GetDirectories(textBox1.Text))
                {
                    foreach (String file in Directory.GetFiles(subDir))
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(file);
                            size += fi.Length;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                if(reportedSize == size)
                {
                    rounds++;
                }
                else
                {
                    rounds = 0;
                    reportedSize = size;
                }
                if(rounds > 100)
                {
                    MessageBox.Show("Es trat beim kopieren oder löschen ein Fehler auf.\nEs hätten " + maxSize + " kopiert werden sollen.\nEs wurden jedoch nur " + size + " kopiert!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Console.WriteLine("maxSize: " + maxSize);
                Console.WriteLine("Size:    " + size);
                double i = (size*100 / maxSize);
                Thread.Sleep(200);
                if (i > 100)
                    i = 100;
                backgroundWorker_Progress.ReportProgress(Convert.ToInt32(i));
            }
        }

        private void backgroundWorker_Progress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            progressBar_Copy.Value = e.ProgressPercentage;
            // Set the text.
            this.Text = e.ProgressPercentage.ToString() + "%";
            if(e.ProgressPercentage == 100)
                button_Save.Enabled = true;
        }

        private void Button_target_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach(List<MyFile> list in MyFile.MyFileLists)
                {
                    if (list != null)
                        list.Clear();
                }
                foreach(ListBox lb in listBoxes)
                {
                    if (lb != null)
                        lb.Dispose();
                }
                foreach(TabPage tp in tabControl1.TabPages)
                {
                    if (tp != null)
                        tp.Dispose();
                }
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                WriteDatatoTXT();
                ReadFromTarget();
            }
        }
        
        private void Button_Up_Click(object sender, EventArgs e)
        {
            MoveItem(-1);
        }

        private void Button_Down_Click(object sender, EventArgs e)
        {
            MoveItem(+1);
        }

        public void MoveItem(int direction)
        {
            String name = tabControl1.TabPages[tabControl1.SelectedIndex].Name;

            int i = Convert.ToInt32(new String(name.Where(Char.IsDigit).ToArray()));

            // Checking selected item
            if (listBoxes[i].SelectedItem == null || listBoxes[i].SelectedIndex < 0)
                return; // No selected item - nothing to do

            // Calculate new index using move direction
            int newIndex = listBoxes[i].SelectedIndex + direction;

            // Checking bounds of the range
            if (newIndex < 0 || newIndex >= listBoxes[i].Items.Count)
                return; // Index out of range - nothing to do

            object selected = listBoxes[i].SelectedItem;

            // Removing removable element
            listBoxes[i].Items.Remove(selected);
            // Insert it in new position
            listBoxes[i].Items.Insert(newIndex, selected);
            // Restore selection
            listBoxes[i].SetSelected(newIndex, true);
            
        }

        private void ChangeLabelText(int tab)
        {
            if (listBoxes[tab] != null)
            {
                label1.Text = listBoxes[tab].Items.Count + " Items in " + listBoxes[tab].Name;
                if (listBoxes[tab].Items.Count > 99)
                    label1.ForeColor = Color.Red;
                else
                    label1.ForeColor = Color.Black;
            }
        }

        private void Button_Delete_Click(object sender, EventArgs e)
        {   
            String name = tabControl1.TabPages[tabControl1.SelectedIndex].Name;

            int i = Convert.ToInt32(new String(name.Where(Char.IsDigit).ToArray()));
            int index = listBoxes[i].SelectedIndex;
            if (index >= 0)
            {
                listBoxes[i].Items.RemoveAt(index);
                if (index < listBoxes[i].Items.Count)
                    listBoxes[i].SetSelected(index, true);
            }
            ChangeLabelText(i);
        }

        private void Button_SortByName_Click(object sender, EventArgs e)
        {
            String name = tabControl1.TabPages[tabControl1.SelectedIndex].Name;

            int i = Convert.ToInt32(new String(name.Where(Char.IsDigit).ToArray()));

            List<String> temp = new List<String>();

            foreach (String s in listBoxes[i].Items)
            {
                temp.Add(s);
            }
            temp.Sort();
            listBoxes[i].Items.Clear();
            foreach (String s in temp)
            {
                listBoxes[i].Items.Add(s);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, TabControlCancelEventArgs e)
        {
            TabControl tb = (TabControl)sender;
            if (tabControl1.SelectedTab != null)
            {
                int i = Convert.ToInt32(new String(tabControl1.SelectedTab.Name.Where(Char.IsDigit).ToArray()));
                
                ChangeLabelText(i);
            }
        }

        private void ListBox_CDx_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void ListBox_CDx_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            ListBox tb = (ListBox)sender;
            int listBox = Convert.ToInt32(new String(tb.Name.Where(Char.IsDigit).ToArray()));

            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (String str in s)
                AddtoListBox(listBox, str);

            ChangeLabelText(listBox);
        }
        
        private void AddtoListBox(int listBox, String s)
        {
            if (Directory.Exists(s))
            {
                foreach (String subDir in Directory.GetDirectories(s))
                {
                    AddtoListBox(listBox, subDir);
                }
                foreach (String fileStr in Directory.GetFiles(s))
                {
                    AddtoListBox(listBox, fileStr);
                }
            }
            else if (File.Exists(s))
            {
                FileInfo fi = new FileInfo(s);
                if (fi.Extension.Equals(".mp3") || fi.Extension.Equals(".wma"))
                {
                    MyFile mf = new MyFile(s, File.GetCreationTime(s), fi.Length);
                    listBoxes[listBox].Items.Add(mf.Name);
                    
                    MyFile.MyFileLists[listBox].Add(mf);
                }
            }
        }

        private void CopyList(List<MyFile> Source, List<MyFile> Target)
        {
            foreach (MyFile mf in Source)
            {
                Target.Add(mf);
            }
        }

        private void backgroundWorker_Copy_DoWork(object sender, DoWorkEventArgs e)
        {

            //Entferne Titel aus der jeweiligen Liste, welche nicht in der jewweiligen ListBox enthalten sind
            List<MyFile> temp = new List<MyFile>();

            int i = 0;
            foreach(ListBox lb in listBoxes)
            {
                if (lb != null)
                {
                    foreach (String str in lb.Items)
                    {
                        foreach (MyFile mf in MyFile.MyFileLists[i])
                        {
                            if (mf.Name.Equals(str))
                            {
                                temp.Add(mf);
                            }
                        }
                    }
                    MyFile.MyFileLists[i].Clear();
                    CopyList(temp, MyFile.MyFileLists[i]);
                    temp.Clear();
                }
                i++;
            }

            backgroundWorker_Copy.ReportProgress(50);

            //Überprüfen ob Dateien gelöscht werden müssen.
            //Wenn Datei im Ordner vorhanden aber nicht in der Liste, dann löschen
            i = 0;
            foreach (ListBox lb in listBoxes)
            {
                if (lb != null)
                {
                    String path = textBox1.Text + lb.Name;

                    if (Directory.Exists(path))
                    {
                        String[] files = Directory.GetFiles(path);

                        if (files.Length > 0)
                        {
                            foreach (String fstr in files)
                            {
                                //Ist Dateiname in ListBox vorhanden?
                                if (!lb.Items.Contains(fstr.Remove(0, fstr.LastIndexOf("\\") + 1)))
                                {
                                    File.Delete(fstr);
                                }
                            }
                        }
                    }
                }
            }

            //Kopiere alle Titel ins Target bzw. falls bereits vorhanden, passe nur das Datum an.
            i = 0;
            foreach (ListBox lb in listBoxes)
            {
                int hour = 0;
                int minute = 0;

                if (lb != null)
                {
                    foreach (MyFile mf in MyFile.MyFileLists[i])
                    {
                        String target = textBox1.Text + "\\" + lb.Name + "\\" + mf.Name;

                        if (!File.Exists(target))
                            File.Copy(mf.FullPath, target);
                        DateTime ct = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, 0);
                        File.SetCreationTime(target, ct);
                        minute++;
                        if (minute == 60)
                        {
                            hour++;
                            minute = 0;
                        }
                    }
                }
                i++;
            }            
        }

        private void backgroundWorker_Copy_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            backgroundWorker_Progress.RunWorkerAsync();
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Delete:
                    this.Button_Delete_Click(this, null);
                    e.Handled = true;
                    break;
                case Keys.Up:
                    this.Button_Up_Click(this, null);
                    e.Handled = true;
                    break;
                case Keys.Down:
                    this.Button_Down_Click(this, null);
                    e.Handled = true;
                    break;
            }
        }

        private void WriteDatatoTXT()
        {
            String configFile = Directory.GetCurrentDirectory() + "\\Yatour_Config.txt";

            try
            {
                if (!File.Exists(configFile))
                    File.Create(configFile);

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(configFile);

                //Write a line of text
                sw.WriteLine(@textBox1.Text);

                //Write a second line of text
                //sw.WriteLine("From the StreamWriter class");

                //Close the file
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }

        private String ReadDatafromTXT()
        {
            String line = String.Empty;

            String configFile = Directory.GetCurrentDirectory() + "\\Yatour_Config.txt";
            if (File.Exists(configFile))
            {
                try
                {
                    //Pass the file path and file name to the StreamReader constructor
                    StreamReader sr = new StreamReader(configFile);

                    //Read the first line of text
                    line = sr.ReadLine();
                    
                    //close the file
                    sr.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
            else
            {
                try
                {
                    File.Create(configFile);
                }
                catch (Exception)
                {
                    MessageBox.Show("Can't create Yatour_Config.txt.\nThe Path to your Target will not be saved.\nNext time you have to choose it again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return line;
        }
        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            backgroundWorker_Progress.CancelAsync();
            backgroundWorker_Copy.CancelAsync();
        }
    }
}
