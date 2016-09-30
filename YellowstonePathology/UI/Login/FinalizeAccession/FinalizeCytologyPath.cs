using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
    public class FinalizeCytologyPath
    {
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

        public delegate void FinishEventHandler(object sender, EventArgs e);
        public event FinishEventHandler Finish;
        
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private YellowstonePathology.UI.Login.FinalizeAccession.PatientLinkingPage m_PatientLinkingPage;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;		
        private YellowstonePathology.Business.Patient.Model.PatientLinker m_PatientLinker;        
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
		private string m_ReportNo;

        public FinalizeCytologyPath(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			string reportNo,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
        {                        
			this.m_ClientOrder = clientOrder;
            this.m_AccessionOrder = accessionOrder;
			this.m_PageNavigator = pageNavigator;            
			this.m_ReportNo = reportNo;
        }        

        public void Start()
        {                                 
			this.ShowPatientDetailsPage();

            if (string.IsNullOrEmpty(this.m_AccessionOrder.SpecialInstructions) == false)
            {
                SpecialInstructionsWindow specialInstructionsWindow = new SpecialInstructionsWindow(this.m_AccessionOrder.SpecialInstructions);
                this.m_PageNavigator.ShowSecondMonitorWindow(specialInstructionsWindow);
            }
        }

        private void ShowPatientDetailsPage()
        {
			FinalizeAccession.PatientDetailsPage patientDetailsPage = new FinalizeAccession.PatientDetailsPage(this.m_AccessionOrder);
			patientDetailsPage.Return += new FinalizeAccession.PatientDetailsPage.ReturnEventHandler(PatientDetailsPage_Return);
			this.m_PageNavigator.Navigate(patientDetailsPage);
        }

		private void PatientDetailsPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					this.ShowClinicalHistoryPage();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.Return(this, new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null));
					break;
			}
		}

		private void ShowClinicalHistoryPage()
		{
			FinalizeAccession.ClinicalHistoryPage clinicalHistoryPage = new FinalizeAccession.ClinicalHistoryPage(this.m_AccessionOrder);
			clinicalHistoryPage.Return += new FinalizeAccession.ClinicalHistoryPage.ReturnEventHandler(ClinicalHistoryPage_Return);
			this.m_PageNavigator.Navigate(clinicalHistoryPage);
		}

		private void ClinicalHistoryPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					this.ShowPatientLinkingPage();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.ShowPatientDetailsPage();
					break;
			}
		}

		private void ShowPatientLinkingPage()
		{
            this.m_PatientLinker = new Business.Patient.Model.PatientLinker(this.m_AccessionOrder.MasterAccessionNo,
				this.m_ReportNo,
				this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName,
				this.m_AccessionOrder.PMiddleInitial,
				this.m_AccessionOrder.PSSN,
				this.m_AccessionOrder.PatientId, this.m_AccessionOrder.PBirthdate);

			this.m_PatientLinkingPage = new FinalizeAccession.PatientLinkingPage(this.m_AccessionOrder, true, Business.Patient.Model.PatientLinkingListModeEnum.AccessionOrder, this.m_PatientLinker);
			this.m_PatientLinkingPage.Return += new FinalizeAccession.PatientLinkingPage.ReturnEventHandler(PatientLinkingPage_Return);

			this.m_PageNavigator.Navigate(this.m_PatientLinkingPage);
		}

		private void PatientLinkingPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
            switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					this.ShowProviderDistributionPage();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.ShowClinicalHistoryPage();
					break;
			}
		}

        private void ShowProviderDistributionPage()
        {
            FinalizeAccession.ProviderDistributionPage providerDistributionPage = new FinalizeAccession.ProviderDistributionPage(this.m_ReportNo, this.m_AccessionOrder, this.m_PageNavigator, System.Windows.Visibility.Visible,
                System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            providerDistributionPage.Next += new ProviderDistributionPage.NextEventHandler(ProviderDistributionPage_Next);
            providerDistributionPage.Back += new ProviderDistributionPage.BackEventHandler(ProviderDistributionPage_Back);
            this.m_PageNavigator.Navigate(providerDistributionPage);			
        }

        private void ProviderDistributionPage_Next(object sender, EventArgs e)
        {
            this.ShowStandingOrderPage();
        }

        private void ProviderDistributionPage_Back(object sender, EventArgs e)
        {
            this.ShowPatientLinkingPage();
        }				

		private void ShowStandingOrderPage()
		{									
			Test.StandingOrderPage standingOrderPage = new Test.StandingOrderPage(this.m_AccessionOrder);
			standingOrderPage.Next += new Test.StandingOrderPage.NextEventHandler(StandingOrderPage_Next);
			standingOrderPage.Back += new Test.StandingOrderPage.BackEventHandler(StandingOrderPage_Back);
			this.m_PageNavigator.Navigate(standingOrderPage);						
		}

		private void StandingOrderPage_Back(object sender, EventArgs e)
		{
            this.ShowProviderDistributionPage();
		}

		private void StandingOrderPage_Next(object sender, EventArgs e)
		{
			this.ShowICDEntryPage();
		}

        private void ShowICDEntryPage()
        {
            ICDEntryPage icdEntryPage = new ICDEntryPage(this.m_AccessionOrder, this.m_ReportNo);
            icdEntryPage.Next += new ICDEntryPage.NextEventHandler(ICDEntryPage_Next);
            icdEntryPage.Back += new ICDEntryPage.BackEventHandler(ICDEntryPage_Back);
            this.m_PageNavigator.Navigate(icdEntryPage);
        }

		public void ICDEntryPage_Back(object sender, EventArgs e)
        {
            this.ShowStandingOrderPage();
        }

        private void ICDEntryPage_Next(object sender, EventArgs e)
        {                        
			ShowScanDocumentPage();
        }        

        private void ShowScanDocumentPage()
        {
            FinalizeAccession.DocumentScanningPage documentScanningPage = new FinalizeAccession.DocumentScanningPage(this.m_AccessionOrder);
            documentScanningPage.Return += new FinalizeAccession.DocumentScanningPage.ReturnEventHandler(DocumentScanningPage_Return);
            this.m_PageNavigator.Navigate(documentScanningPage);
        }

        private void DocumentScanningPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
            switch (e.PageNavigationDirectionEnum)
            {
                case UI.Navigation.PageNavigationDirectionEnum.Next:
                    this.ShowPrintCytologyLabelsPage();
                    break;
                case UI.Navigation.PageNavigationDirectionEnum.Back:                    
					this.ShowICDEntryPage();
                    break;
            }
        }
       
		private void ShowPrintCytologyLabelsPage()
		{
			PrintCytologyLabelsPage printCytologyLabelsPage = new PrintCytologyLabelsPage(this.m_ReportNo, this.m_AccessionOrder);
			printCytologyLabelsPage.Back += new PrintCytologyLabelsPage.BackEventHandler(PrintCytologyLabelsPage_Back);
			printCytologyLabelsPage.Finish += new PrintCytologyLabelsPage.FinishEventHandler(PrintCytologyLabelsPage_Finish);
			this.m_PageNavigator.Navigate(printCytologyLabelsPage);
		}

		private void PrintCytologyLabelsPage_Back(object sender, EventArgs e)
		{
			this.ShowScanDocumentPage();
		}

		private void PrintCytologyLabelsPage_Finish(object sender, EventArgs e)
		{			
            if (this.Finish != null) this.Finish(this, new EventArgs());
		}		
	}
}
