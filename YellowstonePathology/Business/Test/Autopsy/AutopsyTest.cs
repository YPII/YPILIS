using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Autopsy
{
	public class AutopsyTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public AutopsyTest()
		{
			this.m_PanelSetId = 35;
			this.m_PanelSetName = "Autopsy";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Autopsy;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterA();
            this.m_Active = true;            
			
			this.m_AllowMultiplePerAccession = false;
			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.Autopsy.AutopsyTestOrder).AssemblyQualifiedName;

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceAUTOPSY());
		}
	}
}
