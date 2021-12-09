using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MissingInformation
{
	public class MissingInformationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public MissingInformationTest()
		{
			this.m_PanelSetId = 212;
			this.m_PanelSetName = "Missing Information";
            this.m_Abbreviation = "MI";
			this.m_CaseType = Business.CaseType.Technical;
			this.m_HasTechnicalComponent = false;			
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterI();
            this.m_Active = true;
            this.m_IsBillable = false;
            this.m_ExpectedDuration = TimeSpan.FromDays(1);
            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.MissingInformation.MissingInformationTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.NothingToPublishReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_ShowResultPageOnOrder = true;
            this.m_NeverDistribute = true;
            this.m_HasNoOrderTarget = true;
            this.m_ReportAsAdditionalTesting = false;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);

            YellowstonePathology.Business.Facility.Model.Facility ypi = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentFacility = ypi;
            this.m_TechnicalComponentBillingFacility = ypi;
        }
    }
}
