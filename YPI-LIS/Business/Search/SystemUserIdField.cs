using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
    public class SystemUserIdField : Interface.ISearchField
    {
        protected int m_SystemUserId;
        protected string m_SqlFieldName;
        protected string m_Condition;

        public SystemUserIdField(int defaultId, string userFieldName)
        {
            this.m_SystemUserId = defaultId;
            this.m_SqlFieldName = userFieldName;
            this.m_Condition = " = ";            
        }

        public int Value
        {
            get { return this.m_SystemUserId; }
            set { this.m_SystemUserId = value; }
        }

        public string Condition
        {
            get { return this.m_Condition; }
        }

        public string SqlFieldName
        {
            get { return this.m_SqlFieldName; }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(this.m_SqlFieldName);
            result.Append(this.m_Condition);
            result.Append(this.Value.ToString());
            return result.ToString();
        }       
    }
}
