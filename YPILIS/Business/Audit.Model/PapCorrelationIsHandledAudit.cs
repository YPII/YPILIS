using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PapCorrelationIsHandledAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public PapCorrelationIsHandledAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            PapCorrelationIsRequiredAudit papCorrelationAudit = new PapCorrelationIsRequiredAudit(this.m_AccessionOrder);
            papCorrelationAudit.Run();
            if (papCorrelationAudit.Status == AuditStatusEnum.Failure)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                if (surgicalTestOrder.PapCorrelation == 0)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                }
            }
        }
    }
}
