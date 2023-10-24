using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
	public class TaskPOCReport : Task
	{
		public TaskPOCReport()
        {
            this.m_TaskId = "POCRPT";
			this.m_TaskName = "Products of Conception Report";
            this.m_Description = string.Empty;
            this.m_AssignedTo = TaskAssignment.Histology;            
        }
	}
}
