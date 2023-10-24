using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV1618SolidTumor
{
	public class HPV1618SolidTumorCMMCView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
	{
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

		public HPV1618SolidTumorCMMCView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}

        public override void ToXml(XElement document)
        {
			YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder panelSetOrder = (YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

			this.AddNextNteElement("HPV Genotypes 16 and 18 Solid Tumor", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("HPV DNA: " + panelSetOrder.HPVDNAResult, document);
            this.AddNextNteElement("HPV DNA Reference: Not Detected", document);
            this.AddBlankNteElement(document);
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextNteElement("HPV-6/11 Result: " + panelSetOrder.HPV6Result, document);
            }
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextNteElement("HPV-16 Result: " + panelSetOrder.HPV16Result, document);
            }
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextNteElement("HPV-18 Result: " + panelSetOrder.HPV18Result, document);
            }
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextNteElement("HPV-31 Result: " + panelSetOrder.HPV31Result, document);
            }
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextNteElement("HPV-33 Result: " + panelSetOrder.HPV33Result, document);
            }
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextNteElement("HPV-45 Result: " + panelSetOrder.HPV45Result, document);
            }
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextNteElement("HPV-58 Result: " + panelSetOrder.HPV58Result, document);
            }
            this.AddBlankNteElement(document);

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                this.AddNextNteElement("Comment:", document);
                this.HandleLongString(panelSetOrder.Comment, document);
            }

            if (panelSetOrder.Indication == Business.Test.HPV1618ByPCR.HPV1618ByPCRIndication.SquamousCellCarcinoma)
            {
                this.AddNextNteElement(panelSetOrder.Signature, document);
                this.AddNextNteElement("*** E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm") + "***", document);
            }

            this.AddBlankNteElement(document);
            this.AddAmendments(document, panelSetOrder, this.m_AccessionOrder);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetByAliquotOrderId(panelSetOrder.OrderedOnId);
            string description = specimenOrder.Description + " - Block " + aliquotOrder.GetDescription();
            this.AddNextNteElement("Specimen: " + description, document);
			this.AddBlankNteElement(document);

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextNteElement("Collection Date/Time: " + collectionDateTimeString, document);
            this.AddBlankNteElement(document);

            if (panelSetOrder.Indication == Business.Test.HPV1618ByPCR.HPV1618ByPCRIndication.SquamousCellCarcinoma)
            {
                this.AddNextNteElement("Interpretation:", document);
                this.HandleLongString(panelSetOrder.Interpretation, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Method:", document);
			this.HandleLongString(panelSetOrder.Method, document);
            this.AddBlankNteElement(document);

			this.AddNextNteElement("References:", document);
			this.HandleLongString(panelSetOrder.ReportReferences, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.", document);

			string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.HandleLongString(locationPerformed, document);
			this.AddBlankNteElement(document);
		}
	}
}
