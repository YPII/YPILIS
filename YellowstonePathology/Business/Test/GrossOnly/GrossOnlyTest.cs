using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.GrossOnly
{
	public class GrossOnlyTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public GrossOnlyTest()
		{
			this.m_PanelSetId = 238;
			this.m_PanelSetName = "Gross Only";
            this.m_Abbreviation = "GRSONLY";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Technical;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;            
			                    
			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.GrossOnly.GrossOnlyTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.GrossOnly.GrossOnlyWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
            this.m_NeverDistribute = true;            
			this.m_AcceptOnFinal = true;
            this.m_HasNoOrderTarget = true;

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();            
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
