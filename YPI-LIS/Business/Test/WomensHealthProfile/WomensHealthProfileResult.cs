using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.WomensHealthProfile
{
    public class WomensHealthProfileResult
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		private YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest m_PanelSetThinPrepPap;
		private YellowstonePathology.Business.Test.HPV.HPVTest m_PanelSetHPV;
        private YellowstonePathology.Business.Test.HPV1618.HPV1618Test m_HPV1618Test;
        private YellowstonePathology.Business.Test.NGCT.NGCTTest m_NGCTTest;
        private YellowstonePathology.Business.Test.Trichomonas.TrichomonasTest m_TrichomonasTest;

        private bool m_HasThinPrepPap;
        private bool m_HasHighRiskHPV;
        private bool m_HasHPV1618;
        private bool m_HasNGCT;
        private bool m_HasTrichomonas;
        private StringBuilder m_Method;
        private StringBuilder m_References;

		public WomensHealthProfileResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_References = new StringBuilder();
            this.m_Method = new StringBuilder();

            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
			this.m_PanelSetHPV = new Business.Test.HPV.HPVTest();
            this.m_HPV1618Test = new HPV1618.HPV1618Test();
            this.m_NGCTTest = new NGCT.NGCTTest();
            this.m_TrichomonasTest = new Trichomonas.TrichomonasTest();

            this.m_HasThinPrepPap = this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_PanelSetThinPrepPap.PanelSetId);
            this.m_HasHighRiskHPV = this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_PanelSetHPV.PanelSetId);
            this.m_HasHPV1618 = this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_HPV1618Test.PanelSetId);
            this.m_HasNGCT = this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_NGCTTest.PanelSetId);
            this.m_HasTrichomonas = this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_TrichomonasTest.PanelSetId);

            this.SetData();
        }

        private void SetData()
        {   
            if(this.m_HasThinPrepPap == true)
            {
				YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetThinPrepPap.PanelSetId);
                this.m_Method.Append("Thin Prep Pap: " + panelSetOrderCytology.Method);                
                this.m_References.Append("Thin Prep Pap: " + panelSetOrderCytology.ReportReferences);
            }

            if (this.m_HasHighRiskHPV == true)
            {
                YellowstonePathology.Business.Test.HPV.HPVTestOrder hpvTestOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetHPV.PanelSetId);
                if (this.m_HasThinPrepPap == true)
                {
                    this.m_Method.AppendLine();
                    this.m_Method.AppendLine();

                    this.m_References.AppendLine();
                    this.m_References.AppendLine();
                }
                this.m_Method.AppendLine("High Risk HPV: " + hpvTestOrder.TestInformation);
                this.m_References.AppendLine("High Risk HPV: " + hpvTestOrder.ReportReferences);
            }
            
            if(this.m_HasHPV1618 == true)
            {
                if(this.m_HasThinPrepPap == true && this.m_HasHighRiskHPV == false)
                {
                    this.m_Method.AppendLine();
                    this.m_Method.AppendLine();
                    this.m_References.AppendLine();
                    this.m_References.AppendLine();
                }
                else if(this.m_HasHighRiskHPV == true)
                {
                    this.m_Method.AppendLine();
                    this.m_References.AppendLine();
                }
                this.m_Method.AppendLine("HPV Genotypes 16 and 18: " + HPV1618.HPV1618Result.Method);
                this.m_References.AppendLine("HPV Genotypes 16 and 18: " + HPV1618.HPV1618Result.References);

            }

            if (this.m_HasNGCT == true)
            {
                if (this.m_HasThinPrepPap == true && this.m_HasHighRiskHPV == false && this.m_HasHPV1618 == false)
                {
                    this.m_Method.AppendLine();
                    this.m_Method.AppendLine();
                    this.m_References.AppendLine();
                    this.m_References.AppendLine();
                }
                else if (m_HasHighRiskHPV == true || this.m_HasHPV1618 == true)
                {
                    this.m_Method.AppendLine();
                    this.m_References.AppendLine();
                }
                this.m_Method.AppendLine("NG-CT:" + NGCT.NGCTResult.Method);
                this.m_References.AppendLine("NG-CT:" + NGCT.NGCTResult.References);
            }

            if (this.m_HasTrichomonas == true)
            {
                if (this.m_HasThinPrepPap == true && this.m_HasHighRiskHPV == false && this.m_HasHPV1618 == false && this.m_HasNGCT == false)
                {
                    this.m_Method.AppendLine();
                    this.m_Method.AppendLine();
                    this.m_References.AppendLine();
                    this.m_References.AppendLine();
                }
                else if (m_HasHighRiskHPV == true || this.m_HasHPV1618 == true || this.m_HasNGCT == true)
                {
                    this.m_Method.AppendLine();
                    this.m_References.AppendLine();
                }
                this.m_Method.AppendLine("Trichomonas Vaginalis:" + Trichomonas.TrichomonasResult.Method);
                this.m_References.AppendLine("Trichomonas Vaginalis:" + Trichomonas.TrichomonasResult.References);
            }
        }       

        public string Method
        {
            get { return this.m_Method.ToString(); }
        }

        public string References
        {
            get { return this.m_References.ToString(); }
        }
    }
}
