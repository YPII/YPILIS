using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV1618
{
	public class HPV1618EPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public HPV1618EPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			PanelSetOrderHPV1618 panelSetOrder = (PanelSetOrderHPV1618)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "HPV-16/18 Genotyping");
            this.AddNextObxElement("", document, "F");

            string hpv16ResultText = "HPV-16 Result: " + panelSetOrder.HPV16Result;
            this.AddNextObxElement(hpv16ResultText, document, "F");            
            this.AddNextObxElement("HPV-16 Reference: Negative", document, "F");
            this.AddNextObxElement("", document, "F");

            string hpv18ResultText = "HPV-18/45 Result: " + panelSetOrder.HPV18Result;
            this.AddNextObxElement(hpv18ResultText, document, "F");            
            this.AddNextObxElement("HPV-18/45 Reference: Negative", document, "F");
            this.AddNextObxElement("", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                this.AddNextObxElement("Comment:", document, "F");
                this.AddNextObxElement(panelSetOrder.Comment, document, "F");                
            }

            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(panelSetOrder.OrderedOnId);
            this.AddNextObxElement("Specimen: " + specimenOrder.GetDescription(), document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Method: ", document, "F");            
            this.AddNextObxElement(panelSetOrder.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References:", document, "F");            
            this.AddNextObxElement(panelSetOrder.References, document, "F");
            this.AddNextObxElement("", document, "F");            

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
