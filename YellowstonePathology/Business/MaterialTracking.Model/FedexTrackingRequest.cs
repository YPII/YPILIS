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
    public class FedexTrackingRequest
    {        
        private FedexAccount m_FedexAccount;
        private XDocument m_TrackingRequest;
        private string m_TrackingNumber;

        public FedexTrackingRequest(FedexAccount fedexAccount, string trackingNumber)
        {     
            this.m_FedexAccount = fedexAccount;
            this.m_TrackingNumber = trackingNumber;

            this.OpenShipmentRequestFile();
            this.SetShipementRequestData();
        }        

        public string Post()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.m_FedexAccount.URL);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(this.m_TrackingRequest.ToString());
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
                return responseStr;
            }
            return null;
        }

        private void SetShipementRequestData()
        {
            XmlNamespaceManager namespaces = new XmlNamespaceManager(new NameTable());            
            namespaces.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
            namespaces.AddNamespace("v9", "http://fedex.com/ws/track/v9");

            this.m_TrackingRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v9:TrackRequest/v9:WebAuthenticationDetail/v9:UserCredential/v9:Key", namespaces).Value = this.m_FedexAccount.Key;
            this.m_TrackingRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v9:TrackRequest/v9:WebAuthenticationDetail/v9:UserCredential/v9:Password", namespaces).Value = this.m_FedexAccount.Password;

            this.m_TrackingRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v9:TrackRequest/v9:ClientDetail/v9:AccountNumber", namespaces).Value = this.m_FedexAccount.AccountNo;
            this.m_TrackingRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v9:TrackRequest/v9:ClientDetail/v9:MeterNumber", namespaces).Value = this.m_FedexAccount.MeterNo;

            this.m_TrackingRequest.XPathSelectElement("//soapenv:Envelope/soapenv:Body/v9:TrackRequest/v9:SelectionDetails/v9:PackageIdentifier/v9:Value", namespaces).Value = this.m_TrackingNumber;
        }

        private void OpenShipmentRequestFile()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            using (var stream = asm.GetManifestResourceStream("YellowstonePathology.Business.MaterialTracking.Model.FedexTrackingRequest.v9.xml"))
            {
                this.m_TrackingRequest = XDocument.Load(stream);
            }
        }
    }
}
