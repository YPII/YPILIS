﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LynchSyndromeIHCPanelTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
        public LynchSyndromeIHCPanelTest()
		{
			this.m_PanelSetId = 102;
			this.m_PanelSetName = "Lynch Syndrome IHC Panel";
            this.m_Abbreviation = "LSEIHC";
			this.m_CaseType = Business.CaseType.Surgical;
			this.m_HasTechnicalComponent = true;			
			this.m_HasProfessionalComponent = true;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterM();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelWordDocument).AssemblyQualifiedName;
            this.m_EnforceOrderTarget = true;            
            
			this.m_RequiresPathologistSignature = true;
			this.m_AcceptOnFinal = true;
			this.m_AllowMultiplePerAccession = true;
            this.m_IsBillable = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WPH);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.CMMC);

            string taskDescription = "Perform MLH1, MSH2, MSH6, PMS2 testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Histology, taskDescription));

			this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
			this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

			this.m_ProfessionalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPBLGS");
			this.m_ProfessionalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

			YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode88342 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", null), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode88342);

			YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode88341 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88341", null), 3);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode88341);

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServicePathSummary());

            LynchSyndromeIHCPanel lynchSyndromeIHCPanel = new LynchSyndromeIHCPanel();
            this.m_PanelCollection.Add(lynchSyndromeIHCPanel);
        }

        public override YellowstonePathology.Business.Rules.MethodResult OrderTargetIsOk(YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            methodResult.Success = true;

            if (orderTarget.GetType().Name != "AliquotOrder")
            {
                methodResult.Success = false;
                methodResult.Message = "Lynch Syndrome IHC must be ordered on an aliquot.";
            }

            return methodResult;
        }
	}
}
