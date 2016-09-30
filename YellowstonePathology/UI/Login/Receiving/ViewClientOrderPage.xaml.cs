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
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login.Receiving
{
	/// <summary>
	/// Interaction logic for ViewClientOrderPage.xaml
	/// </summary>
	public partial class ViewClientOrderPage : UserControl
	{
        public delegate void UseThisClientOrderEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.ClientOrderReturnEventArgs e);
        public event UseThisClientOrderEventHandler UseThisClientOrder;

		public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
		private string m_PageHeaderText = "Client Order Verification Page";

		public ViewClientOrderPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		{
			this.m_ClientOrder = clientOrder;            
            object o = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Documents;

			InitializeComponent();
			DataContext = this;

			Loaded += new RoutedEventHandler(ViewClientOrderPage_Loaded);
		}

		public void ViewClientOrderPage_Loaded(object sender, RoutedEventArgs e)
		{
            XElement clientOrderElement = this.m_ClientOrder.ToXML(true);
			YellowstonePathology.Document.Result.Data.ClientOrderDataSheetData data = new YellowstonePathology.Document.Result.Data.ClientOrderDataSheetData(clientOrderElement);
			YellowstonePathology.Document.Result.Xps.ClientOrderDataSheet clientOrderDataSheet = new Document.Result.Xps.ClientOrderDataSheet(data);
			this.DocumentViewer.Document = clientOrderDataSheet.FixedDocument;
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}			

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{			
			this.Back(this, new EventArgs());
		}

        private void ButtonUseThisClientOrder_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder =  YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullClientOrder(this.m_ClientOrder.ClientOrderId, System.Windows.Window.GetWindow(this));
            YellowstonePathology.UI.CustomEventArgs.ClientOrderReturnEventArgs eventArgs = new CustomEventArgs.ClientOrderReturnEventArgs(clientOrder);
            this.UseThisClientOrder(this, eventArgs);
        }
	}
}
