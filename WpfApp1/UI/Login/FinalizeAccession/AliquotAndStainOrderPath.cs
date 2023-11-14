using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
    public class AliquotAndStainOrderPath
    {
        public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
        public event ReturnEventHandler Return;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;		
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;

		public AliquotAndStainOrderPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = panelSetOrder;			
			this.m_PageNavigator = pageNavigator;
		}

        public AliquotAndStainOrderPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
            Login.Receiving.LoginPageWindow loginPageWindow)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;
            this.m_LoginPageWindow = loginPageWindow;
            this.m_PageNavigator = loginPageWindow.PageNavigator;
        }

        public void Start()
        {            
            this.ShowAliquotAndStainOrderPage();

            if (this.m_LoginPageWindow != null)
            {
                this.Return += AliquotAndStainOrderPath_Return;
                this.m_LoginPageWindow.ShowDialog();                
            }
        }

        private void AliquotAndStainOrderPath_Return(object sender, Navigation.PageNavigationReturnEventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void ShowAliquotAndStainOrderPage()
        {
			FinalizeAccession.AliquotAndStainOrderPage aliquotAndStainOrderPage = new FinalizeAccession.AliquotAndStainOrderPage(this.m_AccessionOrder, this.m_PanelSetOrder);
            aliquotAndStainOrderPage.Return += new FinalizeAccession.AliquotAndStainOrderPage.ReturnEventHandler(AliquotAndStainOrderPage_Return);
			aliquotAndStainOrderPage.ShowTaskOrderPage += new AliquotAndStainOrderPage.ShowTaskOrderPageEventHandler(AliquotAndStainOrderPage_ShowTaskOrderPage);
            aliquotAndStainOrderPage.ShowSpecimenMappingPage += new AliquotAndStainOrderPage.ShowSpecimenMappingPageEventHandler(AliquotAndStainOrderPage_ShowSpecimenMappingPage);
            this.m_PageNavigator.Navigate(aliquotAndStainOrderPage);				
        }

        private void AliquotAndStainOrderPage_ShowSpecimenMappingPage(object sender, EventArgs e)
        {
            SpecimenMappingPage specimenMappingPage = new SpecimenMappingPage(this.m_AccessionOrder);
            specimenMappingPage.Next += new SpecimenMappingPage.NextEventHandler(SpecimenMappingPage_Next);
            specimenMappingPage.Back += new SpecimenMappingPage.BackEventHandler(SpecimenMappingPage_Back);
            this.m_PageNavigator.Navigate(specimenMappingPage);
        }

        private void SpecimenMappingPage_Next(object sender, EventArgs e)
        {
            this.ShowAliquotAndStainOrderPage();
        }

        private void SpecimenMappingPage_Back(object sender, EventArgs e)
        {
            this.ShowAliquotAndStainOrderPage();
        }

        private void AliquotAndStainOrderPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
			YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = (YellowstonePathology.Business.Task.Model.TaskOrder)e.Data;
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:					
					this.Return(this, new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null));					
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.Return(this, new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null));
					break;
			}
        }

		private void AliquotAndStainOrderPage_ShowTaskOrderPage(object sender, CustomEventArgs.AcknowledgeStainOrderEventArgs e)
		{			
			this.ShowTaskOrderPage(e.TaskOrderStainAcknowledgement);			
		}

		private void ShowTaskOrderPage(YellowstonePathology.Business.Task.Model.TaskOrder taskOrder)
		{
			YellowstonePathology.UI.Login.Receiving.TaskOrderPage taskOrderPage = new Receiving.TaskOrderPage(this.m_AccessionOrder, taskOrder, PageNavigationModeEnum.Inline);
			taskOrderPage.Back += new Receiving.TaskOrderPage.BackEventHandler(TaskOrderPage_Back);
			taskOrderPage.Next += new Receiving.TaskOrderPage.NextEventHandler(TaskOrderPage_Next);
			this.m_PageNavigator.Navigate(taskOrderPage);
		}

		private void TaskOrderPage_Next(object sender, EventArgs e)
		{
            this.Return(this, new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null));
		}

		private void TaskOrderPage_Back(object sender, EventArgs e)
		{
            this.ShowAliquotAndStainOrderPage();
        }
    }
}
