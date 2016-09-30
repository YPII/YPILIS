using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class ProviderClientsHaveDistributionSetAudit : Audit
    {
        private string m_ProviderId;
        private YellowstonePathology.Business.View.PhysicianClientView m_PhysicianClientView;
        private string m_MessageBegining;

        public ProviderClientsHaveDistributionSetAudit(string providerId, YellowstonePathology.Business.View.PhysicianClientView physicianClientView)
        {
            this.m_ProviderId = providerId;
            this.m_PhysicianClientView = physicianClientView;
            this.m_MessageBegining = "Distribution is not set for ";
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();
            this.m_Message.AppendLine(this.m_MessageBegining);

            foreach (YellowstonePathology.Business.Client.Model.Client client in this.m_PhysicianClientView.Clients)
            {
                YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_ProviderId, client.ClientId);
                List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> physicianClientDistributionViews = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(physicianClient.PhysicianClientId);
                if (physicianClientDistributionViews.Count == 0)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.AppendLine(client.ClientName);
                }
            }
        }
    }
}
