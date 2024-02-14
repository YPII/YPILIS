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
    public class FedexLocationSearchRequest
    {        
        private FedexAccount m_FedexAccount;
        private XDocument m_LocationSearchRequest;                

        public FedexLocationSearchRequest()
        {
            this.m_FedexAccount = new FedexAccountProduction();
            this.OpenLocationSearchRequestFile();            
        }        

        public string LocationSearch()
        {            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.m_FedexAccount.URL);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(this.m_LocationSearchRequest.ToString());
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            System.IO.Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse response;
            response = (HttpWebResponse)request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            string responseStr = new System.IO.StreamReader(responseStream).ReadToEnd();
            return responseStr;            
        }
              
        private void OpenLocationSearchRequestFile()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            using (var stream = asm.GetManifestResourceStream("YellowstonePathology.Business.MaterialTracking.Model.FedexSearchLocationRequest.v1.xml"))
            {
                this.m_LocationSearchRequest = XDocument.Load(stream);
            }
        }
    }
}
