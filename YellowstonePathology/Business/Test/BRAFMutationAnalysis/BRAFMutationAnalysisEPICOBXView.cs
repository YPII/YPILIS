using System;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    public class BRAFMutationAnalysisEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public BRAFMutationAnalysisEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            BRAFMutationAnalysisTestOrder panelSetOrder = (BRAFMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(panelSetOrder.PanelSetId);
            this.AddHeader(document, panelSetOrder, panelSet.PanelSetName);
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Result: " + panelSetOrder.Result, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document, "F");
            if (panelSetOrder.ReferenceLabFinalDate.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Indication: " + panelSetOrder.Indication, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Interpretation: ", document, "F");
            this.HandleLongString(panelSetOrder.Interpretation, document, "F");
            this.AddNextObxElement("", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.TumorNucleiPercentage) == false)
            {
                this.AddNextObxElement("Tumor Nuclei Percent: ", document, "F");
                this.HandleLongString(panelSetOrder.TumorNucleiPercentage, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            this.AddNextObxElement("Specimen Description:", document, "F");
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextObxElement(specimenOrder.Description, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string method = panelSetOrder.Method;
            this.HandleLongString("Method: " + method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References: ", document, "F");
            this.HandleLongString(panelSetOrder.ReportReferences, document, "F");
            this.AddNextObxElement("", document, "F");

            string asr = panelSetOrder.ReportDisclaimer;
            this.HandleLongString(asr, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}