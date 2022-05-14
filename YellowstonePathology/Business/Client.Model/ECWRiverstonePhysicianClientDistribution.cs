using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class ECWRiverstonePhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string ECWRIVERSTONE = "ECW Riverstone";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);            
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            if (panelSetOrder.TestOrderReportDistributionCollection.ECWDistributionTypeExists() == false)
            {
                if (accessionOrder.ClientId == 136)
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
