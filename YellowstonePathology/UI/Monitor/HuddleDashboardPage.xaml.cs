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

        public HuddleDashboardPage()
        {                        
            InitializeComponent();
            this.DataContext = this;
        }                

        public void Refresh()
        {
            
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
