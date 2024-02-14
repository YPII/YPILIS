using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ReviewForAdditionalTesting
{
	public class ReviewForAdditionalTestingEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public ReviewForAdditionalTestingEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
            ReviewForAdditionalTestingTestOrder reviewForAdditionalTestingTestOrder = (ReviewForAdditionalTestingTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			
			this.AddHeader(document, reviewForAdditionalTestingTestOrder, reviewForAdditionalTestingTestOrder.PanelSetName);
            this.AddNextObxElement("", document, "F");			
			this.HandleLongString("Comment: " + reviewForAdditionalTestingTestOrder.Comment, document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);
            this.AddNextObxElement("", document, "F");
        }
    }
}
