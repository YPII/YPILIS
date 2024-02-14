using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Reports
{
    public class POCRetensionReportData
    {
        private DateTime m_StartDate;
        private List<POCRetensionReportDataItem> m_POCRetensionReportDataItems;

        public POCRetensionReportData()
        {
            this.m_POCRetensionReportDataItems = new List<POCRetensionReportDataItem>();
        }

        public DateTime StartDate
        {
            get { return this.m_StartDate; }
            set { this.m_StartDate = value; }
        }

        public List<POCRetensionReportDataItem> POCRetensionReportDataItems
        {
            get { return this.m_POCRetensionReportDataItems; }
            set { this.m_POCRetensionReportDataItems = value; }
        }
    }
}
