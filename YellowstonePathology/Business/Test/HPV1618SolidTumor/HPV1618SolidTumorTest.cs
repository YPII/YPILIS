﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618SolidTumor
{
	public class HPV1618SolidTumorTest : YellowstonePathology.Business.PanelSet.Model.PanelSetMolecularTest
	{
		public HPV1618SolidTumorTest()
		{
			this.m_PanelSetId = 269;
			this.m_PanelSetName = "HPV 16 18/45 Solid Tumor";
            this.m_Abbreviation = "HPV1618SLDTMR";
			this.m_CaseType = YellowstonePathology.Business.CaseType.Molecular;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;

            this.m_SendOrderToPanther = true;
            this.m_AddAliquotOnOrder = true;
            this.m_AliquotToAddOnOrder = new YellowstonePathology.Business.Specimen.Model.PantherAliquot();

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorWordDocument).AssemblyQualifiedName;
            this.m_RequiresPathologistSignature = false;
            this.m_AcceptOnFinal = false;
			this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;
            this.m_CMMCDistributionIsImplemented = true;

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_HasSplitCPTCode = false;            

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:87625"), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            string task1Description = "Cut curls and an after H&E. Give to molecular";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, task1Description));

            string task2Description = "Receive materials from histology and run test.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Molecular, task2Description));

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceHPV1618GEN());

            HPV1618SolidTumorPanel hpv1618SolidTumorPanel = new HPV1618SolidTumorPanel();
            this.m_PanelCollection.Add(hpv1618SolidTumorPanel);
        }
	}
}
