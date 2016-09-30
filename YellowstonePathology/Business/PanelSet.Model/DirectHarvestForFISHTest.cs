using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
	public class DirectHarvestForFISHTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public DirectHarvestForFISHTest()
		{
			this.m_PanelSetId = 190;
            this.m_PanelSetName = "Direct Harvest For FISH";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Cytogenetics;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.None;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;            			            
			this.m_AllowMultiplePerAccession = true;
            this.m_NeverDistribute = true;
            this.m_ExpectedDuration = new TimeSpan(5, 0, 0, 0);

            string taskDescription = "Gather materials (Peripheral blood: 2-5 mL in sodium heparin tube and 2x5 mL in EDTA tube or " +
                "Bone marrow: 1-2 mL in sodium heparin tube and 2 mL in EDTA tube) and send out to Neo.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, new Facility.Model.NeogenomicsIrvine())); 

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.NeogenomicsIrvine();            
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();            

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
