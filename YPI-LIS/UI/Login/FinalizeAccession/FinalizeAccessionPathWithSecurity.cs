using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
    public class FinalizeAccessionPathWithSecurity
    {
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;		
		private string m_ReportNo;        
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;        

		public FinalizeAccessionPathWithSecurity(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
		}

        public void Start()
        {
            this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
            YellowstonePathology.UI.Login.FinalizeAccession.FinalizeAccessionPath finalizeAccessionPath =
                    new FinalizeAccessionPath(this.m_ReportNo, this.m_LoginPageWindow.PageNavigator, this.m_AccessionOrder);
            finalizeAccessionPath.Start();
            finalizeAccessionPath.Return += new FinalizeAccessionPath.ReturnEventHandler(FinalizeAccessionPath_Return);
            this.m_LoginPageWindow.ShowDialog();
		}                

        private void FinalizeAccessionPath_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {            			
            this.m_LoginPageWindow.Close();			
		}       
    }
}
