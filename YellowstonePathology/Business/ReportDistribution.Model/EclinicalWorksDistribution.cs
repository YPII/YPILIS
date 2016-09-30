using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class EclinicalWorksDistribution : Distribution
    {
        public const string DistributionType = "Eclinical Works";

        public EclinicalWorksDistribution()
            : base(DistributionType)
        {

        }

        public override YellowstonePathology.Business.Rules.MethodResult Distribute(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {                                    
            Business.HL7View.ECW.ECWResultView ecwResultView = new HL7View.ECW.ECWResultView(reportNo, accessionOrder, false);
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            ecwResultView.CanSend(result);                        
            return result;
        }
    }
}
