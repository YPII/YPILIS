using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LLP
{
	public class LeukemiaLymphomaTest : YellowstonePathology.Business.PanelSet.Model.FlowCytometry.PanelSetFlowCytometry
    {
		public LeukemiaLymphomaTest()
        {
            this.m_PanelSetId = 20;
            this.m_PanelSetName = "Leukemia/Lymphoma Phenotyping";            
			this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(48, 0, 0);
            this.m_CMMCDistributionIsImplemented = true;

            this.m_EpicDistributionIsImplemented = true;
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.LLP.LeukemiaLymphomaWordDocument).AssemblyQualifiedName;

            string taskDescription = "Leukemia/Lymphoma Phenotyping.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_HasProfessionalComponent = true;
            this.m_ProfessionalComponentFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings();
            this.m_ProfessionalComponentBillingFacility = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
		}
    }
}
