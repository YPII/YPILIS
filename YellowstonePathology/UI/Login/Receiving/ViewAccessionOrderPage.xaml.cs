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
	public partial class ViewAccessionOrderPage : UserControl
	{
        public delegate void UseThisAccessionOrderEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.AccessionOrderReturnEventArgs e);
        public event UseThisAccessionOrderEventHandler UseThisAccessionOrder;

		public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText = "Accession Order Verification Page";

		public ViewAccessionOrderPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
			InitializeComponent();
			DataContext = this;

			Loaded += new RoutedEventHandler(ViewAccessionOrderPage_Loaded);
		}

		public void ViewAccessionOrderPage_Loaded(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Document.Result.Data.AccessionOrderDataSheetData accessionOrderDataSheetData = YellowstonePathology.Business.Gateway.XmlGateway.GetAccessionOrderDataSheetData(this.m_AccessionOrder.MasterAccessionNo);
            YellowstonePathology.Document.Result.Xps.AccessionOrderDataSheet accessionOrderDataSheet = new Document.Result.Xps.AccessionOrderDataSheet(accessionOrderDataSheetData);
            this.DocumentViewer.Document = accessionOrderDataSheet.FixedDocument;
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}		
		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{			
			this.Back(this, new EventArgs());
		}

        private void ButtonuseThisAccession_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.CustomEventArgs.AccessionOrderReturnEventArgs eventArgs = new CustomEventArgs.AccessionOrderReturnEventArgs(this.m_AccessionOrder);
            this.UseThisAccessionOrder(this, eventArgs);
        }
	}
}
