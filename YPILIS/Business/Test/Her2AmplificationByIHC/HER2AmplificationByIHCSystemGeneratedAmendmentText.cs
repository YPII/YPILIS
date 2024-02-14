using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
    public class HER2AmplificationByIHCSystemGeneratedAmendmentText
    {
        public HER2AmplificationByIHCSystemGeneratedAmendmentText()
        {
        }

        public static string AmendmentText(YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC testOrder)
        {
            string amendmentText = "HER2 Amplification by immunohistochemistry were performed(see YPI report #" + testOrder.ReportNo + "), with the following results: \n\n" +
                    "HER2 Amplification: " + testOrder.Result;            
            return amendmentText;
        }
    }
}
