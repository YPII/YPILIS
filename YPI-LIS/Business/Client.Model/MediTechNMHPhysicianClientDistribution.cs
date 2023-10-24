using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class MediTechNMHPhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string MEDITECHNMH = "Meditech NMH";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);            
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            if (panelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.Client.Model.MediTechNMHPhysicianClientDistribution.MEDITECHNMH) == false)
            {
                YellowstonePathology.Business.Client.Model.ClientGroupClientCollection nmhGroup = Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId("42");
                if (nmhGroup.ClientIdExists(accessionOrder.ClientId) == true)
                {                    
                    YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();
                    YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);                        
                    panelSetOrder.TestOrderReportDistributionCollection.AddPrimaryDistribution(this, panelSetOrder.ReportNo);                        
                }
                else
                {
                    panelSetOrder.TestOrderReportDistributionCollection.AddAlternateDistribution(this, panelSetOrder.ReportNo);
                }
            }
        }
    }
}
