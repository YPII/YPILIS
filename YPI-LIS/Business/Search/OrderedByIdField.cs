using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
	public class OrderedByIdField : Interface.ISearchField
	{
        protected int m_UserId;
        protected string m_SqlFieldName;		
		protected string m_Condition;

		public OrderedByIdField(int userId)
        {            
            this.m_UserId = userId;
            //this.m_SqlFieldName = "po.i.value('(OrderedById)[1]', 'int') ";
			this.m_SqlFieldName = "po.OrderedById ";
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
            set { this.m_SqlFieldName = value; }
		}        

        public override string ToString()
        {
			StringBuilder result = new StringBuilder();
			result.Append(SqlFieldName);			
			result.Append(Condition);
            result.Append(UserId.ToString());			
			return result.ToString();
        }       
	}
}
