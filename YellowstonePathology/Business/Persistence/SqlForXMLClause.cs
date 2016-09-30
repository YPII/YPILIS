using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public class SqlForXMLClause
    {
        private string m_ForXMLClause;

        public SqlForXMLClause(Type type)
        {
            this.m_ForXMLClause = "For XML Path(''" + type.Name + "''), type";
        }

        public override string ToString()
        {
            return this.m_ForXMLClause;
        }
    }
}
