using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV1618ByPCR
{
	public class HPV1618ByPCRCMMCView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
	{
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

		public HPV1618ByPCRCMMCView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}

        public override void ToXml(XElement document)
        {
			YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTestOrder panelSetOrder = (YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

			this.AddNextNteElement("HPV - 16 / 18 Genotyping By PCR", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

			string hpv16ResultText = "HPV-16 Result: " + panelSetOrder.HPV16Result;
			this.AddNextNteElement(hpv16ResultText, document);
			this.AddNextNteElement("HPV-16 Reference: Negative", document);
			this.AddBlankNteElement(document);

			string hpv18ResultText = "HPV-18 Result: " + panelSetOrder.HPV18Result;
			this.AddNextNteElement(hpv18ResultText, document);
			this.AddNextNteElement("HPV-18 Reference: Negative", document);
			this.AddBlankNteElement(document);

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                this.AddNextNteElement("Comment:", document);
                this.HandleLongString(panelSetOrder.Comment, document);
            }

            if (panelSetOrder.Indication == YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRIndication.SquamousCellCarcinoma)
            {
                this.AddNextNteElement(panelSetOrder.Signature, document);
                this.AddNextNteElement("*** E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm") + "***", document);
            }

            this.AddBlankNteElement(document);
            this.AddAmendments(document, panelSetOrder);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetByAliquotOrderId(panelSetOrder.OrderedOnId);
            string description = specimenOrder.Description + " - Block " + aliquotOrder.GetDescription();
            this.AddNextNteElement("Specimen: " + description, document);
			this.AddBlankNteElement(document);

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeString, document);
            this.AddBlankNteElement(document);

            if (panelSetOrder.Indication == YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRIndication.SquamousCellCarcinoma)
            {
                this.AddNextNteElement("Interpretation:", document);
                this.HandleLongString(panelSetOrder.Interpretation, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Method:", document);
			this.HandleLongString(panelSetOrder.Method, document);
            this.AddBlankNteElement(document);

			this.AddNextNteElement("References:", document);
			this.AddNextNteElement("Highly Effective Detection of Human Papillomavirus 16 and 18 DNA by a Testing Algorithm Combining Broad - Spectrum and Type - Specific PCR J Clin Microbiol. 2006 September; 44(9): 3292–3298", document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.", document);

			string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.AddNextNteElement(locationPerformed, document);
			this.AddBlankNteElement(document);
		}
	}
}
