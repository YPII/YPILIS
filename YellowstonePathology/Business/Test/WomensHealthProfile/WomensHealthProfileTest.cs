using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.WomensHealthProfile
{
	public class WomensHealthProfileTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public WomensHealthProfileTest()
		{
			this.m_PanelSetId = 116;
			this.m_PanelSetName = "Womens Health Profile";
            this.m_CaseType = YellowstonePathology.Business.CaseType.ALLCaseTypes;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterY();
            this.m_Active = true;            
			this.m_ReflexTestingComment = string.Empty;
			this.m_EnforceOrderTarget = false;
            this.m_AttemptOrderTargetLookup = true;
			this.m_RequiresPathologistSignature = true;
			this.m_AcceptOnFinal = true;
			this.m_IsReflexPanel = true;
            this.m_AllowMultiplePerAccession = false;            
            this.m_IsBillable = false;
            this.m_NeverDistribute = false;
            this.m_ShowResultPageOnOrder = true;            
            this.m_ExpectedDuration = TimeSpan.FromDays(6);
            this.m_MonitorPriority = PanelSet.Model.PanelSet.MonitorPriorityNormal;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WPH);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileWordDocument).AssemblyQualifiedName;

			this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());

            YellowstonePathology.Business.Specimen.Model.Specimen thinPrepFluid = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SPCMNTHNPRPFLD"); // Definition.ThinPrepFluid();
            this.OrderTargetTypeCollectionRestrictions.Add(thinPrepFluid);
		}
	}
}
