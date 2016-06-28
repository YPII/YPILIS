using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class ClientBillingRule : DomainBillingRule
    {
        YellowstonePathology.Business.Billing.Model.CptCodeCollection m_AllCptCodes;        

        public ClientBillingRule()
        {
            this.m_AllCptCodes = YellowstonePathology.Business.Billing.Model.CptCodeCollection.GetAll();            
        }

        public override void Run(Domain.CptBillingCode cptBillingCode)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cptCode = this.m_AllCptCodes.GetCptCode(cptBillingCode.CptCode);
            if (cptCode.FeeSchedule == YellowstonePathology.Business.Billing.Model.FeeScheduleEnum.Clinical)
            {
                cptBillingCode.BillTo = YellowstonePathology.Business.Billing.Model.BillingTypeEnum.Global.ToString();
            }
            else if (cptCode.FeeSchedule == YellowstonePathology.Business.Billing.Model.FeeScheduleEnum.Physician)
            {
                cptBillingCode.BillTo = YellowstonePathology.Business.Billing.Model.BillingTypeEnum.Client.ToString();
            }
            this.SetModifier(cptBillingCode);
        }

        private void SetModifier(Domain.CptBillingCode cptBillingCode)
        {
            YellowstonePathology.Business.Billing.Model.CptCode cptCode = this.m_AllCptCodes.GetCptCode(cptBillingCode.CptCode);
            if (cptBillingCode.BillTo == YellowstonePathology.Business.Billing.Model.BillingTypeEnum.Client.ToString())
            {
                if (cptCode.HasTechnicalComponent == true)
                {
                    cptBillingCode.Modifier = YellowstonePathology.Business.Billing.Model.CptCodeModifier.TechnicalComponent;
                }
            }
            else if (cptBillingCode.BillTo == YellowstonePathology.Business.Billing.Model.BillingTypeEnum.Global.ToString())
            {
                if (cptCode.HasProfessionalComponent == true)
                {
                    cptBillingCode.Modifier = YellowstonePathology.Business.Billing.Model.CptCodeModifier.TwentySix;
                }
            }
        }
    }
}
