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
using System.IO;
using System.ComponentModel;

namespace YellowstonePathology.UI.Common
{
    /// <summary>
    /// Interaction logic for UserPreferencesList.xaml
    /// </summary>
    public partial class UserPreferencesList : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<YellowstonePathology.Business.User.UserPreference> m_UserPreferenceList;

        public UserPreferencesList()
        {
            this.m_UserPreferenceList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAllUserPreferences();
            InitializeComponent();
            this.DataContext = this;
        }

        public List<YellowstonePathology.Business.User.UserPreference> UserPreferenceList
        {
            get { return this.m_UserPreferenceList; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            UserPreferences dlg = new Common.UserPreferences(null);
            bool? result = dlg.ShowDialog();
            if (result.HasValue && result.Value == true)
            {
                this.m_UserPreferenceList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAllUserPreferences();
                this.NotifyPropertyChanged("UserPreferenceList");
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListViewUserPreferences_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewUserPreferences.SelectedItem != null)
            {
                YellowstonePathology.Business.User.UserPreference userPreference = (YellowstonePathology.Business.User.UserPreference)this.ListViewUserPreferences.SelectedItem;
                UserPreferences dlg = new Common.UserPreferences(userPreference);
                dlg.ShowDialog();

                this.m_UserPreferenceList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAllUserPreferences();
                this.NotifyPropertyChanged("UserPreferenceList");
            }
        }
    }
}
