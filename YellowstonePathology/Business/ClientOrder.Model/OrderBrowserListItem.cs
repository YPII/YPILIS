using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class OrderBrowserListItem
    {
        private string m_ClientOrderId;
        private Nullable<int> m_PanelSetId;
        private string m_PLastName;
        private string m_PFirstName;
        private string m_OrderedBy;
        private Nullable<DateTime> m_OrderTime;
        private Nullable<DateTime> m_DateOrderReceived;
        private string m_ProviderName;
        private string m_ClientName;
        private bool m_Submitted;
        private bool m_Received;
        private string m_OrderType;
        private string m_OrderStatus;
        private string m_ExternalOrderId;

        public OrderBrowserListItem()
        {

        }

        [DataMember]
        [PersistentPrimaryKeyProperty(false)]
        public string ClientOrderId
        {
            get { return this.m_ClientOrderId; }
            set { this.m_ClientOrderId = value; }
        }

        [DataMember]
        [PersistentProperty()]
        public Nullable<int> PanelSetId
        {
            get { return this.m_PanelSetId; }
            set { this.m_PanelSetId = value; }
        }

        [DataMember]
        [PersistentProperty()]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set { this.m_PLastName = value; }
        }        

        [DataMember]
        [PersistentProperty()]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set { this.m_PFirstName = value; }
        }

        [DataMember]
        [PersistentProperty()]
        public string OrderedBy
        {
            get { return this.m_OrderedBy; }
            set { this.m_OrderedBy = value; }
        }

        [DataMember]
        [PersistentProperty()]
        public Nullable<DateTime> OrderTime
        {
            get { return this.m_OrderTime; }
            set { this.m_OrderTime = value; }
        }

        [DataMember]
        [PersistentProperty()]
        public Nullable<DateTime> DateOrderReceived
        {
            get { return this.m_DateOrderReceived; }
            set { this.m_DateOrderReceived = value; }
        }

        [DataMember]
        [PersistentProperty()]
        public string ProviderName
        {
            get { return this.m_ProviderName; }
            set { this.m_ProviderName = value; }
        }

        [DataMember]
        [PersistentProperty()]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }

        [DataMember]
        [PersistentProperty()]
        public bool Submitted
        {
            get { return this.m_Submitted; }
            set { this.m_Submitted = value; }
        }

        [DataMember]
        [PersistentProperty()]
        public bool Received
        {
            get { return this.m_Received; }
            set { this.m_Received = value; }
        }

        [DataMember]
        [PersistentProperty()]
        public string OrderType
        {
            get { return this.m_OrderType; }
            set { this.m_OrderType = value; }
        }

        [DataMember]
        [PersistentProperty()]
        public string OrderStatus
        {
            get { return this.m_OrderStatus; }
            set { this.m_OrderStatus = value; }
        }

        [DataMember]
        [PersistentProperty()]
        public string ExternalOrderId
        {
            get { return this.m_ExternalOrderId; }
            set { this.m_ExternalOrderId = value; }
        }
    }
}
