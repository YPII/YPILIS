using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.StemCellEnumeration
{
	public class StemCellEnumerationTest : YellowstonePathology.Business.PanelSet.Model.FlowCytometry.PanelSetFlowCytometry
    {
		public StemCellEnumerationTest()
        {
            this.m_PanelSetId = 24;
            this.m_PanelSetName = "Stem Cell Enumeration";
            //this.m_TestId = 12;
			this.m_AllowMultiplePerAccession = true;
            this.m_ExpectedDuration = new TimeSpan(24, 0, 0);
            this.m_WordDocumentClassName = typeof(YellowstonePathology.Business.Test.StemCellEnumeration.StemCellEnumerationWordDocument).AssemblyQualifiedName;

            string taskDescription = "Perform stem cell enum. testing.";
			this.m_TaskCollection.Add(new YellowstonePathology.Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Flow, taskDescription));

            this.m_HasProfessionalComponent = false;
            this.m_ProfessionalComponentFacility = null;

            YellowstonePathology.Business.Billing.Model.PanelSetCptCode panelSetCptCode1 = new Business.Billing.Model.PanelSetCptCode(new YellowstonePathology.Business.Billing.Model.CptCodeDefinition.CPT86367(), 1);            
            this.m_PanelSetCptCodeCollection.Add(panelSetCptCode1);                        
		}
    }
}
