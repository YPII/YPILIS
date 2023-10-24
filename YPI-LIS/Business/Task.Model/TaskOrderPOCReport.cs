using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Task.Model
{
	[PersistentClass(true, "tblTaskOrder", "YPIDATA")]
	public class TaskOrderPOCReport : TaskOrder
	{
		public TaskOrderPOCReport()
        {
        }

		public TaskOrderPOCReport(string taskOrderId, DateTime actionDate, string objectId, YellowstonePathology.Business.User.SystemIdentity systemIdentity)            
        {
			TaskPOCReport taskPOCReport = new TaskPOCReport();
            this.m_TaskOrderId = taskOrderId;
            this.m_ObjectId = objectId;
			this.m_TaskName = "Products of Conception Report";
            this.m_OrderedById = systemIdentity.User.UserId;
            this.m_OrderedByInitials = systemIdentity.User.Initials;
			this.m_OrderDate = DateTime.Now;
			this.m_TaskDate = actionDate;
			this.m_AcknowledgementType = TaskAcknowledgementType.Daily;
			this.m_TaskId = taskPOCReport.TaskId;
        }
	}
}
