using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Task.Model
{
    public class Task 
    {       
        protected string m_TaskId;
        protected string m_TaskName;
        protected string m_AssignedTo;
        protected string m_Description;

        public Task()
        {

        }

        public Task(string assignedTo, string description)
        {
            this.m_TaskId = "GNRCTSK";
            this.m_AssignedTo = assignedTo;
            this.m_Description = description;            
        }

        public string TaskId
        {
            get { return this.m_TaskId; }
            set { this.m_TaskId = value; }
        }

        public string TaskName
        {
            get { return this.m_TaskName; }
            set { this.m_TaskName = value; }
        }

        public string AssignedTo
        {
            get { return this.m_AssignedTo; }
            set { this.m_AssignedTo = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }                
    }
}
