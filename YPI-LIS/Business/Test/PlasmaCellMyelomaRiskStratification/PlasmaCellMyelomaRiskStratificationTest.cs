using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification
{
	public class PlasmaCellMyelomaRiskStratificationTest : YellowstonePathology.Business.PanelSet.Model.FISHTest
	{
		public PlasmaCellMyelomaRiskStratificationTest()
		{
			this.m_PanelSetId = 173;
			this.m_PanelSetName = "Plasma Cell Myeloma Risk Stratification";
            this.m_CaseType = Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
            this.m_Active = false;
            this.m_ExpectedDuration = TimeSpan.FromDays(4);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification.PlasmaCellMyelomaRiskStratificationTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.PlasmaCellMyelomaRiskStratification.PlasmaCellMyelomaRiskStratificationWordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);

            string taskDescription = "Gather materials (Peripheral blood: 2-5 mL in sodium heparin tube, 2x5 mL in EDTA tube; " +
            "Bone marrow: 1-2 mL in sodium heparin tube or 2 mL in EDTA tube; Fresh unfixed tissue in RPMI) and send out to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_ProfessionalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
            this.m_ProfessionalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());

            this.m_ProbeSetCount = 5;
		}
	}
}
