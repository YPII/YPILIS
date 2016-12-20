using System;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.User
{
    public class SystemUserGateway
    {
		public static YellowstonePathology.Business.User.SystemUserCollection GetSystemUserCollection()
        {
            Type t = typeof(YellowstonePathology.Business.User.SystemUserCollection);
            ConstructorInfo ci = t.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[0], null);
            YellowstonePathology.Business.User.SystemUserCollection systemUserCollection = (YellowstonePathology.Business.User.SystemUserCollection)ci.Invoke(null);

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select UserId, Active, UserName, FirstName, LastName, Initials, Signature, DisplayName, " +
                "EmailAddress, NationalProviderId from tblSystemUser order by UserName; " +
                "select * from tblSystemUserRole;";
            cmd.CommandType = System.Data.CommandType.Text;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.User.SystemUser systemUser = new SystemUser();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(systemUser, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        systemUserCollection.Add(systemUser);
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.User.SystemUserRole systemUserRole = new SystemUserRole();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(systemUserRole, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach (SystemUser systemUser in systemUserCollection)
                        {
                            if (systemUser.UserId == systemUserRole.UserID)
                            {
                                systemUser.SystemUserRoleCollection.Add(systemUserRole);
                                break;
                            }
                        }
                    }
                }
            }
            return systemUserCollection;
		}
    }
}
