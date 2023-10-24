using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.HER2AnalysisSummary
{
    public class HER2AnalysisSummarySystemGeneratedAmendmentText
    {
        public HER2AnalysisSummarySystemGeneratedAmendmentText(Test.AccessionOrder accessionOrder)
        { }

        public static string AmendmentText(YellowstonePathology.Business.Test.HER2AnalysisSummary.HER2AnalysisSummaryTestOrder testOrder)
        {
            string amendmentText = "HER2 Analysis using Amplification by D-ISH and IHC were performed (see YPI report #" + testOrder.ReportNo + "), with the following results: \n\n" +
                    "HER2 Amplification: " + testOrder.Result;
            return amendmentText;
        }
    }
}
