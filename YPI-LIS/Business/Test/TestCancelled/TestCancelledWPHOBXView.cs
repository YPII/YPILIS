using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.TestCancelled
{
    public class TestCancelledWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public TestCancelledWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            TestCancelledTestOrder testCancelledTestOrder = (TestCancelledTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            string cancelledTestName = testCancelledTestOrder.CancelledTestName;
            if (string.IsNullOrEmpty(cancelledTestName) == true)
            {
                cancelledTestName = "Test Canceled";
            }
            this.AddHeader(document, testCancelledTestOrder, cancelledTestName);
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Test Canceled", document, "F");
            this.AddNextObxElement("", document, "F");
            this.HandleLongString("Comment: " + testCancelledTestOrder.Comment, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Specimen Description:", document, "F");
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testCancelledTestOrder.OrderedOn, testCancelledTestOrder.OrderedOnId);
            this.HandleLongString(specimenOrder.Description, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddAmendments(document);
            this.AddNextObxElement("", document, "F");
        }
    }
}
