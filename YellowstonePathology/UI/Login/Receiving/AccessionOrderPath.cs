using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Login.Receiving
{
    public class AccessionOrderPath
    {
        public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
        public event ReturnEventHandler Return;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        public delegate void FinishEventHandler(object sender, EventArgs e);
        public event FinishEventHandler Finish;

        private LoginPageWindow m_LoginPageWindow;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private PageNavigationModeEnum m_PageNavigationMode;
        
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;

        public AccessionOrderPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder,			
			PageNavigationModeEnum pageNavigationMode)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_ClientOrder = clientOrder;

			this.m_LoginPageWindow = new LoginPageWindow();
			this.m_PageNavigator = this.m_LoginPageWindow.PageNavigator;
			this.m_PageNavigationMode = pageNavigationMode;
		}

        public AccessionOrderPath(ClientOrderReceivingHandler clientOrderReceivingHandler,            
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, PageNavigationModeEnum pageNavigationMode)
        {
            this.m_AccessionOrder = clientOrderReceivingHandler.AccessionOrder;
            this.m_ClientOrder = clientOrderReceivingHandler.ClientOrder;            
            this.m_PageNavigator = pageNavigator;

            this.m_PageNavigationMode = pageNavigationMode;  
        }

        public void Start()
        {
            if (string.IsNullOrEmpty(this.m_AccessionOrder.SpecialInstructions) == false)
            {
                SpecialInstructionsWindow specialInstructionsWindow = new SpecialInstructionsWindow(this.m_AccessionOrder.SpecialInstructions);
                this.m_PageNavigator.ShowSecondMonitorWindow(specialInstructionsWindow);
            }

            this.ShowAccessionOrderPage();
            if (this.m_PageNavigationMode == PageNavigationModeEnum.Standalone)
            {
                this.m_LoginPageWindow.ShowDialog();
            }
        }		

        private void ShowAccessionOrderPage()
        {                                   
			Login.Receiving.AccessionOrderPage accessionOrderPage = new Login.Receiving.AccessionOrderPage(this.m_AccessionOrder, this.m_ClientOrder, this.m_PageNavigationMode);
			accessionOrderPage.Back += new Receiving.AccessionOrderPage.BackEventHandler(AccessionOrderPage_Back);
			accessionOrderPage.Close += new Receiving.AccessionOrderPage.CloseEventHandler(AccessionOrderPage_Close);
			accessionOrderPage.Next += new Receiving.AccessionOrderPage.NextEventHandler(AccessionOrderPage_Next);
			accessionOrderPage.OrderPanelSet += new AccessionOrderPage.OrderPanelSetEventHandler(AccessionOrderPage_OrderPanelSet);
			accessionOrderPage.ShowSurgicalDiagnosis += new AccessionOrderPage.ShowSurgicalDiagnosisEventHandler(AccessionOrderPage_ShowSurgicalDiagnosis);
			accessionOrderPage.ShowSurgicalGrossDescription += new AccessionOrderPage.ShowSurgicalGrossDescriptionEventHandler(AccessionOrderPage_ShowSurgicalGrossDescription);
			accessionOrderPage.StartAccessionedSpecimenPath += new AccessionOrderPage.StartAccessionedSpecimenPathHandler(AccessionOrderPage_StartSpecimenOrderDetailsPath);
            accessionOrderPage.ShowResultPage += new AccessionOrderPage.ShowResultPageEventHandler(AccessionOrderPage_ShowResultPage);
            accessionOrderPage.ShowMissingInformationPage += AccessionOrderPage_ShowMissingInformationPage;
			this.m_PageNavigator.Navigate(accessionOrderPage);                
        }

        private void AccessionOrderPage_ShowMissingInformationPage(object sender, EventArgs e)
        {
            YellowstonePathology.UI.Login.MissingInformationPage missingInformationPage = new MissingInformationPage(this.m_AccessionOrder);
            missingInformationPage.Next += MissingInformationPage_Next;
            missingInformationPage.Back += MissingInformationPage_Back;
            this.m_PageNavigator.Navigate(missingInformationPage);
        }

        private void MissingInformationPage_Back(object sender, EventArgs e)
        {
            this.ShowAccessionOrderPage();
        }

        private void MissingInformationPage_Next(object sender, EventArgs e)
        {
            this.ShowAccessionOrderPage();
        }

        private void AccessionOrderPage_ShowResultPage(object sender, CustomEventArgs.PanelSetOrderReturnEventArgs e)
        {                        
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;            
            YellowstonePathology.UI.Test.ResultPathFactory resultPathFactory = new Test.ResultPathFactory();
            resultPathFactory.Finished += new Test.ResultPathFactory.FinishedEventHandler(resultPathFactory_Finished);

            bool started = false;
            if (this.m_PageNavigationMode == PageNavigationModeEnum.Inline)
            {
                started = resultPathFactory.Start(e.PanelSetOrder, this.m_AccessionOrder, this.m_PageNavigator, this.m_LoginPageWindow, System.Windows.Visibility.Collapsed);
            }
            else
            {
                started = resultPathFactory.Start(e.PanelSetOrder, this.m_AccessionOrder, this.m_LoginPageWindow.PageNavigator, this.m_LoginPageWindow, System.Windows.Visibility.Collapsed);
            }
             
            if (started == false)
            {
                System.Windows.MessageBox.Show("The result for this case is not available in this view.");
            }            
        }

        private void resultPathFactory_Finished(object sender, EventArgs e)
        {
            this.ShowAccessionOrderPage();
        }        

        private void AccessionOrderPage_StartSpecimenOrderDetailsPath(object sender, CustomEventArgs.SpecimenOrderReturnEventArgs e)
        {
            SpecimenOrderDetailsPath specimenOrderDetailsPath = new SpecimenOrderDetailsPath(e.SpecimenOrder, this.m_AccessionOrder, this.m_PageNavigator);
            specimenOrderDetailsPath.Finish += new SpecimenOrderDetailsPath.FinishEventHandler(SpecimenOrderDetailsPath_Finish);
            specimenOrderDetailsPath.Start();
        }

        private void SpecimenOrderDetailsPath_Finish(object sender, EventArgs e)
        {
			this.ShowAccessionOrderPage();
		}

		private void AccessionOrderPage_Back(object sender, EventArgs e)
		{
			if(this.Back != null) this.Back(this, new EventArgs());
		}

		private void AccessionOrderPage_Close(object sender, EventArgs e)
        {            
            System.Windows.Window.GetWindow((AccessionOrderPage)sender).Close();             
        }

		private void AccessionOrderPage_Next(object sender, CustomEventArgs.ReportNoReturnEventArgs e)
		{
			YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(e.ReportNo);
			if (panelSetOrder.PanelSetId == 3 ||
                panelSetOrder.PanelSetId == 14 ||
				panelSetOrder.PanelSetId == 15 ||
                panelSetOrder.PanelSetId == 61 ||
                panelSetOrder.PanelSetId == 62 ||
				panelSetOrder.PanelSetId == 116)
			{
				YellowstonePathology.UI.Login.FinalizeAccession.FinalizeCytologyPath finalizeCytologyPath = new YellowstonePathology.UI.Login.FinalizeAccession.FinalizeCytologyPath(
					this.m_ClientOrder, this.m_AccessionOrder,
					e.ReportNo, this.m_PageNavigator);
				finalizeCytologyPath.Return += new YellowstonePathology.UI.Login.FinalizeAccession.FinalizeCytologyPath.ReturnEventHandler(CytologyFinalizationPath_Return);
				finalizeCytologyPath.Finish += new YellowstonePathology.UI.Login.FinalizeAccession.FinalizeCytologyPath.FinishEventHandler(CytologyFinalizationPath_Finish);
				finalizeCytologyPath.Start();
			}
			else
			{
				FinalizeAccession.FinalizeAccessionPath finalizeAccessionPath = new FinalizeAccession.FinalizeAccessionPath(e.ReportNo, this.m_PageNavigator, this.m_AccessionOrder);
				finalizeAccessionPath.Return += new FinalizeAccession.FinalizeAccessionPath.ReturnEventHandler(FinalizeAccessionPath_Return);
				finalizeAccessionPath.Start();
			}
		}

        private void AccessionOrderPage_OrderPanelSet(object sender, CustomEventArgs.TestOrderInfoEventArgs e)
		{
			ReportOrderPath reportOrderPath = new ReportOrderPath(this.m_AccessionOrder, this.m_ClientOrder, this.m_PageNavigator, this.m_PageNavigationMode, this.m_LoginPageWindow);
			reportOrderPath.Finish += new ReportOrderPath.FinishEventHandler(ReportOrderPath_Finish);
			reportOrderPath.Start(e.TestOrderInfo);
		}

		private void AccessionOrderPage_ShowSurgicalDiagnosis(object sender, EventArgs e)
        {
            SurgicalDiagnosisPage surgicalDiagnosisPage = new SurgicalDiagnosisPage(this.m_AccessionOrder);
            surgicalDiagnosisPage.Return += new SurgicalDiagnosisPage.ReturnEventHandler(SurgicalDiagnosisPage_Return);
            this.m_PageNavigator.Navigate(surgicalDiagnosisPage);
        }

        private void AccessionOrderPage_ShowSurgicalGrossDescription(object sender, EventArgs e)
        {
            YellowstonePathology.UI.Login.FinalizeAccession.GrossEntryPage grossEntryPage = new FinalizeAccession.GrossEntryPage(this.m_AccessionOrder);
            grossEntryPage.Next += new FinalizeAccession.GrossEntryPage.NextEventHandler(GrossEntryPage_Next);
            this.m_PageNavigator.Navigate(grossEntryPage);
        }

        private void GrossEntryPage_Next(object sender, EventArgs e)
        {
            this.ShowAccessionOrderPage();
        }        

        private void SurgicalDiagnosisPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
			this.ShowAccessionOrderPage();
        }

        private void AliquotsAndStainOrderPage_Return(object sender, Navigation.PageNavigationReturnEventArgs e)
        {
			this.ShowAccessionOrderPage();
        }

        private void ReportOrderPath_Finish(object sender, CustomEventArgs.TestOrderInfoEventArgs e)
		{
            this.ShowAccessionOrderPage();
		}

		private void FinalizeAccessionPath_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.ShowAccessionOrderPage();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Finish:
					this.Return(this, e);
					break;
			}
		}

        private void CytologyFinalizationPath_Finish(object sender, EventArgs e)
        {
            if (this.Finish != null) this.Finish(this, new EventArgs());
        }

        private void CytologyFinalizationPath_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
			this.ShowAccessionOrderPage();
        }
	}
}
