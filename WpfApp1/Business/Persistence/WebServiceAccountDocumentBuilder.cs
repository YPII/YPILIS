using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class WebServiceAccountDocumentBuilder :DocumentBuilder
    {
        private MySqlCommand m_SQLCommand;

        public WebServiceAccountDocumentBuilder(int webServiceAccountId)
        {
            this.m_SQLCommand = new MySqlCommand();
            this.m_SQLCommand.CommandText = "Select * from tblWebServiceAccount where WebServiceAccountId = @WebServiceAccountId; " +
                "SELECT w.*, c.ClientName from tblWebServiceAccountClient w join tblClient c on w.ClientId = c.ClientId where " +
                "w.WebServiceAccountId = @WebServiceAccountId;";
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.AddWithValue("@WebServiceAccountId", webServiceAccountId);
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.WebService.WebServiceAccount webServiceAccount = new WebService.WebServiceAccount();
            this.BuildWebServiceAccount(webServiceAccount);
            return webServiceAccount;
        }

        private void BuildWebServiceAccount(YellowstonePathology.Business.WebService.WebServiceAccount webServiceAccount)
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                this.m_SQLCommand.Connection = cn;
                using (MySqlDataReader dr = this.m_SQLCommand.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    while (dr.Read())
                    {
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(webServiceAccount, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                    if (dr.IsClosed == false)
                    {
                        dr.NextResult();
                        while (dr.Read())
                        {
                            YellowstonePathology.Business.WebService.WebServiceAccountClient webServiceAccountClient = new YellowstonePathology.Business.WebService.WebServiceAccountClient();
                            Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(webServiceAccountClient, dr);
                            sqlDataReaderPropertyWriter.WriteProperties();
                            webServiceAccountClient.ClientName = dr["ClientName"].ToString();
                            webServiceAccount.WebServiceAccountClientCollection.Add(webServiceAccountClient);
                        }
                    }
                }
            }
        }
    }
}
