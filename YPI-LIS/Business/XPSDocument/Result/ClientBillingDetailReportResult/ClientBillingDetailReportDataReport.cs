using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.XPSDocument.Result.ClientBillingDetailReportResult
{
    public class ClientBillingDetailReportDataReport
    {
        private List<Test.PanelSetOrderCPTCode> m_PanelSetOrderCPTCodes;
        private List<Test.PanelSetOrderCPTCodeBill> m_PanelSetOrderCPTCodeBills;

        private string m_ReportNo;
        private string m_PanelSetName;
        private bool m_NoCharge;
        private bool m_Ordered14DaysPostDischarge;
        private string m_BillingType;
        private string m_MasterAccessionNo;

        public ClientBillingDetailReportDataReport()
        {
            this.m_PanelSetOrderCPTCodes = new List<Test.PanelSetOrderCPTCode>();
            this.m_PanelSetOrderCPTCodeBills = new List<Test.PanelSetOrderCPTCodeBill>();
        }

        public List<Test.PanelSetOrderCPTCode> PanelSetOrderCPTCodes
        {
            get { return this.m_PanelSetOrderCPTCodes; }
        }

        public List<Test.PanelSetOrderCPTCodeBill> PanelSetOrderCPTCodeBills
        {
            get { return this.m_PanelSetOrderCPTCodeBills; }
        }

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }

        [PersistentProperty()]
        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set { this.m_PanelSetName = value; }
        }

        [PersistentProperty()]
        public bool NoCharge
        {
            get { return this.m_NoCharge; }
            set { this.m_NoCharge = value; }
        }

        [PersistentProperty()]
        public bool Ordered14DaysPostDischarge
        {
            get { return this.m_Ordered14DaysPostDischarge; }
            set { this.m_Ordered14DaysPostDischarge = value; }
        }

        [PersistentProperty()]
        public string BillingType
        {
            get { return this.m_BillingType; }
            set { this.m_BillingType = value; }
        }

        [PersistentProperty()]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }
    }
}
