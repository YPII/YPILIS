using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.ThrombocytopeniaProfileV2
{
    public class ThrombocytopeniaProfileV2Test : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public ThrombocytopeniaProfileV2Test()
        {
            this.m_PanelSetId = 338;
            this.m_PanelSetName = "Thrombocytopenia Profile";
            this.m_AllowMultiplePerAccession = true;
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterF();
            this.m_CaseType = this.m_CaseType = YellowstonePathology.Business.CaseType.FlowCytometry;
            this.m_Active = true;
            this.m_HasTechnicalComponent = true;
            this.m_HasProfessionalComponent = false;
            this.m_ExpectedDuration = TimeSpan.FromHours(24);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ThrombocytopeniaProfileV2.ThrombocytopeniaProfileV2TestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ThrombocytopeniaProfileV2.ThrombocytopeniaProfileV2WordDocument).AssemblyQualifiedName;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);

            string taskDescription = "Perform thrombocytopenia profile testing.";
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            YellowstonePathology.Business.Facility.Model.Facility ypi = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentFacility = ypi;
            this.m_TechnicalComponentBillingFacility = ypi;

            this.m_ProfessionalComponentFacility = null;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86023", null), 2);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode2 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("85055", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode2);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceTHROMPRO());
        }
    }
}
