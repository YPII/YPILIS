using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskBlockMaterialAlert : Task
    {
        public TaskBlockMaterialAlert()
        {
            this.m_TaskId = "BLCKMTRLLRT";
            this.m_TaskName = "Return Block To Flow";
            this.m_Description = "Please deliver block to Flow Cytometry.";
            this.m_AssignedTo = TaskAssignment.Transcription;
        }
    }
}
