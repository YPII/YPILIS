using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.User
{
	[PersistentClass("tblSystemRole", "YPIDATA")]
	public class SystemRole : INotifyPropertyChanged
	{
        protected delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

		int m_SystemRoleId;
		string m_RoleName;

		public SystemRole()
		{
		}

		[PersistentPrimaryKeyProperty(true)]
		public int SystemRoleId
		{
			get { return this.m_SystemRoleId; }
			set
			{
				if (this.m_SystemRoleId != value)
				{
					this.m_SystemRoleId = value;
					this.NotifyPropertyChanged("SystemRoleId");
				}
			}
		}

		[PersistentProperty()]
		public string RoleName
		{
			get { return this.m_RoleName; }
			set
			{
				if (this.m_RoleName != value)
				{
					this.m_RoleName = value;
					this.NotifyPropertyChanged("RoleName");
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
