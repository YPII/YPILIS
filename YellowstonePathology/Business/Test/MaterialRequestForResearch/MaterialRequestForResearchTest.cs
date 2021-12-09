using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.MaterialRequestForResearch
{
    public class MaterialRequestForResearchTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public MaterialRequestForResearchTest()
        {
            this.m_PanelSetId = 362;
            this.m_PanelSetName = "Material Request For Research";
            this.m_Abbreviation = "Material Request For Research";
            this.m_CaseType = Business.CaseType.Technical;
            this.m_HasTechnicalComponent = false;
            this.m_HasProfessionalComponent = false;
            this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.None;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_NeverDistribute = true;
            this.m_MonitorPriority = MonitorPriorityNormal;

            this.m_ExpectedDuration = TimeSpan.FromDays(2);
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_RequiresPathologistSignature = false;
            this.m_AcceptOnFinal = true;
            this.m_AllowMultiplePerAccession = true;
            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
            this.m_ReportAsAdditionalTesting = false;
            this.m_IsBillable = false;

            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskSendBlockToFrontierCancer());

            YellowstonePathology.Business.Facility.Model.Facility ypi = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentFacility = ypi;
            this.m_TechnicalComponentBillingFacility = ypi;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("99000", null), 4);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);
        }
    }
}
