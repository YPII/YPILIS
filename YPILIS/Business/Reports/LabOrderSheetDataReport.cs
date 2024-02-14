using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Reports
{
    public class LabOrderSheetDataReport
    {
        private string m_ReportNo;
        private List<LabOrderSheetDataPanelOrder> m_LabOrderSheetDataPanelOrders;

        public LabOrderSheetDataReport()
        {
            this.m_LabOrderSheetDataPanelOrders = new List<LabOrderSheetDataPanelOrder>();
        }

        public List<LabOrderSheetDataPanelOrder> LabOrderSheetDataPanelOrders
        {
            get { return this.m_LabOrderSheetDataPanelOrders; }
        }

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }
    }
}
