using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class AncillaryStudiesAreHandledAudit : Audit
    {
        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        public AncillaryStudiesAreHandledAudit(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            this.m_SurgicalTestOrder = surgicalTestOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            StringBuilder resultMsg = new StringBuilder();
            StringBuilder commentMsg = new StringBuilder();
            foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in this.m_SurgicalTestOrder.TypingStainCollection)
            {
                if (stainResultItem.ClientAccessioned == false)
                {
                    if (string.IsNullOrEmpty(stainResultItem.Result) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        resultMsg.AppendLine(stainResultItem.ProcedureName);
                    }
                    if (string.IsNullOrEmpty(stainResultItem.ProcedureComment) == true && (string.IsNullOrEmpty(stainResultItem.Result) || stainResultItem.Result != "Pending"))
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        commentMsg.AppendLine(stainResultItem.ProcedureName);
                    }
                }
            }

            this.m_Message.Clear();

            if(resultMsg.Length > 0)
            {
                this.m_Message.AppendLine("Ancillary study result is not set for:");
                this.m_Message.Append(resultMsg.ToString());
            }

            if(commentMsg.Length > 0)
            {
                this.m_Message.AppendLine("Ancillary study control comment is not set or is pending for:");
                this.m_Message.Append(commentMsg.ToString());
            }
        }
    }
}
