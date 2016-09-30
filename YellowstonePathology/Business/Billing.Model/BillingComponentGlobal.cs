using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillingComponentGlobal : BillingComponent
    {
        public BillingComponentGlobal(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder) :
            base(panelSetOrder)
        {

        }

        public override void Post(BillableObject billableObject)
        {
            switch (billableObject.PanelSetOrder.BillingType)
            {
                case "Global":
                    billableObject.PostGlobal("Patient", "YPIBLGS");
                    break;
                case "Split":
                    billableObject.PostTechnical("Client", "YPIBLGS");
                    billableObject.PostProfessional("Patient", "YPIBLGS");
					billableObject.PostClientGCodes(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Technical);
                    break;
                case "Client":
                    billableObject.PostGlobal("Client", "YPBLGS");
					billableObject.PostClientGCodes(YellowstonePathology.Business.Billing.Model.BillingComponentEnum.Global);
					billableObject.PanelSetOrder.ProfessionalComponentBillingFacilityId = "YPBLGS";
                    break;
            }
        }
    }
}
