using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MPNStandardReflex
{
	public class MPNStandardReflexResult
	{
        public static string PendingResult = "Pending";
        public static string NotPerformedResult = "Not Performed";

        private static string BothNotDetectedInterpretation = "JAK2 V617F and exon 12 mutations activate a tyrosine kinase that leads to clonal proliferation.  " +
            "V617F is present in greater than 95% polycythemia vera (PV).  JAK2 exon 12 mutation is present in 2% of patients with PV.   These mutations have not been " +
            "detected in normal patients.  Thus, the detection of the JAK2 V617F or exon 12 mutations provides confirmation for the diagnosis of PV.   The molecular analysis " +
            "did not detect the V617F or exon 12 mutations within the JAK2 gene.  The results do not support the diagnosis of PV, but do not rule it out. Recommend clinical " +
            "and pathologic correlation.";

        private static string JAK2NotDetectedExon1214DetectedInterpretation = "JAK2 V617F and exon 12 mutations activate a tyrosine kinase that leads to clonal proliferation.  " +
            "V617F is present in greater than 95% polycythemia vera (PV).  JAK2 exon 12 mutation is present in 2% of patients with PV.   These mutations have not been " +
            "detected in normal patients.  Thus, the detection of the JAK2 V617F or exon 12 mutations provides confirmation for the diagnosis of PV.  The molecular analysis " +
            "detected the JAK2 exon12 mutation while the JAK2 V617F was negative.  This result supports the diagnosis of PV.";

        private static string JAK2DetectedInterpretation = "JAK2 V617F and exon 12 mutations activate a tyrosine kinase that leads to clonal proliferation.  " +
            "V617F is present in greater than 95% polycythemia vera (PV).  JAK2 exon 12 mutation is present in 2% of patients with PV.   These mutations have not been " +
            "detected in normal patients.  Thus, the detection of the JAK2 V617F or exon 12 mutations provides confirmation for the diagnosis of PV. The molecular analysis " +
            "detected the JAK2 V617F mutation.  This result supports the diagnosis of PV.";

        private static string References = "1.  Tefferi A, Vardiman JW.  Classification and diagnosis of myeloproliferative neoplasms: The 2008 World Health Organization criteria and point-of-care diagnostic algorithms.  Leukemia (2008) 22, 14–22 " + Environment.NewLine +
            "2.  Levine RL, Gilliland DG.  Myeloproliferative disorders.  Blood. 2008 112: 2190-2198 " + Environment.NewLine +
            "3.  Kralovics R, et al.  A Gain-of-Function Mutation of JAK2 in Myeloproliferative Disorders.  N Engl J Med 2005;352:1779-90JAK2 Exon 12/14: 1. James C, et al. A unique clonal JAK2 mutation leading to constitutive signalling causes polycythaemia vera. Nature. 2005; 434:1144-8.";

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        private bool m_HasJAK2Exon1214;

		private YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex m_PanelSetOrderMPNStandardReflex;
		private YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder m_PanelSetOrderJAK2V617F;
		private YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214TestOrder m_PanelSetOrderJAK2Exon1214;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

		private string m_JAK2V617FResult;
        private string m_JAK2Exon1214Result;

        private string m_Comment;
		private string m_Interpretation;
        private string m_Method;		
		private string m_References;		

		public MPNStandardReflexResult(AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;

			YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexTest panelSetMPNStandardReflex = new YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexTest();
			this.m_PanelSetOrderMPNStandardReflex = (YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMPNStandardReflex.PanelSetId);
			YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest panelSetJAK2V617F = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest();
			YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214Test panelSetExon1214 = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214Test();

			this.m_PanelSetOrderJAK2V617F = (YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetJAK2V617F.PanelSetId);
			this.m_PanelSetOrderJAK2Exon1214 = (YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetExon1214.PanelSetId);

			this.m_SpecimenOrder =  this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrderMPNStandardReflex.OrderedOn, this.m_PanelSetOrderMPNStandardReflex.OrderedOnId);
			this.m_JAK2V617FResult = this.m_PanelSetOrderJAK2V617F.Result;
			
			this.m_Comment = this.m_PanelSetOrderJAK2V617F.Comment;			

			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetExon1214.PanelSetId) == true)
			{
                this.m_HasJAK2Exon1214 = true;				
				this.m_JAK2Exon1214Result = this.m_PanelSetOrderJAK2Exon1214.Result;
			}
            else
            {
                this.m_HasJAK2Exon1214 = false;
                this.m_JAK2Exon1214Result = "Not Performed";
            }

            this.SetInterpretation();
            this.SetMethod();
            this.m_References = References;
		}

        private void SetMethod()
        {
            StringBuilder method = new StringBuilder();
            method.Append("JAK2 V617F: " + this.m_PanelSetOrderJAK2V617F.Method);
            if (this.m_HasJAK2Exon1214 == true)
            {
                method.AppendLine("JAK2 Exon 12/14: " + this.m_PanelSetOrderJAK2Exon1214.Method);   
            }
            this.m_Method = method.ToString();
        }

        private void SetInterpretation()
        {
            if (this.m_HasJAK2Exon1214 == true)
            {
				YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FNotDetectedResult jak2V617FNotDetectedResult = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FNotDetectedResult();
                if (this.m_PanelSetOrderJAK2V617F.Result == jak2V617FNotDetectedResult.Result)
                {
					YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214NotDetectedResult jak2Exon1214NotDetectedResult = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214NotDetectedResult();
					YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214DetectedResult jak2Exon1214DetectedResult = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214DetectedResult();                    

                    if (this.m_PanelSetOrderJAK2Exon1214.ResultCode == jak2Exon1214NotDetectedResult.ResultCode)
                    {
                        this.m_Interpretation = BothNotDetectedInterpretation;
                    }
                    else if (this.m_PanelSetOrderJAK2Exon1214.ResultCode == jak2Exon1214DetectedResult.ResultCode)
                    {
                        this.m_Interpretation = JAK2NotDetectedExon1214DetectedInterpretation;
                    }
                }
            }
            else
            {
				YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FDetectedResult jak2V617FDetectedResult = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FDetectedResult();
                if (this.m_PanelSetOrderJAK2V617F.Result == jak2V617FDetectedResult.Result)
                {
                    this.m_Interpretation = JAK2DetectedInterpretation;
                }
            }
        }

		public void SetResults(PanelSetOrderMPNStandardReflex panelSetOrder)
        {
            panelSetOrder.Comment = this.m_Comment;
            panelSetOrder.Interpretation = this.m_Interpretation;
            panelSetOrder.Method = this.m_Method;
            panelSetOrder.References = this.m_References;
        }

		public string JAK2V617FResult
		{
            get { return this.m_JAK2V617FResult; }
		}

        public string JAK2Exon1214Result
        {
            get { return this.m_JAK2Exon1214Result; }
        }

		public YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex PanelSetOrderMPNStandardReflex
		{
			get { return this.m_PanelSetOrderMPNStandardReflex; }
		}

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
		{
			get { return this.m_SpecimenOrder; }
		}
	}
}
