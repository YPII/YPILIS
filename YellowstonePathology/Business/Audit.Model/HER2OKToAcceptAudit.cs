using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class HER2OKToAcceptAudit : PanelSetOrderAudit
    {
        public HER2OKToAcceptAudit(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder) 
            : base(panelSetOrder)
        {

        }

        
        public override void Run()
        {                        
            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder her2AmplificationByISHTestOrder = (YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_PanelSetOrder;
            if (string.IsNullOrEmpty(her2AmplificationByISHTestOrder.InterpretiveComment) == true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.AppendLine("Unable to accept the results because the interpretation is blank.");                
            }            
        }        
    }
}
