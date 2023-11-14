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
        protected string m_ClientSpecificDescription;
        protected bool m_IsClientSpecific;

        protected List<int> m_ClientIdList;

        public Task()
        {
            this.m_IsClientSpecific = false;
            this.m_ClientIdList = new List<int>();
        }        

        public Task(string assignedTo, string description)
        {
            this.m_IsClientSpecific = false;
            this.m_ClientIdList = new List<int>();
            this.m_TaskId = "GNRCTSK";
            this.m_AssignedTo = assignedTo;
            this.m_Description = description;            
        }

        public Task(string assignedTo, string description, string clientSpecificDescription, List<int> clientIdList)
        {
            this.m_IsClientSpecific = true;
            this.m_ClientIdList = new List<int>();
            this.m_TaskId = "GNRCTSK";
            this.m_AssignedTo = assignedTo;
            this.m_Description = description;
            this.m_ClientSpecificDescription = clientSpecificDescription;
            this.m_ClientIdList = clientIdList;
        }

        public List<int> ClientIdList
        {
            get { return this.m_ClientIdList; }
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

        public string ClientSpecificDescription
        {
            get { return this.m_ClientSpecificDescription; }
            set { this.m_ClientSpecificDescription = value; }
        }

        public bool IsClientSpecific
        {
            get { return this.m_IsClientSpecific; }
            set { this.m_IsClientSpecific = value; }
        }
    }
}
