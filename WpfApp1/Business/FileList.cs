using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace YellowstonePathology.Business
{
    public class FileList : ObservableCollection<FileListItem>
    {
        private string m_DirectoryName;

        public FileList()
        {

        }

        public FileList(string directoryName, string filter)
        {
            this.m_DirectoryName = directoryName;
            string[] files = Directory.GetFiles(directoryName);
            foreach (string file in files)
            {
                string[] slashSplit = file.Split('\\');
                string[] dotSplit = slashSplit[slashSplit.Length - 1].Split('.');

                if (dotSplit[dotSplit.Length - 1] == filter)
                {
                    FileListItem item = new FileListItem(file);                    
                    this.Add(item);
                }
            }            
        }

        public FileList(string directoryName)
        {
            this.m_DirectoryName = directoryName;
            string[] files = Directory.GetFiles(directoryName);            
            foreach (string file in files)
            {
                FileListItem item = new FileListItem(file);                
                this.Add(item);
            }            
        }

        public void LoadDirectory(string directoryName)
        {            
            this.m_DirectoryName = directoryName;
            string[] files = Directory.GetFiles(directoryName);
            foreach (string file in files)
            {
                FileListItem item = new FileListItem(file);                
                this.Add(item);
            }            
        }

        public virtual void Refresh()
        {
            this.ClearItems();
            string[] files = Directory.GetFiles(this.m_DirectoryName);
            foreach (string file in files)
            {
                FileListItem item = new FileListItem(file);
                this.Add(item);
            }            
        }

        public static void OpenFile(FileListItem item)
        {            
            Process p1 = new Process();
            p1.StartInfo = new ProcessStartInfo(item.FullPath);
            p1.Start();
        }
    }    
}
