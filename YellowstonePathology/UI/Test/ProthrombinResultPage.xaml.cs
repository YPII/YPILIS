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
	/// Interaction logic for ProthrombinResultPage.xaml
	/// </summary>
	public partial class ProthrombinResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private string m_PageHeaderText;
		private string m_OrderedOnDescription;
		private YellowstonePathology.Business.Test.Prothrombin.ProthrombinResultCollection m_ResultCollection;
		private YellowstonePathology.Business.Test.Prothrombin.ProthrombinTestOrder m_PanelSetOrder;

		public ProthrombinResultPage(YellowstonePathology.Business.Test.Prothrombin.ProthrombinTestOrder testOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) : base(testOrder, accessionOrder)
		{
			this.m_PanelSetOrder = testOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
			this.m_PageNavigator = pageNavigator;

			this.m_PageHeaderText = "Prothrombin 20210A Mutation Analysis (Factor II) Results For: " + this.m_AccessionOrder.PatientDisplayName;
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			this.m_OrderedOnDescription = specimenOrder.Description;
			this.m_ResultCollection = new YellowstonePathology.Business.Test.Prothrombin.ProthrombinResultCollection();

			InitializeComponent();

			DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Test.Prothrombin.ProthrombinTestOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public YellowstonePathology.Business.Test.Prothrombin.ProthrombinResultCollection ResultCollection
		{
			get { return this.m_ResultCollection; }
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

		public string OrderedOnDescription
		{
			get { return this.m_OrderedOnDescription; }
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

		private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToSetResults();
			if (methodResult.Success == true)
			{
				if (this.ComboBoxResult.SelectedItem != null)
				{
					YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxResult.SelectedItem;
					if (string.IsNullOrEmpty(testResult.ResultCode) == false)
					{
						YellowstonePathology.Business.Test.Prothrombin.ProthrombinResult result = (YellowstonePathology.Business.Test.Prothrombin.ProthrombinResult)testResult;
						result.SetResults(this.m_PanelSetOrder);
					}
				}
				else
				{
					MessageBox.Show("A result must be Selected before results can be set.");
				}
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

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.Prothrombin.ProthrombinWordDocument report = new YellowstonePathology.Business.Test.Prothrombin.ProthrombinWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

        private void HyperLinkProvider_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented");
            //YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath = new Login.FinalizeAccession.ProviderDistributionPath(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, this.m_ObjectTracker, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            //providerDistributionPath.Back += new Login.FinalizeAccession.ProviderDistributionPath.BackEventHandler(ProviderDistributionPath_Back);
            //providerDistributionPath.Next += new Login.FinalizeAccession.ProviderDistributionPath.NextEventHandler(ProviderDistributionPath_Next);
            //providerDistributionPath.Start(this.m_SystemIdentity);
        }

        private void ProviderDistributionPath_Next(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }

        private void ProviderDistributionPath_Back(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
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
