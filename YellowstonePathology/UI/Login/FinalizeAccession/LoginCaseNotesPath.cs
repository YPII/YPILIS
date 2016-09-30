using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class LoginCaseNotesPath
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private Login.Receiving.LoginPageWindow m_LoginPageWindow;		

		public LoginCaseNotesPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
		}

		public void Start()
		{
			this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
            YellowstonePathology.Business.Domain.CaseNotesKeyCollection caseNotesKeyCollection = new YellowstonePathology.Business.Domain.CaseNotesKeyCollection(this.m_AccessionOrder);
            CaseNotesPage caseNotesPage = new CaseNotesPage(this.m_LoginPageWindow.PageNavigator, caseNotesKeyCollection);
            caseNotesPage.Return += new CaseNotesPage.ReturnEventHandler(CaseNotesPage_Return);
            this.m_LoginPageWindow.PageNavigator.Navigate(caseNotesPage);
            this.m_LoginPageWindow.ShowDialog();
		}				

		private void CaseNotesPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
					this.m_LoginPageWindow.Close();
					break;
			}
		}
	}
}
