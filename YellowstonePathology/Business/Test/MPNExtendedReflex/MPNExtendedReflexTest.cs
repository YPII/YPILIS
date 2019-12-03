using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
	public class MPNExtendedReflexTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public MPNExtendedReflexTest()
		{
			this.m_PanelSetId = 137;
			this.m_PanelSetName = "MPN Extended Reflex";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
			this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
			this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexWordDocument).AssemblyQualifiedName;
			
			this.m_ReflexTestingComment = string.Empty;
			this.m_EnforceOrderTarget = false;
			this.m_RequiresPathologistSignature = true;
			this.m_AcceptOnFinal = true;
			this.m_IsReflexPanel = true;
			this.m_AllowMultiplePerAccession = true;
			this.m_IsBillable = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(12);

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);

            YellowstonePathology.Business.Facility.Model.Facility neo = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            string taskDescription = "Gather materials and send to NEO.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription, neo));

			this.m_TechnicalComponentFacility = neo;
			this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

			this.m_ProfessionalComponentFacility = neo;
			this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");

            Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81479", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());
		}
	}
}
