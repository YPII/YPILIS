using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.User
{
	[XmlType("SystemUserRoleCollection")]
	public class SystemUserRoleCollection : ObservableCollection<SystemUserRole>
	{        
		public bool IsUserInRole(SystemUserRoleDescriptionEnum systemRole)
		{
			int roleId = (int)systemRole;
			int count = (from sur in this
						 where sur.RoleID == roleId
						 select sur).Count<SystemUserRole>();
			return count > 0;
		}

		public bool IsUserInRole(SystemUserRoleDescriptionList systemUserRoleDescriptionList)
		{
			int count = (from sur in this
						 from r in systemUserRoleDescriptionList
						 where sur.RoleID == (int)r
						 select sur).Count<SystemUserRole>();
			return count > 0;
		}     
	}
}
