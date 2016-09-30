using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class StringPropertyBridge : PropertyBridge
    {
        string m_StringPropertyValue;

        public StringPropertyBridge(PropertyInfo property, object obj)
            : base(property, obj)
        {
            this.m_StringPropertyValue = (string)property.GetValue(obj, null);
        }

        public StringPropertyBridge(PropertyInfo property, string obj)
            : base(property, obj)
        {
            this.m_StringPropertyValue = obj;
        }

        public override void SetSqlValue(object sqlValue)
        {
            base.SetSqlValue(sqlValue);
            this.m_StringPropertyValue = sqlValue.ToString();
        }

        public override object GetPropertyValue()
        {
            return this.m_StringPropertyValue;
        }

        public override void SetSqlParameter(SqlCommand cmd)
        {
            this.m_SqlParameter = new SqlParameter();
            this.m_SqlParameter.ParameterName = this.m_AtName;
            this.m_SqlParameter.Direction = ParameterDirection.Input;
            this.m_SqlParameter.SqlDbType = SqlDbType.NVarChar;
            
            if (string.IsNullOrEmpty(this.m_StringPropertyValue) == false)
            {
                this.m_SqlParameter.Value = this.m_StringPropertyValue;                
            }
            else
            {
                this.m_SqlParameter.Value = DBNull.Value;                
            }

            cmd.Parameters.Add(this.m_SqlParameter);            
        }
    }
}
