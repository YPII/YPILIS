using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class TaskOrderReturnEventArgs : System.EventArgs
    {
		YellowstonePathology.Business.Task.Model.TaskOrder m_TaskOrder;

		public TaskOrderReturnEventArgs(YellowstonePathology.Business.Task.Model.TaskOrder taskOrder)
        {
            this.m_TaskOrder = taskOrder;
        }

		public YellowstonePathology.Business.Task.Model.TaskOrder TaskOrder
        {
            get { return this.m_TaskOrder; }
        }
    }
}
