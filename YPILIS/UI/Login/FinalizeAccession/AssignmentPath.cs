using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class AssignmentPath
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;		
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;        

		public AssignmentPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }        

        public void Start()
        {
            this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
            FinalizeAccession.AssignmentPage AssignmentPage = new FinalizeAccession.AssignmentPage(this.m_AccessionOrder);
            AssignmentPage.Return += new FinalizeAccession.AssignmentPage.ReturnEventHandler(AssignmentPage_Return);
            this.m_LoginPageWindow.PageNavigator.Navigate(AssignmentPage);
            this.m_LoginPageWindow.ShowDialog();
        }                

		private void AssignmentPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {			
            this.m_LoginPageWindow.Close();			
		}
	}
}
