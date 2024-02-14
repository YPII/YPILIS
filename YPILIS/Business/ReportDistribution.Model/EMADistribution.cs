using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class EMADistribution
    {
        public const string DistributionType = "EMA";        

        public EMADistribution()            
        {

        }

        public DistributionResult Distribute(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            DistributionResult result = new DistributionResult();

            Business.HL7View.EMA.EMAResultView resultView = new Business.HL7View.EMA.EMAResultView(reportNo, accessionOrder, false);
            Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            resultView.Send(methodResult);

            result.IsComplete = methodResult.Success;
            result.Message = methodResult.Message;

            return result;
        }        
    }
}
