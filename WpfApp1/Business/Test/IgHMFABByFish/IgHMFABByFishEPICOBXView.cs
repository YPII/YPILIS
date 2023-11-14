using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.IgHMFABByFish
{
    class IgHMFABByFishEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public IgHMFABByFishEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            IgHMFABByFishTestOrder testOrder = (IgHMFABByFishTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, testOrder, testOrder.PanelSetName);
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Result: " + testOrder.Result, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Pathologist: " + testOrder.ReferenceLabSignature, document, "F");
            if (testOrder.FinalTime.HasValue == true)
            {
                this.AddNextObxElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.HandleLongString("Interpretation: ", document, "F");
            this.HandleLongString(testOrder.Interpretation, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Nuclei Scored: " + testOrder.NucleiScored, document, "F");
            this.AddNextObxElement("", document, "F");

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
