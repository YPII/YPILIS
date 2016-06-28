using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskDeliverBlockToTranscription : Task
    {
        public TaskDeliverBlockToTranscription()
        {
            this.m_TaskId = "DLVRBLCK";
            this.m_TaskName = "Deliver Block To Transcription";
            this.m_Description = "Please deliver block to transcription.";
            this.m_AssignedTo = TaskAssignment.Histology;            
        }
    }
}
