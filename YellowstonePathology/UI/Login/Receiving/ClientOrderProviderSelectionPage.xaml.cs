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

namespace YellowstonePathology.UI.Login.Receiving
{	
	public partial class ClientOrderProviderSelectionPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void ProviderSelectedEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.PhysicianClientReturnEventArgs e);
        public event ProviderSelectedEventHandler ProviderSelected;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
		private string m_PageHeaderText = "Provider Selection";
		private Brush m_ProviderStatusColor;

		YellowstonePathology.Business.Client.Model.PhysicianClientCollection m_PhysicianClientCollection;

        public ClientOrderProviderSelectionPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		{
			this.m_ClientOrder = clientOrder;
            this.m_PhysicianClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientListByClientIdV2(this.m_ClientOrder.ClientId);

			InitializeComponent();

			DataContext = this;
			this.Loaded += new RoutedEventHandler(ProviderDetailPage_Loaded);
		}

		private void ProviderDetailPage_Loaded(object sender, RoutedEventArgs e)
		{
			this.SetProviderStatusColor();
			Keyboard.Focus(this.TextBoxClientSearch);
		}

		public void SetProviderStatusColor()
		{
			Brush brush = Brushes.LightGreen;
			if (YellowstonePathology.Business.Helper.PhysicianConfirmation.IsNPIValid(this.m_ClientOrder.ProviderId) == false)
			{
				brush = Brushes.PaleVioletRed;
			}
			this.ProviderStatusColor = brush;
		}

		public Brush ProviderStatusColor
		{
			get { return this.m_ProviderStatusColor; }
			set
			{
				this.m_ProviderStatusColor = value;
				this.NotifyPropertyChanged("ProviderStatusColor");
			}
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
		{
			get { return this.m_ClientOrder; }
		}

		public YellowstonePathology.Business.Client.Model.PhysicianClientCollection PhysicianClientCollection
		{
			get { return this.m_PhysicianClientCollection; }
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		private void SetPhysician(YellowstonePathology.Business.Client.Model.PhysicianClient physicianClient)
		{
			YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(physicianClient.PhysicianId);
			if (physician != null)
			{
				this.m_ClientOrder.ProviderId = physician.Npi;
				this.m_ClientOrder.ProviderName = physician.FirstName + ' ' + physician.LastName;
				this.SetProviderStatusColor();
			}
		}

		private void listViewPhysicianClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.listViewPhysicianClient.SelectedItem != null)
			{
				this.SetPhysician((YellowstonePathology.Business.Client.Model.PhysicianClient)this.listViewPhysicianClient.SelectedItem);
			}
		}

		public void TextBoxClientSearch_KeyUp(object sender, RoutedEventArgs args)
		{			
            this.m_PhysicianClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientListByPhysicianLastNameV2(this.TextBoxClientSearch.Text);
			this.listViewPhysicianClient.SelectedIndex = -1;
            this.NotifyPropertyChanged("PhysicianClientCollection");
		}		

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			this.Back(this, new EventArgs());
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            if (this.listViewPhysicianClient.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.PhysicianClient physicianClient = (YellowstonePathology.Business.Client.Model.PhysicianClient)this.listViewPhysicianClient.SelectedItem;
                this.ProviderSelected(this, new YellowstonePathology.UI.CustomEventArgs.PhysicianClientReturnEventArgs(physicianClient));
            }
            else
            {
                MessageBox.Show("Please select a provider before continuing.");                   
            }
		}		

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
