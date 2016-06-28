using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class EpicDistribution : Distribution
    {
        public const string DistributionType = "EPIC";

        public EpicDistribution()
            : base(DistributionType)
        {

        }

        public override YellowstonePathology.Business.Rules.MethodResult Distribute(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {            
            Business.HL7View.EPIC.EPICResultView epicResultView = new HL7View.EPIC.EPICResultView(reportNo, accessionOrder, false);
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            epicResultView.CanSend(result);
            return result;            
        }
    }
}
