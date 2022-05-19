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
            this.m_CaseType = Business.CaseType.Surgical;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterS();
            this.m_Active = true;
            this.m_CanHaveMultipleOrderTargets = true;
            this.m_HasNoOrderTarget = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.Surgical.SurgicalWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = false;            
            this.m_ExpectedDuration = TimeSpan.FromDays(3);

            this.m_NmhObxView = typeof(SurgicalNMHObxView);

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WPH);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.ECW);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.MDOH);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WDOH);

            this.m_RequiresAssignment = false;

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            YellowstonePathology.Business.Specimen.Model.Specimen thinPrepSpecimen = Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SPCMNTHNPRPFLD");
            this.m_OrderTargetTypeCollectionExclusions.Add(thinPrepSpecimen);
            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceYPI());
		}
	}
}
