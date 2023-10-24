using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class AthenaPhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string ATHENA = "Athena Health";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);            
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            if (panelSetOrder.TestOrderReportDistributionCollection.AthenaDistributionTypeExists() == false)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);
                if (panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase)
                {
                    YellowstonePathology.Business.Client.Model.ClientGroupClientCollection cmmcGroup = Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId("3");
                    if (cmmcGroup.ClientIdExists(accessionOrder.ClientId) == true)
                    {
                        panelSetOrder.TestOrderReportDistributionCollection.AddPrimaryDistribution(this, panelSetOrder.ReportNo);
                    }
                    else
                    {
                        panelSetOrder.TestOrderReportDistributionCollection.AddAlternateDistribution(this, panelSetOrder.ReportNo);
                    }
                }
                else
                {
                    panelSetOrder.TestOrderReportDistributionCollection.AddAlternateDistribution(this, panelSetOrder.ReportNo);
                }
            }
        }
    }
}
