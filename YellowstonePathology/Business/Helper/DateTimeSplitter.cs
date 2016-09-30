using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Helper
{
    public class DateTimeSplitter
    {
        Nullable<DateTime> m_DateToSplit;

        public DateTimeSplitter(Nullable<DateTime> dateToSplit)
        {
            this.m_DateToSplit = dateToSplit;
        }

        public Nullable<DateTime> GetDate()
        {            
            if (this.m_DateToSplit.HasValue == true)
            {                
                return DateTime.Parse(this.m_DateToSplit.Value.ToShortDateString());
            }
            else
            {
                return null;
            }         
        }

        public Nullable<DateTime> GetDateWithTime()
        {
            if (this.m_DateToSplit.HasValue == true)
            {
                if (this.m_DateToSplit.Value.Hour == 12 && this.m_DateToSplit.Value.Minute == 0)
                {
                    return null;
                }
                else if (this.m_DateToSplit.Value.Hour == 0 && this.m_DateToSplit.Value.Minute == 0)
                {
                    return null;
                }
                else
                {
                    return this.m_DateToSplit;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
