using System;
using System.Reflection;
using System.Data;
using MySql.Data.MySqlClient;

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

        public override void SetSqlParameter(MySqlCommand cmd)
        {
            base.SetSqlParameter(cmd);
            this.m_SqlParameter.MySqlDbType = MySqlDbType.DateTime;
        }
    }
}
