using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis
{
	public class ReticulatedPlateletAnalysisTest : YellowstonePathology.Business.PanelSet.Model.FlowCytometry.PanelSetFlowCytometry
    {
		public ReticulatedPlateletAnalysisTest()
        {
            this.m_PanelSetId = 23;
            this.m_PanelSetName = "Reticulated Platelet Analysis";
            //this.m_TestId = 5;
			this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(24, 0, 0);
            this.m_EpicDistributionIsImplemented = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis.ReticulatedPlateletAnalysisWordDocument).AssemblyQualifiedName;

            string taskDescription = "Perform reticulated platelet testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_HasProfessionalComponent = false;
            this.m_ProfessionalComponentFacility = null;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT85055(), 1);
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);               
		}
    }
}
