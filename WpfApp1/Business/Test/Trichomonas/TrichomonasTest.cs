﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Trichomonas
{
	public class TrichomonasTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
		public TrichomonasTest()
		{
			this.m_PanelSetId = 61;
			this.m_PanelSetName = "Trichomonas Vaginalis";
            this.m_Abbreviation = "TRICH";
            this.m_CaseType = Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;            
			this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WPH);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);

            this.m_AddAliquotOnOrder = true;
            this.m_AliquotToAddOnOrder = new YellowstonePathology.Business.Specimen.Model.PantherAliquot();
            this.m_SendOrderToPanther = true;

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.Trichomonas.TrichomonasWordDocument).AssemblyQualifiedName;

            string taskDescription = "Gather materials and perform testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription));

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");            

            this.m_HasSplitCPTCode = false;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("87661", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceTRCHMNAA());

            YellowstonePathology.Business.Specimen.Model.Specimen thinPrepFluid = Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SPCMNTHNPRPFLD"); // Definition.ThinPrepFluid();
            this.OrderTargetTypeCollectionRestrictions.Add(thinPrepFluid);
		}
	}
}
