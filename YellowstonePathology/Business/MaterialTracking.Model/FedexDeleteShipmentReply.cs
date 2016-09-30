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
    public class FedexDeleteShipmentReply
    {        
        private XDocument m_ShipmentResponse;     
        private bool m_RequestWasSuccessful;

        public FedexDeleteShipmentReply(string response)
        {
            XmlNamespaceManager namespaces = new XmlNamespaceManager(new NameTable());
            namespaces.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            namespaces.AddNamespace("ns", "http://fedex.com/ws/ship/v19");

            this.m_ShipmentResponse = XDocument.Parse(response);            
            string status = this.m_ShipmentResponse.XPathSelectElement("//SOAP-ENV:Envelope/SOAP-ENV:Body/ns:ShipmentReply/ns:HighestSeverity", namespaces).Value;
            if (status == "SUCCESS")
            {
                this.m_RequestWasSuccessful = true;
            }
            else
            {
                this.m_RequestWasSuccessful = false;
            }            
        }

        public FedexDeleteShipmentReply(bool requestWasSuccessful)
        {
            this.m_RequestWasSuccessful = requestWasSuccessful;
        }        

        public bool RequestWasSuccessful
        {
            get { return this.m_RequestWasSuccessful; }
        }
    }
}
