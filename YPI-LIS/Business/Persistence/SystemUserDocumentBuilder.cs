using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    class SystemUserDocumentBuilder : DocumentBuilder
    {
        MySqlCommand m_SQLCommand;

        public SystemUserDocumentBuilder(int userId)
        {
            this.m_SQLCommand = new MySqlCommand("select UserId, Active, UserName, FirstName, MiddleInitial, LastName, Initials, Signature, DisplayName, " +
                "EmailAddress, NationalProviderId from tblSystemUser where UserId = @UserId; " +
                "select * from tblSystemUserRole where UserID = @UserId;");
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.AddWithValue("@UserId", userId);
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.User.SystemUser user = new User.SystemUser();
            this.Build(user);
            return user;
        }

        private void Build(YellowstonePathology.Business.User.SystemUser user)
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (MySqlDataReader dr = this.m_SQLCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(user, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.User.SystemUserRole systemUserRole = new User.SystemUserRole();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(systemUserRole, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        user.SystemUserRoleCollection.Add(systemUserRole);
                    }
                }
            }
        }
    }
}
