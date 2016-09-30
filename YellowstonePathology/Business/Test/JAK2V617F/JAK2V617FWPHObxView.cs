using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	public class JAK2V617FWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public JAK2V617FWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			JAK2V617FTestOrder panelSetOrder = (JAK2V617FTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "JAK2 Mutation V617F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Result: " + panelSetOrder.Result, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + panelSetOrder.Signature, document, "F");
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Interpretation: ", document, "F");
            this.HandleLongString(panelSetOrder.Interpretation, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Method: ", document, "F");
			string method = panelSetOrder.Method;
            this.HandleLongString(method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References: ", document, "F");            
            this.HandleLongString(panelSetOrder.Reference, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement(panelSetOrder.Disclosure, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
