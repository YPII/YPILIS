using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskInternalDelivery : Task
    {
        public TaskInternalDelivery(string assignedTo, string description) 
            : base(assignedTo, description)
        {
            this.m_TaskId = "INTDLVRY";
            this.m_TaskName = "Internal Delivery";            
        }
    }
}
