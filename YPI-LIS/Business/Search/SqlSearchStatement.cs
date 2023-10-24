using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
    public abstract class SqlSearchStatement
    {
        protected string m_Statement;
        protected string m_SelectClause;
        protected string m_FromClause;
        protected string m_OrderByClause;

        List<Interface.ISearchField> m_SearchFields;

        public SqlSearchStatement()
        {
            this.m_SearchFields = new List<Interface.ISearchField>();
        }

        public List<Interface.ISearchField> SearchFields
        {
            get { return this.m_SearchFields; }
            set { this.m_SearchFields = value; }
        }

        public override string ToString()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(this.m_SelectClause);
            sql.Append(" ");
            sql.Append(this.m_FromClause);
            sql.Append(" ");
            sql.Append("Where ");
            for (int i = 0; i < this.m_SearchFields.Count; i++ )
            {
                sql.Append(this.m_SearchFields[i].ToString());
                if (i != this.m_SearchFields.Count - 1)
                {
                    sql.Append(" And ");
                }
            }            
            sql.Append(" ");
            sql.Append(this.m_OrderByClause);
            return sql.ToString().Trim();
        }
    }
}
