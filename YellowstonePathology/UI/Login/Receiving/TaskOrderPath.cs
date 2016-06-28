using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.Receiving
{
	public class TaskOrderPath
	{		
		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Task.Model.TaskOrder m_TaskOrder;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private PageNavigationModeEnum m_PageNavigationMode;

		public TaskOrderPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Task.Model.TaskOrder taskOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
			PageNavigationModeEnum pageNavigationMode)
		{
			this.m_AccessionOrder = accessionOrder;
            this.m_TaskOrder = taskOrder;

			this.m_PageNavigationMode = pageNavigationMode;            
            this.m_PageNavigator = pageNavigator;
		}        

		public void Start()
		{
            this.ShowTaskOrderPage();
		}                	

		private void ShowTaskOrderPage()
		{
			TaskOrderPage taskOrderPage = new TaskOrderPage(this.m_AccessionOrder, this.m_TaskOrder, this.m_PageNavigationMode);
			taskOrderPage.Back += new Receiving.TaskOrderPage.BackEventHandler(TaskOrderPage_Back);
			taskOrderPage.Close += new Receiving.TaskOrderPage.CloseEventHandler(TaskOrderPage_Close);
			taskOrderPage.Next += new TaskOrderPage.NextEventHandler(TaskOrderPage_Next);		
			this.m_PageNavigator.Navigate(taskOrderPage);
		}		

		private void TaskOrderPage_Next(object sender, EventArgs e)
		{
			this.Next(this, new EventArgs());			
		}

		private void TaskOrderPage_Close(object sender, EventArgs e)
		{
            if (this.Close != null) this.Close(this, new EventArgs());
		}

		private void TaskOrderPage_Back(object sender, EventArgs e)
		{
			
		}

		private void ShowTaskOrderEditPage(YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail)
		{
			TaskOrderEditPage taskOrderEditPage = new TaskOrderEditPage(taskOrderDetail, this.m_AccessionOrder, this.m_PageNavigationMode);
			taskOrderEditPage.Next += new Receiving.TaskOrderEditPage.NextEventHandler(TaskOrderEditPage_Next);
			taskOrderEditPage.Back += new Receiving.TaskOrderEditPage.BackEventHandler(TaskOrderEditPage_Back);
			taskOrderEditPage.Close += new Receiving.TaskOrderEditPage.CloseEventHandler(TaskOrderEditPage_Close);
			this.m_PageNavigator.Navigate(taskOrderEditPage);
		}

		private void TaskOrderEditPage_Close(object sender, EventArgs e)
		{
            if (this.Close != null) this.Close(this, new EventArgs());
		}

        private void TaskOrderEditPage_Back(object sender, EventArgs e)
		{
			this.ShowTaskOrderPage();
		}

        private void TaskOrderEditPage_Next(object sender, EventArgs e)
		{
			this.ShowTaskOrderPage();
		}
	}
}
