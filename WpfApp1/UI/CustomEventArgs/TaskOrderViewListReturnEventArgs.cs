using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class TaskOrderViewListReturnEventArgs : System.EventArgs
    {
		YellowstonePathology.Business.Task.Model.TaskOrderViewList m_TaskOrderViewList;

		public TaskOrderViewListReturnEventArgs(Business.Task.Model.TaskOrderViewList taskOrderViewList)
        {
            this.m_TaskOrderViewList = taskOrderViewList;
        }

		public Business.Task.Model.TaskOrderViewList TaskOrderViewList
        {
            get { return this.m_TaskOrderViewList; }
        }
    }
}
