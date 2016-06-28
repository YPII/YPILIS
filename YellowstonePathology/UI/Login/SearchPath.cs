using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class SearchPath
	{        
		private Login.Receiving.LoginPageWindow m_LoginPageWindow;
        private LoginUIV2 m_LoginUI;

		public SearchPath(LoginUIV2 loginUI)
        {
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;
            this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
			this.m_LoginPageWindow.Height = 400;
			this.m_LoginPageWindow.Width = 500;
			this.m_LoginUI = loginUI;
        }

        public void Start()
        {
            YellowstonePathology.UI.Login.SearchSelectionPage searchSelectionPage = new SearchSelectionPage(this.m_LoginUI);
            this.m_LoginPageWindow.PageNavigator.Navigate(searchSelectionPage);
            this.m_LoginPageWindow.ShowDialog();
        }		
	}
}
