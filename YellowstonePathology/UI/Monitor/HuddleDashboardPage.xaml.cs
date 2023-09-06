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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.WebServices.Data;


namespace YellowstonePathology.UI.Monitor
{    
    public partial class HuddleDashboardPage : UserControl, INotifyPropertyChanged, IMonitorPage
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private HuddleDashboard m_HuddleDashboard;               
        private bool m_AllowEdits;

        public HuddleDashboardPage(bool allowEdits)
        {
            this.m_AllowEdits = allowEdits;                      
            this.m_HuddleDashboard = HuddleDashboard.Load();
            InitializeComponent();
            this.DataContext = this;            
        }                   

        public void Refresh()
        {
            this.m_HuddleDashboard = HuddleDashboard.Load();
            this.NotifyPropertyChanged(string.Empty);
        } 

        public void Save()
        {
            if(this.m_AllowEdits == true)
            {
                this.m_HuddleDashboard.Save();
            }            
        }
        
        public HuddleDashboard HuddleDashboard
        {
            get { return m_HuddleDashboard; }
        }
        
        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void MenuItemSave_Click(object sender, RoutedEventArgs e)
        {
            this.m_HuddleDashboard.Save();
        }

        private void MenuItemAddPathologist(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            string[] lines = this.m_HuddleDashboard.Pathologist.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            List<string> list = lines.ToList<string>();
            list.Add(menuItem.Header.ToString());
            this.m_HuddleDashboard.Pathologist = String.Join("\r\n", list);
        }
    }
}
