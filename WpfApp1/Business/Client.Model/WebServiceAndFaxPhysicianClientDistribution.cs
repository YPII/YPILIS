using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class WebServiceAndFaxPhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string WEBSERVICEANDFAX = "Web Service and Fax";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);
            this.m_DistributionType = WEBSERVICEANDFAX;
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            WebServicePhysicianClientDistribution webServiceDistribution = new WebServicePhysicianClientDistribution();
            webServiceDistribution.From(this);
            webServiceDistribution.DistributionType = "Web Service";
            webServiceDistribution.SetDistribution(panelSetOrder, accessionOrder);

            FaxPhysicianClientDistribution faxDistribution = new FaxPhysicianClientDistribution();
            faxDistribution.From(this);
            faxDistribution.DistributionType = "Fax";
            faxDistribution.SetDistribution(panelSetOrder, accessionOrder);
        }
    }
}
