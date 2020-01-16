using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class GrossCameraPubSubHandler
    {
        private Business.Test.AccessionOrder m_AccessionOrder;

        public GrossCameraPubSubHandler()
        {
            
        }

        public void ShowDialog()
        {         
            YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("gross_camera_show_dialog", "Hello");
        }

        public void CloseDialog()
        {            
            YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("gross_camera_hide_dialog", "Goodbye");
        }

        public void CaseAquired(Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            string message = GetMessage(this.m_AccessionOrder);
            YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("gross_camera_case_aquired", this.GetMessage(accessionOrder));
        }

        public void CaseReleased()
        {            
            YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("gross_camera_case_released", "Case Released");
        }

        private string GetMessage(Business.Test.AccessionOrder accessionOrder)
        {
            Business.OrderIdParser orderIdParser = new Business.OrderIdParser(accessionOrder.MasterAccessionNo);
            string caseDocumentPath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
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
