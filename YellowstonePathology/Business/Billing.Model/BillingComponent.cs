using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class BillingComponent
    {
        protected YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        public BillingComponent(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_PanelSetOrder = panelSetOrder;
        }        

        public virtual void Post(BillableObject billableObject)
        {
            
        }

        public static BillingComponent GetBillingComponent(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            BillingComponent result = null;
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = YellowstonePathology.Business.Facility.Model.FacilityCollection.GetAllFacilities();
            YellowstonePathology.Business.Facility.Model.Facility technicalComponentBillingFacility = facilityCollection.GetByFacilityId(panelSetOrder.TechnicalComponentBillingFacilityId);
            YellowstonePathology.Business.Facility.Model.Facility professionalComponentBillingFacility = facilityCollection.GetByFacilityId(panelSetOrder.ProfessionalComponentBillingFacilityId);
            
            if (panelSetOrder.HasTechnicalComponent == true && panelSetOrder.HasProfessionalComponent == true)
            {
                if (YellowstonePathology.Business.Facility.Model.FacilityCollection.IsAYellowstonePathologyFacility(technicalComponentBillingFacility) &&
                    YellowstonePathology.Business.Facility.Model.FacilityCollection.IsAYellowstonePathologyFacility(professionalComponentBillingFacility) == true)
                {
                    result = new BillingComponentGlobal(panelSetOrder);                    
                }                
                else if (technicalComponentBillingFacility.IsReferenceLab == true && professionalComponentBillingFacility.IsReferenceLab == true)
                {
                    result = new BillingComponentNoBilling(panelSetOrder);
                }
                else if (YellowstonePathology.Business.Facility.Model.FacilityCollection.IsAYellowstonePathologyFacility(technicalComponentBillingFacility) == true)
                {
                    result = new BillingComponentTechnicalOnly(panelSetOrder);
                }
                else if (YellowstonePathology.Business.Facility.Model.FacilityCollection.IsAYellowstonePathologyFacility(professionalComponentBillingFacility) == true)
                {
                    result = new BillingComponentProfessionalOnly(panelSetOrder);
                }                
            }
            else if (panelSetOrder.HasTechnicalComponent == true && panelSetOrder.HasProfessionalComponent == false)
            {
                if (technicalComponentBillingFacility.IsReferenceLab == false)
                {
                    result = new BillingComponentTechnicalOnly(panelSetOrder);   
                }
                else
                {
                    result = new BillingComponentNoBilling(panelSetOrder);
                }
            }
            else if (panelSetOrder.HasTechnicalComponent == false && panelSetOrder.HasProfessionalComponent == true)
            {
                if (professionalComponentBillingFacility.IsReferenceLab == false)
                {
                    result = new BillingComponentProfessionalOnly(panelSetOrder);
                }
                else
                {
                    result = new BillingComponentNoBilling(panelSetOrder);
                }
            }
            else if (panelSetOrder.HasTechnicalComponent == false && panelSetOrder.HasProfessionalComponent == false)
            {
                result = new BillingComponentNoBilling(panelSetOrder);
            }

            return result;
        }
    }
}
