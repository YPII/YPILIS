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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for SystemUserListDialog.xaml
    /// </summary>
    public partial class SystemUserListDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.User.SystemUserCollection m_SystemUserCollection;

        public SystemUserListDialog()
        {
            this.m_SystemUserCollection = Business.User.SystemUserGateway.GetSystemUserCollection();
            InitializeComponent();
            DataContext = this;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.User.SystemUserCollection SystemUserCollection
        {
            get { return this.m_SystemUserCollection; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonNewUser_Click(object sender, RoutedEventArgs e)
        {
            SystemUserDialog dlg = new UI.SystemUserDialog(null);
            dlg.ShowDialog();
            this.m_SystemUserCollection = Business.User.SystemUserGateway.GetSystemUserCollection();
            this.NotifyPropertyChanged("SystemUserCollection");
        }

        private void ListBoxSystemUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListBoxSystemUsers.SelectedItem != null)
            {
                YellowstonePathology.Business.User.SystemUser user = (YellowstonePathology.Business.User.SystemUser)this.ListBoxSystemUsers.SelectedItem;
                SystemUserDialog dlg = new UI.SystemUserDialog(user);
                dlg.ShowDialog();
                this.m_SystemUserCollection = Business.User.SystemUserGateway.GetSystemUserCollection();
                this.NotifyPropertyChanged("SystemUserCollection");
            }
        }
    }
}
