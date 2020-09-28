using System;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    public class BRAFMutationAnalysisEPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
        public BRAFMutationAnalysisEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            BRAFMutationAnalysisTestOrder panelSetOrder = (BRAFMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            //Add the first element as narrative for Nikki to see.
            Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

            this.AddCompanyHeaderNTE(document);
            this.AddNextNTEElement(panelSetOrder.PanelSetName, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("Result: " + panelSetOrder.Result, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document);
            if (panelSetOrder.ReferenceLabFinalDate.HasValue == true)
            {
                this.AddNextNTEElement("E-signed " + panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddNextNTEElement("", document);
            this.AddAmendments(document);

            this.AddNextNTEElement("Indication: " + panelSetOrder.Indication, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("Interpretation: ", document);
            this.AddNextNTEElement(panelSetOrder.Interpretation, document);
            this.AddNextNTEElement("", document);

            if (string.IsNullOrEmpty(panelSetOrder.TumorNucleiPercentage) == false)
            {
                this.AddNextNTEElement("Tumor Nuclei Percent: ", document);
                this.AddNextNTEElement(panelSetOrder.TumorNucleiPercentage, document);
                this.AddNextNTEElement("", document);
            }

            this.AddNextNTEElement("Specimen Description:", document);
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            this.AddNextNTEElement(specimenOrder.Description, document);
            this.AddNextNTEElement(string.Empty, document);

            string method = panelSetOrder.Method;
            this.AddNextNTEElement("Method: " + method, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("References: ", document);
            this.AddNextNTEElement(panelSetOrder.ReportReferences, document);
            this.AddNextNTEElement("", document);

            string asr = panelSetOrder.ReportDisclaimer;
            this.AddNextNTEElement(asr, document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNTEElement(locationPerformed, document);
            this.AddNextNTEElement(string.Empty, document);
        }
    }
}