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
    /// Interaction logic for SystemUserDialog.xaml
    /// </summary>
    public partial class SystemUserDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.User.SystemUserCollection m_SystemUserCollection;
        private YellowstonePathology.Business.User.SystemUser m_SystemUser;
        private YellowstonePathology.Business.User.SystemRoleViewCollection m_SystemRoleViewCollection;

        public SystemUserDialog(YellowstonePathology.Business.User.SystemUser systemUser)
        {
            this.m_SystemUserCollection = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection;
            if(systemUser == null)
            {
                YellowstonePathology.Business.User.SystemUser user = this.m_SystemUserCollection.OrderByDescending(item => item.UserId).First();
                this.m_SystemUser = new Business.User.SystemUser();
                this.m_SystemUser.UserId = user.UserId + 1;
            }
            else
            {
                this.m_SystemUser = Business.Persistence.DocumentGateway.Instance.PullSystemUser(systemUser.UserId, this);
            }

            this.m_SystemRoleViewCollection = new Business.User.SystemRoleViewCollection(this.m_SystemUser);

            InitializeComponent();
            this.DataContext = this;
            this.Closed += SystemUserDialog_Closed;
        }

        private void SystemUserDialog_Closed(object sender, EventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
            YellowstonePathology.Business.User.SystemUserCollectionInstance.Refresh();
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
            set
            {
                this.m_SystemUserCollection = value;
                NotifyPropertyChanged("SystemUserCollection");
            }
        }

        public YellowstonePathology.Business.User.SystemUser SystemUser
        {
            get { return this.m_SystemUser; }
            set
            {
                this.m_SystemUser = value;
                NotifyPropertyChanged("SystemUser");
            }
        }

        public YellowstonePathology.Business.User.SystemRoleViewCollection SystemRoleViewCollection
        {
            get { return this.m_SystemRoleViewCollection; }
        }

        private bool CanSave()
        {
            bool result = true;
            if (string.IsNullOrEmpty(this.m_SystemUser.UserName) == true)
            {
                result = false;
            }
            else
            {
                foreach(YellowstonePathology.Business.User.SystemUser user in this.m_SystemUserCollection)
                {
                    if(user.UserId != this.m_SystemUser.UserId && user.UserName == this.m_SystemUser.UserName)
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.CanSave() == true)
            {
                YellowstonePathology.Business.User.SystemUser user = this.m_SystemUserCollection.SingleOrDefault(item => item.UserId == this.m_SystemUser.UserId);
                if(user == null)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_SystemUser, this);
                }
                this.SetUserRoles();
                this.Close();
            }
            else
            {
                MessageBox.Show("The User must have a User Name.");
            }
        }

        private void SetUserRoles()
        {
            int systemUserRoleId = Business.User.SystemUserGateway.GetMaxSystemUserRoleId();
            foreach(YellowstonePathology.Business.User.SystemRoleView roleView in this.m_SystemRoleViewCollection)
            {
                if(roleView.IsAMember == true)
                {
                    YellowstonePathology.Business.User.SystemUserRole role = this.m_SystemUser.SystemUserRoleCollection.SingleOrDefault(item => item.RoleID == roleView.SystemRole.SystemRoleId);
                    if(role == null)
                    {
                        systemUserRoleId += 1;
                        YellowstonePathology.Business.User.SystemUserRole roleToAdd = new Business.User.SystemUserRole();
                        roleToAdd.SystemUserRoleId = systemUserRoleId;
                        roleToAdd.RoleID = roleView.SystemRole.SystemRoleId;
                        roleToAdd.UserID = this.m_SystemUser.UserId;
                        this.m_SystemUser.SystemUserRoleCollection.Add(roleToAdd);
                    }
                }
                else
                {
                    YellowstonePathology.Business.User.SystemUserRole role = this.m_SystemUser.SystemUserRoleCollection.SingleOrDefault(item => item.RoleID == roleView.SystemRole.SystemRoleId);
                    if(role != null)
                    {
                        this.m_SystemUser.SystemUserRoleCollection.Remove(role);
                    }
                }
            }
        }
    }
}
