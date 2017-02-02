using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.MySQLMigration
{
    public class SqlServerFromMySqlRefresher
    {
        public SqlServerFromMySqlRefresher()
        {

        }

        public Business.Rules.MethodResult GetStatus(MySQLMigration.NonpersistentTableDef nonpersistentTableDef)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();

            string cmdString = "Select count(*) from " + nonpersistentTableDef.TableName + ";";
            nonpersistentTableDef.SqlServerRowCount = this.GetSqlServerRowCount(cmdString);
            nonpersistentTableDef.MySqlRowCount = this.GetMySqlRowCount(nonpersistentTableDef.TableName);
            this.GetRefreshKeys(nonpersistentTableDef);
            //Business.Rules.MethodResult missingColumnResult = CompareTable(nonpersistentTableDef);
            //nonpersistentTableDef.HasAllColumns = missingColumnResult.Success;

            return methodResult;
        }

        public Business.Rules.MethodResult HasInserts(MySQLMigration.NonpersistentTableDef nonpersistentTableDef)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            return methodResult;
        }

        public Business.Rules.MethodResult HasUpdates(MySQLMigration.NonpersistentTableDef nonpersistentTableDef)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            return methodResult;
        }

        public Business.Rules.MethodResult Refresh(MySQLMigration.NonpersistentTableDef nonpersistentTableDef)
        {
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            return methodResult;
        }

        private int GetSqlServerRowCount(string commandText)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand(commandText);
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = (int)dr[0];
                    }
                }
            }
            return result;
        }

        private int GetMySqlRowCount(string tableName)
        {
            int result = 0;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.MySqlConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("Select count(*) from " + tableName + ";");
                cmd.Connection = cn;

                using (MySqlDataReader msdr = cmd.ExecuteReader())
                {
                    while (msdr.Read())
                    {
                        result = ((int)(long)msdr[0]);
                    }
                }
            }
            return result;
        }

        private void GetRefreshKeys(MySQLMigration.NonpersistentTableDef nonpersistentTableDef)
        {
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                List<string> createdKeys = new List<string>();
                List<string> modifiedKeys = new List<string>();
                List<string> deletedKeys = new List<string>();
                cn.Open();
                SqlCommand cmd = new SqlCommand("AATestRefresh");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = nonpersistentTableDef.TableName;
                cmd.Parameters.Add("@KeyField", SqlDbType.VarChar).Value = nonpersistentTableDef.KeyField;
                cmd.Connection = cn;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        createdKeys.Add(dr[0].ToString());
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        modifiedKeys.Add(dr[0].ToString());
                    }
                    dr.NextResult();
                    while (dr.Read())
                    {
                        deletedKeys.Add(dr[0].ToString());
                    }
                }
                nonpersistentTableDef.CreatedKeys = createdKeys;
                nonpersistentTableDef.ModifiedKeys = modifiedKeys;
                nonpersistentTableDef.DeletedKeys = deletedKeys;
            }
        }

        private List<string> GetAllKeys(MySQLMigration.NonpersistentTableDef nonpersistentTableDef)
        {
            List<string> keys = new List<string>();
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.SqlServerConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("AATestGetAllKeys");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TableName", SqlDbType.VarChar).Value = nonpersistentTableDef.TableName;
                cmd.Parameters.Add("@KeyField", SqlDbType.VarChar).Value = nonpersistentTableDef.KeyField;
                cmd.Connection = cn;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        keys.Add(dr[0].ToString());
                    }
                }
            }
            return keys;
        }

        private string KeyStringFromList(NonpersistentTableDef nonpersistentTableDef, List<string> keys)
        {
            bool needsTic = false;
            foreach(NonpersistentColumnDef columnDef in nonpersistentTableDef.ColumnDefinitions)
            {
                if(columnDef.ColumnName == nonpersistentTableDef.KeyField)
                {
                    needsTic = columnDef.ColumnType.ToUpper().Contains("VARCHAR") ? true : false;
                    break;
                }
            }
            string result = string.Empty;

            foreach (string key in keys)
            {
                if (needsTic == true) result += "'" + key + "', ";
                else result += key + ", ";
            }
            result = result.Remove(result.Length - 2, 2);
            return result;
        }
    }
}
