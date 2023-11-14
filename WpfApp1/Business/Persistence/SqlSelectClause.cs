using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlSelectClause
    {        
        private string m_Select;
        private string m_ObjectTypeField;
        private string m_AssemblyQualifiedClassNameField;
        private List<SqlStatement> m_SqlStatements;

        public SqlSelectClause(Type type)
        {            
            this.m_ObjectTypeField = "''Object'' as ''@ObjectType''";            
            this.m_AssemblyQualifiedClassNameField = "''" + type.AssemblyQualifiedName + "'' as ''@AssemblyQualifiedClassName''";
            this.m_Select = "Select " + this.m_ObjectTypeField + ", " + this.m_AssemblyQualifiedClassNameField + ", *";
            this.m_SqlStatements = new List<SqlStatement>();
        }

        public List<SqlStatement> SqlStatements
        {
            get { return this.m_SqlStatements; }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder(this.m_Select);
            foreach (SqlStatement sqlStatement in this.m_SqlStatements)
            {
                result.Append(", (" + sqlStatement.ToString() + ")");
            }
            return result.ToString();
        }
    }
}
