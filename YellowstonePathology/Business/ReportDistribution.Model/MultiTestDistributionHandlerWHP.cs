using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class MultiTestDistributionHandlerWHP : MultiTestDistributionHandler
    {
		private YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest m_PanelSetThinPrepPap;
		private YellowstonePathology.Business.Test.HPV.HPVTest m_PanelSetHPV;
		private YellowstonePathology.Business.Test.HPV1618.HPV1618Test m_PanelSetHPV1618;
        private YellowstonePathology.Business.Test.NGCT.NGCTTest m_NGCTTest;
        private YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest m_TrichomonasTest;
        private YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest m_WomensHealthProfileTest;
        private YellowstonePathology.Business.Test.PanelSetOrder m_WomensHealthProfileTestOrder;                

        private List<YellowstonePathology.Business.Test.PanelSetOrder> m_PanelSetOrderList;        

        protected bool m_DistributePap;
        protected bool m_DistributeHPV;
        protected bool m_DistributeHPV1618;
        protected bool m_DistributeNGCT;
        protected bool m_DistributeTrich;
        protected bool m_DistributeHWP;

        public MultiTestDistributionHandlerWHP(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrderList = new List<Test.PanelSetOrder>();

			this.m_PanelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
			this.m_PanelSetHPV = new Test.HPV.HPVTest();
			this.m_PanelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
            this.m_NGCTTest = new YellowstonePathology.Business.Test.NGCT.NGCTTest();
            this.m_TrichomonasTest = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest();
            this.m_WomensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
            this.m_WomensHealthProfileTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_WomensHealthProfileTest.PanelSetId);

            if (this.m_WomensHealthProfileTestOrder.Final == false)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_PanelSetThinPrepPap.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.PanelSetOrder psoThinPrep = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetThinPrepPap.PanelSetId);
                    this.m_PanelSetOrderList.Add(psoThinPrep);
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_PanelSetHPV.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.PanelSetOrder psoHPV = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetHPV.PanelSetId);
                    this.m_PanelSetOrderList.Add(psoHPV);
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_PanelSetHPV1618.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.PanelSetOrder pso1618 = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetHPV1618.PanelSetId);
                    this.m_PanelSetOrderList.Add(pso1618);
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_NGCTTest.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.PanelSetOrder psoNGCT = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_NGCTTest.PanelSetId);
                    this.m_PanelSetOrderList.Add(psoNGCT);
                }

                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_TrichomonasTest.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.PanelSetOrder psoTrich = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_TrichomonasTest.PanelSetId);
                    this.m_PanelSetOrderList.Add(psoTrich);
                }
                
                this.m_PanelSetOrderList.Add(this.m_WomensHealthProfileTestOrder);
            }            
        }

        public override void Set()
        {
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.m_PanelSetOrderList)
            {                
                if (panelSetOrder.TestOrderReportDistributionCollection.HasDistributedItems() == false)
                {
                    if (panelSetOrder.PanelSetId == this.m_PanelSetThinPrepPap.PanelSetId)
                    {
                        panelSetOrder.Distribute = this.m_DistributePap;
                    }
                    else if (panelSetOrder.PanelSetId == this.m_PanelSetHPV.PanelSetId)
                    {
                        panelSetOrder.Distribute = this.m_DistributeHPV;
                    }
                    else if (panelSetOrder.PanelSetId == this.m_PanelSetHPV1618.PanelSetId)
                    {
                        panelSetOrder.Distribute = this.m_DistributeHPV1618;
                    }
                    else if (panelSetOrder.PanelSetId == this.m_NGCTTest.PanelSetId)
                    {
                        panelSetOrder.Distribute = this.m_DistributeNGCT;
                    }
                    else if (panelSetOrder.PanelSetId == this.m_TrichomonasTest.PanelSetId)
                    {
                        panelSetOrder.Distribute = this.m_DistributeTrich;
                    }
                    else if (panelSetOrder.PanelSetId == this.m_WomensHealthProfileTest.PanelSetId)
                    {
                        panelSetOrder.Distribute = this.m_DistributeHWP;
                    }
                }                
            }
        }
    }
}
