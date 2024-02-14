using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskAfterSlidePreparation : Task
    {
        public TaskAfterSlidePreparation()
        {
            this.m_TaskId = "ASPREP";
            this.m_TaskName = "After Slide  Preparation";
            this.m_Description = "1 H&E after slide";
            this.m_AssignedTo = TaskAssignment.Histology;            
        }
    }
}
