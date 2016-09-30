using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class SigningUserIsAssignedUserAudit : Audit
    {
        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;

        public SigningUserIsAssignedUserAudit(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            this.m_SurgicalTestOrder = surgicalTestOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();
            if (this.m_SurgicalTestOrder.AssignedToId == 0)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.AppendLine("The case is not assigned to a pathologist.");
            }
            else if (Business.User.SystemIdentity.Instance.User.UserId != this.m_SurgicalTestOrder.AssignedToId)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.AppendLine("The case is assigned to another pathologist.");
            }
        }
    }
}
