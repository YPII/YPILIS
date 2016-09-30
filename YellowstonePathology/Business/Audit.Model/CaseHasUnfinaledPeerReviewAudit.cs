using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CaseHasUnfinaledPeerReviewAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public CaseHasUnfinaledPeerReviewAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                if (surgicalTestOrder.HoldForPeerReview == true)
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.HasUnfinaledPeerReview() == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.Append("There is one or more unfinaled prospective peer reviews.");
                    }
                }
            }
        }
    }
}
