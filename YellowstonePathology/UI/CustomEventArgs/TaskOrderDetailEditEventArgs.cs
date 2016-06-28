using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
	public class TaskOrderDetailEditEventArgs
	{
		YellowstonePathology.Business.Task.Model.TaskOrderDetail m_TaskOrderDetail;

		public TaskOrderDetailEditEventArgs(YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail)
        {
			this.m_TaskOrderDetail = taskOrderDetail;
        }

		public YellowstonePathology.Business.Task.Model.TaskOrderDetail TaskOrderDetail
        {
			get { return this.m_TaskOrderDetail; }
        }
	}
}
