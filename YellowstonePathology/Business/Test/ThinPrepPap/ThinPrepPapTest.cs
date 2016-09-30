using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
	public class ThinPrepPapTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public ThinPrepPapTest()
		{
			this.m_PanelSetId = 15;
			this.m_PanelSetName = "Thin Prep Pap";
            this.m_Abbreviation = "PAP";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Cytology;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterP();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = false;            
            this.m_ExpectedDuration = new TimeSpan(30, 0, 0);
			this.m_AcceptOnFinal = true;
            this.m_AttemptOrderTargetLookup = true;
            this.m_RequireAssignmentOnOrder = false;
            this.m_CMMCDistributionIsImplemented = true;

            this.m_AddAliquotOnOrder = true;
            this.m_AliquotToAddOnOrder = new YellowstonePathology.Business.Specimen.Model.ThinPrepSlide();

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceTHINPREP());

            YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapScreeningPanel thinPrepPapScreeningPanel = new Test.ThinPrepPap.ThinPrepPapScreeningPanel();
            this.m_PanelCollection.Add(thinPrepPapScreeningPanel);

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ThinPrepFluid thinPrepFluid = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ThinPrepFluid();
            this.OrderTargetTypeCollectionRestrictions.Add(thinPrepFluid);
		}
	}
}
