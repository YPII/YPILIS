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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Client
{
	/// <summary>
	/// Interaction logic for ClientSupplyOrderDialog.xaml
	/// </summary>
	public partial class ClientSupplyOrderDialog : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.Client.Model.ClientSupplyCollection m_ClientSupplyCollection;
		private YellowstonePathology.Business.Client.Model.ClientSupplyOrder m_ClientSupplyOrder;
		private YellowstonePathology.Business.User.SystemUserCollection m_UserCollection;

		public ClientSupplyOrderDialog(string clientSupplyOrderId)
		{
            this.m_ClientSupplyOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientSupplyOrder(clientSupplyOrderId, this);
            this.m_UserCollection = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(Business.User.SystemUserRoleDescriptionEnum.Log, true);

			InitializeComponent();

			this.DataContext = this;
            Closing += ClientSupplyOrderDialog_Closing;
            Loaded += ClientSupplyOrderDialog_Loaded;
            Unloaded += ClientSupplyOrderDialog_Unloaded;
		}

        private void ClientSupplyOrderDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.CheckBoxFinal.Checked += CheckBoxFinal_Checked;
            this.CheckBoxFinal.Unchecked += CheckBoxFinal_Unchecked;
        }

        private void ClientSupplyOrderDialog_Unloaded(object sender, RoutedEventArgs e)
        {
            this.CheckBoxFinal.Checked -= CheckBoxFinal_Checked;
            this.CheckBoxFinal.Unchecked -= CheckBoxFinal_Unchecked;
        }

        private void ClientSupplyOrderDialog_Closing(object sender, CancelEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this.m_ClientSupplyOrder, this);
        }

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.Client.Model.ClientSupplyOrder ClientSupplyOrder
		{
			get { return this.m_ClientSupplyOrder; }
		}

		public YellowstonePathology.Business.Client.Model.ClientSupplyCollection ClientSupplyCollection
		{
			get { return this.m_ClientSupplyCollection; }
		}

		public YellowstonePathology.Business.User.SystemUserCollection UserCollection
		{
			get { return this.m_UserCollection; }
		}

		private void FillClientSupplyCollection(string clientSupplyCategory)
		{
			this.m_ClientSupplyCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientSupplyCollection(clientSupplyCategory);
			this.NotifyPropertyChanged("ClientSupplyCollection");
		}

		private void ButtonCytologySupplies_Click(object sender, RoutedEventArgs e)
		{
			this.FillClientSupplyCollection(YellowstonePathology.Business.Client.Model.ClientSupplyCategory.Cytology);
		}

		private void ButtonBiopsySupplies_Click(object sender, RoutedEventArgs e)
		{
			this.FillClientSupplyCollection(YellowstonePathology.Business.Client.Model.ClientSupplyCategory.Histology);
		}

		private void ButtonTransportSupplies_Click(object sender, RoutedEventArgs e)
		{
			this.FillClientSupplyCollection(YellowstonePathology.Business.Client.Model.ClientSupplyCategory.Transport);
		}

		private void ButtonForms_Click(object sender, RoutedEventArgs e)
		{
			this.FillClientSupplyCollection(YellowstonePathology.Business.Client.Model.ClientSupplyCategory.Forms);
		}

		private void ButtonRemoveItem_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewOrderDetails.SelectedItem != null)
			{
				this.m_ClientSupplyOrder.ClientSupplyOrderDetailCollection.Remove((YellowstonePathology.Business.Client.Model.ClientSupplyOrderDetail)this.ListViewOrderDetails.SelectedItem);
			}
		}

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{			
			this.Close();			
		}

		private void ListViewSupplies_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (string.IsNullOrEmpty(this.TextBoxQuantity.Text) == false)
			{
				string quantityOrdered = this.TextBoxQuantity.Text;												
				if (this.ListViewSupplies.SelectedItem != null)
				{						
					YellowstonePathology.Business.Client.Model.ClientSupply clientSupply = (YellowstonePathology.Business.Client.Model.ClientSupply)this.ListViewSupplies.SelectedItem;
					string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.Client.Model.ClientSupplyOrderDetail clientSupplyOrderDetail = new Business.Client.Model.ClientSupplyOrderDetail(objectId,
						this.m_ClientSupplyOrder.ClientSupplyOrderId, clientSupply.clientsupplyid, clientSupply.supplyname, clientSupply.description, quantityOrdered);
					this.m_ClientSupplyOrder.ClientSupplyOrderDetailCollection.Add(clientSupplyOrderDetail);
				}
				else
				{
					MessageBox.Show("Select a supply item to add");
				}				
			}
			else
			{
				MessageBox.Show("Enter a quantity");
			}
		}

        private void CheckBoxFinal_Checked(object sender, RoutedEventArgs e)
        {
            this.m_ClientSupplyOrder.DateOrderSent = DateTime.Now;
        }

        private void CheckBoxFinal_Unchecked(object sender, RoutedEventArgs e)
        {
            this.m_ClientSupplyOrder.DateOrderSent = null;
        }
    }
}
