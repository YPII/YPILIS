using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
	public class ThinPrepPapWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
	{
		public ThinPrepPapWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
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

            this.HandleLongString("Other Conditions:", document, "F");
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

            this.AddAmendments(document);            

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderCytology.OrderedOn, panelSetOrderCytology.OrderedOnId);
            this.AddNextObxElement("Specimen: " + specimenOrder.SpecimenNumber.ToString(), document, "F");
            this.HandleLongString(specimenOrder.Description, document, "F");
            YellowstonePathology.Business.Helper.DateTimeJoiner collectionDateTimeJoiner = new YellowstonePathology.Business.Helper.DateTimeJoiner(specimenOrder.CollectionDate.Value, specimenOrder.CollectionTime);
            this.AddNextObxElement("Collection Date/Time: " + collectionDateTimeJoiner.DisplayString, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Specimen Adequacy:", document, "F");
            this.AddNextObxElement(panelSetOrderCytology.SpecimenAdequacy, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.HandleLongString("Comment:", document, "F");
            string reportComment = panelSetOrderCytology.ReportComment;
            if (string.IsNullOrEmpty(reportComment) == false)
            {
                this.HandleLongString(reportComment, document, "F");
                this.AddNextObxElement(string.Empty, document, "F");
            }

            this.HandleLongString("Clinical History:", document, "F");
            string clinicalHistory = this.m_AccessionOrder.ClinicalHistory;
            if (string.IsNullOrEmpty(clinicalHistory) == false)
            {
                this.HandleLongString(clinicalHistory, document, "F");
                this.AddNextObxElement(string.Empty, document, "F");
            }

            this.AddNextObxElement(string.Empty, document, "F");            

            this.AddNextObxElement("Finaled By: " + panelSetOrderCytology.Signature, document, "F");
            if (panelSetOrderCytology.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrderCytology.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddAmendments(document);

            this.HandleLongString("Screening Method: ", document, "F");
            this.AddNextObxElement(panelSetOrderCytology.Method, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.HandleLongString("References: ", document, "F");
            this.AddNextObxElement(panelSetOrderCytology.ReportReferences, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string disclaimer = "This Pap test is only a screening test. A negative result does not definitively rule out the presence of disease. Women should, therefore, in consultation with their physician, have this test performed at mutually agreed intervals.";            
            this.HandleLongString(disclaimer, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrderCytology.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
		}        		
	}
}
