using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class DateTimePropertyBridge : PropertyBridge
    {
        Nullable<DateTime> m_DateTimeValue;

        public DateTimePropertyBridge(PropertyInfo property, object obj)
            : base(property, obj)
        {
            
        }

        public override void SetSqlValue(object sqlValue)
        {
            base.SetSqlValue(sqlValue);

            if (sqlValue == null || sqlValue == DBNull.Value)
            {
                this.m_DateTimeValue = null;
            }
            else
            {
                this.m_DateTimeValue = DateTime.Parse(sqlValue.ToString());
            }            
        }

        public override object GetPropertyValue()
        {
            return this.m_DateTimeValue;
        }

        public override void SetSqlParameter(SqlCommand cmd)
        {
            base.SetSqlParameter(cmd);
            this.m_SqlParameter.SqlDbType = SqlDbType.DateTime;
        }
    }
}
