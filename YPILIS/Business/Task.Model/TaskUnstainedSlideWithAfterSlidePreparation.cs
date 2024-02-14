using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskUnstainedSlideWithAfterSlidePreparation : Task
    {
        public TaskUnstainedSlideWithAfterSlidePreparation()
        {
            this.m_TaskId = "USSPREP";
            this.m_TaskName = "Unstained slide preparation with after slide";
            this.m_Description = "Prepare 2 unstained slides with 1 H&E after slide";
            this.m_AssignedTo = TaskAssignment.Histology;            
        }
    }
}
