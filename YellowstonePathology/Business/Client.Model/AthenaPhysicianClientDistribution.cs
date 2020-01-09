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
            this.m_DistributionType = ATHENA;
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            if (panelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.Client.Model.AthenaPhysicianClientDistribution.ATHENA) == false)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);
                if (panelSet.ResultDocumentSource == YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase)
                {
                    YellowstonePathology.Business.Client.Model.ClientGroupClientCollection cmmcGroup = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId("3");
                    if (cmmcGroup.ClientIdExists(this.ClientId) == true)
                    {
                        this.AddTestOrderReportDistribution(panelSetOrder, this.m_PhysicianId, this.m_PhysicianName, this.m_ClientId, this.m_ClientName, YellowstonePathology.Business.Client.Model.AthenaPhysicianClientDistribution.ATHENA, this.FaxNumber);
                    }
                }
                else
                {
                    WebServicePhysicianClientDistribution webServiceDistribution = new WebServicePhysicianClientDistribution();
                    webServiceDistribution.From(this);
                    webServiceDistribution.SetDistribution(panelSetOrder, accessionOrder);
                }
            }
        }
    }
}
