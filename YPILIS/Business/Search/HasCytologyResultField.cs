using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
    public class HasCytologyResultField : Interface.ISearchField
    {
        string m_SqlFieldName;
        string m_Condition;

        public HasCytologyResultField()
        {
            //this.m_SqlFieldName = "po.i.exist('CytologyResult')";
            //this.m_Condition = "=1";
			this.m_SqlFieldName = "pso.PanelSetId";
			this.m_Condition = "= 15";
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
            result.Append(" ");
            result.Append(this.m_Condition);            
            return result.ToString();
        }
    }
}
