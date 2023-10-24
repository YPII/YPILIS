using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ThrombocytopeniaProfile
{
	public class ThrombocytopeniaProfileTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
    {
        public ThrombocytopeniaProfileTest()
        {
            this.m_PanelSetId = 21;
            this.m_PanelSetName = "Thrombocytopenia Profile - Retired";            
			this.m_AllowMultiplePerAccession = true;
            this.m_CaseType = this.m_CaseType = Business.CaseType.FlowCytometry;
            this.m_Active = true;
            this.m_ExpectedDuration = TimeSpan.FromHours(24);

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);

            this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            string taskDescription = "Perform thrombocytopenia profile testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_HasProfessionalComponent = false;
            this.m_ProfessionalComponentFacility = null;

            YellowstonePathology.Business.Facility.Model.Facility ypi = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentFacility = ypi;
            this.m_TechnicalComponentBillingFacility = ypi;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86023", null), 2);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode2 = new Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("85055", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode2);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceTHROMPRO());
        }
    }
}
