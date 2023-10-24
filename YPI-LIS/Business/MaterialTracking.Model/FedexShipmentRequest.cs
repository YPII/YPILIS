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
    public class FedexShipmentRequest
    {
        private FedexAccount m_FedexAccount;
        private XDocument m_ProcessShipmentRequest;
        private string m_MasterAccessionNo;

        private string m_TrackingNumber;
        private string m_ShipToName;
        private string m_ShipToPhone;
        private string m_ShipToAddress1;
        private string m_ShipToAddress2;
        private string m_ShipToCity;
        private string m_ShipToState;
        private string m_ShipToZip;
        private string m_PaymentType;
        private string m_AccountNo;
        private string m_ServiceType;

        public FedexShipmentRequest(FedexAccount fedexAccount, string masterAccessionNo, string paymentType, 
            string serviceType, string trackingNumber, string shipToName, string shipToPhone, string shipToAddress1,
            string shipToAddress2, string shipToCity, string shipToState, string shipToZip, string accountNo)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_FedexAccount = fedexAccount;
            this.m_PaymentType = paymentType;
            this.m_ServiceType = serviceType;

            this.m_TrackingNumber = trackingNumber;
            this.m_ShipToName = shipToName;
            this.m_ShipToPhone = shipToPhone;
            this.m_ShipToAddress1 = shipToAddress1;
            this.m_ShipToAddress2 = shipToAddress2;
            this.m_ShipToCity = shipToCity;
            this.m_ShipToState = shipToState;
            this.m_ShipToZip = shipToZip;
            this.m_AccountNo = accountNo;

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

            return result;
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

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ServiceType", namespaces).Value = this.m_ServiceType;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShipTimestamp", namespaces).Value = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"); //2016-08-25T16:30:12

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Contact/v19:PersonName", namespaces).Value = string.Empty;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Contact/v19:CompanyName", namespaces).Value = this.m_ShipToName;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Contact/v19:PhoneNumber", namespaces).Value = this.m_ShipToPhone;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Contact/v19:EMailAddress", namespaces).Value = string.Empty;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Address/v19:StreetLines[1]", namespaces).Value = this.m_ShipToAddress1;

            if(string.IsNullOrEmpty(this.m_ShipToAddress2) == false)
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Address/v19:StreetLines[2]", namespaces).Value = this.m_ShipToAddress2;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Address/v19:City", namespaces).Value = this.m_ShipToCity;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Address/v19:StateOrProvinceCode", namespaces).Value = this.m_ShipToState;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Address/v19:PostalCode", namespaces).Value = this.m_ShipToZip;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:Recipient/v19:Address/v19:CountryCode", namespaces).Value = "US";

            string userName = Business.User.SystemIdentity.Instance.User.UserName;
            if(userName.Length > 6)
            {
                userName = userName.Substring(0, 6);
            }

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:RequestedPackageLineItems/v19:CustomerReferences/v19:Value", namespaces).Value = userName + ": " + m_MasterAccessionNo;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:PaymentType", namespaces).Value = this.m_PaymentType;
            if (this.m_PaymentType == "THIRD_PARTY" || this.m_PaymentType == "RECIPIENT")
            {
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:AccountNumber", namespaces).Value = this.m_AccountNo;

                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Contact/v19:CompanyName", namespaces).Value = this.m_ShipToName;
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Contact/v19:PhoneNumber", namespaces).Value = this.m_ShipToPhone;

                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:StreetLines[1]", namespaces).Value = this.m_ShipToAddress1;
                if(string.IsNullOrEmpty(this.m_ShipToAddress2) == false)
                {
                    this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:StreetLines[2]", namespaces).Value = this.m_ShipToAddress2;
                }
                
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:City", namespaces).Value = this.m_ShipToCity;
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:StateOrProvinceCode", namespaces).Value = this.m_ShipToState;
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:PostalCode", namespaces).Value = this.m_ShipToZip;
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:CountryCode", namespaces).Value = "US";
            }
            else if(this.m_PaymentType == "SENDER")
            {
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:AccountNumber", namespaces).Value = this.m_FedexAccount.AccountNo;

                Business.Facility.Model.Facility ypi = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Contact/v19:CompanyName", namespaces).Value = ypi.FacilityName;
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Contact/v19:PhoneNumber", namespaces).Value = ypi.PhoneNumber;

                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:StreetLines[1]", namespaces).Value = ypi.Address1;
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:StreetLines[2]", namespaces).Value = ypi.Address2;
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:City", namespaces).Value = ypi.City;
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:StateOrProvinceCode", namespaces).Value = ypi.State;
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:PostalCode", namespaces).Value = ypi.ZipCode;
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment/v19:Payor/v19:ResponsibleParty/v19:Address/v19:CountryCode", namespaces).Value = "US";
            }
            else
            {
                throw new Exception("Payment Type not supported.");
            }   
            
            if(DateTime.Today.DayOfWeek == DayOfWeek.Friday && this.m_ServiceType.Contains("OVERNIGHT") == true)
            {
                XNamespace v19 ="http://fedex.com/ws/ship/v19";                                
                this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:ProcessShipmentRequest/v19:RequestedShipment/v19:ShippingChargesPayment", namespaces)
                    .AddAfterSelf(new XElement(v19 + "SpecialServicesRequested", new XElement(v19 + "SpecialServiceTypes", "SATURDAY_DELIVERY")));
            }        
        }

        private void OpenShipmentRequestFile()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            using (var stream = asm.GetManifestResourceStream("YellowstonePathology.Business.MaterialTracking.Model.FedexProcessShipmentRequest.v19.xml"))
            {
                this.m_ProcessShipmentRequest = XDocument.Load(stream);
            }
        }
    }
}
