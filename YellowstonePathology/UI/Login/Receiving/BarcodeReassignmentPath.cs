using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.Receiving
{
	public class BarcodeReassignmentPath
	{
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;        

        public BarcodeReassignmentPath()
        {
        }        

        public void Start()
        {            
            this.m_LoginPageWindow = new Login.Receiving.LoginPageWindow();
            this.ShowPatientNameLookupPage();
			this.m_LoginPageWindow.ShowDialog();
        }

        

		private void ShowPatientNameLookupPage()
		{			
			PatientNameLookupPage patientNameLookupPage = new PatientNameLookupPage();
			patientNameLookupPage.Return += new PatientNameLookupPage.ReturnEventHandler(PatientNameLookupPage_Return);
			this.m_LoginPageWindow.PageNavigator.Navigate(patientNameLookupPage);
		}

		private void PatientNameLookupPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					this.ShowClientOrderPage();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.m_LoginPageWindow.Close();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Command:
					this.ShowPatientNameLookupPage();
					break;
			}
		}

		private void ShowClientOrderPage()
		{
			ClientOrderPage clientOrderPage = new ClientOrderPage(this.m_ClientOrder);
			clientOrderPage.Return += new Login.ClientOrderPage.ReturnEventHandler(ClientOrderPage_Return);
			this.m_LoginPageWindow.PageNavigator.Navigate(clientOrderPage);
		}

		private void ClientOrderPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					ClientOrderPage clientOrderPage = (ClientOrderPage)sender;
					this.m_ClientOrderDetail = clientOrderPage.SelectedClientOrderDetail;
					this.ShowScanContainerPage();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.ShowPatientNameLookupPage();
					break;
			}
		}

		private void ShowScanContainerPage()
		{
			Gross.ScanContainerPage scanContainerPage = new Gross.ScanContainerPage(this.m_SystemIdentity, "Please Scan Replacement Barcode");
            scanContainerPage.UseThisContainer += new Gross.ScanContainerPage.UseThisContainerEventHandler(ScanContainerPage_UseThisContainer);
            scanContainerPage.BarcodeWontScan += new Gross.ScanContainerPage.BarcodeWontScanEventHandler(ScanContainerPage_BarcodeWontScan);
			this.m_LoginPageWindow.PageNavigator.Navigate(scanContainerPage);
		}

        private void ScanContainerPage_BarcodeWontScan(object sender, EventArgs e)
        {
            // Barcode mannual entry
        }

        private void ScanContainerPage_UseThisContainer(object sender, string containerId)
        {
            this.ReplaceContainerId(containerId);
        }		

		private void ReplaceContainerId(string containerId)
		{
            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromContainerId(this.m_ClientOrderDetail.ContainerId);
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_LoginPageWindow);             
						
			if (accessionOrder != null)
			{
				YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrderByContainerId(this.m_ClientOrderDetail.ContainerId);								
				specimenOrder.ContainerId = containerId;
                //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(specimenOrder, false);				
			}
						
			this.m_ClientOrderDetail.ContainerId = containerId;
            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(this.m_ClientOrderDetail, false);           

			ClientOrderPage clientOrderPage = new ClientOrderPage(this.m_ClientOrder);
			clientOrderPage.Return += new Login.ClientOrderPage.ReturnEventHandler(ClientOrderPageConfirm_Return);
			this.m_LoginPageWindow.PageNavigator.Navigate(clientOrderPage);
		}

		private void ClientOrderPageConfirm_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Next:
					this.m_LoginPageWindow.Close();
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.ShowPatientNameLookupPage();
					break;
			}
		}
	}
}
