using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskSendBlockToFrontierCancer : Task
    {
        public TaskSendBlockToFrontierCancer()
        {
            this.m_TaskId = "SNDBLCKTFRNTRCNCR";
            this.m_TaskName = "Send Block To Frontier Cancer";
            this.m_Description = "Send blocks to Frontier Cancer.";
            this.m_AssignedTo = TaskAssignment.Transcription;
        }
    }
}
