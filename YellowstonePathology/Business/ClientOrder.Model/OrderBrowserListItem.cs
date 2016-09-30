using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

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
        private string m_ProviderName;
        private string m_ClientName;
        private bool m_Submitted;
        private bool m_Received;
        private string m_OrderType;

        public OrderBrowserListItem()
        {

        }

        [DataMember]
        public string ClientOrderId
        {
            get { return this.m_ClientOrderId; }
            set { this.m_ClientOrderId = value; }
        }

        [DataMember]
        public Nullable<int> PanelSetId
        {
            get { return this.m_PanelSetId; }
            set { this.m_PanelSetId = value; }
        }

        [DataMember]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set { this.m_PLastName = value; }
        }        

        [DataMember]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set { this.m_PFirstName = value; }
        }

        [DataMember]
        public string OrderedBy
        {
            get { return this.m_OrderedBy; }
            set { this.m_OrderedBy = value; }
        }

        [DataMember]
        public Nullable<DateTime> OrderTime
        {
            get { return this.m_OrderTime; }
            set { this.m_OrderTime = value; }
        }

        [DataMember]
        public string ProviderName
        {
            get { return this.m_ProviderName; }
            set { this.m_ProviderName = value; }
        }

        [DataMember]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }

        [DataMember]
        public bool Submitted
        {
            get { return this.m_Submitted; }
            set { this.m_Submitted = value; }
        }

        [DataMember]
        public bool Received
        {
            get { return this.m_Received; }
            set { this.m_Received = value; }
        }

        [DataMember]
        public string OrderType
        {
            get { return this.m_OrderType; }
            set { this.m_OrderType = value; }
        }

        public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
        {
            this.m_ClientOrderId = propertyWriter.WriteString("ClientOrderId");
            this.m_PanelSetId = propertyWriter.WriteNullableInt("PanelSetId");
            this.m_Received = propertyWriter.WriteBoolean("Received");
            this.m_Submitted = propertyWriter.WriteBoolean("Submitted");            
            this.m_OrderTime = propertyWriter.WriteNullableDateTime("OrderTime");
            this.m_OrderedBy = propertyWriter.WriteString("OrderedBy");
            this.m_PFirstName = propertyWriter.WriteString("PFirstName");
            this.m_PLastName = propertyWriter.WriteString("PLastName");
            this.m_ProviderName = propertyWriter.WriteString("ProviderName");
            this.m_ClientName = propertyWriter.WriteString("ClientName");
            this.m_OrderType = propertyWriter.WriteString("OrderType");
        }
    }
}
