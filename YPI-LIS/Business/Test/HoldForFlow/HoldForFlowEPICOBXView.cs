using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HoldForFlow
{
    public class HoldForFlowEPICOBXView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public HoldForFlowEPICOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            HoldForFlowTestOrder holdForFlowTestOrder = (HoldForFlowTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, holdForFlowTestOrder, holdForFlowTestOrder.PanelSetName);
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Comment: " + holdForFlowTestOrder.Comment, document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);
            this.AddNextObxElement("", document, "F");
        }
    }
}
