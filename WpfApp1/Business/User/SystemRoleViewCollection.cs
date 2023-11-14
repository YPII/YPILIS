using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.User
{
    public class SystemRoleViewCollection : ObservableCollection<SystemRoleView>
    {
        private SystemUser m_SystenUser;
        private SystemRoleCollection m_SystemRoleCollection;
        public SystemRoleViewCollection(SystemUser systemUser)
        {
            this.m_SystenUser = systemUser;
            this.m_SystemRoleCollection = SystemUserGateway.GetAllSystemRoles();
            foreach (SystemRole role in this.m_SystemRoleCollection)
            {
                SystemRoleView roleView = new User.SystemRoleView(role);
                foreach (SystemUserRole userRole in this.m_SystenUser.SystemUserRoleCollection)
                {
                    if(userRole.RoleID == role.SystemRoleId)
                    {
                        roleView.IsAMember = true;
                        break;
                    }
                }
                this.Add(roleView);
            }            
        }
    }
}
