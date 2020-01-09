using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class FaxPhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string FAX = "Fax";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);            
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            if (string.IsNullOrEmpty(this.m_FaxNumber) == false)
            {
                if (panelSetOrder.TestOrderReportDistributionCollection.Exists(this.m_PhysicianId, this.m_ClientId, YellowstonePathology.Business.Client.Model.FaxPhysicianClientDistribution.FAX) == false)
                {
                    panelSetOrder.TestOrderReportDistributionCollection.AddPrimaryDistribution(this, panelSetOrder.ReportNo);
                }
            }            
        }
    }
}
