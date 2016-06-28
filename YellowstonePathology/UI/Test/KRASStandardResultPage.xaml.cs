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
	/// Interaction logic for KRASResultPage.xaml
	/// </summary>
	public partial class KRASStandardResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;
		public delegate void OrderBRAFEventHandler(object sender, EventArgs e);
		public event OrderBRAFEventHandler OrderBRAF;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private string m_PageHeaderText;
		private string m_OrderedOnDescription;
		private YellowstonePathology.Business.Test.KRASStandard.KRASStandardResultCollection m_KRASStandardResultCollection;
		private YellowstonePathology.Business.Test.KRASStandard.KRASStandardMutationCollection m_KRASStandardMutationCollection;
		private YellowstonePathology.Business.Test.KRASStandard.KRASStandardResultSelectionCollection m_KRASStandardResultSelectionCollection;
		private YellowstonePathology.Business.Test.IndicationCollection m_IndicationCollection;
		private YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder m_PanelSetOrder;

		public KRASStandardResultPage(YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder panelSetOrderKRASStandard,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) : base(panelSetOrderKRASStandard, accessionOrder)
		{
			this.m_PanelSetOrder = panelSetOrderKRASStandard;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
			this.m_PageNavigator = pageNavigator;

            this.m_PageHeaderText = "KRAS Standard Mutation Analysis Results For: " + this.m_AccessionOrder.PatientDisplayName + " (" + this.m_PanelSetOrder.ReportNo + ")";
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetByAliquotOrderId(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description + ": " + aliquotOrder.Label;

			this.m_KRASStandardResultCollection = YellowstonePathology.Business.Test.KRASStandard.KRASStandardResultCollection.GetAll();
            this.m_IndicationCollection = YellowstonePathology.Business.Test.IndicationCollection.GetAll();
            this.m_KRASStandardMutationCollection = new YellowstonePathology.Business.Test.KRASStandard.KRASStandardMutationCollection();
            this.m_KRASStandardResultSelectionCollection = new YellowstonePathology.Business.Test.KRASStandard.KRASStandardResultSelectionCollection();

			InitializeComponent();

			DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Test.IndicationCollection IndicationCollection
        {
            get { return this.m_IndicationCollection; }
        }

		public YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
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

		public string OrderedOnDescription
		{
			get { return this.m_OrderedOnDescription; }
		}

		public YellowstonePathology.Business.Test.KRASStandard.KRASStandardResultCollection KRASStandardResultCollection
		{
			get { return this.m_KRASStandardResultCollection; }
		}

		public YellowstonePathology.Business.Test.KRASStandard.KRASStandardResultSelectionCollection KRASStandardResultSelectionCollection
		{
			get { return this.m_KRASStandardResultSelectionCollection; }
		}

		public YellowstonePathology.Business.Test.KRASStandard.KRASStandardMutationCollection KRASStandardMutationCollection
		{
			get { return this.m_KRASStandardMutationCollection; }
		}

		public bool IsBRAFV600EKOrdered
		{
			get
			{
				bool result = false;
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
				{
					result = true;
				}
				return result;
			}
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			this.Next(this, new EventArgs());
		}		

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrder.IsOkToFinalize(this.m_AccessionOrder);
			if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
			{
                this.m_PanelSetOrder.Finish(this.m_AccessionOrder);

                YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest();
				if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasStandardReflexTest.PanelSetId) == true)
				{
					YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder krasStandardReflexTestOrder = (YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasStandardReflexTest.PanelSetId);
					krasStandardReflexTestOrder.UpdateFromChildren(this.m_AccessionOrder, this.m_PanelSetOrder);
				}
			}
			else
			{
				MessageBox.Show(auditResult.Message);
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
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToSetResult();
			if (methodResult.Success == true)
			{
				if (this.ComboBoxResult.SelectedItem != null)
				{
					string resultString = this.ComboBoxResult.SelectedItem.ToString();
					string resultDescription = null;
					if (this.ComboBoxResultDescription.SelectedItem != null) resultDescription = this.ComboBoxResultDescription.SelectedItem.ToString();

					YellowstonePathology.Business.Test.KRASStandard.KRASStandardResultCollection resultCollection = YellowstonePathology.Business.Test.KRASStandard.KRASStandardResultCollection.GetAll();
					if (resultCollection.IsValid(resultString, resultDescription) == true)
					{
						YellowstonePathology.Business.Test.KRASStandard.KRASStandardResult result = resultCollection.GetResult(resultString, resultDescription);
						result.SetResults(this.m_PanelSetOrder);
					}
					else
					{
						MessageBox.Show("The Result and the Result Description do not match.");
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
			if (this.m_PanelSetOrder.PanelOrderCollection.GetUnacceptedPanelCount() == 0)
			{
				YellowstonePathology.Business.Test.KRASStandard.KRASStandardWordDocument report = new YellowstonePathology.Business.Test.KRASStandard.KRASStandardWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
				report.Render();

				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
				string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
				YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
			}
			else
			{
				MessageBox.Show("The results must be accepted before the document can be viewed.", "Accept then view");
			}
		}

		private void HyperLinkProvider_Click(object sender, RoutedEventArgs e)
		{
            MessageBox.Show("Not Implemented");
            YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath = new Login.FinalizeAccession.ProviderDistributionPath(this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            providerDistributionPath.Back += new Login.FinalizeAccession.ProviderDistributionPath.BackEventHandler(ProviderDistributionPath_Back);
            providerDistributionPath.Next += new Login.FinalizeAccession.ProviderDistributionPath.NextEventHandler(ProviderDistributionPath_Next);
            providerDistributionPath.Start();
		}

        private void ProviderDistributionPath_Next(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }

        private void ProviderDistributionPath_Back(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }	

		private void HyperLinkOrderBRAFV600EK_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasStandardReflexTest.PanelSetId) == true)
			{
				string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasStandardReflexTest.PanelSetId).ReportNo;
				YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexResult krasStandardReflexResult = YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexResultFactory.GetResult(reportNo, this.m_AccessionOrder);
				YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexResult.IsOkToOrderBRAF(krasStandardReflexResult);
				if (methodResult.Success == true)
				{
					this.OrderBRAF(this, new EventArgs());
				}
				else
				{
					MessageBox.Show(methodResult.Message);
				}
			}
			else
			{
				MessageBox.Show("A BRAF V600E/K may only be ordered from here when a KRAS Standard Reflex has been ordered.");
			}
		}
	}
}
