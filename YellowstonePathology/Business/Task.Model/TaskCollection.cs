using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskCollection : List<Task>
    {
        public TaskCollection()
        {            
            
        }

        public static TaskCollection GetAllTasks()
        {
            TaskCollection result = new TaskCollection();
            result.Add(new TaskParaffinCurlPreparation());
            result.Add(new TaskUnstainedSlideWithAfterSlidePreparation());
            result.Add(new TaskMicrodissectionForMolecular());
            return result;
        }
    }
}
