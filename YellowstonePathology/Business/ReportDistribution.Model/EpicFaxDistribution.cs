using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class EpicFaxDistribution : Distribution
    {
        public const string DistributionType = "EPIC and Fax";

        public EpicFaxDistribution()
            : base(DistributionType)
        {

        }		
    }
}
