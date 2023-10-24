using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Helper
{
    public class DateTimeJoiner
    {
        string m_DateTimeFormat = "MM/dd/yyyy HH:mm";
        string m_DateFormat = "MM/dd/yyyy";
        string m_DisplayString;

        public DateTimeJoiner(DateTime date, Nullable<DateTime> time)
        {
            if (time.HasValue == false)
            {
                this.m_DisplayString = date.ToString(this.m_DateFormat);
            }
            else
            {
                this.m_DisplayString = time.Value.ToString(this.m_DateTimeFormat);
            }
        }

        public DateTimeJoiner(DateTime date, string dateFormat, Nullable<DateTime> time, string dateTimeFormat)
        {
            this.m_DateFormat = dateFormat;
            this.m_DateTimeFormat = dateTimeFormat;

            if (time.HasValue == false)
            {
                this.m_DisplayString = date.ToString(this.m_DateFormat);
            }
            else
            {
                this.m_DisplayString = time.Value.ToString(this.m_DateTimeFormat);
            }
        }

        public DateTimeJoiner(string date, string time)
        {
            if (string.IsNullOrEmpty(time) == true)
            {
                DateTime dateOnly = DateTime.Parse(date);
                this.m_DisplayString = dateOnly.ToString(this.m_DateFormat);
            }
            else
            {
                DateTime dateTime = DateTime.Parse(time);
                this.m_DisplayString = dateTime.ToString(this.m_DateTimeFormat);
            }
        }

        public string DisplayString
        {
            get { return this.m_DisplayString; }
        }        
    }
}
