using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.Receiving
{
    public class GetClientOrderPath
    {
        public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
        public event ReturnEventHandler Return;

        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;        
        private ClientOrderReceivingHandler m_ClientOrderReceivingHandler;        

        public GetClientOrderPath(ClientOrderReceivingHandler clientOrderReceivingHandler, YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
        {            
            this.m_ClientOrderReceivingHandler = clientOrderReceivingHandler;            
            this.m_PageNavigator = pageNavigator;            
        }

        public void Start()
        {
            switch (this.m_ClientOrderReceivingHandler.ExpectedOrderType)
            {
                case OrderTypeEnum.EPIC:
                case OrderTypeEnum.YPICONNECT:
                case OrderTypeEnum.ECLINICALWORKS:
                    this.ShowClientOrderLookupPage();
                    break;
                case OrderTypeEnum.REQUISITION:
                    this.HandleRequisitionOrderExpected();
                    break;
            }            
        }

		public void StartFromEnd()
		{
			switch (this.m_ClientOrderReceivingHandler.ExpectedOrderType)
			{
				case OrderTypeEnum.EPIC:
				case OrderTypeEnum.YPICONNECT:
                case OrderTypeEnum.ECLINICALWORKS:
					this.ShowViewClientOrderPage(this.m_ClientOrderReceivingHandler.ClientOrder);
					break;
				case OrderTypeEnum.REQUISITION:
					this.ShowClientOrderShortDetailsPage(this.m_ClientOrderReceivingHandler.ClientOrder);
					break;
			}
		}

        private void ShowClientOrderLookupPage()
        {
            Receiving.ClientOrderLookupPage clientOrderLookupPage = new Receiving.ClientOrderLookupPage(this.m_ClientOrderReceivingHandler.ExpectedOrderType);            
            clientOrderLookupPage.ClientOrderFound += new ClientOrderLookupPage.ClientOrderFoundEventHandler(ClientOrderLookupPage_ClientOrderFound);
            clientOrderLookupPage.MultipleClientOrdersFound += new ClientOrderLookupPage.MultipleClientOrdersFoundEventHandler(ClientOrderLookupPage_MultipleClientOrdersFound);
            clientOrderLookupPage.Back += new ClientOrderLookupPage.BackEventHandler(ClientOrderLookupPage_Back);
            this.m_PageNavigator.Navigate(clientOrderLookupPage);
        }

        private void ClientOrderLookupPage_Back(object sender, EventArgs e)
        {
            this.HandleLeavePath();
        }

        private void ClientOrderLookupPage_MultipleClientOrdersFound(object sender, CustomEventArgs.ClientOrderCollectionReturnEventArgs e)
        {
            this.ShowClientOrderSelectionPage(e.ClientOrderCollection);            
        }

        private void ClientOrderLookupPage_ClientOrderFound(object sender, CustomEventArgs.ClientOrderReturnEventArgs e)
        {
            this.ShowViewClientOrderPage(e.ClientOrder);
        }

        private void HandleRequisitionOrderExpected()
        {
			if(this.m_ClientOrderReceivingHandler.ClientOrder == null) this.m_ClientOrderReceivingHandler.LetsUseANewClientOrder();
            this.ShowClientOrderShortDetailsPage(this.m_ClientOrderReceivingHandler.ClientOrder);            
        }

        private void ShowClientOrderShortDetailsPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {            
            ClientOrderShortDetailsPage clientOrderShortDetailsPage = new ClientOrderShortDetailsPage(clientOrder);
            clientOrderShortDetailsPage.Return += new ClientOrderShortDetailsPage.ReturnEventHandler(ClientOrderShortDetailsPage_Return);
            this.m_PageNavigator.Navigate(clientOrderShortDetailsPage);
        }

        private void ClientOrderShortDetailsPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
            switch (e.PageNavigationDirectionEnum)
            {
                case UI.Navigation.PageNavigationDirectionEnum.Next:
					this.m_ClientOrderReceivingHandler.SetPatientAsVerified();
					this.m_ClientOrderReceivingHandler.ConfirmTheClientOrder();
                    this.HandleEndOfPath();
                    break;
                case UI.Navigation.PageNavigationDirectionEnum.Back:
                    this.HandleLeavePath();
                    break;
            }
        }
              
        private void ShowClientOrderSelectionPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection)
        {
            Receiving.ClientOrderSelectionPage clientOrderSelectionPage = new Receiving.ClientOrderSelectionPage(clientOrderCollection, this.m_PageNavigator);            
            clientOrderSelectionPage.Back += new ClientOrderSelectionPage.BackEventHandler(ClientOrderSelectionPage_Back);            
            clientOrderSelectionPage.ClientOrderSelected += new ClientOrderSelectionPage.ClientOrderSelectedEventHandler(ClientOrderSelectionPage_ClientOrderSelected);
            this.m_PageNavigator.Navigate(clientOrderSelectionPage);
        }

        private void ClientOrderSelectionPage_ClientOrderSelected(object sender, CustomEventArgs.ClientOrderReturnEventArgs e)
        {            
            this.m_ClientOrderReceivingHandler.IFoundAClientOrder(e.ClientOrder);            
            this.HandleEndOfPath();
        }
        
        private void ClientOrderSelectionPage_Back(object sender, EventArgs e)
        {
            this.ShowClientOrderLookupPage();
        }        

		private void ShowViewClientOrderPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {            
			ViewClientOrderPage viewClientOrderPage = new ViewClientOrderPage(clientOrder);
            viewClientOrderPage.UseThisClientOrder += new ViewClientOrderPage.UseThisClientOrderEventHandler(ViewClientOrderPage_UseThisClientOrder);
            viewClientOrderPage.Back += new ViewClientOrderPage.BackEventHandler(ViewClientOrderPage_Back);
			this.m_PageNavigator.Navigate(viewClientOrderPage);
		}

        private void ViewClientOrderPage_Back(object sender, EventArgs e)
        {
            this.ShowClientOrderLookupPage();
        }        

        private void ViewClientOrderPage_UseThisClientOrder(object sender, CustomEventArgs.ClientOrderReturnEventArgs e)
        {
            this.m_ClientOrderReceivingHandler.IFoundAClientOrder(e.ClientOrder);            
            this.m_ClientOrderReceivingHandler.SetPatientAsVerified();
            this.m_ClientOrderReceivingHandler.ConfirmTheClientOrder();
            this.HandleEndOfPath();            
        }

        private void HandleEndOfPath()
        {            
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
            this.Return(this, args);
        }

        private void HandleLeavePath()
        {
            this.m_ClientOrderReceivingHandler.ResetClientOrder();
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
            this.Return(this, args);
        }
    }
}
