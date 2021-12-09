using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class GrossCameraPubSubHandler
    {
        public GrossCameraPubSubHandler()
        {

        }
        
        public void CaseAquired(Business.Test.AccessionOrder accessionOrder)
        {
            if(accessionOrder != null)
            {
                string message = GetMessage(accessionOrder);
                YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("gross_camera_case_aquired", this.GetMessage(accessionOrder));
            }
        }

        private string GetMessage(Business.Test.AccessionOrder accessionOrder)
        {
            Business.OrderIdParser orderIdParser = new Business.OrderIdParser(accessionOrder.MasterAccessionNo);
            string caseDocumentPath = Business.Document.CaseDocumentPath.GetPath(orderIdParser);
            caseDocumentPath = caseDocumentPath.Replace(@"\", @"\\");

            StringBuilder result = new StringBuilder();
            result.Append("{");
            result.Append("\"masterAccessionNo\": ");
            result.Append("\"" + accessionOrder.MasterAccessionNo + "\"");
            result.Append(",\"patientName\": ");
            result.Append("\"" + accessionOrder.PatientDisplayName + "\"");
            result.Append(",\"caseDocumentPath\": \"" + caseDocumentPath + "\"");
            result.Append("}");
            return result.ToString();    
        }
    }
}
