using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.MySQLMigration
{
    public class MySqlDataReaderPropertyWriter
    {
        object m_ObjectToWriteTo;
        MySqlDataReader m_MySqlDataReader;
        Type m_ObjectType;

        public MySqlDataReaderPropertyWriter(object objectToWriteTo, MySqlDataReader dataReader)
        {
            this.m_ObjectToWriteTo = objectToWriteTo;
            this.m_MySqlDataReader = dataReader;
            this.m_ObjectType = objectToWriteTo.GetType();
        }

        public void WriteProperties()
        {
            PropertyInfo[] properties = this.m_ObjectType.GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(Business.Persistence.PersistentProperty)) || Attribute.IsDefined(prop, typeof(Business.Persistence.PersistentPrimaryKeyProperty))).ToArray();
            foreach (PropertyInfo property in properties)
            {
                Type dataType = property.PropertyType;
                if (dataType == typeof(string))
                {
                    this.WriteString(property);
                }
                else if (dataType == typeof(int))
                {
                    this.WriteInt(property);
                }
                else if (dataType == typeof(double))
                {
                    this.WriteDouble(property);
                }
                else if (dataType == typeof(Nullable<int>))
                {
                    this.WriteNullableInt(property);
                }
                else if (dataType == typeof(DateTime))
                {
                    this.WriteDateTime(property);
                }
                else if (dataType == typeof(bool))
                {
                    this.WriteBoolean(property);
                }
                else if (dataType == typeof(Nullable<bool>))
                {
                    this.WriteNullableBoolean(property);
                }
                else if (dataType == typeof(Nullable<DateTime>))
                {
                    this.WriteNullableDateTime(property);
                }
                else
                {
                    throw new Exception("This Data Type is Not Implemented: " + dataType.Name);
                }
            }
        }

        private void WriteString(PropertyInfo property)
        {
            if (this.m_MySqlDataReader[property.Name] != DBNull.Value)
            {
                string sqlValue = this.m_MySqlDataReader[property.Name].ToString();
                property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
            }
        }

        private void WriteInt(PropertyInfo property)
        {
            int sqlValue = Convert.ToInt32(this.m_MySqlDataReader[property.Name].ToString());
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteDouble(PropertyInfo property)
        {
            double sqlValue = Convert.ToDouble(this.m_MySqlDataReader[property.Name].ToString());
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteNullableInt(PropertyInfo property)
        {
            Nullable<int> sqlValue = null;
            if (this.m_MySqlDataReader[property.Name] != DBNull.Value)
            {
                sqlValue = Convert.ToInt32(this.m_MySqlDataReader[property.Name].ToString());
            }
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteDateTime(PropertyInfo property)
        {
            DateTime sqlValue = DateTime.Parse(this.m_MySqlDataReader[property.Name].ToString());
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteBoolean(PropertyInfo property)
        {
            bool sqlValue = Convert.ToBoolean(this.m_MySqlDataReader[property.Name]);
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteNullableBoolean(PropertyInfo property)
        {
            Nullable<bool> sqlValue = null;
            if (this.m_MySqlDataReader[property.Name] != DBNull.Value)
            {
                sqlValue = Convert.ToBoolean(this.m_MySqlDataReader[property.Name].ToString());
            }
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteNullableDateTime(PropertyInfo property)
        {
            Nullable<DateTime> sqlValue = null;
            if (this.m_MySqlDataReader[property.Name] != DBNull.Value)
            {
                sqlValue = DateTime.Parse(this.m_MySqlDataReader[property.Name].ToString());
            }
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }
    }
}