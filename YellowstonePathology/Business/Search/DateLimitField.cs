using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
	public class DateLimitField : Interface.ISearchField
    {
        protected string m_SqlFieldName;
        protected string m_Condition;

		public DateLimitField(int numberOfDaysBackFromToday, string dateFieldName)
        {
			this.m_SqlFieldName = dateFieldName;
			this.m_Condition = " > DateAdd(d, -" + numberOfDaysBackFromToday.ToString() + ", getdate())";            
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
            return result.ToString();
        }       
    }
}

