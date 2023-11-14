using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ReviewForAdditionalTesting
{
	public class ReviewForAdditionalTestingTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public ReviewForAdditionalTestingTest()
		{
			this.m_PanelSetId = 203;
			this.m_PanelSetName = "Review For Additional Testing";
			this.m_CaseType = Business.CaseType.ALLCaseTypes;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = false;            
            this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;
            this.m_IsBillable = false;
            this.m_NeverDistribute = true;
            this.m_HasNoOrderTarget = false;
            this.m_ExpectedDuration = TimeSpan.FromDays(1);
            this.m_IsClientAccessioned = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;

            this.m_MonitorPriority = PanelSet.Model.PanelSet.MonitorPriorityNormal;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
        }
	}
}
