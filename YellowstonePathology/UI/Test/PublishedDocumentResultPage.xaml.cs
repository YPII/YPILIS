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

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for PublishedDocumentResultPage.xaml
	/// </summary>
	public partial class PublishedDocumentResultPage : UserControl 
	{
		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

		private string m_PageHeaderText;

		public PublishedDocumentResultPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
		{
			this.m_PanelSetOrder = panelSetOrder;

			this.m_PageHeaderText = "Report For: " + accessionOrder.PatientDisplayName;

			InitializeComponent();

			DataContext = this;
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.Next != null) this.Next(this, new EventArgs());
		}
	}
}
