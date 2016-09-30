using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Common
{
    /// <summary>
    /// Interaction logic for PathologistSelection.xaml
    /// </summary>
    public partial class PathologistSelectionDialog : Window
    {
		YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;

		public PathologistSelectionDialog(YellowstonePathology.Business.User.SystemUserCollection pathologistUsers)
        {
            this.m_PathologistUsers = pathologistUsers;
            InitializeComponent();
            this.DataContext = this;
        }

		public YellowstonePathology.Business.User.SystemUser SelectedPathologistUser
        {
            get
            {
				YellowstonePathology.Business.User.SystemUser systemUser = null;
                if (this.ListViewPathologistUsers.SelectedItems.Count != 0)
                {
					systemUser = (YellowstonePathology.Business.User.SystemUser)this.ListViewPathologistUsers.SelectedItem;
                }
                return systemUser;
            }
        }

		public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
        {
            get { return this.m_PathologistUsers; }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPathologistUsers.SelectedItems.Count != 0)
            {
                this.DialogResult = true;
            }
            else
            {
                this.DialogResult = false;
            }
            this.Close();
        }
    }
}
