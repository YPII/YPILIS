using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class DistributionNotSetAudit : PanelSetOrderAudit
    {
        public DistributionNotSetAudit(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder) 
            : base(panelSetOrder)
        {

        }

        
        public override void  Run()
        {                        
            if (this.m_PanelSetOrder.Distribute == true)
            {
                if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.Count == 0)
                {
                    this.m_ActionRequired = true;
                    this.m_Message.AppendLine("The distribution for this case is not set.");
                }
            }            
        }        
    }
}
