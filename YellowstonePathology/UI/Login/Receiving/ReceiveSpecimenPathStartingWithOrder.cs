using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login.Receiving
{
	public class ReceiveSpecimenPathStartingWithOrder
	{        
        private YellowstonePathology.UI.Login.Receiving.LoginPageWindow m_LoginPageWindow;
        private YellowstonePathology.UI.Login.Receiving.ClientOrderReceivingHandler m_ClientOrderReceivingHandler;        
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;

        public ReceiveSpecimenPathStartingWithOrder(string clientOrderId)
        {            
            this.m_LoginPageWindow = new Receiving.LoginPageWindow();
            this.m_ClientOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrder(clientOrderId, this.m_LoginPageWindow);
        }

        public void Start()
        {                       			
			this.m_LoginPageWindow.Show();
            this.SetClient();
        }
        
		private void SetClient()
		{			
			this.m_ClientOrderReceivingHandler = new Receiving.ClientOrderReceivingHandler(this.m_LoginPageWindow);
			this.m_ClientOrderReceivingHandler.IFoundAClientOrder(this.m_ClientOrder);			

			YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_ClientOrderReceivingHandler.ClientOrder.ClientId);
			client.ClientLocationCollection.SetCurrentLocation(client.ClientLocationCollection[0].ClientLocationId);
			this.m_ClientOrderReceivingHandler.IFoundAClient(client);
            
			this.ShowItemsReceivedPage();
		}
		
		private void ShowItemsReceivedPage()
		{
            Receiving.ItemsReceivedPage itemsReceivedPage = new Receiving.ItemsReceivedPage(this.m_ClientOrderReceivingHandler);
            itemsReceivedPage.Next += new ItemsReceivedPage.NextEventHandler(ItemsReceivedPage_Next);
            itemsReceivedPage.Back += new ItemsReceivedPage.BackEventHandler(ItemsReceivedPage_Back);
            itemsReceivedPage.BarcodeWontScan += new ItemsReceivedPage.BarcodeWontScanEventHandler(ItemsReceivedPage_BarcodeWontScan);            
            itemsReceivedPage.ContainerScannedReceived += new ItemsReceivedPage.ContainScanReceivedEventHandler(ItemsReceivedPage_ContainerScannedReceived);
			itemsReceivedPage.ShowClientOrderDetailsPage += new ItemsReceivedPage.ShowClientOrderDetailsPageEventHandler(ItemsReceivedPage_ShowClientOrderDetailsPage);
			this.m_LoginPageWindow.PageNavigator.Navigate(itemsReceivedPage);
		}

        private void ItemsReceivedPage_Back(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

		private void ItemsReceivedPage_ContainerScannedReceived(object sender, YellowstonePathology.Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            this.ReceiveContainerScan(containerBarcode.ToString());
        }        

        private void ItemsReceivedPage_BarcodeWontScan(object sender, EventArgs e)
        {
            BarcodeManualEntryPage containerManualEntryPage = new BarcodeManualEntryPage();
            containerManualEntryPage.Return += new BarcodeManualEntryPage.ReturnEventHandler(ContainerManualEntryPage_Return);
            containerManualEntryPage.Back += new BarcodeManualEntryPage.BackEventHandler(ContainerManualEntryPage_Back);
            this.m_LoginPageWindow.PageNavigator.Navigate(containerManualEntryPage);						
        }

        private void ContainerManualEntryPage_Back()
        {
            this.ShowItemsReceivedPage();
        }        

        private void ItemsReceivedPage_Next(object sender, EventArgs e)
        {
			this.m_ClientOrderReceivingHandler.Save(false);
			this.StartReviewClientOrderPath();
        }

		private void ItemsReceivedPage_ShowClientOrderDetailsPage(object sender, CustomEventArgs.ClientOrderDetailReturnEventArgs e)
		{
			this.m_ClientOrderReceivingHandler.ResetTheSelectedClientOrderDetailToThisOne(e.ClientOrderDetail);
			this.ShowClientOrderDetailsPage();
		}

        private void ContainerManualEntryPage_Return(object sender, string containerId)
        {
            if (string.IsNullOrEmpty(containerId) == false)
            {
                this.ReceiveContainerScan(containerId);
            }           
        }

		private void ReceiveContainerScan(string containerId)
		{
            YellowstonePathology.UI.Login.Receiving.IFoundAContainerResult result = this.m_ClientOrderReceivingHandler.IFoundAContainer(containerId);
			if (result.OkToReceive == true)
			{
				this.ShowClientOrderDetailsPage();
			}
			else
			{
				System.Windows.MessageBox.Show(result.Message);
			}
		}

		private void ShowClientOrderDetailsPage()
		{
            Receiving.ClientOrderDetailsPage clientOrderDetailsPage = new Receiving.ClientOrderDetailsPage(this.m_LoginPageWindow.PageNavigator, this.m_ClientOrderReceivingHandler.CurrentClientOrderDetail, this.m_ClientOrderReceivingHandler.ClientOrder.SpecialInstructions);
            clientOrderDetailsPage.Next += new ClientOrderDetailsPage.NextEventHandler(ClientOrderDetailsPage_Next);
            clientOrderDetailsPage.Back += new ClientOrderDetailsPage.BackEventHandler(ClientOrderDetailsPage_Back);
            clientOrderDetailsPage.SaveClientOrderDetail += new ClientOrderDetailsPage.SaveClientOrderDetailEventHandler(ClientOrderDetailsPage_SaveClientOrderDetail);            
			this.m_LoginPageWindow.PageNavigator.Navigate(clientOrderDetailsPage);            
		}

        private void ClientOrderDetailsPage_SaveClientOrderDetail(object sender, EventArgs e)
        {
            this.m_ClientOrderReceivingHandler.Save(false);
        }        

        private void ClientOrderDetailsPage_Back(object sender, EventArgs e)
        {
            this.ShowItemsReceivedPage();
        }

        private void ClientOrderDetailsPage_Next(object sender, EventArgs e)
        {
            this.ShowItemsReceivedPage();
        }			

		private void HandleCommand(UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch ((ReceiveSpecimenCommandTypeEnum)e.Data)
			{
				case ReceiveSpecimenCommandTypeEnum.Finalize:
                    this.m_LoginPageWindow.Close();
					break;
			}
		}

		private void StartReviewClientOrderPath()
		{
			ReviewClientOrderPath reviewClientOrderPath = new ReviewClientOrderPath(this.m_LoginPageWindow.PageNavigator, this.m_ClientOrderReceivingHandler, this.m_LoginPageWindow);
			reviewClientOrderPath.Return += new ReviewClientOrderPath.ReturnEventHandler(ReviewClientOrderPath_Return);
			reviewClientOrderPath.Back += new ReviewClientOrderPath.BackEventHandler(ReviewClientOrderPath_Back);
			reviewClientOrderPath.Finish += new ReviewClientOrderPath.FinishEventHandler(ReviewClientOrderPath_Finish);
			reviewClientOrderPath.Start();
		}

		private void ReviewClientOrderPath_Finish(object sender, EventArgs e)
		{
			this.m_LoginPageWindow.Close();
		}

		private void ReviewClientOrderPath_Back(object sender, EventArgs e)
		{
			this.ShowItemsReceivedPage();
		}


		private void ReviewClientOrderPath_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Command:
					this.HandleCommand(e);
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Finish:
                    this.m_LoginPageWindow.Close();
					break;
			}
		}        
	}
}
