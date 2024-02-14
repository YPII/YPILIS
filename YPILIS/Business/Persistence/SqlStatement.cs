using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlStatement
    {
        private SqlParameterClause m_SqlParameterClause;
        private SqlSelectClause m_SqlSelectClause;
        private SqlFromClause m_SqlFromClause;
        private SqlWhereClause m_SqlWhereClause;
        private SqlForXMLClause m_SqlForXMLClause;

        public SqlStatement(SqlParameterClause sqlParameterClause, SqlSelectClause sqlSelectClause, Type type)
        {
            this.m_SqlParameterClause = sqlParameterClause;
            this.m_SqlSelectClause = sqlSelectClause;

            this.m_SqlFromClause = new SqlFromClause(type);
            this.m_SqlWhereClause = new SqlWhereClause(type);
            this.m_SqlForXMLClause = new SqlForXMLClause(type);
        }        

        public SqlStatement(SqlSelectClause sqlSelectClause, Type parentType, Type childType)
        {
            this.m_SqlSelectClause = sqlSelectClause;            
            this.m_SqlFromClause = new SqlFromClause(childType);
            this.m_SqlWhereClause = new SqlWhereClause(parentType, childType);
            this.m_SqlForXMLClause = new SqlForXMLClause(childType);
        }        

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("'");
            if (this.m_SqlParameterClause != null) result.Append(this.m_SqlParameterClause.ToString());
            result.Append(this.m_SqlSelectClause + " ");
            result.Append(this.m_SqlFromClause + " ");
            result.Append(this.m_SqlWhereClause + " ");
            result.Append(this.m_SqlForXMLClause + " ");
            result.Append("'");
            return result.ToString();
        }
    }
}
