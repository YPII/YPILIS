using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSetIHCHerpesVirus : PanelSet
	{
        public PanelSetIHCHerpesVirus()
		{
			this.m_PanelSetId = 154;
			this.m_PanelSetName = "HSV I/II (IHC)";
            this.m_CaseType = YellowstonePathology.Business.CaseType.IHC;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

            this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);
            this.m_NeverDistribute = true;

            string taskDescription = "Gather materials and send to NEO";
            YellowstonePathology.Business.Facility.Model.Facility phenoPath = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Transcription, taskDescription, phenoPath));

            this.m_TechnicalComponentFacility = phenoPath;
            this.m_ProfessionalComponentFacility = phenoPath;

            this.m_TechnicalComponentBillingFacility = phenoPath;
            this.m_ProfessionalComponentBillingFacility = phenoPath;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);


            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMOLEGEN());
		}
	}
}
