using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Reports
{
    public class POCRetensionReportDataItem
    {
        private string m_Status;
        private DateTime? m_FinalDate;
        private string m_ReportNo;
        private string m_PFirstName;
        private string m_PLastName;
        private string m_Description;

        public POCRetensionReportDataItem()
        {

        }

        [PersistentProperty()]
        public string Status
        {
            get { return this.m_Status; }
            set { this.m_Status = value; }
        }

        [PersistentProperty()]
        public DateTime? FinalDate
        {
            get { return this.m_FinalDate; }
            set { this.m_FinalDate = value; }
        }

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        [PersistentProperty()]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set { this.m_PFirstName = value; }
        }

        [PersistentProperty()]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set { this.m_PLastName = value; }
        }

        [PersistentProperty()]
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }
    }
}
