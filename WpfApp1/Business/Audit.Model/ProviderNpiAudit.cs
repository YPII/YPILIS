using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class ProviderNpiAudit : Audit
    {
        private YellowstonePathology.Business.Domain.Physician m_Physician;

        public ProviderNpiAudit(YellowstonePathology.Business.Domain.Physician physician)
        {
            this.m_Physician = physician;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;

            if (string.IsNullOrEmpty(this.m_Physician.Npi) == true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.AppendLine("The NPI is missing");
            }
            else if(this.m_Physician.Npi == "0")
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.AppendLine("The NPI is not valid");
            }
            else if(this.m_Physician.Npi.Length != 10)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.AppendLine("The NPI is not ten digits");
            }
            else
            {
                foreach(char c in this.m_Physician.Npi)
                {
                    if (Char.IsDigit(c) == false)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.AppendLine("The NPI is not all digits");
                        break;
                    }
                }
            }
        }
    }
}
