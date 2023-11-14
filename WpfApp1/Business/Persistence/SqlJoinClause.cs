using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlJoinClause
    {
        string m_Join;

        public SqlJoinClause(Type baseType, Type inheritedType)
        {
            PersistentClass baseClassAttribute = (PersistentClass)baseType.GetCustomAttributes(typeof(PersistentClass), false).Single();
            PersistentClass inheritedClassAttribute = (PersistentClass)inheritedType.GetCustomAttributes(typeof(PersistentClass), false).Single();

            System.Reflection.PropertyInfo basePrimaryKeyPropertyInfo = baseType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();
            System.Reflection.PropertyInfo inheritedPrimaryKeyPropertyInfo = inheritedType.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(PersistentPrimaryKeyProperty))).Single();            

            this.m_Join = "Join " + baseClassAttribute.StorageName + " on " + inheritedClassAttribute.StorageName + "." + basePrimaryKeyPropertyInfo.Name + " = " + inheritedClassAttribute.StorageName + "." + inheritedPrimaryKeyPropertyInfo.Name;
        }

        public override string ToString()
        {
            return this.m_Join;
        }
    }
}
