using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class SvhCaseHasMRNAndAccountNoAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public SvhCaseHasMRNAndAccountNoAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            if(this.CaseIsSvh() == true)
            {
                if(string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == true)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.AppendLine("The case is an SVH case but has no MRN.");
                }
                if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhAccount) == true)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.AppendLine("The case is an SVH case but has no SVH account.");
                }
            }
        }

        private bool CaseIsSvh()
        {
            bool result = false;
            if(this.m_AccessionOrder.ClientId == 558)
            {
                result = true;
            }
            return result;
        }
    }
}
