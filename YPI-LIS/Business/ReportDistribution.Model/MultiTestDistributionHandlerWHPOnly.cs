using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MultiTestDistributionHandlerWHPOnly : MultiTestDistributionHandlerWHP
    {
        public MultiTestDistributionHandlerWHPOnly(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {
            YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfile = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(womensHealthProfile.PanelSetId) == true)
            {
                this.m_DistributePap = false;
            }
            else
            {
                this.m_DistributePap = true;
            }
            
            this.m_DistributeHPV = false;
            this.m_DistributeHPV1618 = false;
            this.m_DistributeNGCT = false;
            this.m_DistributeTrich = false;
            this.m_DistributeWHP = true;
        }       
    }
}
