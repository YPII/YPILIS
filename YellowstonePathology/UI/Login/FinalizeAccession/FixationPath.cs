using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class FixationPath
	{		
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;		
		private Login.Receiving.LoginPageWindow m_LoginPageWindow;		

		public FixationPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
		}		

		public void Start()
		{
			this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();			
		    this.ShowFixationDetailsPage();			

            this.m_LoginPageWindow.WindowState = System.Windows.WindowState.Maximized;
			this.m_LoginPageWindow.ShowDialog();
		}			

		private void ShowFixationDetailsPage()
		{
			FixationDetailsPage fixationDetailsPage = new FixationDetailsPage(this.m_AccessionOrder);
			fixationDetailsPage.Return += new FixationDetailsPage.ReturnEventHandler(FixationDetailsPage_Return);
			this.m_LoginPageWindow.PageNavigator.Navigate(fixationDetailsPage);
		}

		private void FixationDetailsPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Finish:
					this.m_LoginPageWindow.Close();					
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.m_LoginPageWindow.Close();					
					break;
			}
		}
	}
}
