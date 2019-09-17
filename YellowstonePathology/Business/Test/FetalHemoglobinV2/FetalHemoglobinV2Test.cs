using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.FetalHemoglobinV2
{
    public class FetalHemoglobinV2Test : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public FetalHemoglobinV2Test()
        {
            this.m_PanelSetId = 333;
            this.m_PanelSetName = "Fetal Hemoglobin";
            this.m_AllowMultiplePerAccession = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterF();
            this.m_CaseType = this.m_CaseType = YellowstonePathology.Business.CaseType.FlowCytometry;
            this.m_Active = true;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
            this.m_ExpectedDuration = TimeSpan.FromHours(24);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.FetalHemoglobinV2.FetalHemoglobinV2TestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.FetalHemoglobinV2.FetalHemoglobinV2WordDocument).AssemblyQualifiedName;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);

            string taskDescription = "Perform fetal hemoglobin testing.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            YellowstonePathology.Business.Facility.Model.Facility ypi = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentFacility = ypi;
            this.m_TechnicalComponentBillingFacility = ypi;

            this.m_ProfessionalComponentFacility = null;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86356", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
        }
    }
}
