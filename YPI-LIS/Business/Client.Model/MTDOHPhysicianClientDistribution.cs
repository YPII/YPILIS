using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class MTDOHPhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string MTDOH = "MTDOH";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);            
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            if (panelSetOrder.PanelSetId == 13)
            {
                if (panelSetOrder.TestOrderReportDistributionCollection.Exists(this.m_PhysicianId, this.m_ClientId, this.m_DistributionType) == false)
                {
                    panelSetOrder.TestOrderReportDistributionCollection.AddPrimaryDistribution(this, panelSetOrder.ReportNo);
                }
            }
        }
    }
}
