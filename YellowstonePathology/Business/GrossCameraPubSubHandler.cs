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

        public void ShowDialog(Business.Test.AccessionOrder accessionOrder)
        {
            string message = GetMessage(accessionOrder);
            YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("Please_Show_Gross_Camera_Dialog", message);
        }

        public void CloseDialog(Business.Test.AccessionOrder accessionOrder)
        {
            string message = GetMessage(accessionOrder);
            YellowstonePathology.Store.RedisServerProd1.Instance.Subscriber.Publish("Please_Hide_Gross_Camera_Dialog", message);
        }

        private string GetMessage(Business.Test.AccessionOrder accessionOrder)
        {
            Business.OrderIdParser orderIdParser = new Business.OrderIdParser(accessionOrder.MasterAccessionNo);
            string caseDocumentPath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);

            StringBuilder result = new StringBuilder();
            result.Append("{");
            result.Append("\"Master AccessionNo\": ");
            result.Append("\"" + accessionOrder.MasterAccessionNo + "\"");
            result.Append(",\"Patient Name\": ");
            result.Append("\"" + accessionOrder.PatientDisplayName + "\"");
            result.Append(",\"Case Document Path\": " + caseDocumentPath);
            result.Append("}");
            return result.ToString();
        }
    }
}
