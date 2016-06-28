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
	/// Interaction logic for DocumentScanPage.xaml
	/// </summary>
	public partial class DocumentScanPage : PageFunction<PageFunctionResult>
	{
		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
		private string m_StepText;

		public DocumentScanPage(string stepText)
		{
			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
			this.m_StepText = stepText;			
			InitializeComponent();
			DataContext = this;
		}

		public string StepText
		{
			get { return this.m_StepText; }
		}		

		private void ButtonBarcodeDidNotScan_Click(object sender, RoutedEventArgs e)
		{
			//ReceiveSpecimen.BarcodeManualEntryPage barcodeManualEntryPage = new ReceiveSpecimen.BarcodeManualEntryPage(BarcodeTypeEnum.Document);
			//barcodeManualEntryPage.Return += new ReturnEventHandler<PageFunctionResult>(BarcodeManualEntryPage_Return);
			//this.NavigationService.Navigate(barcodeManualEntryPage);
		}

		private void BarcodeManualEntryPage_Return(object sender, ReturnEventArgs<PageFunctionResult> e)
		{
			if (e.Result.PageNavigationDirectionEnum == YellowstonePathology.UI.Navigation.PageNavigationDirectionEnum.Next)
			{
				OnReturn(new ReturnEventArgs<PageFunctionResult>(e.Result));
			}
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			PageFunctionResult result = new PageFunctionResult(true, YellowstonePathology.UI.Navigation.PageNavigationDirectionEnum.Back, null);
			OnReturn(new ReturnEventArgs<PageFunctionResult>(result));
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
			Window.GetWindow(this).Close();
		}
	}
}
