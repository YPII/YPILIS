using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class EMAPhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string EMA = "EMA";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);            
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            if (panelSetOrder.TestOrderReportDistributionCollection.EMADistributionTypeExists() == false)
            {
                if (accessionOrder.ClientId == 1822)
                {
                    panelSetOrder.TestOrderReportDistributionCollection.AddPrimaryDistribution(this, panelSetOrder.ReportNo);
                }           
                else
                {
                    panelSetOrder.TestOrderReportDistributionCollection.AddFaxDistribution(this, panelSetOrder.ReportNo);
                }
            }
        }
    }
}
