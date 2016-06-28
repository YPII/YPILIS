using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.IHCQC
{
	public class IHCQCTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public IHCQCTest()
		{
			this.m_PanelSetId = 201;
			this.m_PanelSetName = "IHC QC";
			this.m_CaseType = YellowstonePathology.Business.CaseType.IHC;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = false;            
            this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;
            this.m_IsBillable = true;
            this.m_NeverDistribute = true;
            this.m_HasNoOrderTarget = false;
            this.m_ExpectedDuration = TimeSpan.FromDays(1);
            this.m_IsClientAccessioned = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.IHCQC.IHCQCTestOrder).AssemblyQualifiedName;            
			this.m_AllowMultiplePerAccession = true;

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
		}
	}
}
