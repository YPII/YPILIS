using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class DoNotDistributeDistribution : Distribution
    {
        public const string DistributionType = "Do Not Distribute";

        public DoNotDistributeDistribution()
            : base(DistributionType)
        {

        }		
    }
}
