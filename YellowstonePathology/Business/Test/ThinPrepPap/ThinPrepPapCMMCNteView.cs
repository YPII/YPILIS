using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
	public class ThinPrepPapCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public ThinPrepPapCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}		

		public override void ToXml(XElement document)
		{            
			YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Thin Prep Pap Report", document);            
            this.AddNextNteElement("Report #: " + panelSetOrderCytology.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Epithelial Cell Description:", document);
            this.AddNextNteElement(panelSetOrderCytology.ScreeningImpression, document);
            this.AddBlankNteElement(document);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderCytology.OrderedOn, panelSetOrderCytology.OrderedOnId);

            this.AddNextNteElement("Specimen Description:", document);
            this.AddNextNteElement(specimenOrder.Description, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Specimen Source:", document);
            this.AddNextNteElement(specimenOrder.SpecimenSource, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Specimen Adequacy:", document);
            this.AddNextNteElement(panelSetOrderCytology.SpecimenAdequacy, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Report Comment:", document);
            this.AddNextNteElement(panelSetOrderCytology.ReportComment, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Other Conditions:", document);
            string otherConditions = panelSetOrderCytology.OtherConditions;
            if (string.IsNullOrEmpty(otherConditions) == true) otherConditions = "None.";
            this.AddNextNteElement(otherConditions, document);
            this.AddBlankNteElement(document);

            bool hpvHasBeenOrdered = this.m_AccessionOrder.PanelSetOrderCollection.Exists(14);
            string additionalTestingComment = string.Empty;
            if (hpvHasBeenOrdered == true)
            {
				additionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.HPVHasBeenOrderedComment;
            }
            else
            {
				additionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.NoAdditionalTestingOrderedComment;
            }

            this.AddNextNteElement("Additional Testing:", document);
            this.AddNextNteElement(additionalTestingComment, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Finaled By: ", document);
            this.AddNextNteElement(panelSetOrderCytology.Signature, document);

            if (panelSetOrderCytology.FinalDate.HasValue == true)
            {
                this.AddNextNteElement("*** E-Signed " + panelSetOrderCytology.FinalTime.Value.ToString("MM/dd/yyyy HH:mm") + " ***", document);
            }
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Specimen: ThinPrep fluid", document);
            this.AddBlankNteElement(document);
            
            this.AddNextNteElement("Screening Method: ", document);
            this.AddNextNteElement(panelSetOrderCytology.Method, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("References: ", document);
            this.AddNextNteElement(panelSetOrderCytology.References, document);
            this.AddBlankNteElement(document);            

            string disclaimer = "This Pap test is only a screening test. A negative result does not definitively rule out the presence of disease. Women should, therefore, in consultation with their physician, have this test performed at mutually agreed intervals.";
            this.AddNextNteElement(disclaimer, document);
            this.AddBlankNteElement(document);
		}        
	}
}
