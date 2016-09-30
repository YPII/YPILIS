/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/5/2016
 * Time: 9:48 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace YellowstonePathology.Business.Test.BCellEnumeration
{
	/// <summary>
	/// Description of BCellEnumerationTest.
	/// </summary>
	public class BCellEnumerationTest : YellowstonePathology.Business.PanelSet.Model.PanelSet
	{
		public BCellEnumerationTest()
		{
			this.m_PanelSetId = 222;
			this.m_PanelSetName = "B-Cell Enumeration";
			this.m_CaseType = YellowstonePathology.Business.CaseType.FlowCytometry;
			this.m_HasTechnicalComponent = true;			
            this.m_HasProfessionalComponent = false;
			this.m_ResultDocumentSource = YellowstonePathology.Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase;
            this.m_ReportNoLetter = new YellowstonePathology.Business.ReportNoLetterF();
            this.m_Active = true;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode = new YellowstonePathology.Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT86356(), 2);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode);

            this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationTestOrder).AssemblyQualifiedName;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationWordDocument).AssemblyQualifiedName;
            
			this.m_AllowMultiplePerAccession = true;
            this.m_EpicDistributionIsImplemented = true;

            string taskDescription = "Gather materials and perform test.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_TechnicalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
            this.m_TechnicalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();

            this.m_UniversalServiceIdCollection.Add(new YellowstonePathology.Business.ClientOrder.Model.UniversalServiceDefinitions.UniversalServiceFLOWYPI());
		}
	}
}
