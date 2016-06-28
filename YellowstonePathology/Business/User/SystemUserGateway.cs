using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.User
{
    public class SystemUserGateway
    {
		public static YellowstonePathology.Business.User.SystemUserCollection GetSystemUserCollection()
        {
			SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select su.UserId, su.Active, su.UserName, su.FirstName, su.LastName, su.Initials, su.Signature, su.DisplayName, su.EmailAddress, su.NationalProviderId, (select sr.* from tblSystemUserRole sr where sr.UserId = su.UserId " +
				"for xml Path('SystemUserRole'), type) [SystemUserRoleCollection] from tblSystemUser su order by su.UserName for xml Path('SystemUser'), root('SystemUserCollection')";
			cmd.CommandType = System.Data.CommandType.Text;
			YellowstonePathology.Business.User.SystemUserCollection systemUserCollection = Persistence.SqlCommandHelper.ExecuteCollectionCommand<YellowstonePathology.Business.User.SystemUserCollection>(cmd);
			return systemUserCollection;
		}		
	}
}
