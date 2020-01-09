using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class EPICPhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string EPIC = "EPIC";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);
            this.m_DistributionType = EPIC;
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            if (panelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.Client.Model.EPICPhysicianClientDistribution.EPIC) == false)
            {
                List<string> clientGroupIds = new List<string>();
                clientGroupIds.Add("1");
                clientGroupIds.Add("2");

                YellowstonePathology.Business.Client.Model.ClientGroupClientCollection stVincentAndHRHGroup = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId(clientGroupIds);
                if (stVincentAndHRHGroup.ClientIdExists(accessionOrder.ClientId) == true)
                {
                    if (string.IsNullOrEmpty(accessionOrder.SvhAccount) == true || string.IsNullOrEmpty(accessionOrder.SvhMedicalRecord) == true)
                    {
                        FaxPhysicianClientDistribution faxDistribution = new FaxPhysicianClientDistribution();
                        faxDistribution.From(this);
                        faxDistribution.SetDistribution(panelSetOrder, accessionOrder);
                    }
                    else
                    {
                        YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                        YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);
                        if (panelSet.ResultDocumentSource == YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase)
                        {
                            this.AddTestOrderReportDistribution(panelSetOrder, accessionOrder.PhysicianId, accessionOrder.PhysicianName, accessionOrder.ClientId, accessionOrder.ClientName, YellowstonePathology.Business.Client.Model.EPICPhysicianClientDistribution.EPIC, this.FaxNumber);
                        }
                        else
                        {
                            FaxPhysicianClientDistribution faxDistribution = new FaxPhysicianClientDistribution();
                            faxDistribution.From(this);
                            faxDistribution.SetDistribution(panelSetOrder, accessionOrder);
                        }
                    }
                }
                else
                {
                    FaxPhysicianClientDistribution faxDistribution = new FaxPhysicianClientDistribution();
                    faxDistribution.From(this);
                    faxDistribution.SetDistribution(panelSetOrder, accessionOrder);
                }
            }
        }
    }
}
