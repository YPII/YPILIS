﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.PanelSet.Model
{
	class PanelSetMultipleMyelomaMGUSByFishRetired : PanelSet
	{
		public PanelSetMultipleMyelomaMGUSByFishRetired()
		{
			this.m_PanelSetId = 126;
			this.m_PanelSetName = "Multiple Myeloma MGUS By Fish - Retired";
			this.m_CaseType = Business.CaseType.FISH;
			this.m_HasTechnicalComponent = true;
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = ResultDocumentSourceEnum.PublishedDocument;
		    this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();												
			this.m_AllowMultiplePerAccession = true;
            this.m_Active = false;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Document.ReferenceLabReport).AssemblyQualifiedName;

this.m_ImplementedResultTypes.Add(Business.Test.ResultType.REFLAB);

            string taskDescription = "Gather materials (Bone Marrow Aspirate: 1-2 mL sodium heparin tube. EDTA tube is acceptable. " +
				"Peripheral Blood: 2-5 mL sodium heparin tube. EDTA tube is acceptable." +
			"Fresh, Unfixed Tissue: Tissue in RPMI. Fluids: Equal parts RPMI to specimen volume) and send out to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription, neogenomicsIrvine));

            Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88367", null), 11);
			this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
