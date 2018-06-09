using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatour_File_Manager
{
    class MyFile
    {
        public String FullPath { get; set; }
        public String Name { get; set; }
        public DateTime CreationTime { get; set; }
        public long Size { get; set; }
        public static List<MyFile>[] MyFileLists = new List<MyFile>[100];
        
        public MyFile(String fullPath, DateTime creationTime, long size)
        {
            FullPath = fullPath;
            CreationTime = creationTime;
            Size = size;
            Name = fullPath.Remove(0, fullPath.LastIndexOf('\\') + 1);
        }
    }
}
