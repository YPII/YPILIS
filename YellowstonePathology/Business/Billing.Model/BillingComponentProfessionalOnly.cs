using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillingComponentProfessionalOnly : BillingComponent
    {
        public BillingComponentProfessionalOnly(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder) :
            base(panelSetOrder)           
        {

        }

        public override void Post(BillableObject billableObject)
        {
            switch (this.m_PanelSetOrder.BillingType)
            {
                case "Global":
                case "Split":
                    billableObject.PostProfessional("Patient", "YPIBLGS");
					billableObject.PostClientGCodes(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical);
                    break;
                case "Client":
                    billableObject.PostProfessional("Client", "YPBLGS");
					billableObject.PostClientGCodes(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical);
					billableObject.PanelSetOrder.ProfessionalComponentBillingFacilityId = "YPBLGS";
                    break;
            }
        }
    }
}
