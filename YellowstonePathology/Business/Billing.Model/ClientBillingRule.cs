using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class ClientBillingRule : DomainBillingRule
    {

        public ClientBillingRule()
        {
        }

        public override void Run(Domain.CptBillingCode cptBillingCode)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cptCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(cptBillingCode.CptCode, cptBillingCode.Modifier);
            if (cptCode.FeeSchedule == Business.Billing.Model.FeeScheduleEnum.Clinical)
            {
                cptBillingCode.BillTo = Business.Billing.Model.BillingTypeEnum.Global.ToString();
            }
            else if (cptCode.FeeSchedule == Business.Billing.Model.FeeScheduleEnum.Physician)
            {
                cptBillingCode.BillTo = Business.Billing.Model.BillingTypeEnum.Client.ToString();
            }
            this.SetModifier(cptBillingCode);
        }

        private void SetModifier(Domain.CptBillingCode cptBillingCode)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cptCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(cptBillingCode.CptCode, cptBillingCode.Modifier);
            if (cptBillingCode.BillTo == Business.Billing.Model.BillingTypeEnum.Client.ToString())
            {
                if (cptCode.HasTechnicalComponent == true)
                {
                    cptBillingCode.Modifier = Business.Billing.Model.CptCodeModifier.TechnicalComponent;
                }
            }
            else if (cptBillingCode.BillTo == Business.Billing.Model.BillingTypeEnum.Global.ToString())
            {
                if (cptCode.HasProfessionalComponent == true)
                {
                    cptBillingCode.Modifier = Business.Billing.Model.CptCodeModifier.TwentySix;
                }
            }
        }
    }
}
