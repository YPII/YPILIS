using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlDataTableReaderPropertyWriter
    {
        object m_ObjectToWriteTo;
        DataTableReader m_DataTableReader;
        Type m_ObjectType;

        public SqlDataTableReaderPropertyWriter(object objectToWriteTo, DataTableReader dataTableReader)
        {
            this.m_ObjectToWriteTo = objectToWriteTo;
            this.m_DataTableReader = dataTableReader;
            this.m_ObjectType = objectToWriteTo.GetType();
        }

        public void WriteProperties()
        {
            PropertyInfo[] properties = this.m_ObjectType.GetProperties().
                Where(prop => Attribute.IsDefined(prop, typeof(PersistentProperty)) || Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).ToArray();
            foreach (PropertyInfo property in properties)
            {
                if (this.ColumnExists(property.Name))
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
                    else if (dataType == typeof(Nullable<float>))
                    {
                        this.WriteNullableDouble(property);
                    }
                    else if (dataType == typeof(Nullable<double>))
                    {
                        this.WriteNullableDouble(property);
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
        }

        private bool ColumnExists(string name)
        {
            try
            {
                int a = this.m_DataTableReader.GetOrdinal(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void WriteString(PropertyInfo property)
        {
            if (this.m_DataTableReader[property.Name] != DBNull.Value)
            {
                string sqlValue = this.m_DataTableReader[property.Name].ToString();
                property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
            }
        }

        private void WriteInt(PropertyInfo property)
        {
            int sqlValue = Convert.ToInt32(this.m_DataTableReader[property.Name].ToString());
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteDouble(PropertyInfo property)
        {
            double sqlValue = Convert.ToDouble(this.m_DataTableReader[property.Name].ToString());
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteNullableInt(PropertyInfo property)
        {
            Nullable<int> sqlValue = null;
            if (this.m_DataTableReader[property.Name] != DBNull.Value)
            {
                sqlValue = Convert.ToInt32(this.m_DataTableReader[property.Name].ToString());
            }
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }        

        private void WriteNullableDouble(PropertyInfo property)
        {
            Nullable<double> sqlValue = null;
            if (this.m_DataTableReader[property.Name] != DBNull.Value)
            {
                sqlValue = Convert.ToDouble(this.m_DataTableReader[property.Name].ToString());
            }
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteDateTime(PropertyInfo property)
        {
            DateTime sqlValue = (DateTime)this.m_DataTableReader[property.Name];
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteBoolean(PropertyInfo property)
        {
            bool sqlValue = false;
            Type type = this.m_DataTableReader[property.Name].GetType();
            if(type == typeof(Boolean))
            {
                sqlValue = (Boolean)this.m_DataTableReader[property.Name];
            }
            else if (type == typeof(UInt64))
            {
                sqlValue = Convert.ToBoolean((UInt64)this.m_DataTableReader[property.Name]);
            }
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteNullableBoolean(PropertyInfo property)
        {
            Nullable<bool> sqlValue = null;
            if (this.m_DataTableReader[property.Name] != DBNull.Value)
            {
                Type type = this.m_DataTableReader[property.Name].GetType();
                if (type == typeof(Boolean))
                {
                    sqlValue = (Boolean)this.m_DataTableReader[property.Name];
                }
                else if (type == typeof(UInt64))
                {
                    sqlValue = Convert.ToBoolean((UInt64)this.m_DataTableReader[property.Name]);
                }
            }
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteNullableDateTime(PropertyInfo property)
        {
            Nullable<DateTime> sqlValue = null;
            if (this.m_DataTableReader[property.Name] != DBNull.Value)
            {
                sqlValue = (DateTime)this.m_DataTableReader[property.Name];
            }
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }
    }
}

