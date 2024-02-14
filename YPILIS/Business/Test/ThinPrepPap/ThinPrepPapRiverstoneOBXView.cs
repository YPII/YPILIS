using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class ThinPrepPapRiverstoneOBXView : YellowstonePathology.Business.HL7View.Riverstone.RiverstoneOBXView
    {
        public ThinPrepPapRiverstoneOBXView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
            : base(accessionOrder, reportNo, obxCount)
        {

        }

        public override void ToXml(XElement document, string resultStatus)
        {
            PanelSetOrderCytology panelSetOrderCytology = (PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);            
        }        
    }
}
