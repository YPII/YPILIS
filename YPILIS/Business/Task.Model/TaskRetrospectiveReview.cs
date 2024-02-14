using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
	public class TaskRetrospectiveReview : Task
    {
		public TaskRetrospectiveReview()
        {
            this.m_TaskId = "TSKRTRSPVRVW";
            this.m_TaskName = "Retrospective Reviews";
            this.m_Description = string.Empty;
            this.m_AssignedTo = TaskAssignment.Histology;            
        }
	}
}
