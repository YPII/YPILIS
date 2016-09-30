using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public NGCTEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			NGCTTestOrder testOrder = (NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

			this.AddHeader(document, testOrder, "Chlamydia Gonorrhea Screening");            
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Chlamydia trachomatis result: " + testOrder.ChlamydiaTrachomatisResult, document, "F");
            this.AddNextObxElement("Reference: Negative", document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Neisseria gonorrhoeae result: " + testOrder.NeisseriaGonorrhoeaeResult, document, "F");
            this.AddNextObxElement("Reference: Negative", document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddAmendments(document);

            this.AddNextObxElement("Specimen: Thin Prep Fluid", document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Method:", document, "F");
            this.AddNextObxElement(testOrder.Method, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References:", document, "F");
            this.AddNextObxElement(testOrder.ReportReferences, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement(testOrder.TestInformation, document, "F");

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
