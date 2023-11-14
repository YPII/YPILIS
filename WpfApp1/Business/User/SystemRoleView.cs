using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace YellowstonePathology.Business.User
{
    public class SystemRoleView
    {
        private SystemRole m_SystemRole;
        private bool m_IsAMember;
        private string m_RoleName;
        public SystemRoleView()
        { }

        public SystemRoleView(SystemRole role)
        {
            this.m_SystemRole = role;
        }

        public SystemRole SystemRole
        {
            get { return this.m_SystemRole; }
        }

        public bool IsAMember
        {
            get { return this.m_IsAMember; }
            set
            {
                this.m_IsAMember = value;
            }
        }

        public string RoleName
        {
            get { return this.m_RoleName; }
            set
            {
                this.m_RoleName = value;
            }
        }
    }
}
