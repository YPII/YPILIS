using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.LLP
{
    public class LLPEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public LLPEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{ }

        public override void ToXml(XElement document)
        {
            PanelSetOrderLeukemiaLymphoma panelSetOrder = (PanelSetOrderLeukemiaLymphoma)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextObxElementBeaker("Report No", this.m_ReportNo, document, "F");
            this.AddNextObxElementBeaker("Impression", panelSetOrder.Impression, document, "F");
            this.AddNextObxElementBeaker("Interpretive Comment", panelSetOrder.InterpretiveComment, document, "F");
            this.AddNextObxElementBeaker("Pathologist", panelSetOrder.Signature, document, "F");

            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElementBeaker("E-signed", panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

            if (amendmentCollection.Count != 0)
            {
                StringBuilder amendments = new StringBuilder();
                foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
                {
                    if (amendment.Final == true)
                    {
                        amendments.AppendLine(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"));
                        amendments.AppendLine(amendment.Text);
                        if (amendment.RequirePathologistSignature == true)
                        {
                            amendments.AppendLine("Signature: " + amendment.PathologistSignature);
                            amendments.AppendLine("E-signed " + amendment.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"));
                        }
                    }
                }
                amendments.AppendLine();
                this.AddNextObxElementBeaker("Amendments", amendments.ToString(), document, "F");
            }

            
            this.AddNextObxElementBeaker("Cell Population Of Interest", panelSetOrder.FlowMarkerCollection.CellPopulationsOfInterest[0].Description, document, "F");

            StringBuilder markers = new StringBuilder();
            foreach (YellowstonePathology.Business.Flow.FlowMarkerItem flowMarkerItem in panelSetOrder.FlowMarkerCollection)
            {
                markers.AppendLine(flowMarkerItem.Name);
                markers.AppendLine("Interpretation: " + flowMarkerItem.Interpretation);
                markers.AppendLine("Intensity: " + flowMarkerItem.Intensity);
            }
            this.AddNextObxElementBeaker("Markers", markers.ToString(), document, "F");

            StringBuilder cellDistribution = new StringBuilder();

            double LymphCnt = Convert.ToDouble(panelSetOrder.LymphocyteCount);
            double MonoCnt = Convert.ToDouble(panelSetOrder.MonocyteCount);
            double MyeCnt = Convert.ToDouble(panelSetOrder.MyeloidCount);
            double DimCnt = Convert.ToDouble(panelSetOrder.DimCD45ModSSCount);
            double OtherCnt = Convert.ToDouble(panelSetOrder.OtherCount);

            double TotalCnt = LymphCnt + MonoCnt + MyeCnt + DimCnt;
            double LymphPcnt = LymphCnt / TotalCnt;
            double MonoPcnt = MonoCnt / TotalCnt;
            double MyePcnt = MyeCnt / TotalCnt;
            double DimPcnt = DimCnt / TotalCnt;
            double OtherPcnt = OtherCnt / TotalCnt;

            cellDistribution.AppendLine("Lymphocytes " + this.GetGatingPercent(LymphPcnt));
            cellDistribution.AppendLine("Monocytes   " + this.GetGatingPercent(MonoPcnt));
            cellDistribution.AppendLine("Myeloid " + this.GetGatingPercent(MyePcnt));
            cellDistribution.AppendLine("Dim CD45/Mod SS " + this.GetGatingPercent(DimPcnt));

            AddNextObxElementBeaker("Cell Distribution", cellDistribution.ToString(), document, "F");

            string blastCD34Percent = panelSetOrder.EGateCD34Percent;
            string blastCD117Percent = panelSetOrder.EGateCD117Percent;

            if (string.IsNullOrEmpty(blastCD34Percent) == false || string.IsNullOrEmpty(blastCD117Percent) == false)
            {
                StringBuilder blastPercent = new StringBuilder();
                blastPercent.AppendLine("CD34  " + blastCD34Percent);
                blastPercent.AppendLine("CD117 " + blastCD117Percent);
                this.AddNextObxElementBeaker("Blast Marker Percentages", blastPercent.ToString(), document, "F");
            }

            if (string.IsNullOrEmpty(panelSetOrder.SpecimenViabilityPercent) == false && panelSetOrder.SpecimenViabilityPercent != "0")
            {
                this.AddNextObxElementBeaker("Specimen Viability Percentage", panelSetOrder.SpecimenViabilityPercent, document, "F");
            }

            this.AddNextObxElementBeaker("Clinical History", this.m_AccessionOrder.ClinicalHistory, document, "F");

            this.AddNextObxElementBeaker("Method", "Qualitative Flow Cytometry", document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElementBeaker("Specimen", specimenOrder.Description, document, "F");

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElementBeaker("Collection Date/Time", collectionDateTimeString, document, "F");

            this.AddNextObxElementBeaker("Specimen Adequacy", specimenOrder.SpecimenAdequacy, document, "F");

            string asrStatement = "Tests utilizing Analytic Specific Reagents (ASR's) were developed and performance characteristics determined by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has determined that such clearance or approval is not necessary.  ASR's may be used for clinical purposes and should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            this.AddNextObxElementBeaker("ASR", asrStatement, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("Location Performed", locationPerformed, document, "F");
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
