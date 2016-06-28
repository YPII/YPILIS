using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
	public class TaskAcknowledgeStainOrder : Task
    {
		public TaskAcknowledgeStainOrder()
        {
            this.m_TaskId = "ACKSTN";
            this.m_TaskName = "Acknowledge Stain Order";
            this.m_Description = string.Empty;
            this.m_AssignedTo = TaskAssignment.Histology;            
        }
	}
}
