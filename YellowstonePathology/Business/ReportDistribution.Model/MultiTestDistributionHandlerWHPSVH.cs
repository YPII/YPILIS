using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MultiTestDistributionHandlerWHPSVH : MultiTestDistributionHandlerWHP
    {
        public MultiTestDistributionHandlerWHPSVH(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {
            this.m_DistributePap = true;
            this.m_DistributeHPV = true;
            this.m_DistributeHPV1618 = true;
            this.m_DistributeNGCT = true;
            this.m_DistributeTrich = true;
            this.m_DistributeHWP = false;
        }       
    }
}
