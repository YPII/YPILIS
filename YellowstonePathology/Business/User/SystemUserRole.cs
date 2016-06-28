using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.User
{
	[XmlType("SystemUserRole")]
	[PersistentClass("tblSystemUserRole", "YPIDATA")]
	public class SystemUserRole : INotifyPropertyChanged
	{
        protected delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

		int m_SystemUserRoleId;
		int m_UserId;
		int m_RoleId;

		public SystemUserRole()
		{
		}

		[PersistentPrimaryKeyProperty(true)]
		public int SystemUserRoleID
		{
			get { return this.m_SystemUserRoleId; }
			set
			{
				if (this.m_SystemUserRoleId != value)
				{
					this.m_SystemUserRoleId = value;
					this.NotifyPropertyChanged("SystemUserRoleId");
				}
			}
		}

		[PersistentProperty()]
		public int UserID
		{
			get { return this.m_UserId; }
			set
			{
				if (this.m_UserId != value)
				{
					this.m_UserId = value;
					this.NotifyPropertyChanged("UserId");
				}
			}
		}

		public int RoleID
		{
			get { return this.m_RoleId; }
			set
			{
				if (this.m_RoleId != value)
				{
					this.m_RoleId = value;
					this.NotifyPropertyChanged("RoleId");
				}
			}
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
