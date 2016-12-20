using System.Reflection;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class IntPropertyBridge : PropertyBridge
    {
        public IntPropertyBridge(PropertyInfo property, object obj)
            : base(property, obj)
        {
            
        }

        public override void SetSqlParameter(MySqlCommand cmd)
        {
            base.SetSqlParameter(cmd);
            this.m_SqlParameter.MySqlDbType = MySqlDbType.Int32;            
        }
    }
}
