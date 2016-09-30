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

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for PNHResultPage.xaml
	/// </summary>
	public partial class PNHResultPage : ResultControl, INotifyPropertyChanged 
        
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.PNH.PNHTestOrder m_PanelSetOrder;
		private YellowstonePathology.Business.Test.PNH.PNHResultCollection m_ResultCollection;
		private string m_PageHeaderText;


		public PNHResultPage(YellowstonePathology.Business.Test.PNH.PNHTestOrder testOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(testOrder, accessionOrder)
		{
			this.m_PanelSetOrder = testOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;

			this.m_PageHeaderText = "PNH, Highly Sensitive(FLAER) Results For: " + this.m_AccessionOrder.PatientDisplayName;
            this.m_ResultCollection = new YellowstonePathology.Business.Test.PNH.PNHResultCollection();

			InitializeComponent();

			DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Test.PNH.PNHTestOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public YellowstonePathology.Business.Test.PNH.PNHResultCollection ResultCollection
		{
			get { return this.m_ResultCollection; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			this.Next(this, new EventArgs());
		}

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToFinalize();
			if (methodResult.Success == true)
			{
				this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToUnfinalize();
			if (methodResult.Success == true)
			{
				this.m_PanelSetOrder.Unfinalize();
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToAccept();
			if (methodResult.Success == true)
			{
				this.m_PanelSetOrder.Accept();
				this.NotifyPropertyChanged("");
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToUnaccept();
			if (methodResult.Success == true)
			{
				this.m_PanelSetOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToSetResults();
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.PNH.PNHResult pnhResult = YellowstonePathology.Business.Test.PNH.PNHResultCollection.GetResult(this.m_PanelSetOrder, this.m_AccessionOrder);
				pnhResult.SetResults(this.m_PanelSetOrder);
				this.NotifyPropertyChanged(string.Empty);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(this.m_PanelSetOrder.ResultCode) == false)
			{
				YellowstonePathology.Business.Test.PNH.PNHWordDocument report = new YellowstonePathology.Business.Test.PNH.PNHWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
				report.Render();

				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
				string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
				YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
			}
			else
			{
				MessageBox.Show("The result must be set before the report can be viewed.");
			}
		}

		private void ComboBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ComboBoxResult.SelectedItem != null)
			{
				YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxResult.SelectedItem;
				this.m_PanelSetOrder.ResultCode = testResult.ResultCode;
			}
		}
	}
}
