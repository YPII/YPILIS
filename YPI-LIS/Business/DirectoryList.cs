using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace YellowstonePathology.Business
{
    public class DirectoryList : ObservableCollection<DirectoryListItem>
    {
        public DirectoryList()
        {
            
        }

        public void FillWithExclusion(string path, string exclusionExtension)
        {
            this.Clear();
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {
                string[] slashSplit = folder.Split('\\');
                string[] dotSplit = slashSplit[slashSplit.Length - 1].Split('.');
                
                if (dotSplit[dotSplit.Length - 1] != exclusionExtension)
                {
                    DirectoryListItem item = new DirectoryListItem();
                    item.FolderName = folder;
                    this.Add(item);
                }                
            }
        }

        public void Fill(string path)
        {
            this.Clear();
            string[] folders = Directory.GetDirectories(path);
            foreach (string folder in folders)
            {                
                DirectoryListItem item = new DirectoryListItem();
                item.FolderName = folder;
                this.Add(item);                
            }
        }        
    }

    public class DirectoryListItem : ListItem
    {
        string m_FolderName;

        public DirectoryListItem()
        {

        }

        public string FolderName
        {
            get { return this.m_FolderName; }
            set { this.m_FolderName = value; }
        }
    }
}
