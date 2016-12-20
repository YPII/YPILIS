using System;
using System.Linq;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlDataReaderPropertyWriter
    {
		object m_ObjectToWriteTo;
        MySqlDataReader m_SqlDataReader;
        Type m_ObjectType;        

		public SqlDataReaderPropertyWriter(object objectToWriteTo, MySqlDataReader dataReader)
		{
			this.m_ObjectToWriteTo = objectToWriteTo;
            this.m_SqlDataReader = dataReader;
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

        /*private bool ColumnExists(string name)
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
        }*/

        private bool ColumnExists(string name)
        {
            bool result = false;
            for(int idx = 0; idx < this.m_SqlDataReader.FieldCount; idx++)
            {
                string fieldName = this.m_SqlDataReader.GetName(idx);
                if(fieldName == name)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private void WriteString(PropertyInfo property)
        {
            if (this.m_SqlDataReader[property.Name] != DBNull.Value)
            {
                string sqlValue = this.m_SqlDataReader[property.Name].ToString();                
				property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
            }
        }

        private void WriteInt(PropertyInfo property)
        {
            int sqlValue = 0;
            if (this.m_SqlDataReader[property.Name] != DBNull.Value) //ClientSupplyOrderDetail
            {
                sqlValue = Convert.ToInt32(this.m_SqlDataReader[property.Name].ToString());
            }
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
            bool sqlValue = false;
            if (this.m_SqlDataReader[property.Name] != DBNull.Value) //ClientLocation
            {
                sqlValue = Convert.ToBoolean(this.m_SqlDataReader[property.Name]);
            }
			property.SetValue(this.m_ObjectToWriteTo, sqlValue, null);
        }

        private void WriteNullableBoolean(PropertyInfo property)
        {
            Nullable<bool> sqlValue = null;
            if (this.m_SqlDataReader[property.Name] != DBNull.Value)
            {
                sqlValue = Convert.ToBoolean(this.m_SqlDataReader[property.Name]);
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
