﻿/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/5/2016
 * Time: 10:42 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace YellowstonePathology.Business.Test.TCellSubsetAnalysis
{
	/// <summary>
	/// Description of TCellSubsetAnalysisTest.
	/// </summary>
	public class TCellSubsetAnalysisTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public TCellSubsetAnalysisTest()
		{
			this.m_PanelSetId = 223;
			this.m_PanelSetName = "BAL T-Cell Subset Analysis";
			this.m_CaseType = Business.CaseType.FlowCytometry;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterF();
            this.m_Active = true;

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.TCellSubsetAnalysis.TCellSubsetAnalysisTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.TCellSubsetAnalysis.TCellSubsetAnalysisWordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;

            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.WORD);
            this.m_ImplementedResultTypes.Add(Business.Test.ResultType.EPIC);

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode cpt86359 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86359", null), 1);
            YellowstonePathology.Business.Billing.Model.PanelSetCptCode cpt86360 = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("86360", null), 1);
            
            this.m_PanelSetCptCodeCollection.Add(cpt86359);
            this.m_PanelSetCptCodeCollection.Add(cpt86360);            

            string taskDescription = "Gather materials and perform test.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_TechnicalComponentFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");
            this.m_TechnicalComponentBillingFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId("YPIBLGS");

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());            
        }
	}
}
