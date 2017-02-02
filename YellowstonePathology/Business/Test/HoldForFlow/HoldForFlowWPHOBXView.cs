using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HoldForFlow
{
    public class HoldForFlowWPHOBXView : YellowstonePathology.Business.HL7View.WPH.WPHOBXView
    {
        public HoldForFlowWPHOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            HoldForFlowTestOrder holdForFlowTestOrder = (HoldForFlowTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, holdForFlowTestOrder, "Hold For Flow");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Comment: " + holdForFlowTestOrder.Comment, document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);
            this.AddNextObxElement("", document, "F");
        }
    }
}
