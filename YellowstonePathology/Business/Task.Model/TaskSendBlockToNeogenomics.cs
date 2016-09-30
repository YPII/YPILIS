using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskSendBlockToNeogenomics : Task
    {
        public TaskSendBlockToNeogenomics()
        {
            this.m_TaskId = "BLCKSNDOUTNEO";
            this.m_TaskName = "Send block to Neogenomics";
            this.m_Description = "Send block to Neogenomics for testing";
            this.m_AssignedTo = TaskAssignment.Molecular;
        }
    }
}
