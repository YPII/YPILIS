using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.PDL122C3forTNBCBreast
{
    public class PDL122C3forTNBCBreastSystemGeneratedAmendmentText
    {
        public PDL122C3forTNBCBreastSystemGeneratedAmendmentText()
        {
        }

        public static string AmendmentText(PDL122C3forTNBCBreast.PDL122C3forTNBCBreastTestOrder testOrder)
        {
            string comment = "Comment: This test utilizes PD-L1 antibody clone 223C.  In general, higher levels of " +
                "PD-L1 expression are associated with better response to PD-1 antagonists.  For full details, refer to separate report.";

            StringBuilder result = new StringBuilder();
            result.Append("PD-L1 immunohistochemical stain was performed (YPI report # " + testOrder.ReportNo + ")");
            result.AppendLine(", which yielded the following result:");
            result.AppendLine();
            result.AppendLine(testOrder.Interpretation);

            result.AppendLine();
            result.AppendLine(comment);

            return result.ToString();
        }
    }
}
