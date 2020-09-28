using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MultiTestDistributionHandlerNoWHP : MultiTestDistributionHandlerWHP
    {
        public MultiTestDistributionHandlerNoWHP(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {
            this.m_DistributePap = true;
            this.m_DistributeHPV = true;
            this.m_DistributeHPV1618 = true;
            this.m_DistributeNGCT = true;
            this.m_DistributeTrich = true;
            this.m_DistributeWHP = false;

            this.m_HoldPap = false;
            this.m_HoldHPV = false;
            this.m_HoldHPV1618 = false;
            this.m_HoldNGCT = false;
            this.m_HoldTrich = false;
            this.m_HoldWHP = false;

            YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder whpTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetWomensHealthProfile();
            if (whpTestOrder.ManagePerASCCP == true || whpTestOrder.ManagePerASCCPWithCotest == true)
            {
                this.m_DistributeWHP = true;
                this.m_HoldWHP = false;
            }
        }
    }
}
