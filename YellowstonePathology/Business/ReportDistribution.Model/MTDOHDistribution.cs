using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MTDOHDistribution : Distribution
    {
        public const string DistributionType = "MTDOH";

        public MTDOHDistribution()
            : base(DistributionType)
        {

        }

        public override YellowstonePathology.Business.Rules.MethodResult IsOkToSend(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            if (string.IsNullOrEmpty(accessionOrder.PAddress1) == true)
            {
                methodResult.Success = false;
            }
            if (string.IsNullOrEmpty(accessionOrder.PCity) == true)
            {
                methodResult.Success = false;
            }
            if (string.IsNullOrEmpty(accessionOrder.PState) == true)
            {
                methodResult.Success = false;
            }
            if (string.IsNullOrEmpty(accessionOrder.PZipCode) == true)
            {
                methodResult.Success = false;
            }
            return methodResult;
        }		

        public override YellowstonePathology.Business.Rules.MethodResult Distribute(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {            
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            YellowstonePathology.Business.HL7View.CDC.MTDohResultView mtDohResultView = new HL7View.CDC.MTDohResultView(reportNo, accessionOrder);
            mtDohResultView.CanSend(result);            
            return result;
        }
    }
}
