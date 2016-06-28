using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class FinalizeAccessionPath
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;        

		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;		
		private string m_ReportNo;

		public FinalizeAccessionPath(string reportNo, 
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, 
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_PageNavigator = pageNavigator;
			this.m_AccessionOrder = accessionOrder;			
			this.m_ReportNo = reportNo;
		}

		public void Start()
		{            
            this.ShowPatientDetailsPage();            
		}

		private void PatientDetailsPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					this.ShowPatientLinkingPage();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.Return(this, e);
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Command:
					this.PatientDetailsPage_Return_HandleCommand(e);
					break;
			}
		}

		private void CaseNotesPath_PatientDetailsPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			this.ShowPatientDetailsPage();
		}

		private void PatientLinkingPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
            switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					this.ShowProviderDistributionPage();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.ShowPatientDetailsPage();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Command:
					this.PatientLinkingPage_Return_HandleCommand(e);
					break;
			}
		}

		private void CaseNotesPath_PatientLinkingPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			this.ShowPatientLinkingPage();
		}		

		private void CaseNotesPath_ProviderDetailPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			this.ShowProviderDistributionPage();
		}

		private void PatientHistoryPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
                    this.StartAliquotAndStainOrderPath();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
                    this.ShowProviderDistributionPage();
					break;
			}
		}		

		private void AssignmentPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
                    this.ShowPaperScanningPage();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
                    if (this.ShowSpecimenMappingPage() == false)
                    {
                        if (this.ShowCytologyClinicalHistoryPage() == false)
                        {
                            this.StartAliquotAndStainOrderPath();
                        }
                    }
					break;
			}
		}

		private void PaperScanningPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					if (this.ShowPrintCytologyLabelsPage() == false)
					{
                        this.Return(this, new UI.Navigation.PageNavigationReturnEventArgs(Navigation.PageNavigationDirectionEnum.Finish, null));
					}
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
                    if (this.ShowAssignmentPage() == false)
                    {
                        if (this.ShowGrossEntryPage() == false)
                        {
                            this.StartAliquotAndStainOrderPath();
                        }
                    }
					break;
			}
		}        

		private void ShowPatientDetailsPage()
		{
			FinalizeAccession.PatientDetailsPage patientDetailsPage = new FinalizeAccession.PatientDetailsPage(this.m_AccessionOrder);
			patientDetailsPage.Return += new FinalizeAccession.PatientDetailsPage.ReturnEventHandler(PatientDetailsPage_Return);
			this.m_PageNavigator.Navigate(patientDetailsPage);
		}

		private void ShowPatientLinkingPage()
		{
            YellowstonePathology.Business.Patient.Model.PatientLinker patientLinker = new Business.Patient.Model.PatientLinker(this.m_AccessionOrder.MasterAccessionNo,
				this.m_ReportNo,
				this.m_AccessionOrder.PFirstName, 
                this.m_AccessionOrder.PLastName,
                this.m_AccessionOrder.PMiddleInitial, 
                this.m_AccessionOrder.PSSN,
				this.m_AccessionOrder.PatientId, this.m_AccessionOrder.PBirthdate);
			PatientLinkingPage patientLinkingPage = new PatientLinkingPage(this.m_AccessionOrder, true, Business.Patient.Model.PatientLinkingListModeEnum.AccessionOrder, patientLinker);
			patientLinkingPage.Return += new PatientLinkingPage.ReturnEventHandler(PatientLinkingPage_Return);
			this.m_PageNavigator.Navigate(patientLinkingPage);
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
            if (this.ShowPatientHistoryPage() == false) this.StartAliquotAndStainOrderPath();
        }

        private void ProviderDistributionPage_Back(object sender, EventArgs e)
        {
            this.ShowPatientLinkingPage();
        }

		private bool ShowAssignmentPage()
		{
            bool result = false;

			YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyTest panelSetTechnicalOnly = new Business.Test.TechnicalOnly.TechnicalOnlyTest();
            YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTest reviewForAdditionalTestingTest = new Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTest();
            YellowstonePathology.Business.Test.IHCQC.IHCQCTest ihcQCTest = new Business.Test.IHCQC.IHCQCTest();

            if (this.m_AccessionOrder.IsDermatologyClient() == true || this.m_AccessionOrder.PanelSetOrderCollection.HasGrossBeenOrdered() == true 
                || this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetTechnicalOnly.PanelSetId) == true
                || this.m_AccessionOrder.PanelSetOrderCollection.Exists(reviewForAdditionalTestingTest.PanelSetId) == true
                || this.m_AccessionOrder.PanelSetOrderCollection.Exists(ihcQCTest.PanelSetId) == true
                || this.m_AccessionOrder.ClientId == 1260)
			{
			    AssignmentPage assignmentPage = new AssignmentPage(this.m_AccessionOrder);
			    assignmentPage.Return += new AssignmentPage.ReturnEventHandler(AssignmentPage_Return);
			    this.m_PageNavigator.Navigate(assignmentPage);
                result = true;
            }
            return result;
		}

        private void StartAliquotAndStainOrderPath()
		{
			YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			YellowstonePathology.UI.Login.FinalizeAccession.AliquotAndStainOrderPath aliquotAndStainOrderPath = new AliquotAndStainOrderPath(this.m_AccessionOrder, panelSetOrder, this.m_PageNavigator);
            aliquotAndStainOrderPath.Return += new AliquotAndStainOrderPath.ReturnEventHandler(AliquotAndStainOrderPath_Return);
            aliquotAndStainOrderPath.Start();            			
		}

        private void AliquotAndStainOrderPath_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
            switch (e.PageNavigationDirectionEnum)
            {
                case UI.Navigation.PageNavigationDirectionEnum.Next:
                    if (this.ShowSpecimenMappingPage() == false)
                    {
                        if (this.ShowCytologyClinicalHistoryPage() == false)
                        {
                            if (this.ShowAssignmentPage() == false)
                            {
                                if (this.ShowGrossEntryPage() == false)
                                {
                                    this.ShowPaperScanningPage();
                                }
                            }
                        }
                    }
                    break;
                case UI.Navigation.PageNavigationDirectionEnum.Back:
                    if (this.ShowPatientHistoryPage() == false) ShowProviderDistributionPage();
                    break;
            }
        }	

		private void ShowPaperScanningPage()
		{
            FinalizeAccession.DocumentScanningPage documentScanningPage = new FinalizeAccession.DocumentScanningPage(this.m_AccessionOrder);
            documentScanningPage.Return += new FinalizeAccession.DocumentScanningPage.ReturnEventHandler(PaperScanningPage_Return);
            this.m_PageNavigator.Navigate(documentScanningPage);
		}               

		private bool ShowPatientHistoryPage()
		{
			bool result = false;
			Common.PatientHistoryReview patientHistoryReview = new Common.PatientHistoryReview();

			if (patientHistoryReview.Run(this.m_AccessionOrder.SpecimenOrderCollection) == true)
			{
				Business.Rules.ExecutionStatus executionStatus = new Business.Rules.ExecutionStatus();
				executionStatus = patientHistoryReview.ExecutionStatus;

				PatientHistoryPage patientHistoryPage = new PatientHistoryPage(this.m_AccessionOrder, executionStatus.ExecutionMessages[0].Message);
				patientHistoryPage.Return += new PatientHistoryPage.ReturnEventHandler(PatientHistoryPage_Return);
				this.m_PageNavigator.Navigate(patientHistoryPage);
				result = true;
			}

			return result;
		}        

		private void PatientDetailsPage_Return_HandleCommand(UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch ((FinalizeAccessionCommandTypeEnum)e.Data)
			{
				case FinalizeAccessionCommandTypeEnum.ShowCaseNotes:
					YellowstonePathology.Business.Domain.CaseNotesKeyCollection caseNotesKeyCollection = new YellowstonePathology.Business.Domain.CaseNotesKeyCollection(this.m_AccessionOrder);
					CaseNotesPath caseNotesPath = new CaseNotesPath(this.m_PageNavigator, caseNotesKeyCollection);
					caseNotesPath.Return += new CaseNotesPath.ReturnEventHandler(CaseNotesPath_PatientDetailsPage_Return);
					caseNotesPath.Start();
					break;
			}
		}

		private void PatientLinkingPage_Return_HandleCommand(UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch ((FinalizeAccessionCommandTypeEnum)e.Data)
			{
				case FinalizeAccessionCommandTypeEnum.ShowCaseNotes:
					YellowstonePathology.Business.Domain.CaseNotesKeyCollection caseNotesKeyCollection = new YellowstonePathology.Business.Domain.CaseNotesKeyCollection(this.m_AccessionOrder);
					CaseNotesPath caseNotesPath = new CaseNotesPath(this.m_PageNavigator, caseNotesKeyCollection);
					caseNotesPath.Return += new CaseNotesPath.ReturnEventHandler(CaseNotesPath_PatientLinkingPage_Return);
					caseNotesPath.Start();
					break;
			}
		}

		private void ProviderDetailPage_Return_HandleCommand(UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch ((FinalizeAccessionCommandTypeEnum)e.Data)
			{
				case FinalizeAccessionCommandTypeEnum.ShowCaseNotes:
					YellowstonePathology.Business.Domain.CaseNotesKeyCollection caseNotesKeyCollection = new YellowstonePathology.Business.Domain.CaseNotesKeyCollection(this.m_AccessionOrder);
					CaseNotesPath caseNotesPath = new CaseNotesPath(this.m_PageNavigator, caseNotesKeyCollection);
					caseNotesPath.Return += new CaseNotesPath.ReturnEventHandler(CaseNotesPath_ProviderDetailPage_Return);
					caseNotesPath.Start();
					break;
			}
		}

        private bool ShowSpecimenMappingPage()
        {
            bool result = false;

            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            YellowstonePathology.Business.Test.IHCQC.IHCQCTest ihcQCTest = new Business.Test.IHCQC.IHCQCTest();
            YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTest reviewForAdditionalTesting = new Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTest();

            if (this.m_AccessionOrder.ClientAccessioned == true)
            {
                YellowstonePathology.UI.Login.FinalizeAccession.SpecimenMappingPage specimenMappingPage = new SpecimenMappingPage(this.m_AccessionOrder);
                specimenMappingPage.Next += new SpecimenMappingPage.NextEventHandler(SpecimenMappingPage_Next);
                specimenMappingPage.Back += new SpecimenMappingPage.BackEventHandler(SpecimenMappingPage_Back);
                this.m_PageNavigator.Navigate(specimenMappingPage);
                result = true;
            }

            return result;
        }

        private void SpecimenMappingPage_Back(object sender, EventArgs e)
        {
            this.StartAliquotAndStainOrderPath();
        }

        private void SpecimenMappingPage_Next(object sender, EventArgs e)
        {
            if (this.ShowCytologyClinicalHistoryPage() == false)
            {
                if (this.ShowAssignmentPage() == false)
                {
                    if (this.ShowGrossEntryPage() == false)
                    {
                        this.ShowPaperScanningPage();
                    }
                }
            }
        }

		private bool ShowCytologyClinicalHistoryPage()
		{
			bool result = false;
			if (this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo).PanelSetId == 15)
			{
				CytologyClinicalHistoryPage cytologyClinicalHistoryPage = new CytologyClinicalHistoryPage(this.m_AccessionOrder);
				cytologyClinicalHistoryPage.Return += new CytologyClinicalHistoryPage.ReturnEventHandler(CytologyClinicalHistoryPage_Return);
				this.m_PageNavigator.Navigate(cytologyClinicalHistoryPage);
				result = true;
			}
			return result;
		}

		private void CytologyClinicalHistoryPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					this.ShowPaperScanningPage();
                    break;
                case UI.Navigation.PageNavigationDirectionEnum.Back:
                    this.StartAliquotAndStainOrderPath();
                    break;
			}
		}
       
		private bool ShowPrintCytologyLabelsPage()
		{
			bool result = false;
			if (this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo).PanelSetId == 15)
			{
				PrintCytologyLabelsPage printCytologyLabelsPage = new PrintCytologyLabelsPage(this.m_ReportNo, this.m_AccessionOrder);
				printCytologyLabelsPage.Finish += new PrintCytologyLabelsPage.FinishEventHandler(PrintCytologyLabelsPage_Finish);
                printCytologyLabelsPage.Back += new PrintCytologyLabelsPage.BackEventHandler(PrintCytologyLabelsPage_Back);
				this.m_PageNavigator.Navigate(printCytologyLabelsPage);
				result = true;
			}
			return result;
		}

        private void PrintCytologyLabelsPage_Back(object sender, EventArgs e)
        {
            this.ShowPaperScanningPage();
        }        

		private void PrintCytologyLabelsPage_Finish(object sender, EventArgs e)
		{
            this.Return(this, new Navigation.PageNavigationReturnEventArgs(Navigation.PageNavigationDirectionEnum.Finish, null));		
		}          

        private bool ShowGrossEntryPage()
        {
            bool result = false;
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(81) == true && 
                this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                result = true;
                YellowstonePathology.UI.Login.FinalizeAccession.GrossEntryPage grossEntryPage = new FinalizeAccession.GrossEntryPage(this.m_AccessionOrder);
                grossEntryPage.Back += new GrossEntryPage.BackEventHandler(GrossEntryPage_Back);
                grossEntryPage.Next += new GrossEntryPage.NextEventHandler(GrossEntryPage_Next);
                this.m_PageNavigator.Navigate(grossEntryPage);                
            }
            return result;
        }

        private void GrossEntryPage_Back(object sender, EventArgs e)
        {
            if (this.ShowCytologyClinicalHistoryPage() == false)
            {
                if (this.ShowAssignmentPage() == false)
                {
                    this.StartAliquotAndStainOrderPath();
                }
            }
        }

        private void GrossEntryPage_Next(object sender, EventArgs e)
        {
            this.ShowPaperScanningPage();
        }        

        private void FnaResultPath_Back(object sender, EventArgs e)
        {
            if (this.ShowCytologyClinicalHistoryPage() == false)
            {
                if (this.ShowAssignmentPage() == false)
                {
                    this.StartAliquotAndStainOrderPath();
                }
            }
        }

        private void FnaResultPath_Next(object sender, EventArgs e)
        {            
            if(this.ShowGrossEntryPage() == false)
            {
                this.ShowPaperScanningPage();
            }
        }                
	}
}
