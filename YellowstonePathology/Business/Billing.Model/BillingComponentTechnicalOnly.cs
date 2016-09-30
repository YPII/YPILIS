using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillingComponentTechnicalOnly : BillingComponent
    {
        public BillingComponentTechnicalOnly(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder) :
            base(panelSetOrder)            
        {

        }

        public override void Post(BillableObject billableObject)
        {            
            switch (this.m_PanelSetOrder.BillingType)
            {
                case "Global":
                    billableObject.PostTechnical("Patient", "YPIBLGS");
					billableObject.PostClientGCodes(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical);                      
                    break;
                case "Split":
                    billableObject.PostTechnical("Client", "YPBLGS");
					billableObject.PostClientGCodes(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical);
                    break;
                case "Client":
                    billableObject.PanelSetOrder.TechnicalComponentBillingFacilityId = "YPBLGS";
                    billableObject.PostTechnical("Client", "YPBLGS");
					billableObject.PostClientGCodes(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical);                                               					
                    break;
            }
        }
    }
}
