using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class GradedStainsAreHandledAudit : Audit
    {
        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;

        public GradedStainsAreHandledAudit(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            this.m_SurgicalTestOrder = surgicalTestOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection = this.m_SurgicalTestOrder.GetTestOrders();
            YellowstonePathology.Business.SpecialStain.StainResultItemCollection allStaints = this.m_SurgicalTestOrder.GetAllStains();
            YellowstonePathology.Business.SpecialStain.StainResultItemCollection gradedStains = allStaints.GetGradedStains(testOrderCollection);

            foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResult in gradedStains)
            {
                if (stainResult.IsResultPositive() == true)
                {
                    if (stainResult.ReportCommentContainsNumber() == false)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.AppendLine("Graded stain " + stainResult.ProcedureName + " is not graded.");
                    }
                }
            }
        }
    }
}
