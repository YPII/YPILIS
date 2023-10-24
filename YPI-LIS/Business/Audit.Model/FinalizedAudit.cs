using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class FinalizedAudit : PanelSetOrderAudit
    {
        public FinalizedAudit(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder) 
            : base(panelSetOrder)
        {

        }

        
        public override void Run()
        {                        
            if (this.m_PanelSetOrder.Final == true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.AppendLine("The case has been finalized.");                
            }            
        }        
    }
}
