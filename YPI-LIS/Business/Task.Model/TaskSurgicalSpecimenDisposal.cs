using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
	public class TaskSurgicalSpecimenDisposal : Task
    {
		public TaskSurgicalSpecimenDisposal()
        {
            this.m_TaskId = "SRGCLSPCMNDSPSL";
			this.m_TaskName = "Surgical Specimen Disposal";
            this.m_Description = string.Empty;
            this.m_AssignedTo = TaskAssignment.Histology;            
        }
	}
}
