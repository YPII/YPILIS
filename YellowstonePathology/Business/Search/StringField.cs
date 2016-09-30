using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
    public class StringField : Interface.ISearchField
    {
        string m_Value;
        string m_SqlFieldName;
        string m_Condition;
        public StringField(string sqlFieldName, string value, string condition)
        {
            this.m_Value = value;
            this.m_SqlFieldName = sqlFieldName;
            this.m_Condition = condition;     
        }

        public string Value
        {
            get { return this.m_Value; }
            set { this.m_Value = value; }
        }

        public string Condition
        {
            get { return this.m_Condition; }
            set { this.m_Condition = value; }
        }

        public string SqlFieldName
        {
            get { return this.m_SqlFieldName; }
            set { this.m_SqlFieldName = value; }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(this.SqlFieldName);
            result.Append(this.Condition);
            result.Append("'" + this.Value + "'");

            return result.ToString();
        }    
    }
}
