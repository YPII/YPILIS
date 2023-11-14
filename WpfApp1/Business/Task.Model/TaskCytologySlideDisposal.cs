using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
	public class TaskCytologySlideDisposal : Task
    {
		public TaskCytologySlideDisposal()
        {
            this.m_TaskId = "CYTLGYSLDDSPSL";
            this.m_TaskName = "Cytology Slide Disposal";
            this.m_Description = string.Empty;
            this.m_AssignedTo = TaskAssignment.Histology;            
        }
	}
}
