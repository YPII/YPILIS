using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class MediTechPhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);
            this.m_DistributionType = YellowstonePathology.Business.ReportDistribution.Model.DistributionType.MEDITECH;
        }        
    }
}
