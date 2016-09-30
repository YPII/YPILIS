using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PanelSetOrderAudit : Audit
    {
        protected YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        public PanelSetOrderAudit(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_PanelSetOrder = panelSetOrder;
        }

        public override void Run()
        {
            base.Run();
        } 

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public override string ToString()
        {
            return "Report No: " + this.m_PanelSetOrder.ReportNo + ": " + this.m_Message;
        }
    }
}
