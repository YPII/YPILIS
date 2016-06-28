using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class TaskOrderCollectionReturnEventArgs : System.EventArgs
    {
		YellowstonePathology.Business.Task.Model.TaskOrderCollection m_TaskOrderCollection;

		public TaskOrderCollectionReturnEventArgs(YellowstonePathology.Business.Task.Model.TaskOrderCollection taskOrderCollection)
        {
            this.m_TaskOrderCollection = taskOrderCollection;
        }

		public YellowstonePathology.Business.Task.Model.TaskOrderCollection TaskOrderCollection
        {
            get { return this.m_TaskOrderCollection; }
        }
    }
}
