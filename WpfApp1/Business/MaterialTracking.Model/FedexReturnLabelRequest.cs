using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Xml.XPath;

namespace YellowstonePathology.Business.MaterialTracking.Model
{
    public class FedexReturnLabelRequest
    {
        private string m_ShipperName;
        private string m_ShipperPhone;
        private string m_ShipperAddress1;
        private string m_ShipperAddress2;
        private string m_ShipperCity;
        private string m_ShipperState;
        private string m_ShipperZip;

        private Business.Facility.Model.Facility m_ShipToFacility;
        private FedexAccount m_FedexAccount;
        private XDocument m_ProcessShipmentRequest;                

        public FedexReturnLabelRequest(string shipperName, string shipperPhone, string shipperAddress1, string shipperAddress2, string shipperCity, string shipperState, string shipperZip, FedexAccount fedexAccount)
        {
            this.m_ShipToFacility = Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ShipperName = shipperName;
            this.m_ShipperPhone = shipperPhone;
            this.m_ShipperAddress1 = shipperAddress1;
            this.m_ShipperAddress2 = shipperAddress2;
            this.m_ShipperCity = shipperCity;
            this.m_ShipperState = shipperState;
            this.m_ShipperZip = shipperZip;

            this.m_FedexAccount = fedexAccount;                        

            this.OpenShipmentRequestFile();
            this.SetShipmentRequestData();
        }        

        public Business.MaterialTracking.Model.FedexProcessShipmentReply RequestShipment()
        {
            Business.MaterialTracking.Model.FedexProcessShipmentReply result = null;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.m_FedexAccount.URL);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(this.m_ProcessShipmentRequest.ToString());
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            System.IO.Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                System.IO.Stream responseStream = response.GetResponseStream();
                string responseStr = new System.IO.StreamReader(responseStream).ReadToEnd();
                result = new FedexProcessShipmentReply(responseStr);
            }
            else
            {
                result = new FedexProcessShipmentReply(false);
            }

            return result; ;
        }

        private void SetShipmentRequestData()
        {
            XmlNamespaceManager namespaces = new XmlNamespaceManager(new NameTable());            
            namespaces.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            namespaces.AddNamespace("v19", "http://fedex.com/ws/ship/v19");

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:WebAuthenticationDetail/v19:UserCredential/v19:Key", namespaces).Value = this.m_FedexAccount.Key;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:WebAuthenticationDetail/v19:UserCredential/v19:Password", namespaces).Value = this.m_FedexAccount.Password;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:ClientDetail/v19:AccountNumber", namespaces).Value = this.m_FedexAccount.AccountNo;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:ClientDetail/v19:MeterNumber", namespaces).Value = this.m_FedexAccount.MeterNo;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShipTimestamp", namespaces).Value = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"); //2016-08-25T16:30:12

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Shipper/v19:Contact/v19:PersonName", namespaces).Value = string.Empty;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Origin/v19:Contact/v19:PersonName", namespaces).Value = string.Empty;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Shipper/v19:Contact/v19:CompanyName", namespaces).Value = this.m_ShipperName;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Origin/v19:Contact/v19:CompanyName", namespaces).Value = this.m_ShipperName;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Shipper/v19:Contact/v19:PhoneNumber", namespaces).Value = this.m_ShipperPhone;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Origin/v19:Contact/v19:PhoneNumber", namespaces).Value = this.m_ShipperPhone;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Shipper/v19:Contact/v19:EMailAddress", namespaces).Value = string.Empty;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Origin/v19:Contact/v19:EMailAddress", namespaces).Value = string.Empty;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Shipper/v19:Address/v19:StreetLines[1]", namespaces).Value = this.m_ShipperAddress1;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Origin/v19:Address/v19:StreetLines[1]", namespaces).Value = this.m_ShipperAddress1;

            if (string.IsNullOrEmpty(this.m_ShipperAddress2) == false)
            {
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Shipper/v19:Address/v19:StreetLines[2]", namespaces).Value = this.m_ShipperAddress2;
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Origin/v19:Address/v19:StreetLines[2]", namespaces).Value = this.m_ShipperAddress2;
            }
                        
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Shipper/v19:Address/v19:City", namespaces).Value = this.m_ShipperCity;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Origin/v19:Address/v19:City", namespaces).Value = this.m_ShipperCity;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Shipper/v19:Address/v19:StateOrProvinceCode", namespaces).Value = this.m_ShipperState;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Origin/v19:Address/v19:StateOrProvinceCode", namespaces).Value = this.m_ShipperState;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Shipper/v19:Address/v19:PostalCode", namespaces).Value = this.m_ShipperZip;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Origin/v19:Address/v19:PostalCode", namespaces).Value = this.m_ShipperZip;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Shipper/v19:Address/v19:CountryCode", namespaces).Value = "US";
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Origin/v19:Address/v19:CountryCode", namespaces).Value = "US";

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:PaymentType", namespaces).Value = "THIRD_PARTY";                        
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:AccountNumber", namespaces).Value = this.m_FedexAccount.OptiFreightAccountNo;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Contact/v19:CompanyName", namespaces).Value = this.m_ShipToFacility.FacilityName;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Contact/v19:PhoneNumber", namespaces).Value = this.m_ShipToFacility.PhoneNumber;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:StreetLines[1]", namespaces).Value = this.m_ShipToFacility.Address1;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:StreetLines[2]", namespaces).Value = this.m_ShipToFacility.Address2;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:City", namespaces).Value = this.m_ShipToFacility.City;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:StateOrProvinceCode", namespaces).Value = this.m_ShipToFacility.State;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:PostalCode", namespaces).Value = this.m_ShipToFacility.ZipCode;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:CountryCode", namespaces).Value = "US";            
        }

        private void OpenShipmentRequestFile()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            using (var stream = asm.GetManifestResourceStream("YellowstonePathology.Business.MaterialTracking.Model.FedexProcessShipmentRequestReturn.v19.xml"))
            {
                this.m_ProcessShipmentRequest = XDocument.Load(stream);
            }
        }
    }
}
