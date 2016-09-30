using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class CaseNotesPath
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;		
		private YellowstonePathology.Business.Domain.CaseNotesKeyCollection m_CaseNotesKeyCollection;

		public CaseNotesPath(YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, YellowstonePathology.Business.Domain.CaseNotesKeyCollection caseNotesKeyCollection)
		{
			this.m_PageNavigator = pageNavigator;			
			this.m_CaseNotesKeyCollection = caseNotesKeyCollection;
		}

		public void Start()
		{
			this.ShowCaseNotesPage();
		}

		private void ShowCaseNotesPage()
		{
			CaseNotesPage caseNotesPage = new CaseNotesPage(this.m_PageNavigator, this.m_CaseNotesKeyCollection);
			caseNotesPage.Return += new CaseNotesPage.ReturnEventHandler(CaseNotesPage_Return);
			this.m_PageNavigator.Navigate(caseNotesPage);
		}

		private void CaseNotesPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
					this.Return(this, args);
					break;
			}
		}
	}
}
