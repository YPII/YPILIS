using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PhoneNumberAudit : Audit
    {
        private string m_PhoneNumber;

        public PhoneNumberAudit(string phoneNumber)
        {
            this.m_PhoneNumber = phoneNumber;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            if(string.IsNullOrEmpty(this.m_PhoneNumber) == true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.AppendLine("The phone number is missing.");
            }
        }
    }
}
