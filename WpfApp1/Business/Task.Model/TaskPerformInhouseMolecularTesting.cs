using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskPerformInhouseMolecularTesting : Task
    {
        public TaskPerformInhouseMolecularTesting(string testName)
        {
            this.m_TaskId = "INHSMOLGEN";
            this.m_TaskName = testName;
            this.m_Description = "The following test has been ordered: " + testName;
            this.m_AssignedTo = TaskAssignment.Molecular;            
        }
    }
}
