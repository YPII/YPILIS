using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlDataReaderPropertyWriter
    {
		object m_ObjectToWriteTo;
        SqlDataReader m_SqlDataReader;
        Type m_ObjectType;
        //bool m_RemoveCarriageReturn; // for xml comparison testing

		public SqlDataReaderPropertyWriter(object objectToWriteTo, SqlDataReader dataReader)
		{
			this.m_ObjectToWriteTo = objectToWriteTo;
            this.m_SqlDataReader = dataReader;
			this.m_ObjectType = objectToWriteTo.GetType();
            //this.m_RemoveCarriageReturn = false;
        }

        /*public SqlDataReaderPropertyWriter(object objectToWriteTo, SqlDataReader dataReader, bool removeCarriageReturn)
        {
            this.m_ObjectToWriteTo = objectToWriteTo;
            this.m_SqlDataReader = dataReader;
            this.m_ObjectType = objectToWriteTo.GetType();
            this.m_RemoveCarriageReturn = removeCarriageReturn;
        }*/

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
                this.m_SqlDataReader.GetOrdinal(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void WriteString(PropertyInfo property)
        {
            if (this.m_SqlDataReader[property.Name] != DBNull.Value)
            {
                string sqlValue = this.m_SqlDataReader[property.Name].ToString();
                /*if (this.m_RemoveCarriageReturn == true && sqlValue.Contains("\r\n"))
                {
                    sqlValue = sqlValue.Replace("\r\n", "\n");
                }*/
				property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
            }
        }

        private void WriteInt(PropertyInfo property)
        {
            int sqlValue = Convert.ToInt32(this.m_SqlDataReader[property.Name].ToString());
			property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteDouble(PropertyInfo property)
        {
            double sqlValue = Convert.ToDouble(this.m_SqlDataReader[property.Name].ToString());
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteNullableInt(PropertyInfo property)
        {
            Nullable<int> sqlValue = null;
            if (this.m_SqlDataReader[property.Name] != DBNull.Value)
            {
                sqlValue = Convert.ToInt32(this.m_SqlDataReader[property.Name].ToString());
            }
			property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteDateTime(PropertyInfo property)
        {
            //DateTime sqlValue = DateTime.Parse(this.m_SqlDataReader[property.Name].ToString());
            DateTime sqlValue = (DateTime)this.m_SqlDataReader[property.Name];
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteBoolean(PropertyInfo property)
        {
            bool sqlValue = Convert.ToBoolean(this.m_SqlDataReader[property.Name].ToString());
			property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteNullableBoolean(PropertyInfo property)
        {
            Nullable<bool> sqlValue = null;
            if (this.m_SqlDataReader[property.Name] != DBNull.Value)
            {
                sqlValue = Convert.ToBoolean(this.m_SqlDataReader[property.Name].ToString());
            }
			property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteNullableDateTime(PropertyInfo property)
        {
            Nullable<DateTime> sqlValue = null;
            if (this.m_SqlDataReader[property.Name] != DBNull.Value)
            {
                //sqlValue = DateTime.Parse(this.m_SqlDataReader[property.Name].ToString());
                sqlValue = (DateTime)this.m_SqlDataReader[property.Name];
            }
            property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }
    }
}
