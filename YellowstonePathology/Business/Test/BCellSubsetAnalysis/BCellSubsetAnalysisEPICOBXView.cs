using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BCellSubsetAnalysis
{
    public class BCellSubsetAnalysisEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public BCellSubsetAnalysisEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
			: base(accessionOrder, reportNo, obxCount)
		{
        }

        public override void ToXml(XElement document)
        {
            BCellSubsetAnalysisTestOrder panelSetOrder = (BCellSubsetAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "B-Cell Subset Analysis");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Mature B-Cells 21+ 38+ : " + panelSetOrder.MatureBCellsPlusPercent, document, "F");
            this.AddNextObxElement("Normal Range : 49.76 - 100", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("21 + 38 - : " + panelSetOrder.MatureBCellsMinusPercent, document, "F");
            this.AddNextObxElement("Normal Range : 1.03 - 30.99", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Memory B-Cells 27+ : " + panelSetOrder.MemoryBCellPercent, document, "F");
            this.AddNextObxElement("Normal Range : 1.2 – 31.48", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Non-Switched Memory B-Cells IgD+ 27+ : " + panelSetOrder.NonSwitchedMemoryBCellPercent, document, "F");
            this.AddNextObxElement("Normal Range : 0 - 12.62", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Marginal Zone B-Cells IgD+ IgM+ 27+ : " + panelSetOrder.MarginalZoneBCellPercent, document, "F");
            this.AddNextObxElement("Normal Range : 0 – 13.33", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Class Switched Memory B-Cells IgD- Igm- 27+ : " + panelSetOrder.ClassSwitchedMemoryBCellPercent, document, "F");
            this.AddNextObxElement("Normal Range : 0 – 15.73", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Naïve B-Cells IgD+ 38++ : " + panelSetOrder.NaiveBCellPercent, document, "F");
            this.AddNextObxElement("Normal Range : 42.00 – 95.91", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Transitional B-Cells IgM+ 38++ : " + panelSetOrder.TransitionalBCellPercent, document, "F");
            this.AddNextObxElement("Normal Range : 0 – 4.35", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Plasmablasts 38+++ Igm- : " + panelSetOrder.PlasmaBlastsPercent, document, "F");
            this.AddNextObxElement("Normal Range : 0 – 1.81", document, "F");
            this.AddNextObxElement("", document, "F");            
            this.AddNextObxElement("Total Lymphs % of Nucleated Cells : " + panelSetOrder.TotalNucleatedPercent, document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("B-Cells % of Total Lymphocytes : " + panelSetOrder.TotalLymphocytesPercent, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen Information:", document, "F");
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Identification: " + specimenOrder.Description, document, "F");
            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");            

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Method:", document, "F");
            this.HandleLongString(panelSetOrder.Method, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("References:", document, "F");
            this.HandleLongString(panelSetOrder.ReportReferences, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement(panelSetOrder.ASRComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement(panelSetOrder.GetLocationPerformedComment(), document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}

