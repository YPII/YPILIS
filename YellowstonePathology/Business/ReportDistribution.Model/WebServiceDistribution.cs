using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class WebServiceDistribution : Distribution
    {
        public const string DistributionType = "Web Service";

        public WebServiceDistribution()
            : base(DistributionType)
        {

        }

        public override YellowstonePathology.Business.Rules.MethodResult Distribute(string reportNo, Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            result.Message = "Nothing to distribute on WebService";
            result.Success = true;
            return result;
        }        
    }
}
