using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
    public class ShipMaterialTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public ShipMaterialTest()
		{
			this.m_PanelSetId = 244;
			this.m_PanelSetName = "Ship Material";
            this.m_Abbreviation = "SHPMTRL";			
			this.m_HasTechnicalComponent = false;			
            this.m_HasProfessionalComponent = false;
            this.m_IsBillable = false;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.None;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(3);
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_NeverDistribute = true;
            this.m_AllowMultiplePerAccession = true;
            this.m_MonitorPriority = MonitorPriorityNormal;            
			
            string taskDescription = "Ship materials";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Transcription, taskDescription, new Facility.Model.Facility()));

            YellowstonePathology.Business.Facility.Model.Facility ypi = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentFacility = ypi;
            this.m_TechnicalComponentBillingFacility = ypi;
        }
    }
}
