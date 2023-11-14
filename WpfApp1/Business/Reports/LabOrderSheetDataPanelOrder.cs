using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Reports
{
    public class LabOrderSheetDataPanelOrder
    {
        private string m_PanelOrderId;
        private string m_Comment;
        private string m_Initials;
        private DateTime m_OrderTime;
        private string m_ReportNo;
        private List<LabOrderSheetDataTestOrder> m_LabOrderSheetDataTestOrders;

        public LabOrderSheetDataPanelOrder()
        {
            this.m_LabOrderSheetDataTestOrders = new List<LabOrderSheetDataTestOrder>();
        }

        public List<LabOrderSheetDataTestOrder> LabOrderSheetDataTestOrders
        {
            get { return this.m_LabOrderSheetDataTestOrders; }
        }

        [PersistentProperty()]
        public string PanelOrderId
        {
            get { return this.m_PanelOrderId; }
            set { this.m_PanelOrderId = value; }
        }

        [PersistentProperty()]
        public string Comment
        {
            get { return this.m_Comment; }
            set { this.m_Comment = value; }
        }

        [PersistentProperty()]
        public string Initials
        {
            get { return this.m_Initials; }
            set { this.m_Initials = value; }
        }

        [PersistentProperty()]
        public DateTime OrderTime
        {
            get { return this.m_OrderTime; }
            set { this.m_OrderTime = value; }
        }

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }
    }
}
