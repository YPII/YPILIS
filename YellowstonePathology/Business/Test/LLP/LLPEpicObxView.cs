using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.LLP
{
	public class LLPEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public LLPEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			PanelSetOrderLeukemiaLymphoma panelSetOrder = (PanelSetOrderLeukemiaLymphoma)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "Leukemia/Lymphoma Phenotyping");
			
			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Impression", document, "F");
			this.HandleLongString(panelSetOrder.Impression, document, "F");

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Interpretive Comment:", document, "F");
			this.HandleLongString(panelSetOrder.InterpretiveComment, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }            
			this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Cell Population Of Interest: " + panelSetOrder.CellPopulationOfInterest, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Markers:", document, "F");
            foreach(YellowstonePathology.Business.Flow.FlowMarkerItem flowMarkerItem in panelSetOrder.FlowMarkerCollection)
            {
                this.AddNextObxElement(flowMarkerItem.Name, document, "F");
                this.AddNextObxElement("Interpretation: " + flowMarkerItem.Interpretation, document, "F");
                this.AddNextObxElement("Intensity: " + flowMarkerItem.Intensity, document, "F");
            }

            this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Cell Distribution:", document, "F");

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

			this.AddNextObxElement("Lymphocytes " + this.GetGatingPercent(LymphPcnt), document, "F");
			this.AddNextObxElement("Monocytes   " + this.GetGatingPercent(MonoPcnt), document, "F");
			this.AddNextObxElement("Myeloid " + this.GetGatingPercent(MyePcnt), document, "F");
			this.AddNextObxElement("Dim CD45/Mod SS " + this.GetGatingPercent(DimPcnt), document, "F");

			if (string.IsNullOrEmpty(blastCD34Percent) == false || string.IsNullOrEmpty(blastCD117Percent) == false)
			{
				this.AddNextObxElement("", document, "F");
				this.AddNextObxElement("Blast Marker Percentages (as % of nucleated cells):", document, "F");
				this.AddNextObxElement("CD34  " + blastCD34Percent, document, "F");
				this.AddNextObxElement("CD117 " + blastCD117Percent, document, "F");
			}

			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Clinical History:", document, "F");
			this.HandleLongString(this.m_AccessionOrder.ClinicalHistory, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Method: Qualitative Flow Cytometry", document, "F");            

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextObxElement("", document, "F");
			this.AddNextObxElement("Specimen: " + specimenOrder.Description, document, "F");
			this.AddNextObxElement("", document, "F");

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Specimen Adequacy: " + specimenOrder.SpecimenAdequacy, document, "F");
            this.AddNextObxElement("", document, "F");

            string asrStatement = "Tests utilizing Analytic Specific Reagents (ASR's) were developed and performance characteristics determined by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has determined that such clearance or approval is not necessary.  ASR's may be used for clinical purposes and should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";            
            this.HandleLongString(asrStatement, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
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
