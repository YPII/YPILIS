using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.MySQLMigration
{
    public class MySQLDatabaseBuilder
    {
        public MySQLDatabaseBuilder()
        {

        }

        public void Build()
        {
            ///this.BuildCreateTableCommand(typeof(YellowstonePathology.Business.Client.Model.Client));
            this.UpdateTableSchema(typeof(YellowstonePathology.Business.Client.Model.Client));
        }

        private void UpdateTableSchema(Type type)
        {             
            PropertyInfo[] properties = type.GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.PersistentProperty)) || Attribute.IsDefined(prop, typeof(Business.Persistence.PersistentPrimaryKeyProperty))).ToArray();

            for (int i = 0; i < properties.Length - 1; i++)
            {
                PropertyInfo property = properties[i];

                string tableName = "tbl" + type.Name;
                string sqlCommand = "IF NOT EXISTS( SELECT NULL " +
                    "FROM INFORMATION_SCHEMA.COLUMNS " +
                    "WHERE table_name = '" + tableName + "' " +
                    "AND table_schema = 'lis' " +
                    "AND column_name = '" + property.Name + "')  THEN " +
                    "ALTER TABLE '" + tableName + "' ADD '" + property.Name + "' " + this.GetMySQLDataType(property.PropertyType) + "; " +
                    "END IF;";                

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = sqlCommand;
                cmd.Connection = new MySqlConnection("Server = 10.1.2.26; Uid = sid; Pwd = ctlnbr4760; Database = lis;");
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }                       
        }        

        private void BuildCreateTableCommand(Type type)
        {
            string sqlCommand ="Create Table If Not Exists tbl" + type.Name + "(";

            PropertyInfo[] properties = type.GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.PersistentProperty)) || Attribute.IsDefined(prop, typeof(Business.Persistence.PersistentPrimaryKeyProperty))).ToArray();

            for (int i = 0; i < properties.Length - 1; i++)
            {
                PropertyInfo property = properties[i];
                sqlCommand = sqlCommand + property.Name + " ";
                sqlCommand = sqlCommand + this.GetMySQLDataType(property.PropertyType) + ", ";                
            }

            if (sqlCommand.Length != 0)
            {
                sqlCommand = sqlCommand.Remove(sqlCommand.Length - 2, 2);
            }

            sqlCommand += ");";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sqlCommand;
            cmd.Connection = new MySqlConnection("Server = 10.1.2.26; Uid = sid; Pwd = ctlnbr4760; Database = lis;");
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();            
        }

        private string GetMySQLDataType(Type type)
        {
            string result = null;    
                    
            if (type == typeof(string))
            {
                result = "TEXT";
            }
            else if (type == typeof(int))
            {
                result = "INT";
            }
            else if (type == typeof(double))
            {
                result = "DOUBLE";
            }
            else if (type == typeof(Nullable<int>))
            {
                result = "INT";
            }
            else if (type == typeof(DateTime))
            {
                result = "DATETIME";
            }
            else if (type == typeof(bool))
            {
                result = "BIT";
            }
            else if (type == typeof(Nullable<bool>))
            {
                result =  "BIT";
            }
            else if (type == typeof(Nullable<DateTime>))
            {
                result = "DATETIME";
            }
            else
            {
                throw new Exception("This Data Type is Not Implemented: " + type.Name);
            }

            return result;
        }
    }
}
