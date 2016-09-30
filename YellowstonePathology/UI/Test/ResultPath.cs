using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace YellowstonePathology.UI.Test
{
    public class ResultPath
    {
        public delegate void FinishEventHandler(object sender, EventArgs e);
        public event FinishEventHandler Finish;        

		protected ResultDialog m_ResultDialog;
        protected YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		protected YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        protected System.Windows.Window m_Window;
        protected string m_ResultPageClassName;

        public ResultPath(YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, System.Windows.Window window)
        {
			this.m_PageNavigator = pageNavigator;
            this.m_Window = window;     
        }

        public virtual void Start()
        {
            this.ShowResultPage();
        }        

        public void RegisterCancelATest(IResultPage resultPage)
        {
            resultPage.CancelTest += new CustomEventArgs.EventHandlerDefinitions.CancelTestEventHandler(CancelTest);
        }        
        
        private void CancelTest(object sender, YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs e)
        {
            CancelATestPath cancelATestPath = new CancelATestPath(e, this.m_PageNavigator, this.m_Window);
            cancelATestPath.Finish += new FinishEventHandler(CancellATestPath_Finish);
            cancelATestPath.Back += new CancelATestPath.BackEventHandler(CancellATestPath_Back);
            cancelATestPath.Start();
        }

        private void CancellATestPath_Back(object sender, YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs e)
        {
            System.Windows.Controls.UserControl userControl = (System.Windows.Controls.UserControl)e.ResultPage;
            this.m_PageNavigator.Navigate(userControl);
        }

        private void CancellATestPath_Finish(object sender, EventArgs e)
        {
            this.Finish(this, new EventArgs());
        }				

        public void Finished()
        {
            if (this.Finish != null) this.Finish(this, new EventArgs());
        }


        protected virtual void ShowResultPage()
        {
            throw new NotImplementedException("ShowResultPage not implemented in result path base.");
        }
    }
}
