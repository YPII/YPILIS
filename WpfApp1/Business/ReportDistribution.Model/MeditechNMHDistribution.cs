using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MeditechNMHDistribution
    {
        public const string DistributionType = "Meditech NMH";        

        public MeditechNMHDistribution()            
        {

        }

        public DistributionResult Distribute(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            DistributionResult result = new DistributionResult();

            Business.HL7View.NMH.NMHResultView resultView = new Business.HL7View.NMH.NMHResultView(reportNo, accessionOrder, false);
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            resultView.Send(methodResult);

            result.IsComplete = methodResult.Success;
            result.Message = methodResult.Message;

            return result;
        }        
    }
}
