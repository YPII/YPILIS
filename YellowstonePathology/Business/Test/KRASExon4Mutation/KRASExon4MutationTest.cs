using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASExon4Mutation
{
	public class KRASExon4MutationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public KRASExon4MutationTest()
		{
			this.m_PanelSetId = 175;
			this.m_PanelSetName = "KRAS Exon 4 Mutation Analysis (Molecular)";
            this.m_Abbreviation = "KRASX4";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;
			this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
			this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterR();
			this.m_Active = true;


			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationWordDocument).AssemblyQualifiedName;
			
			this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);

            string taskDescription = "Collect paraffin block from Histology and send out to Neo.";

            YellowstonePathology.Business.Facility.Model.Facility neogenomicsIrvine = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("NEOGNMCIRVN");
            this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.TaskFedexShipment(YellowstonePathology.Business.Task.Model.TaskAssignment.Transcription, taskDescription, neogenomicsIrvine));

            this.m_TechnicalComponentFacility = neogenomicsIrvine;
			this.m_TechnicalComponentBillingFacility = YellowstonePathology.Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

			YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics taskSendBlockToNeogenomics = new YellowstonePathology.Business.Task.Model.TaskSendBlockToNeogenomics();
			this.m_TaskCollection.Add(taskSendBlockToNeogenomics);

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("81276", null), 1);

            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

			this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceMiscellaneous());
		}
	}
}
