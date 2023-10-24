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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for ClientOrderPage.xaml
	/// </summary>
	public partial class ClientOrderPage : UserControl
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;		
		private ClientOrderDetailViewCollection m_ClientOrderDetailViewCollection;		
		private string m_PageHeaderText = "Client Order page";
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_SelectedClientOrderDetail;

		public ClientOrderPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		{
			this.m_ClientOrder = clientOrder;			
			this.m_ClientOrderDetailViewCollection = new Login.ClientOrderDetailViewCollection(this.m_ClientOrder.ClientOrderDetailCollection);

			InitializeComponent();

			this.DataContext = this;
			this.Loaded += new RoutedEventHandler(ClientOrderPage_Loaded);            
		}

        public ClientOrderPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, 
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection, 
            Visibility accessionButtonVisibility)
        {
            this.m_ClientOrder = clientOrder;
            this.m_ClientOrderDetailViewCollection = new Login.ClientOrderDetailViewCollection(clientOrderDetailCollection);

            InitializeComponent();

            this.DataContext = this;
            this.Loaded += new RoutedEventHandler(ClientOrderPage_Loaded);            
        }        

        private void ClientOrderPage_Loaded(object sender, RoutedEventArgs e)
        {
			this.ListViewClientOrderSpecimen.SelectedIndex = -1;
			this.TextBoxClientOrderPLastName.Focus();
        }

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}
		
        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
        {
			get { return this.m_ClientOrder; }
        }

        public ClientOrderDetailViewCollection ClientOrderDetailViewCollection
        {
            get { return this.m_ClientOrderDetailViewCollection; }
        }       

		private void ButtonClientDetailFresh_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewClientOrderSpecimen.SelectedItems.Count != 0)
			{
				ClientOrderDetailView clientOrderDetailView = (ClientOrderDetailView)this.ListViewClientOrderSpecimen.SelectedItem;
				clientOrderDetailView.ClientOrderDetail.ClientFixation = "Fresh";
				clientOrderDetailView.ClientOrderDetail.LabFixation = "Formalin";				
			}
		}

		private void ButtonClientDetailFormalin_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewClientOrderSpecimen.SelectedItems.Count != 0)
			{
				ClientOrderDetailView clientOrderDetailView = (ClientOrderDetailView)this.ListViewClientOrderSpecimen.SelectedItem;
				clientOrderDetailView.ClientOrderDetail.ClientFixation = "Formalin";
				clientOrderDetailView.ClientOrderDetail.LabFixation = "Formalin";				
			}
		}

		private void ButtonClientDetailProcessorSelection_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("This feature is currently not ready.");
		}

        private void BorderPanelSetOrderHeader_Loaded(object sender, RoutedEventArgs e)
        {
            Border border = sender as Border;
            ContentPresenter contentPresenter = border.TemplatedParent as ContentPresenter;
            contentPresenter.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}		

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
            this.Return(this, args);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }		

		private void ListViewClientOrderSpecimen_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ListViewClientOrderSpecimen.SelectedItem != null)
			{
				this.m_SelectedClientOrderDetail = ((ClientOrderDetailView)this.ListViewClientOrderSpecimen.SelectedItem).ClientOrderDetail;
			}
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail SelectedClientOrderDetail
		{
			get { return this.m_SelectedClientOrderDetail; }
		}		
	}
}
