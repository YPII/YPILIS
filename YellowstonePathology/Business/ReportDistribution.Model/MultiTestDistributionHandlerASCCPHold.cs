using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MultiTestDistributionHandlerASCCPHold : MultiTestDistributionHandler
    {
        public MultiTestDistributionHandlerASCCPHold(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {

        }

        public override void Set()
        {
            YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder whpTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetWomensHealthProfile();
            whpTestOrder.HoldDistribution = true;
        }
    }
}
