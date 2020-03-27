using System;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV1618
{
    public class HPV1618WPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public HPV1618WPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
            { }

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
                this.HandleLongString("Comment:", document, "F");
                this.AddNextObxElement(panelSetOrder.Comment, document, "F");
            }

            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(panelSetOrder.OrderedOnId);
            this.AddNextObxElement("Specimen: " + specimenOrder.GetDescription(), document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Method: ", document, "F");
            this.HandleLongString(panelSetOrder.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References:", document, "F");
            this.HandleLongString(panelSetOrder.ReportReferences, document, "F");
            this.AddNextObxElement("", document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
