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
