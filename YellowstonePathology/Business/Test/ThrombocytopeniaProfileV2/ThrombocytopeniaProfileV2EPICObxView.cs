using System;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ThrombocytopeniaProfileV2
{
    public class ThrombocytopeniaProfileV2EPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public ThrombocytopeniaProfileV2EPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            ThrombocytopeniaProfileV2TestOrder panelSetOrder = (ThrombocytopeniaProfileV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrder, "Thrombocytopenia Profile");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Test: Anti-Platelet Antibody - IgG", document, "F");
            this.AddNextObxElement("Result: " + panelSetOrder.AntiPlateletAntibodyIgG, document, "F");
            this.AddNextObxElement("Reference: " + panelSetOrder.AntiPlateletAntibodyIgGReference, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Test: Anti-Platelet Antibody - IgM", document, "F");
            this.AddNextObxElement("Result: " + panelSetOrder.AntiPlateletAntibodyIgM, document, "F");
            this.AddNextObxElement("Reference: " + panelSetOrder.AntiPlateletAntibodyIgMReference, document, "F");
            this.AddNextObxElement("", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.ReticulatedPlateletAnalysis) == false)
            {
                this.AddNextObxElement("Test: Reticulated Platelet Analysis", document, "F");
                this.AddNextObxElement("Result: " + panelSetOrder.ReticulatedPlateletAnalysis, document, "F");
                this.AddNextObxElement("Reference Range: " + panelSetOrder.ReticulatedPlateletAnalysisReference, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.HandleLongString("Specimen Description: " + specimenOrder.Description, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Method", document, "F");
            this.HandleLongString(panelSetOrder.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Interpretation:", document, "F");
            this.HandleLongString(panelSetOrder.Interpretation, document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString(panelSetOrder.ASRComment, document, "F");
            this.AddNextObxElement("", document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
