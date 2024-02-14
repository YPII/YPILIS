using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class EPICAndFaxPhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string EPICANDFAX = "EPIC and Fax";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);            
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            EPICPhysicianClientDistribution epicDistribution = new EPICPhysicianClientDistribution();
            epicDistribution.From(this);
            epicDistribution.SetDistribution(panelSetOrder, accessionOrder);

            FaxPhysicianClientDistribution faxDistribution = new FaxPhysicianClientDistribution();
            faxDistribution.From(this);
            faxDistribution.SetDistribution(panelSetOrder, accessionOrder);
        }
    }
}
