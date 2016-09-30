using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear
{
	public class TechInitiatedPeripheralSmearTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public TechInitiatedPeripheralSmearTest()
		{
			this.m_PanelSetId = 214;
			this.m_PanelSetName = "Tech Initiated Peripheral Smear";
            this.m_Abbreviation = "TCHPRPHRLSMR";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Technical;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterT();
            this.m_Active = true;            
			                    
			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear.TechInitiatedPeripheralSmearWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
            this.m_NeverDistribute = false;            
			this.m_AcceptOnFinal = true;
            this.m_HasNoOrderTarget = true;
            this.m_IsBillable = false;
            this.m_ShowResultPageOnOrder = true;
            this.m_EpicDistributionIsImplemented = true;
            this.m_CMMCDistributionIsImplemented = true;

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();            
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
