using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace YellowstonePathology.Business
{ 
    public class FileListItem
    {        
        string m_FullPath = string.Empty;        
        string m_FullName = string.Empty;
        string m_Name = string.Empty;
        string m_Extension = string.Empty;
        DateTime m_ModifiedDate;

        public FileListItem()
        {

        }

        public FileListItem(string fullPath)
        {
            this.m_FullPath = fullPath;
            this.m_ModifiedDate = File.GetLastWriteTime(fullPath);
        }

        public string Path
        {
            get 
            {
                string[] slashSplit = this.m_FullPath.Split('\\');
                string path = this.FullPath.Replace(slashSplit[slashSplit.Length - 1], "");
                return path;
            }            
        }

        public string FileNameWithoutExtension
        {
            get
            {
                string [] dotSplit = this.FullName.Split('.');
                return dotSplit[0];
            }
        }

        public string FullPath
        {
            get { return this.m_FullPath; }
            set { this.m_FullPath = value; }
        }

        public string FullName
        {
            get
            {
                string[] slashSplit = this.m_FullPath.Split('\\');
                return slashSplit[slashSplit.Length - 1];
            }
        }

        public DateTime ModifiedDate
        {
            get { return this.m_ModifiedDate; }
            set { this.m_ModifiedDate = value; }
        }
        
    }
}
