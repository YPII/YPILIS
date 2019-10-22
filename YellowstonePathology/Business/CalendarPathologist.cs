using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class CalendarPathologist
    {
        private string m_PathologistName;
        private string m_Status;

        public CalendarPathologist()
        { }

        public string PathologistName
        {
            get { return this.m_PathologistName; }
            set { this.m_PathologistName = value; }
        }

        public string Status
        {
            get { return this.m_Status; }
            set { this.m_Status = value; }
        }
    }
}
