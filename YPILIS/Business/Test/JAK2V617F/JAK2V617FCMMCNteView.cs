using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
    public class JAK2V617FCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
    {
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

        public JAK2V617FCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;
        }

        public override void ToXml(XElement document)
        {
            JAK2V617FTestOrder panelSetOrder = (JAK2V617FTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement(panelSetOrder.PanelSetName, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Result: " + panelSetOrder.Result, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Pathologist: " + panelSetOrder.ReferenceLabSignature, document);
            if (panelSetOrder.FinalTime.HasValue == true)
            {
                this.AddNextNteElement("E-signed " + panelSetOrder.ReferenceLabFinalDate.Value.ToString("MM/dd/yyyy HH:mm"), document);
            }
            this.AddBlankNteElement(document);
            this.AddAmendments(document, panelSetOrder, this.m_AccessionOrder);

            this.AddNextNteElement("Interpretation: ", document);
            this.HandleLongString(panelSetOrder.Interpretation, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Method: ", document);
            string method = panelSetOrder.Method;
            this.HandleLongString(method, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("References: ", document);
            this.HandleLongString(panelSetOrder.Reference, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement(panelSetOrder.Disclosure, document);
            this.AddBlankNteElement(document);

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextNteElement(locationPerformed, document);
            this.AddBlankNteElement(document);
        }
    }
}
