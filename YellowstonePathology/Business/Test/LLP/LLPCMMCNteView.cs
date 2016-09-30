using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.LLP
{
    public class LLPCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public LLPCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)             
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;   
		}

        public override void ToXml(XElement document)
        {
			YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrder = (YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Leukemia/Lymphoma Phenotyping", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Impression", document);
            this.HandleLongString(panelSetOrder.Impression, document);
            this.AddBlankNteElement(document);
            
            this.AddNextNteElement("Interpretive Comment:", document);
            this.HandleLongString(panelSetOrder.InterpretiveComment, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Finaled By: ", document);
            this.AddNextNteElement(panelSetOrder.Signature, document);

            if (panelSetOrder.FinalDate.HasValue == true)
            {
                this.AddNextNteElement("*** E-Signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm") + " ***", document);
            }
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Cell Population Of Interest:", document);
            this.AddNextNteElement(panelSetOrder.CellPopulationOfInterest, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Cell Distribution:", document);            

			double LymphCnt = Convert.ToDouble(panelSetOrder.LymphocyteCount);
			double MonoCnt = Convert.ToDouble(panelSetOrder.MonocyteCount);
			double MyeCnt = Convert.ToDouble(panelSetOrder.MyeloidCount);
			double DimCnt = Convert.ToDouble(panelSetOrder.DimCD45ModSSCount);
			double OtherCnt = Convert.ToDouble(panelSetOrder.OtherCount);

			string blastCD34Percent = panelSetOrder.EGateCD34Percent;
			string blastCD117Percent = panelSetOrder.EGateCD117Percent;

			double TotalCnt = LymphCnt + MonoCnt + MyeCnt + DimCnt;
			double LymphPcnt = LymphCnt / TotalCnt;
			double MonoPcnt = MonoCnt / TotalCnt;
			double MyePcnt = MyeCnt / TotalCnt;
			double DimPcnt = DimCnt / TotalCnt;
			double OtherPcnt = OtherCnt / TotalCnt;

            this.AddNextNteElement("Lymphocytes " + this.GetGatingPercent(LymphPcnt), document);
            this.AddNextNteElement("Monocytes   " + this.GetGatingPercent(MonoPcnt), document);
            this.AddNextNteElement("Myeloid " + this.GetGatingPercent(MyePcnt), document);
            this.AddNextNteElement("Dim CD45/Mod SS " + this.GetGatingPercent(DimPcnt), document);
            this.AddBlankNteElement(document);

			if (string.IsNullOrEmpty(blastCD34Percent) == false || string.IsNullOrEmpty(blastCD117Percent) == false)
			{                
                this.AddNextNteElement("Blast Marker Percentages (as % of nucleated cells): ", document);
                this.AddNextNteElement("CD34  " + blastCD34Percent, document);
                this.AddNextNteElement("CD117 " + blastCD117Percent, document);
                this.AddBlankNteElement(document);
			}
			
			this.AddNextNteElement("Clinical History:", document);
			this.HandleLongString(this.m_AccessionOrder.ClinicalHistory, document);
            this.AddBlankNteElement(document);
			
			this.AddNextNteElement("Specimen Type:", document);
			this.AddNextNteElement(this.m_AccessionOrder.SpecimenOrderCollection[0].Description, document);
            this.AddBlankNteElement(document);
			
			this.AddNextNteElement("Specimen Adequacy:", document);
			this.AddNextNteElement(this.m_AccessionOrder.SpecimenOrderCollection[0].SpecimenAdequacy, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Method: Qualitative Flow Cytometry", document);
            this.AddBlankNteElement(document);

            string asrStatement = "Tests utilizing Analytic Specific Reagents (ASR's) were developed and performance characteristics determined by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has determined that such clearance or approval is not necessary.  ASR's may be used for clinical purposes and should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            this.HandleLongString(asrStatement, document);
		}

		public string GetGatingPercent(double gatingPercent)
		{
			string result = string.Empty;
			switch (gatingPercent.ToString())
			{
				case "":
				case "0":
					result = "< 1%";
					break;
				case "1":
					result = "~100%";
					break;
				default:
					result = gatingPercent.ToString("###.##%");
					break;
			}
			if (result == "NaN")
			{
				result = "0";
			}
			return result;
		}
	}
}
