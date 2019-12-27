using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetArupAlpha1AntitrypsinStain : PanelSet
	{
        public PanelSetArupAlpha1AntitrypsinStain()
		{
			this.m_PanelSetId = 83;
            this.m_PanelSetName = "Alpha 1 Antitrypsin Stain";
            this.m_CaseType = YellowstonePathology.Business.CaseType.IHC;
			this.m_HasTechnicalComponent = true;            
            this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            string taskDescription = "Gather materials and send to ARUP.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("ARUPSPD");
            this.m_ProfessionalComponentFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("ARUPSPD");

            this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("ARUPSPD");
            this.m_ProfessionalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("ARUPSPD");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
