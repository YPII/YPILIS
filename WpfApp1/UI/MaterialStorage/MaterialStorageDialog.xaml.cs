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

namespace YellowstonePathology.UI.MaterialStorage
{
    /// <summary>
    /// Interaction logic for MaterialStorageDialog.xaml
    /// </summary>
    public partial class MaterialStorageDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private MaterialStorageScanLog m_MaterialStorageScanLog;

        public MaterialStorageDialog()
        {
            this.m_MaterialStorageScanLog = MaterialStorageScanLog.GetAll();
            InitializeComponent();
            //this.DataContext = this;
            this.ListViewItems.ItemsSource = this.m_MaterialStorageScanLog; ;
        }

        internal MaterialStorageScanLog MaterialStorageScanLog
        {
            get { return this.m_MaterialStorageScanLog; }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_MaterialStorageScanLog = MaterialStorageScanLog.GetAll();
            //this.NotifyPropertyChanged(string.Empty);
            this.ListViewItems.ItemsSource = this.m_MaterialStorageScanLog;
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
