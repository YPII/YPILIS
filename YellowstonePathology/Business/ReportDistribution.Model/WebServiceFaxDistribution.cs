using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class WebServiceFaxDistribution : Distribution
    {
        public const string DistributionType = "Web Service and Fax";

        public WebServiceFaxDistribution()
            : base(DistributionType)
        { 

        }		
    }
}
