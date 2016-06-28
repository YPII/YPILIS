using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV1618ByPCR
{
	public class HPV1618ByPCREPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public HPV1618ByPCREPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			HPV1618ByPCRTestOrder panelSetOrder = (HPV1618ByPCRTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "HPV-16/18 Genotyping By PCR");
            this.AddNextObxElement("", document, "F");

            string hpv16ResultText = "HPV-16 Result: " + panelSetOrder.HPV16Result;
            this.AddNextObxElement(hpv16ResultText, document, "F");            
            this.AddNextObxElement("HPV-16 Reference: Negative", document, "F");
            this.AddNextObxElement("", document, "F");

            string hpv18ResultText = "HPV-18 Result: " + panelSetOrder.HPV18Result;
            this.AddNextObxElement(hpv18ResultText, document, "F");            
            this.AddNextObxElement("HPV-18 Reference: Negative", document, "F");
            this.AddNextObxElement("", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                this.AddNextObxElement("Comment:", document, "F");
                this.AddNextObxElement(panelSetOrder.Comment, document, "F");                
            }

            if (panelSetOrder.Indication == YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRIndication.SquamousCellCarcinoma)
            {
                this.AddNextObxElement(panelSetOrder.Signature, document, "F");
                this.AddNextObxElement("*** E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm") + "***", document, "F");                               
            }

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetByAliquotOrderId(panelSetOrder.OrderedOnId);
            string description = specimenOrder.Description + " - Block " + aliquotOrder.GetDescription();
            this.AddNextObxElement("Specimen: " + description, document, "F");
            this.AddNextObxElement("", document, "F");

            string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            if (panelSetOrder.Indication == YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRIndication.SquamousCellCarcinoma)
            {
                this.AddNextObxElement("Interpretation:", document, "F");
                this.AddNextObxElement(panelSetOrder.Interpretation, document, "F");
                this.AddNextObxElement("", document, "F");
            }

            this.AddNextObxElement("Method: ", document, "F");            
            this.AddNextObxElement(panelSetOrder.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References:", document, "F");            
            this.AddNextObxElement(panelSetOrder.References, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
