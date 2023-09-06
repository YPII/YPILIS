using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KappaLambdaByISH
{
	public class KappaLambdaByISHTest : YellowstonePathology.Business.PanelSet.Model.FISHTest
	{
        public KappaLambdaByISHTest()
		{
			this.m_PanelSetId = 235;
            this.m_PanelSetName = "Kappa Lambda By ISH";
            this.m_CaseType = Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(1);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;
            this.m_AllowMultiplePerAccession = true;
            this.m_NeverDistribute = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            string taskDescription = "Gather materials (Bone Marrow Aspirate: 1-2 mL sodium heparin tube. EDTA tube is acceptable. " +
                "Peripheral Blood: 2-5 mL sodium heparin tube. EDTA tube is acceptable." +
                "Fresh, Unfixed Tissue: Tissue in RPMI. Fluids: Equal parts RPMI to specimen volume) and send out to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88364", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode2 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88365", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode2);

            this.m_ProbeSetCount = 0;
		}
	}
}
