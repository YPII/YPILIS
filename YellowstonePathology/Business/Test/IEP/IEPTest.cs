using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.IEP
{
    public class IEPTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public IEPTest()
        {
            this.m_PanelSetId = 378;
            this.m_PanelSetName = "Immunoelectrophoresis";
            this.m_Abbreviation = "IEP";
            this.m_CaseType = Business.CaseType.Surgical;
            this.m_HasTechnicalComponent = false;
            this.m_HasProfessionalComponent = true;
            this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_AttemptOrderTargetLookup = true;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_NeverDistribute = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(6);
            this.m_MonitorPriority = MonitorPriorityNormal;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            YellowstonePathology.Business.Facility.Model.Facility yp = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            YellowstonePathology.Business.Facility.Model.Facility ypi = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86334", "26"), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);            

            this.m_ProfessionalComponentFacility = yp;
            this.m_ProfessionalComponentBillingFacility = ypi;            
        }
    }
}
