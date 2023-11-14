using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.XPSDocument.Result.Data
{
    public class AccessionOrderDataSheetDataClientOrder
    {
        private string m_ClientName;
        private string m_OrderedBy;
        private string m_OrderDate;
        private string m_Submitted;
        private string m_Accessioned;
        private string m_SystemInitiatingOrder;
        public AccessionOrderDataSheetDataClientOrder(ClientOrder.Model.ClientOrder clientOrder)
        {
            this.m_ClientName = clientOrder.ClientName;
            this.m_OrderedBy = clientOrder.OrderedBy;
            this.m_OrderDate = clientOrder.OrderDate.Value.ToShortDateString();
            this.m_Submitted = clientOrder.Submitted.ToString();
            this.m_Accessioned = clientOrder.Accessioned.ToString();
            this.m_SystemInitiatingOrder = string.IsNullOrEmpty(clientOrder.SystemInitiatingOrder) == false ? clientOrder.SystemInitiatingOrder : string.Empty;
        }

        public string ClientName
        {
            get { return this.m_ClientName; }
        }

        public string OrderedBy
        {
            get { return this.m_OrderedBy; }
        }

        public string OrderDate
        {
            get { return this.m_OrderDate; }
        }

        public string Submitted
        {
            get { return this.m_Submitted; }
        }

        public string Accessioned
        {
            get { return this.m_Accessioned; }
        }

        public string SystemInitiatingOrder
        {
            get { return this.m_SystemInitiatingOrder; }
        }

    }
}
