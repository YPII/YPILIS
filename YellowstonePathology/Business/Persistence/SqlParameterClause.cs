using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlParameterClause
    {
        private string m_DeclareStatement;
        private string m_SelectStatement;

        public SqlParameterClause(Type type, string primaryKeyValue)
        {
            System.Reflection.PropertyInfo primaryKeyPropertyInfo = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            this.m_DeclareStatement = "Declare @" + primaryKeyPropertyInfo.Name + " varchar(100)";
            this.m_SelectStatement = "Select @" + primaryKeyPropertyInfo.Name + " = ''" + primaryKeyValue + "''";
        }

        public override string ToString()
        {
            return this.m_DeclareStatement + " " + this.m_SelectStatement;
        }
    }
}
