using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Common
{
	public partial class PatientLinkingDialog : Window
	{       
		private Login.FinalizeAccession.PatientLinkingPage m_PatientLinkingPage;

        public PatientLinkingDialog(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Patient.Model.PatientLinkingListModeEnum mode, YellowstonePathology.Business.Patient.Model.PatientLinker patientLinker)
		{
			this.m_PatientLinkingPage = new Login.FinalizeAccession.PatientLinkingPage(accessionOrder, false, mode, patientLinker);
			this.m_PatientLinkingPage.Return += new Login.FinalizeAccession.PatientLinkingPage.ReturnEventHandler(PatientLinkingPage_Return);
	
			InitializeComponent();
			this.MainContent.Content = this.m_PatientLinkingPage;
		}

		private void PatientLinkingPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.DialogResult = false;
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					break;
			}
			this.Close();
		}
	}
}
