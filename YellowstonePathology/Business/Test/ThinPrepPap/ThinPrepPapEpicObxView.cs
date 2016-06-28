using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
	public class ThinPrepPapEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
	{
		public ThinPrepPapEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}
	
		public override void ToXml(XElement document)
		{			            
            YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrderCytology, "Thin Prep Pap Report");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("##############################################", document, "F");
            this.AddNextObxElement("Epithelial Cell Description:", document, "F");            
            this.AddNextObxElement(panelSetOrderCytology.ScreeningImpression.ToUpper(), document, "F");

            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Other Conditions:", document, "F");
            string otherConditions = panelSetOrderCytology.OtherConditions;
            if (string.IsNullOrEmpty(otherConditions) == true)
            {
                otherConditions = "None.";
            }
            else
            {
                this.HandleLongString(otherConditions, document, "F");
            }

            this.AddNextObxElement("##############################################", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Specimen Description:", document, "F");
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderCytology.OrderedOn, panelSetOrderCytology.OrderedOnId);
            this.AddNextObxElement(specimenOrder.Description, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Specimen Adequacy:", document, "F");
            this.AddNextObxElement(panelSetOrderCytology.SpecimenAdequacy, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Comment:", document, "F");
            string reportComment = panelSetOrderCytology.ReportComment;
            if (string.IsNullOrEmpty(reportComment) == false)
            {
                this.HandleLongString(reportComment, document, "F");
                this.AddNextObxElement(string.Empty, document, "F");
            }                      
                    
            this.AddNextObxElement(string.Empty, document, "F");

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

            this.AddNextObxElement("Additional Testing:", document, "F");
            this.AddNextObxElement(additionalTestingComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Finaled By: " + panelSetOrderCytology.Signature, document, "F");
            if (panelSetOrderCytology.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrderCytology.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Screening Method: ", document, "F");
            this.AddNextObxElement(panelSetOrderCytology.Method, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("References: ", document, "F");
            this.AddNextObxElement(panelSetOrderCytology.References, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string disclaimer = "This Pap test is only a screening test. A negative result does not definitively rule out the presence of disease. Women should, therefore, in consultation with their physician, have this test performed at mutually agreed intervals.";            
            this.AddNextObxElement(disclaimer, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrderCytology.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
		}        		
	}
}
