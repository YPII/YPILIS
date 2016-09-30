using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MultiTestDistributionHandlerWHPStandard : MultiTestDistributionHandlerWHP
    {
        public MultiTestDistributionHandlerWHPStandard(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {
            this.m_DistributePap = true;
            this.m_DistributeHPV = false;
            this.m_DistributeHPV1618 = false;
            this.m_DistributeNGCT = false;
            this.m_DistributeTrich = false;

			YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
		    YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
            YellowstonePathology.Business.Test.NGCT.NGCTTest ngctTTest = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
            YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest trichomonasTest = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();

            this.m_DistributeHWP = false;
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == true) this.m_DistributeHWP = true;
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == true) this.m_DistributeHWP = true;
            if (accessionOrder.PanelSetOrderCollection.Exists(ngctTTest.PanelSetId) == true) this.m_DistributeHWP = true;
            if (accessionOrder.PanelSetOrderCollection.Exists(trichomonasTest.PanelSetId) == true) this.m_DistributeHWP = true;
        }        
    }
}
