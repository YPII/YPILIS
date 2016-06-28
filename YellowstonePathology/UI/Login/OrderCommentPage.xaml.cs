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

namespace YellowstonePathology.UI.Login
{
	/// <summary>
	/// Interaction logic for OrderCommentPage.xaml
	/// </summary>
	public partial class OrderCommentPage : UserControl
	{
		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.Domain.OrderCommentLogCollection m_OrderCommentLogCollection;
		private string m_PageHeaderText;

		public OrderCommentPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		{
			this.m_OrderCommentLogCollection = YellowstonePathology.Business.Gateway.OrderCommentGateway.GetOrderCommentLogCollectionByClientOrderId(clientOrder.ClientOrderId);
			this.m_PageHeaderText = "Events for " + clientOrder.PatientDisplayName;

			InitializeComponent();

			this.DataContext = this;            
		}		        

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.Domain.OrderCommentLogCollection OrderCommentLogCollection
		{
			get { return this.m_OrderCommentLogCollection; }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			this.Back(this, new EventArgs());
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			this.Next(this, new EventArgs());
		}

        private void BorderPanelSetOrderHeader_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
