using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
    public class PanelSetRenalBiopsyPanel : PanelSet
	{
        public PanelSetRenalBiopsyPanel()
		{
			this.m_PanelSetId = 96;
			this.m_PanelSetName = "Renal Biopsy Panel";
            this.m_CaseType = YellowstonePathology.Business.CaseType.Surgical;
			this.m_HasTechnicalComponent = true;            
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            string taskDescription = "Gather materials and send to U of W.";

            YellowstonePathology.Business.Facility.Model.Facility universityOfWashington = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("UWRLS");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription, universityOfWashington));

            this.m_TechnicalComponentFacility = universityOfWashington;
            this.m_ProfessionalComponentFacility = universityOfWashington;

            this.m_TechnicalComponentBillingFacility = universityOfWashington;
            this.m_ProfessionalComponentBillingFacility = universityOfWashington;

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
