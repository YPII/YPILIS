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
    public class FedexDeleteShipmentRequest
    {        
        private FedexAccount m_FedexAccount;
        private XDocument m_ProcessShipmentRequest;
        private string m_TrackingNumber;

        public FedexDeleteShipmentRequest(FedexAccount fedexAccount, string trackingNumber)
        {            
            this.m_FedexAccount = fedexAccount;
            this.m_TrackingNumber = trackingNumber;
            this.OpenShipmentRequestFile();
            this.SetShipementRequestData();
        }        

        public FedexDeleteShipmentReply Post()
        {
            FedexDeleteShipmentReply result = null;

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
                result = new FedexDeleteShipmentReply(responseStr);
            }
            else
            {
                result = new FedexDeleteShipmentReply(false);
            }                

            return result;
        }

        private void SetShipementRequestData()
        {
            XmlNamespaceManager namespaces = new XmlNamespaceManager(new NameTable());            
            namespaces.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            namespaces.AddNamespace("v19", "http://fedex.com/ws/ship/v19");

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:DeleteShipmentRequest/v19:WebAuthenticationDetail/v19:UserCredential/v19:Key", namespaces).Value = this.m_FedexAccount.Key;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:DeleteShipmentRequest/v19:WebAuthenticationDetail/v19:UserCredential/v19:Password", namespaces).Value = this.m_FedexAccount.Password;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:DeleteShipmentRequest/v19:ClientDetail/v19:AccountNumber", namespaces).Value = this.m_FedexAccount.AccountNo;
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:DeleteShipmentRequest/v19:ClientDetail/v19:MeterNumber", namespaces).Value = this.m_FedexAccount.MeterNo;

            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:DeleteShipmentRequest/v19:ShipTimestamp", namespaces).Value = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            this.m_ProcessShipmentRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v19:DeleteShipmentRequest/v19:TrackingId/v19:TrackingNumber", namespaces).Value = this.m_TrackingNumber;
        }

        private void OpenShipmentRequestFile()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            using (var stream = asm.GetManifestResourceStream("YellowstonePathology.Business.MaterialTracking.Model.FedexDeleteShipmentRequest.v19.xml"))
            {
                this.m_ProcessShipmentRequest = XDocument.Load(stream);
            }
        }
    }
}
