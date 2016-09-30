using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class IntraoperativeConsultationCorrelationAudit : Audit
    {
        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;

        public IntraoperativeConsultationCorrelationAudit(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            this.m_SurgicalTestOrder = surgicalTestOrder;
        }

        public override void Run()
        {
            this.m_Message.Clear();
            this.m_Status = AuditStatusEnum.OK;
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in this.m_SurgicalTestOrder.SurgicalSpecimenCollection)
            {
                foreach (YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult in surgicalSpecimen.IntraoperativeConsultationResultCollection)
                {
                    if (intraoperativeConsultationResult.Correlation == "Not Correlated")
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.Append("The case has an intraoperative consultation that is not correlated.");
                        break;
                    }
                }
            }
        }
    }
}
