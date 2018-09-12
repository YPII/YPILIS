using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	public class JAK2V617FTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
		public JAK2V617FTest()
        {
			this.m_PanelSetId = 1;
			this.m_PanelSetName = "JAK2 V617F";
            this.m_Abbreviation = "J2V617F";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;            
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;

            this.m_ExpectedDuration = new TimeSpan(5, 0, 0, 0);

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = neogenomicsIrvine;
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_HasSplitCPTCode = true;            

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81270", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);
			
            string taskDescription = "Gather materials and send to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceJAK2());
        }
	}
}
