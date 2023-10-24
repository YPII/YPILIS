using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MultiTestDistributionHandlerWHPHold : MultiTestDistributionHandlerWHP
    {
        public MultiTestDistributionHandlerWHPHold(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {
            this.m_DistributePap = true;
            this.m_DistributeHPV = true;
            this.m_DistributeHPV1618 = true;
            this.m_DistributeNGCT = true;
            this.m_DistributeTrich = true;
            this.m_DistributeWHP = true;

            this.m_HoldPap = true;
            this.m_HoldHPV = true;
            this.m_HoldHPV1618 = true;
            this.m_HoldNGCT = true;
            this.m_HoldTrich = true;
            this.m_HoldWHP = false;
        }
    }
}
