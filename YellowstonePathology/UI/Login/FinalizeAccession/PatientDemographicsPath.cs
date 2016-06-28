using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
    public class PatientDemographicsPath
    {
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;		
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;        

        public PatientDemographicsPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }        

        public void Start()
        {
            this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
            FinalizeAccession.PatientDetailsPage patientDetailsPage = new FinalizeAccession.PatientDetailsPage(this.m_AccessionOrder);
            patientDetailsPage.Return += new FinalizeAccession.PatientDetailsPage.ReturnEventHandler(PatientDetailsPage_Return);
            this.m_LoginPageWindow.PageNavigator.Navigate(patientDetailsPage);
            this.m_LoginPageWindow.ShowDialog();
        }       		        

        private void PatientDetailsPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {			
            this.m_LoginPageWindow.Close();			
		}       
    }
}
