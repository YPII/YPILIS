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
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for PatientNameLookupPage.xaml
	/// </summary>
	public partial class PatientNameLookupPage : UserControl
	{
		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection m_ClientOrderCollection;
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ReturnClientOrder;		
		private string m_PageHeaderText = "Patient Name Lookup";

		public PatientNameLookupPage()
		{
            this.m_ClientOrderCollection = new Business.ClientOrder.Model.ClientOrderCollection();
			InitializeComponent();
            this.Loaded += new RoutedEventHandler(PatientNameLookupPage_Loaded);
		}

        private void PatientNameLookupPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBoxPatientName.Focus();
        }

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}
		
		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection ClientOrderCollection
		{
			get { return this.m_ClientOrderCollection; }
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrder SelectedClientOrder
		{
			get { return this.ListBoxClientOrder.SelectedItem as YellowstonePathology.Business.ClientOrder.Model.ClientOrder; }
		}

		private void ButtonSearch_Click(object sender, RoutedEventArgs e)
		{
			this.m_ClientOrderCollection.Clear();

			if (!string.IsNullOrEmpty(this.TextBoxPatientName.Text))
			{
				this.Search();
			}
		}

		private void ButtonCreateClientOrder_Click(object sender, RoutedEventArgs e)
		{
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Command, Receiving.ClientOrderLookupPathCommandTypeEnum.CreateClientOrder);
			this.Return(this, args);
		}

		private void ButtonUseSelectedClientOrder_Click(object sender, RoutedEventArgs e)
		{
			if (this.SelectedClientOrder != null)
			{								
			     this.m_ReturnClientOrder = this.SelectedClientOrder;				
				UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, null);
				this.Return(this, args);
			}
		}

		private void Search()
		{
			string firstName = string.Empty;
			string lastName = string.Empty;
			string[] commaSplit = this.TextBoxPatientName.Text.Split(',');
			lastName = commaSplit[0].Trim();

			if (commaSplit.Length > 1)
			{
				firstName = commaSplit[1].Trim();
			}

			this.m_ClientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByPatientName(firstName, lastName);
			this.DataContext = ClientOrderCollection;
			this.ListBoxClientOrder.SelectedIndex = -1;
		}

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
			UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
			this.Return(this, args);
		}

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

		private void TextBoxPatientName_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (!string.IsNullOrEmpty(this.TextBoxPatientName.Text))
				{
					this.Search();
				}
			}
		}		
	}
}
