using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MultipleMyelomaIgHByFish
{
	public class MultipleMyelomaIgHByFishTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public MultipleMyelomaIgHByFishTest()
		{
			this.m_PanelSetId = 161;
            this.m_PanelSetName = "Multiple Myeloma IgH Complex By Fish";
            this.m_CaseType = YellowstonePathology.Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;


			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.MultipleMyelomaIgHByFish.PanelSetOrderMultipleMyelomaIgHComplexByFish).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.MultipleMyelomaIgHByFish.MultipleMyelomaIgHByFishWordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;
            //changed by MS and TK
            this.m_EpicDistributionIsImplemented = true;

            string taskDescription = "Gather materials (Peripheral blood: 2-5 mL in sodium heparin tube and 2x5 mL in EDTA tube or " +
            "Bone marrow: 1-2 mL in sodium heparin tube and 2 mL in EDTA tube or Fresh unfixed tissue in RPMI) and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, new Facility.Model.NeogenomicsIrvine()));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
