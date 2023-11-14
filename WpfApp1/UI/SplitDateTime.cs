using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class SplitDateTime
    {
        private Nullable<DateTime> m_Date;
        private Nullable<DateTime> m_Time;               

        private string m_MergedDateString;
        private bool m_HasError;

        public SplitDateTime(string mergedDateString)
        {
            this.m_MergedDateString = mergedDateString;
            this.SetDateTime();
        }

        public SplitDateTime(Nullable<DateTime> date, Nullable<DateTime> time)
        {
            this.m_Date = date;
            this.m_Time = time;
            this.SetMergedDateString();
        }

        public void AddDay()
        {            
            if (this.m_Date.HasValue == true) this.m_Date = this.m_Date.Value.AddDays(1);
            if (this.m_Time.HasValue == true) this.m_Time = this.m_Time.Value.AddDays(1);
        }

        public void SubtractDay()
        {            
            if (this.m_Date.HasValue == true) this.m_Date = this.m_Date.Value.AddDays(-1);
            if (this.m_Time.HasValue == true) this.m_Time = this.m_Time.Value.AddDays(-1);
        }

        public bool HasError
        {
            get { return this.m_HasError; }
        }

        public string MergedDateString
        {
            get { return this.m_MergedDateString; }
        }

        public Nullable<DateTime> Date
        {
            get { return this.m_Date; }
        }

        public Nullable<DateTime> Time
        {
            get { return this.m_Time; }
        }

        private void SetMergedDateString()
        {
            string result = string.Empty;
            if (this.m_Time.HasValue == true)
            {                                
                result = this.m_Time.Value.ToString("MM/dd/yyyy HH:mm");                
            }
            else
            {
                if (this.m_Date.HasValue == true)
                {                    
                    result = this.m_Date.Value.ToString("MM/dd/yyyy");                 
                }
                else
                {
                    result = string.Empty;
                }
            }
            this.m_MergedDateString = result;
        }        

        private void SetDateTime()
        {            
            if (string.IsNullOrEmpty(this.m_MergedDateString) == false)
            {
                DateTime dateIn;
                bool parseResult = DateTime.TryParse(this.m_MergedDateString.ToString(), out dateIn);
                if (parseResult == true)
                {
                    if (this.m_MergedDateString.Length >= 8 && this.m_MergedDateString.Length <= 10)
                    {
                        this.m_Date = DateTime.Parse(dateIn.ToString("MM/dd/yyyy"));
                        this.m_Time = null;                        
                    }
                    else if (this.m_MergedDateString.Length >= 14 && this.m_MergedDateString.Length <= 16)
                    {
                        this.m_Date = DateTime.Parse(dateIn.ToString("MM/dd/yyyy"));
                        this.m_Time = DateTime.Parse(dateIn.ToString("MM/dd/yyyy HH:mm"));
                    }
                }
                else
                {
                    this.m_HasError = true;
                }
            }
            else
            {
                this.m_Date = null;
                this.m_Time = null;                
            }            
        }
    }
}
