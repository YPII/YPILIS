using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class MRNAudit : AccessionOrderAudit
    {
        public MRNAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {

        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            if (this.m_AccessionOrder.ClientId == 558)
            {
                if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == true)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.AppendLine("The MRN is blank.");
                }
            }                           
        }        
    }
}
