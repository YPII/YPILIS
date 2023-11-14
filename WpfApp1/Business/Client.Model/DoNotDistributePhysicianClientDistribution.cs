using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class DoNotDistributePhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string DONOTDISTRIBUTE = "Do Not Distribute";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);
            this.m_DistributionType = DONOTDISTRIBUTE;
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            //do nothing on purpose
        }
    }
}
