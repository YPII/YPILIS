using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class ProviderHomeBaseAudit : Audit
    {
        private YellowstonePathology.Business.Domain.Physician m_Physician;

        public ProviderHomeBaseAudit(YellowstonePathology.Business.Domain.Physician physician)
        {
            this.m_Physician = physician;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            if (this.m_Physician.HomeBaseClientId.HasValue == false)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.AppendLine("The Homebase client is missing");
            }
        }
    }
}
