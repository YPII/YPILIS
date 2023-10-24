using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Task.Model
{
	[PersistentClass(true, "tblTaskOrder", "YPIDATA")]
	public class TaskOrderCytologySlideDisposal : TaskOrder
	{
		public TaskOrderCytologySlideDisposal()
		{

		}

		public TaskOrderCytologySlideDisposal(string taskOrderId, DateTime actionDate, string objectId, YellowstonePathology.Business.User.SystemIdentity systemIdentity)            
        {            
			TaskCytologySlideDisposal taskCytologySlideDisposal = new TaskCytologySlideDisposal();

            this.m_TaskOrderId = taskOrderId;
            this.m_ObjectId = objectId;
			this.m_TaskName = "Cytology Slide Disposal";
            this.m_OrderedById = systemIdentity.User.UserId;
            this.m_OrderedByInitials = systemIdentity.User.Initials;
			this.m_OrderDate = DateTime.Now;
			this.m_TaskDate = actionDate;
			this.m_AcknowledgementType = TaskAcknowledgementType.Daily;
			this.m_TaskId = taskCytologySlideDisposal.TaskId;
        }
	}
}
