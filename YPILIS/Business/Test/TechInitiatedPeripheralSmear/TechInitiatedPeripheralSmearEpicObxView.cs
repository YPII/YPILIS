using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear
{
    public class TechInitiatedPeripheralSmearEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public TechInitiatedPeripheralSmearEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            TechInitiatedPeripheralSmearTestOrder testOrder = (TechInitiatedPeripheralSmearTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, testOrder, "Tech Initiated Peripheral Smear");
            this.AddNextObxElement(string.Empty, document, "F");

            this.HandleLongString("Technologists Question: " + testOrder.TechnologistsQuestion, document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString("Pathologist Feedback: " + testOrder.PathologistFeedback, document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString("CBC Comment: " + testOrder.CBCComment, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + testOrder.Signature, document, "F");
            if (testOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
