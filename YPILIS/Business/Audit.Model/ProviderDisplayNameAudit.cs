using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class ProviderDisplayNameAudit : Audit
    {
        private string m_DisplayName;

        public ProviderDisplayNameAudit(string displayName)
        {
            this.m_DisplayName = displayName;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;

            if(string.IsNullOrEmpty(this.m_DisplayName) == true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.AppendLine("The display name is not valid.");
            }
        }
    }
}
