using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CaseHasNotFoundProviderAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public CaseHasNotFoundProviderAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();
            if (this.m_AccessionOrder.PhysicianId == 2371)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.Append("The physician for this case is not set.");
            }
        }
    }
}
