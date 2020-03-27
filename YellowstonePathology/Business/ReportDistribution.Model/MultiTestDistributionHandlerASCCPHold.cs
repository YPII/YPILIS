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
            foreach(Business.Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                panelSetOrder.HoldDistribution = true;
            }
        }
    }
}
