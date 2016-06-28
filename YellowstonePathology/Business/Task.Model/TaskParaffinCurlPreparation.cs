using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskParaffinCurlPreparation : Task
    {
        public TaskParaffinCurlPreparation()
        {
            this.m_TaskId = "PCPREP";
            this.m_TaskName = "Paraffin Curl Preparation";
            this.m_Description = "Prepare paraffin curls and 1 H&E after slide for molecular.";
            this.m_AssignedTo = TaskAssignment.Histology;            
        }
    }
}
