using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
    public class AccessionDateField : Interface.ISearchField
    {       
        DateTime m_AccessionDate;
        string m_SqlFieldName;        
        string m_Condition;

        public AccessionDateField(DateTime defaultDate)
        {            
            this.m_AccessionDate = defaultDate;
            this.m_SqlFieldName = "AccessionDate";
            this.m_Condition = "=";            
        }        

        public DateTime Value
        {
            get { return this.m_AccessionDate; }
            set { this.m_AccessionDate = value; }
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
            result.Append("'" + this.Value.ToShortDateString() + "'");
            return result.ToString();
        }       
    }
}
