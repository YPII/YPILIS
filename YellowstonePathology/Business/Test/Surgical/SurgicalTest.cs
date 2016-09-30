using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Surgical
{
	public class SurgicalTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public SurgicalTest()
		{
			this.m_PanelSetId = 13;
			this.m_PanelSetName = "Surgical Pathology";
            this.m_Abbreviation = "SRGCL";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Surgical;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterS();
            this.m_Active = true;
            this.m_CanHaveMultipleOrderTargets = true;
            this.m_HasNoOrderTarget = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.Surgical.SurgicalWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = false;            
            this.m_ExpectedDuration = new TimeSpan(3, 0, 0, 0);
            this.m_EpicDistributionIsImplemented = true;
            this.m_CMMCDistributionIsImplemented = true;

            this.m_RequireAssignmentOnOrder = false;

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_OrderTargetTypeCollectionExclusions.Add(new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ThinPrepFluid());
            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceYPI());
		}
	}
}
