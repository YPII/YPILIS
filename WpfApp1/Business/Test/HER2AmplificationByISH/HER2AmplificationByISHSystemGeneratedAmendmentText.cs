using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
    public class HER2AmplificationByISHSystemGeneratedAmendmentText
    {
        public HER2AmplificationByISHSystemGeneratedAmendmentText()
        {
        }

        public static string AmendmentText(YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder testOrder)
        {
            string amendmentText = string.Empty;
            if (testOrder.Her2byIHCOrder == 1)
            {
                amendmentText = "HER2 Amplification ISH and immunohistochemistry analyses were performed (YPI report #" + testOrder.ReportNo + "), which generated the following results: \n\n" +
                    "HER2 Amplification: " + testOrder.Result + "\n" +
                    testOrder.ResultDescription + "\n" +
                    "Average HER2 Copy Number = " + testOrder.AverageHer2NeuSignal.Value.ToString() + "\n" +
                    "HER2 Amplification: ???";
            }
            else
            {
                amendmentText = "HER2 Amplification ISH analysis was performed (YPI report #" + testOrder.ReportNo + "), which generated the following result: \n\n" +
                    "HER2 Amplification: " + testOrder.Result + "\n" +
                    testOrder.ResultDescription + "\n" +
                    "Average HER2 Copy Number = " + testOrder.AverageHer2NeuSignal.Value.ToString();
            }
            return amendmentText;
        }
    }
}
