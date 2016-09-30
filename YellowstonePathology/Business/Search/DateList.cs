using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
    public class DateList : List<DateTime>
    {
        int m_DateCount = 90;
        DateTime m_StartDate = DateTime.Today;

        public DateList()
        {
            this.Add(this.m_StartDate);
            for (int i = 0; i < this.m_DateCount; i++)
            {
                DateTime nextDate = this.m_StartDate.AddDays(-i);
                this.Add(nextDate);
            }
        }
    }
}
