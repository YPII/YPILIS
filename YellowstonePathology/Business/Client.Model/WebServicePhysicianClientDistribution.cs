using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.Business.Client.Model
{
    public class WebServicePhysicianClientDistribution : PhysicianClientDistributionListItem
    {
        public const string WEBSERVICE = "Web Service";

        public override void From(PhysicianClientDistributionListItem physicianClientDistribution)
        {
            base.From(physicianClientDistribution);            
        }

        public override void SetDistribution(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {            
            if (panelSetOrder.TestOrderReportDistributionCollection.Exists(this.m_PhysicianId, this.m_ClientId, this.DistributionType) == false)
            {
                Business.PanelSet.Model.PanelSetCollection panelSetCollection = Business.PanelSet.Model.PanelSetCollection.GetAll();
                Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(panelSetOrder.PanelSetId);

                if(panelSet.NeverDistribute == false)
                {
                    panelSetOrder.TestOrderReportDistributionCollection.AddPrimaryDistribution(this, panelSetOrder.ReportNo);
                }                
            }
        }
    }
}
