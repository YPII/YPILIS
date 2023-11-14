using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskFax : Task
    {
        private string m_DocumentName;

        public TaskFax(string assignedTo, string description, string documentName) 
            : base(assignedTo, description)
        {
            this.m_TaskId = "TSKFAX";
            this.m_TaskName = "Fax";
            this.m_DocumentName = documentName;
        }        

        public string DocumentName
        {
            get { return this.m_DocumentName; }
        }
    }
}
