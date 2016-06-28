using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class IntPropertyBridge : PropertyBridge
    {
        public IntPropertyBridge(PropertyInfo property, object obj)
            : base(property, obj)
        {
            
        }

        public override void SetSqlParameter(SqlCommand cmd)
        {
            base.SetSqlParameter(cmd);
            this.m_SqlParameter.SqlDbType = SqlDbType.Int;            
        }
    }
}
