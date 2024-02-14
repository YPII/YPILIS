using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace YellowstonePathology.Policy
{
    /// <summary>
    /// Interaction logic for PolicyExplorer.xaml
    /// </summary>
    public partial class PolicyExplorer : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private DirectoryCollection m_Directories;      

        public PolicyExplorer()
        {
            this.DoIt();
            InitializeComponent();
            this.DataContext = this;
        }      
        
        public DirectoryCollection Directories
        {
            get { return this.m_Directories; }
        }  

        private void ContextMenuAddDirectory_Click(object sender, RoutedEventArgs e)
        {
            if(this.TreeViewDirectories.SelectedItem != null)
            {
                Directory selectedDirectory = (Directory)this.TreeViewDirectories.SelectedItem;
                Directory newDirectory = new Directory("New Directory", selectedDirectory.Path);
                EditDirectoryDialog addDirectoryDialog = new EditDirectoryDialog(newDirectory, selectedDirectory, true);
                addDirectoryDialog.Finished += AddDirectoryDialog_Finished;
                addDirectoryDialog.Show();
            }
        }

        private void ButtonDoIt_Click(object sender, RoutedEventArgs e)
        {
            this.PubSubPub();
        }

        private async void PubSubPub()
        {
            //await IPFS.PubSubPub("cow", "jumped");
            await IPFS.PubSubSub("cow");
        }

        private void AddDirectoryDialog_Finished(object sender, EventArgs e)
        {
            this.NotifyPropertyChanged("Directories");
        }

        private async void AddDirectory(string path)
        {
            await IPFS.FilesMkdir(path);
            this.m_Directories = await DirectoryCollection.Build();
            this.NotifyPropertyChanged("Directories");
        }

        private async void RemoveDirectory(string path)
        {
            await IPFS.FilesMkdir(path);
        }

        private async void DoIt()
        {
            //string fileName = @"c:\testing\phoneprefix.txt";
            //JObject result = await IPFS.AddAsync(fileName);
            //Console.WriteLine(result["Hash"]);            
            //JObject result = await IPFS.FilesLs("/policy_chain/one");
            
            this.m_Directories = await DirectoryCollection.Build();
            this.NotifyPropertyChanged("Directories");
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }        
    }
}
