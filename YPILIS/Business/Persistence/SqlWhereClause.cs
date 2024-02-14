using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlWhereClause
    {
        private string m_Where;

        public SqlWhereClause(Type type)
        {
            System.Reflection.PropertyInfo primaryKeyPropertyInfo = type.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            this.m_Where = "Where " + primaryKeyPropertyInfo.Name + "= @" + primaryKeyPropertyInfo.Name;
        }

        public SqlWhereClause(Type parentType, Type childType)
        {
            PersistentClass parentClassAttribute = (PersistentClass)parentType.GetCustomAttributes(typeof(PersistentClass), false).Single();
            System.Reflection.PropertyInfo primaryKeyPropertyInfo = parentType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();

            PersistentClass childClassAttribute = (PersistentClass)childType.GetCustomAttributes(typeof(PersistentClass), false).Single();
            this.m_Where = "Where " + parentClassAttribute.StorageName + "." + primaryKeyPropertyInfo.Name + " = " + childClassAttribute.StorageName + "." + primaryKeyPropertyInfo.Name;
        }

        public override string ToString()
        {
            return this.m_Where;
        }
    }
}
