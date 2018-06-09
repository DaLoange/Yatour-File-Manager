namespace Yatour_File_Manager
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_Save = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_target = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button_Up = new System.Windows.Forms.Button();
            this.button_Down = new System.Windows.Forms.Button();
            this.button_Delete = new System.Windows.Forms.Button();
            this.button_SortByName = new System.Windows.Forms.Button();
            this.backgroundWorker_Progress = new System.ComponentModel.BackgroundWorker();
            this.progressBar_Copy = new System.Windows.Forms.ProgressBar();
            this.backgroundWorker_Copy = new System.ComponentModel.BackgroundWorker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Save
            // 
            this.helpProvider1.SetHelpString(this.button_Save, "Saves all Changes. New Files will be copied and from the List deleted Files will " +
        "be deleted in the target-Directory!");
            this.button_Save.Location = new System.Drawing.Point(794, 13);
            this.button_Save.Name = "button_Save";
            this.helpProvider1.SetShowHelp(this.button_Save, true);
            this.button_Save.Size = new System.Drawing.Size(176, 26);
            this.button_Save.TabIndex = 1;
            this.button_Save.Text = "Save and Copy";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 15);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(416, 22);
            this.textBox1.TabIndex = 2;
            // 
            // button_target
            // 
            this.button_target.BackColor = System.Drawing.SystemColors.Control;
            this.helpProvider1.SetHelpString(this.button_target, "Select your Directory!");
            this.button_target.Location = new System.Drawing.Point(435, 13);
            this.button_target.Name = "button_target";
            this.helpProvider1.SetShowHelp(this.button_target, true);
            this.button_target.Size = new System.Drawing.Size(110, 28);
            this.button_target.TabIndex = 3;
            this.button_target.Text = "Select target";
            this.button_target.UseVisualStyleBackColor = false;
            this.button_target.Click += new System.EventHandler(this.Button_target_Click);
            // 
            // button_Up
            // 
            this.helpProvider1.SetHelpString(this.button_Up, "Moves the selected File up.");
            this.button_Up.Location = new System.Drawing.Point(13, 41);
            this.button_Up.Name = "button_Up";
            this.helpProvider1.SetShowHelp(this.button_Up, true);
            this.button_Up.Size = new System.Drawing.Size(35, 35);
            this.button_Up.TabIndex = 4;
            this.button_Up.Text = "▲";
            this.button_Up.UseVisualStyleBackColor = true;
            this.button_Up.Click += new System.EventHandler(this.Button_Up_Click);
            // 
            // button_Down
            // 
            this.helpProvider1.SetHelpString(this.button_Down, "Moves the selected File down.");
            this.button_Down.Location = new System.Drawing.Point(54, 41);
            this.button_Down.Name = "button_Down";
            this.helpProvider1.SetShowHelp(this.button_Down, true);
            this.button_Down.Size = new System.Drawing.Size(35, 35);
            this.button_Down.TabIndex = 5;
            this.button_Down.Text = "▼";
            this.button_Down.UseVisualStyleBackColor = true;
            this.button_Down.Click += new System.EventHandler(this.Button_Down_Click);
            // 
            // button_Delete
            // 
            this.button_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Delete.ForeColor = System.Drawing.Color.Red;
            this.helpProvider1.SetHelpString(this.button_Delete, "Deletes the selected File from the selected List.");
            this.button_Delete.Location = new System.Drawing.Point(95, 41);
            this.button_Delete.Name = "button_Delete";
            this.helpProvider1.SetShowHelp(this.button_Delete, true);
            this.button_Delete.Size = new System.Drawing.Size(35, 35);
            this.button_Delete.TabIndex = 6;
            this.button_Delete.Text = "❌";
            this.button_Delete.UseVisualStyleBackColor = true;
            this.button_Delete.Click += new System.EventHandler(this.Button_Delete_Click);
            // 
            // button_SortByName
            // 
            this.helpProvider1.SetHelpKeyword(this.button_SortByName, "Sort all Files in the List by Name.");
            this.button_SortByName.Location = new System.Drawing.Point(136, 41);
            this.button_SortByName.Name = "button_SortByName";
            this.helpProvider1.SetShowHelp(this.button_SortByName, true);
            this.button_SortByName.Size = new System.Drawing.Size(120, 35);
            this.button_SortByName.TabIndex = 7;
            this.button_SortByName.Text = "Sort by Name";
            this.button_SortByName.UseVisualStyleBackColor = true;
            this.button_SortByName.Click += new System.EventHandler(this.Button_SortByName_Click);
            // 
            // backgroundWorker_Progress
            // 
            this.backgroundWorker_Progress.WorkerReportsProgress = true;
            this.backgroundWorker_Progress.WorkerSupportsCancellation = true;
            this.backgroundWorker_Progress.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_Progress_DoWork);
            this.backgroundWorker_Progress.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_Progress_ProgressChanged);
            // 
            // progressBar_Copy
            // 
            this.helpProvider1.SetHelpString(this.progressBar_Copy, "Shows the progress when you click \"Save and Copy\".");
            this.progressBar_Copy.Location = new System.Drawing.Point(471, 45);
            this.progressBar_Copy.Name = "progressBar_Copy";
            this.helpProvider1.SetShowHelp(this.progressBar_Copy, true);
            this.progressBar_Copy.Size = new System.Drawing.Size(499, 31);
            this.progressBar_Copy.TabIndex = 9;
            // 
            // backgroundWorker_Copy
            // 
            this.backgroundWorker_Copy.WorkerReportsProgress = true;
            this.backgroundWorker_Copy.WorkerSupportsCancellation = true;
            this.backgroundWorker_Copy.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_Copy_DoWork);
            this.backgroundWorker_Copy.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_Copy_ProgressChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.AllowDrop = true;
            this.tabControl1.Location = new System.Drawing.Point(12, 82);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(958, 459);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(263, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 10;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(982, 553);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar_Copy);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button_SortByName);
            this.Controls.Add(this.button_Delete);
            this.Controls.Add(this.button_Down);
            this.Controls.Add(this.button_Up);
            this.Controls.Add(this.button_target);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_Save);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.helpProvider1.SetShowHelp(this, false);
            this.Text = "Yatour File Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_target;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button_Up;
        private System.Windows.Forms.Button button_Down;
        private System.Windows.Forms.Button button_Delete;
        private System.Windows.Forms.Button button_SortByName;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Progress;
        private System.Windows.Forms.ProgressBar progressBar_Copy;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Copy;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.HelpProvider helpProvider1;
        private System.Windows.Forms.Label label1;
    }
}

