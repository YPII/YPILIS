using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class ECWPhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string ECW = "Eclinical Works";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);
            this.m_DistributionType = ECW;
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            if (panelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.Client.Model.ECWPhysicianClientDistribution.ECW) == false)
            {
                if (accessionOrder.ClientId == 1203)
                {
                    this.AddTestOrderReportDistribution(panelSetOrder, accessionOrder.PhysicianId, accessionOrder.PhysicianName, accessionOrder.ClientId, accessionOrder.ClientName, YellowstonePathology.Business.Client.Model.ECWPhysicianClientDistribution.ECW, this.FaxNumber);
                }
            }
        }
    }
}
