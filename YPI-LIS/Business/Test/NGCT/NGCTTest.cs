using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
		public NGCTTest()
		{
			this.m_PanelSetId = 3;
			this.m_PanelSetName = "NG-CT";
            this.m_Abbreviation = "NGCT";
            this.m_CaseType = Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;
            this.m_AttemptOrderTargetLookup = true;
			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.NGCT.NGCTTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.NGCT.NGCTWordDocument).AssemblyQualifiedName;
			this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(4);

            this.m_AddAliquotOnOrder = true;
            this.m_AliquotToAddOnOrder = new YellowstonePathology.Business.Specimen.Model.PantherAliquot();
            this.m_SendOrderToPanther = true;

            string taskDescription = "Gather materials and perform NGCT.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, taskDescription));

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_HasSplitCPTCode = false;
            this.m_NmhObxView = typeof(NGCTNMHObxView);

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WPH);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("87491", null), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode2 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("87591", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode2);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceCTGC());

            YellowstonePathology.Business.Specimen.Model.Specimen thinPrepFluid = Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SPCMNTHNPRPFLD"); // Definition.ThinPrepFluid();
            this.OrderTargetTypeCollectionRestrictions.Add(thinPrepFluid);
		}
	}
}
