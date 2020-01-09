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
            this.m_DistributionType = FAX;
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            if (string.IsNullOrEmpty(this.m_FaxNumber) == false)
            {
                if (panelSetOrder.TestOrderReportDistributionCollection.Exists(this.m_PhysicianId, this.m_ClientId, YellowstonePathology.Business.Client.Model.FaxPhysicianClientDistribution.FAX) == false)
                {
                    this.AddTestOrderReportDistribution(panelSetOrder, this.m_PhysicianId, this.m_PhysicianName, this.m_ClientId, this.m_ClientName, YellowstonePathology.Business.Client.Model.FaxPhysicianClientDistribution.FAX, this.FaxNumber);
                }
            }
            else
            {
                //MessageBox.Show("Unable to add a fax distribution because the fax number is blank.");
                //result = false;
            }
        }
    }
}
