using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
	public class AcceptedField : Interface.ISearchField
	{
        protected bool m_Accepted;
        protected string m_SqlFieldName;		
		protected string m_Condition;

		public AcceptedField(Boolean accepted)
        {
            this.m_Accepted = accepted;
            //this.m_SqlFieldName = "po.i.value('(Accepted)[1]', 'bit') ";		
			//this.m_Condition = " = ";
			this.m_SqlFieldName = "po.Accepted ";
			this.m_Condition = " = ";
		}

        public bool AcceptedId
        {
            get { return this.m_Accepted; }
            set { this.m_Accepted = value; }
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
			result.Append(SqlFieldName);
            result.Append(Condition);
            if (this.m_Accepted == true)
            {
                result.Append("1");
            }
            else
            {
                result.Append("0");
            }			
			return result.ToString();
        }       
	}
}

