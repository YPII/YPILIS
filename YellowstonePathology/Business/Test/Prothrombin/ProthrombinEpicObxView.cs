using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.Prothrombin
{
	public class ProthrombinEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public ProthrombinEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}
		
        public override void ToXml(XElement document)
        {
			ProthrombinTestOrder testOrder = (ProthrombinTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, testOrder, "Prothrombin 20210A Mutation Analysis (Factor II)");
            this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Result: " + testOrder.Result, document, "F");

			if (string.IsNullOrEmpty(testOrder.ResultDescription) == false)
			{
				this.AddNextObxElement("  " + testOrder.ResultDescription, document, "F");
			}
			this.AddNextObxElement("", document, "F");

			if (string.IsNullOrEmpty(testOrder.Comment) == false)
			{
				this.HandleLongString("Comment: " + testOrder.Comment, document, "F");
				this.AddNextObxElement("", document, "F");
			}

            this.AddNextObxElement("Pathologist: " + testOrder.Signature, document, "F");
            if (testOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.HandleLongString("Indication: " + testOrder.Indication, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Interpretation: ", document, "F");
			this.HandleLongString(testOrder.Interpretation, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Method: ", document, "F");
			this.HandleLongString(testOrder.Method, document, "F");
			this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("References: ", document, "F");
			this.HandleLongString(testOrder.ReportReferences, document, "F");
			this.AddNextObxElement("", document, "F");

			this.HandleLongString(testOrder.TestDevelopment, document, "F");
			this.AddNextObxElement("", document, "F");

            string locationPerformed = testOrder.GetLocationPerformedComment();
			this.HandleLongString(locationPerformed, document, "F");
			this.AddNextObxElement(string.Empty, document, "F");
		}
    }
}
