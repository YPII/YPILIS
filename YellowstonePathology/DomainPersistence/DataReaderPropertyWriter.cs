using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Xml;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public class DataReaderPropertyWriter : IPropertyWriter
    {
        MySqlDataReader m_SqlDataReader;

        public DataReaderPropertyWriter(MySqlDataReader sqlDataReader)
        {
            this.m_SqlDataReader = sqlDataReader;
        }

        public string WriteString(string propertyName)
        {
			string result = null; 
            if (this.m_SqlDataReader[propertyName] != DBNull.Value)
            {
                result = this.m_SqlDataReader[propertyName].ToString();
            }
            return result;
        }

        public int WriteInt(string propertyName)
        {            
            return Convert.ToInt32(this.m_SqlDataReader[propertyName].ToString());         
        }
        
        public Nullable<int> WriteNullableInt(string propertyName)
        {
            Nullable<int> result = null;
            if (this.m_SqlDataReader[propertyName] != DBNull.Value)
            {
                result = Convert.ToInt32(this.m_SqlDataReader[propertyName].ToString());
            }
            return result;
        }

        public bool WriteBoolean(string propertyName)
        {            
            return Convert.ToBoolean(this.m_SqlDataReader[propertyName].ToString());            
        }

        public DateTime WriteDateTime(string propertyName)
        {            
            return DateTime.Parse(this.m_SqlDataReader[propertyName].ToString());         
        }

        public Nullable<DateTime> WriteNullableDateTime(string propertyName)
        {
            Nullable<DateTime> result = null;
            if (this.m_SqlDataReader[propertyName] != DBNull.Value)
            {
                result = DateTime.Parse(this.m_SqlDataReader[propertyName].ToString());     
            }
            return result;           
        }

        public XElement WriteXElement(string propertyName)
        {                        
			XElement result = null;
			string s = this.m_SqlDataReader[propertyName].ToString();
			if (string.IsNullOrEmpty(s) == false)
			{
				 result = XElement.Parse(s);
			}

			return result;
        }

        public double WriteFloat(string propertyName)
        {
            return Convert.ToDouble(this.m_SqlDataReader[propertyName].ToString());  
        }

        public Nullable<double> WriteNullableFloat(string propertyName)
        {
            Nullable<double> result = null;
            if (this.m_SqlDataReader[propertyName] != DBNull.Value)
            {
                result = Convert.ToDouble(this.m_SqlDataReader[propertyName].ToString());
            }
            return result;
        }
    }
}
