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

namespace YellowstonePathology.UI.Flow
{
    /// <summary>
    /// Interaction logic for PageScanningWindow.xaml
    /// </summary>
    public partial class PageScanningWindow : Window
    {
        public PageScanningWindow(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            InitializeComponent();
            YellowstonePathology.UI.Login.FinalizeAccession.DocumentScanningPage documentScanningPage = new Login.FinalizeAccession.DocumentScanningPage(accessionOrder);
			documentScanningPage.Return += new Login.FinalizeAccession.DocumentScanningPage.ReturnEventHandler(DocumentScanningPage_Return);
            this.MainContent.Content = documentScanningPage;
        }

		private void DocumentScanningPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			Close();
		}
    }
}
