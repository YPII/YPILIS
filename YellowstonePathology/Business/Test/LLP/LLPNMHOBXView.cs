using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.LLP
{
    public class LLPNMHOBXView : YellowstonePathology.Business.HL7View.NMH.NMHOBXViewOld
    {
        public LLPNMHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{ }

        public override void ToXml(XElement document)
        {
            PanelSetOrderLeukemiaLymphoma panelSetOrder = (PanelSetOrderLeukemiaLymphoma)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_ReportNo);

            this.AddNextOBXElement("", "Report No", this.m_ReportNo, document, "F");
            this.AddNextOBXElement("", "Impression", panelSetOrder.Impression, document, "F");
            this.AddNextOBXElement("", "Interpretive Comment", panelSetOrder.InterpretiveComment, document, "F");
            this.AddNextOBXElement("", "Pathologist", panelSetOrder.Signature, document, "F");

            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextOBXElement("", "E-signed", panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
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
                this.AddNextOBXElement("", "Amendments", amendments.ToString(), document, "F");
            }

            
            this.AddNextOBXElement("", "Cell Population Of Interest", panelSetOrder.FlowMarkerCollection.CellPopulationsOfInterest[0].Description, document, "F");

            StringBuilder markers = new StringBuilder();
            foreach (YellowstonePathology.Business.Flow.FlowMarkerItem flowMarkerItem in panelSetOrder.FlowMarkerCollection)
            {
                markers.AppendLine(flowMarkerItem.Name);
                markers.AppendLine("Interpretation: " + flowMarkerItem.Interpretation);
                markers.AppendLine("Intensity: " + flowMarkerItem.Intensity);
            }
            this.AddNextOBXElement("", "Markers", markers.ToString(), document, "F");

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

            AddNextOBXElement("", "Cell Distribution", cellDistribution.ToString(), document, "F");

            string blastCD34Percent = panelSetOrder.EGateCD34Percent;
            string blastCD117Percent = panelSetOrder.EGateCD117Percent;

            if (string.IsNullOrEmpty(blastCD34Percent) == false || string.IsNullOrEmpty(blastCD117Percent) == false)
            {
                StringBuilder blastPercent = new StringBuilder();
                blastPercent.AppendLine("CD34  " + blastCD34Percent);
                blastPercent.AppendLine("CD117 " + blastCD117Percent);
                this.AddNextOBXElement("", "Blast Marker Percentages", blastPercent.ToString(), document, "F");
            }

            if (string.IsNullOrEmpty(panelSetOrder.SpecimenViabilityPercent) == false && panelSetOrder.SpecimenViabilityPercent != "0")
            {
                this.AddNextOBXElement("", "Specimen Viability Percentage", panelSetOrder.SpecimenViabilityPercent, document, "F");
            }

            this.AddNextOBXElement("", "Clinical History", this.m_AccessionOrder.ClinicalHistory, document, "F");

            this.AddNextOBXElement("", "Method", "Qualitative Flow Cytometry", document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextOBXElement("", "Specimen", specimenOrder.Description, document, "F");

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextOBXElement("", "Collection Date/Time", collectionDateTimeString, document, "F");

            this.AddNextOBXElement("", "Specimen Adequacy", specimenOrder.SpecimenAdequacy, document, "F");

            string asrStatement = "Tests utilizing Analytic Specific Reagents (ASR's) were developed and performance characteristics determined by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has determined that such clearance or approval is not necessary.  ASR's may be used for clinical purposes and should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            this.AddNextOBXElement("", "ASR", asrStatement, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextOBXElement("", "Location Performed", locationPerformed, document, "F");
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
