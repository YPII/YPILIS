using System.Reflection;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class BooleanPropertyBridge : PropertyBridge
    {
        public BooleanPropertyBridge(PropertyInfo property, object obj)
            : base(property, obj)
        {
            
        }

        public override void SetSqlParameter(MySqlCommand cmd)
        {
            base.SetSqlParameter(cmd);
            this.m_SqlParameter.MySqlDbType = MySqlDbType.TinyText;
        }
    }
}
