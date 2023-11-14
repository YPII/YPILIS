using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
	public class AcceptedByIdField : Interface.ISearchField
	{
        protected int m_UserId;
        protected string m_SqlFieldName;		
		protected string m_Condition;

		public AcceptedByIdField(int userId)
        {
            this.m_UserId = userId;
            //this.m_SqlFieldName = "po.i.value('(AcceptedById)[1]', 'int') ";
			this.m_SqlFieldName = "po.AcceptedById ";
			this.m_Condition = " = ";
        }

        public int UserId
        {
            get { return this.m_UserId; }
            set { this.m_UserId = value; }
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
            result.Append(this.m_UserId.ToString());			
			return result.ToString();
        }       
	}
}

