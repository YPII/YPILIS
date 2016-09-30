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

namespace YellowstonePathology.UI.Surgical
{
	/// <summary>
	/// Interaction logic for PublishedDocumentReview.xaml
	/// </summary>
	public partial class PublishedDocumentReview : UserControl
	{
		private PathologistUI m_PathologistUI;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private PageNavigationWindow m_PageNavigationWindow;

		public PublishedDocumentReview(PathologistUI pathologistUI, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_PathologistUI = pathologistUI;
			this.m_SystemIdentity = systemIdentity;

			InitializeComponent();
			this.DataContext = this.PanelSetOrder;
        }

        YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PathologistUI.PanelSetOrder; }
		}

        private void HyperLinkShowFinalizeDialog_Click(object sender, RoutedEventArgs e)
        {
            this.m_PageNavigationWindow = new PageNavigationWindow(this.m_SystemIdentity);
            PublishedDocumentFinalPage publishedDocumentFinalPage = new PublishedDocumentFinalPage(this.m_PathologistUI.PanelSetOrder, this.m_PathologistUI.AccessionOrder, this.m_SystemIdentity);
            publishedDocumentFinalPage.Close += new PublishedDocumentFinalPage.CloseEventHandler(PublishedDocumentFinalPage_Close);
            this.m_PageNavigationWindow.Show();
            this.m_PageNavigationWindow.PageNavigator.Navigate(publishedDocumentFinalPage);            
        }

        private void PublishedDocumentFinalPage_Close(object sender, EventArgs e)
        {
            this.m_PageNavigationWindow.Close();
        }
    }
}
