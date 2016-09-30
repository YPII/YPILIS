using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class DistributionCanBeSetAudit : AccessionOrderAudit
    {

        public DistributionCanBeSetAudit(Test.AccessionOrder accessionOrder) : base(accessionOrder)
        { }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList physicianClientDistributionCollection = Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(this.m_AccessionOrder.PhysicianId, this.m_AccessionOrder.ClientId);
            if(physicianClientDistributionCollection.Count == 0)
            {
                this.m_Status = AuditStatusEnum.Failure;
                this.m_Message.Append("There are no distributions set for this physician client combination.");
            }
        }
    }
}
