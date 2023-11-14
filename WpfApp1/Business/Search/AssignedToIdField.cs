using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
	public class AssignedToIdField : Interface.ISearchField
	{
        protected int m_UserId;
        protected string m_SqlFieldNamePart1;
		protected string m_SqlFieldNamePart2;
		protected string m_Condition;

		public AssignedToIdField(int userId)
        {            
            this.m_UserId = userId;
            //this.m_SqlFieldNamePart1 = "po.i.value('(AssignedToId)[1]', 'int') ";
			this.m_SqlFieldNamePart1 = "po.AssignedToId ";
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
			get { return this.SqlFieldNamePart1 + UserId.ToString(); }
		}

        public string SqlFieldNamePart1
        {
			get { return this.m_SqlFieldNamePart1; }
        }		

        public override string ToString()
        {
			StringBuilder result = new StringBuilder();
			result.Append(SqlFieldNamePart1);			
			result.Append(Condition);
            result.Append(UserId.ToString());			
			return result.ToString();
        }       
	}
}
