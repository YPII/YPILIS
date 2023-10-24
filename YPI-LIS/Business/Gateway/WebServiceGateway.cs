using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class WebServiceGateway
    {

        public static WebService.WebServiceAccountCollection GetWebServiceAccounts()
        {
            WebService.WebServiceAccountCollection result = new WebService.WebServiceAccountCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblWebServiceAccount;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        WebService.WebServiceAccount webServiceAccount = new WebService.WebServiceAccount();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlServerDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(webServiceAccount, dr);
                        sqlServerDataReaderPropertyWriter.WriteProperties();
                        result.Add(webServiceAccount);
                    }
                }
            }

            return result;
        }

        public static List<WebService.WebServiceAccountView> GetWebServiceAccountViewList()
        {
            List<WebService.WebServiceAccountView> result = new List<WebService.WebServiceAccountView>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select c.ClientName, w.WebServiceAccountId, w.PrimaryClientId, w.DisplayName, w.InitialPage from  tblWebServiceAccount w  left outer join tblClient c on w.PrimaryClientId = c.ClientId order by w.DisplayName;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        WebService.WebServiceAccountView webServiceAccountView = new WebService.WebServiceAccountView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlServerDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(webServiceAccountView, dr);
                        sqlServerDataReaderPropertyWriter.WriteProperties();
                        result.Add(webServiceAccountView);
                    }
                }
            }

            return result;
        }

        public static int GetNextWebServiceAccountId()
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select max(WebServiceAccountId) + 1 id from tblWebServiceAccount;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = (int)((Int64)dr[0]);
                    }
                }
            }
            return result;
        }

        public static int GetNextWebServiceAccountClientId()
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select max(WebServiceAccountClientId) + 1 id from tblWebServiceAccountClient;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = (int)((Int64)dr[0]);
                    }
                }
            }
            return result;
        }

        public static List<WebService.WebServiceClientView> GetWebServiceClientViews()
        {
            List<WebService.WebServiceClientView> result = new List<WebService.WebServiceClientView>();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select ClientId, ClientName from tblClient Order By ClientName;";
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        WebService.WebServiceClientView webServiceClientView = new WebService.WebServiceClientView();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlServerDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(webServiceClientView, dr);
                        sqlServerDataReaderPropertyWriter.WriteProperties();
                        result.Add(webServiceClientView);
                    }
                }
            }

            return result;
        }

        public static void UpDateSqlServerFromMySQL()
        {
            SqlParameter tableNameParameter = new SqlParameter("@TableName", SqlDbType.VarChar);
            tableNameParameter.Value = "tblWebServiceAccount";
            SqlParameter keyFieldParameter = new SqlParameter("@KeyField", SqlDbType.VarChar);
            keyFieldParameter.Value = "WebServiceAccountId";
            SqlParameter lastUpdateParameter = new SqlParameter("@LastUpdate", SqlDbType.DateTime);
            lastUpdateParameter.Value = DateTime.Now.AddDays(-1);
            SqlParameter currentUpdateParameter = new SqlParameter("@CurrentUpdate", SqlDbType.DateTime);
            currentUpdateParameter.Value = DateTime.Now;
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "mysqlupdatesstable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(tableNameParameter);
            cmd.Parameters.Add(keyFieldParameter);
            cmd.Parameters.Add(lastUpdateParameter);
            cmd.Parameters.Add(currentUpdateParameter);

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "mysqlinsertsstable";

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "mysqldeletesstable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Remove(lastUpdateParameter);
            cmd.Parameters.Remove(currentUpdateParameter);

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            cmd.Parameters.Remove(tableNameParameter);
            cmd.Parameters.Remove(keyFieldParameter);
            tableNameParameter.Value = "tblWebServiceAccountClient";
            keyFieldParameter.Value = "WebServiceAccountClientId";

            cmd = new SqlCommand();
            cmd.CommandText = "mysqlupdatesstable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(tableNameParameter);
            cmd.Parameters.Add(keyFieldParameter);
            cmd.Parameters.Add(lastUpdateParameter);
            cmd.Parameters.Add(currentUpdateParameter);

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "mysqlinsertsstable";

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }

            cmd.CommandText = "mysqldeletesstable";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Remove(lastUpdateParameter);
            cmd.Parameters.Remove(currentUpdateParameter);

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
