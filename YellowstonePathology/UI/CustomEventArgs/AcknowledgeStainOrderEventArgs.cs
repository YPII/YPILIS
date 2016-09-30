using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
	public class AcknowledgeStainOrderEventArgs : System.EventArgs
	{
		private YellowstonePathology.Business.Task.Model.TaskOrderStainAcknowledgement m_TaskOrderStainAcknowledgement;

		public AcknowledgeStainOrderEventArgs(YellowstonePathology.Business.Task.Model.TaskOrderStainAcknowledgement taskOrderStainAcknowledgement)
		{
			this.m_TaskOrderStainAcknowledgement = taskOrderStainAcknowledgement;
		}

		public YellowstonePathology.Business.Task.Model.TaskOrderStainAcknowledgement TaskOrderStainAcknowledgement
		{
			get { return this.m_TaskOrderStainAcknowledgement; }
		}
	}
}
