using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;


namespace YellowstonePathology.UI
{
    public class TestEnvironment
    {
        public TestEnvironment()
        {

        }

        public static void Sync()
        {            
            HandleSchema();
            HandleCreateTables();
            HandleLowCountRowTables();         
        }        

        public static void HandleTimeStamp()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "select t.table_name from INFORMATION_SCHEMA.TABLES as t left join INFORMATION_SCHEMA.COLUMNS as c on c.TABLE_NAME = t.TABLE_NAME and c.TABLE_SCHEMA = t.TABLE_SCHEMA and c.COLUMN_NAME = 'Timestamp' where c.COLUMN_NAME is null and table_type = 'BASE TABLE' and t.TABLE_SCHEMA = 'lis'; ";
            cmd.CommandType = CommandType.Text;
            StringBuilder sql = new StringBuilder();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string tableName = dr["table_name"].ToString();
                        sql.Append($"ALTER TABLE {tableName} ADD COLUMN Timestamp timestamp default CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP;");
                    }
                }
            }

            cmd.CommandText = sql.ToString();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void HandleSchema()
        {
            List<string> sqlList = new List<string>();
            sqlList.Add("drop schema ypi_lis_test;");
            sqlList.Add("create schema ypi_lis_test;");

            foreach(string sql in sqlList)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;

                using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
                {
                    cn.Open();
                    cmd.Connection = cn;
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }                    
                }
            }            
        }

        public static void HandleCreateTables()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select table_schema, table_name from information_schema.tables where table_type = 'BASE TABLE' and table_schema in ('lis') order by table_name;";
            cmd.CommandType = CommandType.Text;
            StringBuilder sql = new StringBuilder();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;                
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string tableName = dr["table_name"].ToString();
                        sql.Append($"create table ypi_lis_test.{tableName} like lis.{tableName};");
                    }
                }
            }

            cmd.CommandText = sql.ToString();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }
        
        public static void HandleLowCountRowTables()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT table_name, table_rows FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'lis' order by table_rows desc; ";
            cmd.CommandType = CommandType.Text;
            List<string> sqlList = new List<string>();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string tableName = dr["table_name"].ToString();
                        string rowCount = dr["table_rows"].ToString();
                        if(string.IsNullOrEmpty(rowCount) == false)
                        {
                            int cnt = Convert.ToInt32(rowCount);
                            //if (cnt > 0 && cnt < 10000)
                            //{
                            if(tableName != "tblADT") sqlList.Add($"Insert ypi_lis_test.{tableName} select * from lis.{tableName};");
                            //}
                        }                        
                    }
                }
            }

            foreach(string sql in sqlList)
            {
                using (MySqlConnection cn = new MySqlConnection("Server = 10.1.2.26; Uid = sqldude; Pwd = 123Whatsup; Database = lis; Pooling = True; Connection Timeout = 1000"))
                {
                    cn.Open();
                    cmd.Connection = cn;
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 1000;
                    cmd.ExecuteNonQuery();
                }
            }            
        }
    }
}
