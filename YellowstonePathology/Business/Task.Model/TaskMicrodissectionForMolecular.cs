using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskMicrodissectionForMolecular : Task
    {
        public TaskMicrodissectionForMolecular()
        {
            this.m_TaskId = "MDFMPREP";
            this.m_TaskName = "Microdissection for Molecular";
            this.m_Description = "Microdissection for Molecular";
            this.m_AssignedTo = TaskAssignment.Histology;            
        }
    }
}
