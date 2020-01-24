using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	public class JAK2V617FEPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerNTEView
    {
        private Business.Test.AccessionOrder m_AccessionOrder;
        private string m_ReportNo;
        //private int m_NTECount;

        public JAK2V617FEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int nteCount) 
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
            this.m_NTECount = nteCount;
		}

        public override void ToXml(XElement document)
        {
			JAK2V617FTestOrder panelSetOrder = (JAK2V617FTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddCompanyHeader(document);
            this.AddNextNTEElement("",document);

            this.AddNextNTEElement("Result: " + panelSetOrder.Result, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNTEElement("E-signed " + panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddNextNTEElement("", document);
            this.AddAmendments(document, panelSetOrder, this.m_AccessionOrder);

            this.AddNextNTEElement("Interpretation: ", document);
            this.AddNextNTEElement(panelSetOrder.Interpretation, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("Method: ", document);
			string method = panelSetOrder.Method;
            this.AddNextNTEElement(method, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement("References: ", document);            
            this.AddNextNTEElement(panelSetOrder.Reference, document);
            this.AddNextNTEElement("", document);

            this.AddNextNTEElement(panelSetOrder.Disclosure, document);
            this.AddNextNTEElement(string.Empty, document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNTEElement(locationPerformed, document);
            this.AddNextNTEElement(string.Empty, document);
        }
    }
}
