using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.TestCancelled
{
	public class TestCancelledEPICNTEView : YellowstonePathology.Business.HL7View.EPIC.EPICBeakerObxView
    {
		public TestCancelledEPICNTEView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			Business.HL7View.EPIC.EPICBeakerNarrativeOBXView.AddElement(document);

			TestCancelledTestOrder testCancelledTestOrder = (TestCancelledTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			string cancelledTestName = testCancelledTestOrder.CancelledTestName;			

			this.AddCompanyHeaderNTE(document);
			this.AddNextNTEElement(cancelledTestName, document);
			
            this.AddNextNTEElement("", document);
			this.AddNextNTEElement("Test Canceled", document);
			this.AddNextNTEElement("", document);

			this.AddNextNTEElement("Comment: " + testCancelledTestOrder.Comment, document);
            this.AddNextNTEElement("", document);

			this.AddNextNTEElement("Specimen Description:", document);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testCancelledTestOrder.OrderedOn, testCancelledTestOrder.OrderedOnId);
			this.AddNextNTEElement(specimenOrder.Description, document);
			this.AddNextNTEElement(string.Empty, document);
			
            this.AddNextNTEElement("", document);

        }
    }
}
