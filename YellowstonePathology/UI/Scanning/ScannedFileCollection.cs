using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Scanning
{
    public class ScannedFileCollection : ObservableCollection<ScannedFile>
    {
        protected string m_FilePath;

        public ScannedFileCollection()
        {
            
        }

        public virtual void LoadFiles(string filePath)
        {            
            this.m_FilePath = filePath;
            string [] files = System.IO.Directory.GetFiles(this.m_FilePath);
            foreach (string file in files)
            {
                string extension = System.IO.Path.GetExtension(file);
                if (extension.ToUpper() == ".TIF" || extension.ToUpper() == ".JPG")
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                    ScannedFile newfile = new ScannedFile();
                    newfile.Name = file;
                    newfile.ReportNo = "NULL";
                    newfile.FileSize = Convert.ToInt32(fileInfo.Length);
                    newfile.FileDate = fileInfo.CreationTime;
                    this.Add(newfile);
                }
            }
        }        
    }
}
