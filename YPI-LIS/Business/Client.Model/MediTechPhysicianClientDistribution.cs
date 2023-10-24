using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class MediTechPhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string MEDITECH = "Meditech";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);            
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            if (panelSetOrder.TestOrderReportDistributionCollection.DistributionTypeExists(YellowstonePathology.Business.Client.Model.MediTechPhysicianClientDistribution.MEDITECH) == false)
            {
                YellowstonePathology.Business.Client.Model.ClientGroupClientCollection westParkHospitalGroup = Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId("36");
                if (westParkHospitalGroup.ClientIdExists(accessionOrder.ClientId) == true)
                {
                    if (string.IsNullOrEmpty(accessionOrder.SvhAccount) == true || string.IsNullOrEmpty(accessionOrder.SvhMedicalRecord) == true)
                    {
                        panelSetOrder.TestOrderReportDistributionCollection.AddAlternateDistribution(this, panelSetOrder.ReportNo);
                    }
                    else
                    {
                        YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();
                        YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);
                        if (panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase)
                        {
                            panelSetOrder.TestOrderReportDistributionCollection.AddPrimaryDistribution(this, panelSetOrder.ReportNo);
                        }
                        else
                        {
                            panelSetOrder.TestOrderReportDistributionCollection.AddAlternateDistribution(this, panelSetOrder.ReportNo);
                        }
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
