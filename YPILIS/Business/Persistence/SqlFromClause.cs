using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlFromClause
    {
        private string m_From;
        private SqlJoinClause m_Join;
        private bool m_HasJoin;

        public SqlFromClause(Type type)
        {
            PersistentClass persistentClassAttribute = (PersistentClass)type.GetCustomAttributes(typeof(PersistentClass), false).Single();            
            if (persistentClassAttribute.HasPersistentBaseClass == true)
            {
                this.m_HasJoin = true;
                Type baseType = type.BaseType;
                PersistentClass baseClassAttribute = (PersistentClass)type.BaseType.GetCustomAttributes(typeof(PersistentClass), false).Single();
                this.m_From = "From " + persistentClassAttribute.BaseStorageName;
                this.m_Join = new SqlJoinClause(baseType, type);                
            }
            else
            {
                this.m_HasJoin = false;
                this.m_From = "From " + persistentClassAttribute.StorageName;
            }
        }        

        public override string ToString()
        {
            if (this.m_HasJoin == true)
            {
                return this.m_From + this.m_Join.ToString();
            }
            else
            {
                return this.m_From;
            }            
        }
    }
}
