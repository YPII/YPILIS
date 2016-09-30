using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class PatientLinkingPath
	{
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;		
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;        
		private string m_ReportNo;

		public PatientLinkingPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
        }        

        public void Start()
        {
            this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
            YellowstonePathology.Business.Patient.Model.PatientLinker patientLinker = new Business.Patient.Model.PatientLinker(this.m_AccessionOrder.MasterAccessionNo,
                this.m_ReportNo,
                this.m_AccessionOrder.PFirstName,
                this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PMiddleInitial, this.m_AccessionOrder.PSSN,
                this.m_AccessionOrder.PatientId, this.m_AccessionOrder.PBirthdate);
            FinalizeAccession.PatientLinkingPage patientLinkingPage = new FinalizeAccession.PatientLinkingPage(this.m_AccessionOrder, true, Business.Patient.Model.PatientLinkingListModeEnum.AccessionOrder, patientLinker);
            patientLinkingPage.Return += new FinalizeAccession.PatientLinkingPage.ReturnEventHandler(PatientLinkingPage_Return);
            this.m_LoginPageWindow.PageNavigator.Navigate(patientLinkingPage);
            this.m_LoginPageWindow.ShowDialog();
        }       		

		private void PatientLinkingPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {                        
            this.m_LoginPageWindow.Close();
        }       
    }
}
