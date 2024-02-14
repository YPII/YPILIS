using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace YellowstonePathology.Business.MaterialTracking.Model
{
    public class FedexProcessShipmentReply
    {
        private string m_TrackingNumber;
        private XDocument m_ShipmentResponse;
        private string m_ZPLII;
        private bool m_RequestWasSuccessful;
        private string m_Message;

        public FedexProcessShipmentReply(string response)
        {
            XmlNamespaceManager namespaces = new XmlNamespaceManager(new NameTable());
            namespaces.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            namespaces.AddNamespace("ns", "http://fedex.com/ws/ship/v19");

            this.m_ShipmentResponse = XDocument.Parse(response);            
            string status = this.m_ShipmentResponse.XPathSelectElement("//SOAP-ENV:Envelope/SOAP-ENV:Body/ns:ProcessShipmentReply/ns:HighestSeverity", namespaces).Value;
            if (status == "SUCCESS" || status == "WARNING")
            {
                this.m_TrackingNumber = this.m_ShipmentResponse.XPathSelectElement("//SOAP-ENV:Envelope/SOAP-ENV:Body/ns:ProcessShipmentReply/ns:CompletedShipmentDetail/ns:CompletedPackageDetails/ns:TrackingIds/ns:TrackingNumber", namespaces).Value;
                this.m_ZPLII = this.m_ShipmentResponse.XPathSelectElement("//SOAP-ENV:Envelope/SOAP-ENV:Body/ns:ProcessShipmentReply/ns:CompletedShipmentDetail/ns:CompletedPackageDetails/ns:Label/ns:Parts/ns:Image", namespaces).Value;
                this.m_RequestWasSuccessful = true;
            }
            else
            {
                this.m_Message = this.m_ShipmentResponse.XPathSelectElement("//SOAP-ENV:Envelope/SOAP-ENV:Body/ns:ProcessShipmentReply/ns:Notifications/ns:LocalizedMessage", namespaces).Value;                
                this.m_RequestWasSuccessful = false;
            }            
        }

        public FedexProcessShipmentReply(bool requestWasSuccessful)
        {
            this.m_RequestWasSuccessful = requestWasSuccessful;
        }

        public string TrackingNumber
        {
            get { return this.m_TrackingNumber; }
        }

        public string ZPLII
        {
            get { return this.m_ZPLII; }
        }

        public bool RequestWasSuccessful
        {
            get { return this.m_RequestWasSuccessful; }
        }

        public string Message
        {
            get { return this.m_Message; }
        }
    }
}
