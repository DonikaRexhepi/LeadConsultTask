using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LeadConsultTask
{
    public class FileReader
    {
        private string FilePath { get; set; }
        public FileReader(string filePath)
        {
            FilePath = filePath;
        }
        public string[] ReadFile()
        {
            return  File.ReadAllLines(FilePath);
        }
    }
}
