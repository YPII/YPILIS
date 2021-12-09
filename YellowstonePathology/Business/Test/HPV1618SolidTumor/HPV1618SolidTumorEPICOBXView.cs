using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV1618SolidTumor
{
	public class HPV1618SolidTumorEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public HPV1618SolidTumorEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			HPV1618SolidTumorTestOrder panelSetOrder = (HPV1618SolidTumorTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "HPV Genotypes 16 and 18 Solid Tumor");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("HPV DNA: " + panelSetOrder.HPVDNAResult, document, "F");
            this.AddNextObxElement("HPV DNA Reference: Not Detected", document, "F");
            this.AddNextObxElement("", document, "F");
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextObxElement("HPV-6/11 Result: " + panelSetOrder.HPV6Result, document, "F");
            }
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextObxElement("HPV-16 Result: " + panelSetOrder.HPV16Result, document, "F");
            }
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextObxElement("HPV-18 Result: " + panelSetOrder.HPV18Result, document, "F");
            }
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextObxElement("HPV-31 Result: " + panelSetOrder.HPV31Result, document, "F");
            }
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextObxElement("HPV-33 Result: " + panelSetOrder.HPV33Result, document, "F");
            }
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextObxElement("HPV-45 Result: " + panelSetOrder.HPV45Result, document, "F");
            }
            if (panelSetOrder.HPV6Result != PanelSetOrder.NotPerformedResult)
            {
                this.AddNextObxElement("HPV-58 Result: " + panelSetOrder.HPV58Result, document, "F");
            }
            this.AddNextObxElement("", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                this.AddNextObxElement("Comment:", document, "F");
                this.AddNextObxElement(panelSetOrder.Comment, document, "F");                
            }

            if (panelSetOrder.Indication == Business.Test.HPV1618ByPCR.HPV1618ByPCRIndication.SquamousCellCarcinoma)
            {
                this.AddNextObxElement(panelSetOrder.Signature, document, "F");
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");                               
            }

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetByAliquotOrderId(panelSetOrder.OrderedOnId);
            string description = specimenOrder.Description + " - Block " + aliquotOrder.GetDescription();
            this.AddNextObxElement("Specimen: " + description, document, "F");
            this.AddNextObxElement("", document, "F");

            string collectionDateTimeString = Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            if (panelSetOrder.Indication == Business.Test.HPV1618ByPCR.HPV1618ByPCRIndication.SquamousCellCarcinoma)
            {
                this.AddNextObxElement("Interpretation:", document, "F");
                this.AddNextObxElement(panelSetOrder.Interpretation, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            this.AddNextObxElement("Method: ", document, "F");            
            this.AddNextObxElement(panelSetOrder.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References:", document, "F");            
            this.HandleLongString(panelSetOrder.ReportReferences, document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString("This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
