using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class DocumentScanningPath
	{
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_ReportNo;
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;        

		public DocumentScanningPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_ReportNo = reportNo;
            this.m_AccessionOrder = accessionOrder;
        }        

        public void Start()
        {
            this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
            FinalizeAccession.DocumentScanningPage documentScanningPage = new FinalizeAccession.DocumentScanningPage(this.m_AccessionOrder, this.m_ReportNo);
            documentScanningPage.Return += new FinalizeAccession.DocumentScanningPage.ReturnEventHandler(DocumentScanPage_Return);
            this.m_LoginPageWindow.PageNavigator.Navigate(documentScanningPage);
            this.m_LoginPageWindow.ShowDialog();
        }        		        

		private void DocumentScanPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {			
            this.m_LoginPageWindow.Close();
        }
	}
}
