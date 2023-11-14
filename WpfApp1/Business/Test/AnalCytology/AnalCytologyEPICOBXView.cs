using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.AnalCytology
{
    public class AnalCytologyEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public AnalCytologyEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        { }

        public override void ToXml(XElement document)
        {
            AnalCytologyTestOrder testOrder = (AnalCytologyTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, testOrder, testOrder.PanelSetName);

            this.AddNextObxElement("", document, "F");
            string result = "Result: " + testOrder.ScreeningImpression;
            this.AddNextObxElement(result, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Pathologist: " + testOrder.Signature, document, "F");
            if (testOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }

            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("", document, "F");
            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
