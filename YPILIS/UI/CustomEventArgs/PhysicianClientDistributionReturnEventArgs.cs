using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class PhysicianClientDistributionReturnEventArgs : System.EventArgs
    {
        private Business.Client.Model.PhysicianClientDistributionListItem m_PhysicianClientDistribution;

        public PhysicianClientDistributionReturnEventArgs(Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution)
        {
            this.m_PhysicianClientDistribution = physicianClientDistribution;
        }

        public Business.Client.Model.PhysicianClientDistributionListItem PhysicianClientDistribution
        {
            get { return this.m_PhysicianClientDistribution; }
        }
    }
}
