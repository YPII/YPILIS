using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    [DataContract(Namespace = "YellowstonePathology.Business.ClientOrder.Model")]      
    public class ShipmentReturnResult
    {
        private YellowstonePathology.Business.ClientOrder.Model.Shipment m_Shipment;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection m_ClientOrderDetailCollection;

        public ShipmentReturnResult()
        {
        
        }

        [DataMember]
        public YellowstonePathology.Business.ClientOrder.Model.Shipment Shipment
        {
            get { return this.m_Shipment; }
            set { this.m_Shipment = value; }
        }

        [DataMember]
        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection ClientOrderDetailCollection
        {
            get { return this.m_ClientOrderDetailCollection; }
            set { this.m_ClientOrderDetailCollection = value; }
        }
    }
}
