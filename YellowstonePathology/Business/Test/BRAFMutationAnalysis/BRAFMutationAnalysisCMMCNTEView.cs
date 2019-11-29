using System;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    public class BRAFMutationAnalysisCMMCNTEView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public BRAFMutationAnalysisCMMCNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            BRAFMutationAnalysisTestOrder testOrder = (BRAFMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll().GetPanelSet(testOrder.PanelSetId);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement(panelSet.PanelSetName, document);
            this.AddNextNteElement("Master Accession #: " + testOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + testOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Result: " + testOrder.Result, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Finaled By: ", document);
            this.AddNextNteElement(testOrder.ReferenceLabSignature, document);

            if (testOrder.ReferenceLabFinalDate.HasValue == true)
            {
                this.AddNextNteElement("*** E-Signed " + testOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm") + " ***", document);
            }
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Indication: " + testOrder.Indication, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Interpretation: ", document);
            this.HandleLongString(testOrder.Interpretation, document);
            this.AddBlankNteElement(document);

            if (string.IsNullOrEmpty(testOrder.TumorNucleiPercentage) == false)
            {
                this.AddNextNteElement("Tumor Nuclei Percent: ", document);
                this.HandleLongString(testOrder.TumorNucleiPercentage, document);
                this.AddBlankNteElement(document);
            }

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.AddNextNteElement("Specimen: " + specimenOrder.SpecimenNumber.ToString(), document);
            this.HandleLongString(specimenOrder.Description, document);
            this.AddBlankNteElement(document);

            string method = "Following lysis of paraffin embedded tissue; highly purified DNA was extracted from the specimen using an automated method.  PCR amplification using fluorescently labeled primers was then performed.  The products of the PCR reaction were then separated by high resolution capillary electrophoresis to look for the presence of the 107 nucleotide fragment indicative of a BRAF V600E mutation.";
            this.AddNextNteElement("Method:", document);
            this.AddNextNteElement(method, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("References: ", document);
            this.HandleLongString(testOrder.ReportReferences, document);
            this.AddBlankNteElement(document);

            string asr = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
            this.AddNextNteElement(asr, document);

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextNteElement(locationPerformed, document);
            this.AddBlankNteElement(document);
        }
    }
}