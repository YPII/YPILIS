using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskRefernceLabSendout : Task
    {
        public TaskRefernceLabSendout(string assignedTo, string description) 
            : base(assignedTo, description)
        {
            this.m_TaskId = "SNDOUT";
            this.m_TaskName = "Reference Lab Sendout";            
        }
    }
}
