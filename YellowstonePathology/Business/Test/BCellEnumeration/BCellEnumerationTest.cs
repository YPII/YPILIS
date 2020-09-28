using System;

namespace YellowstonePathology.Business.Test.BCellEnumeration
{
	/// <summary>
	/// Description of BCellEnumerationTest.
	/// </summary>
	public class BCellEnumerationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public BCellEnumerationTest()
		{
			this.m_PanelSetId = 222;
			this.m_PanelSetName = "B-Cell Lymphocyte Enumeration";
			this.m_CaseType = YellowstonePathology.Business.CaseType.FlowCytometry;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterF();
            this.m_Active = true;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode cpt86356 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86356", null), 2);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode cpt86355 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86355", null), 1);            
            this.m_PanelSetCptCodeCollection.Add(cpt86356);
            this.m_PanelSetCptCodeCollection.Add(cpt86355);
            

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationWordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WPH);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);

            string taskDescription = "Gather materials and perform test.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicBCELEnumeration());
		}
	}
}
