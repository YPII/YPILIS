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
using System.Xml.Linq;

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for HPV1618ResultPage.xaml
	/// </summary>
	public partial class HPV1618ResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection m_Genotype16ResultCollection;
        private YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection m_Genotype18ResultCollection;
        private string m_PageHeaderText;

		private YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 m_PanelSetOrder;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;

		public HPV1618ResultPage(YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 panelSetOrderHPV1618,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) : base(panelSetOrderHPV1618, accessionOrder)
		{
			this.m_PanelSetOrder = panelSetOrderHPV1618;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
			this.m_PageNavigator = pageNavigator;

            this.m_Genotype16ResultCollection = YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection.GetGenotype16Results();
            this.m_Genotype18ResultCollection = YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection.GetGenotype18Results();

            this.m_PageHeaderText = "HPV Genotypes 16 and 18 Results For: " + this.m_AccessionOrder.PatientDisplayName;
			
			InitializeComponent();

			DataContext = this;

            this.Loaded += HPV1618ResultPage_Loaded;
            this.Unloaded += HPV1618ResultPage_Unloaded;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);

        }

        private void HPV1618ResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxGenotype16Result.SelectionChanged += ComboBoxGenotype16Result_SelectionChanged;
            this.ComboBoxGenotype18Result.SelectionChanged += ComboBoxGenotype18Result_SelectionChanged;
             
        }

        private void HPV1618ResultPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxGenotype16Result.SelectionChanged -= ComboBoxGenotype16Result_SelectionChanged;
            this.ComboBoxGenotype18Result.SelectionChanged -= ComboBoxGenotype18Result_SelectionChanged;
             
        }

        public YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

        public YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection Genotype16ResultCollection
        {
            get { return this.m_Genotype16ResultCollection; }
        }

        public YellowstonePathology.Business.Test.HPV1618.HPV1618ResultCollection Genotype18ResultCollection
        {
            get { return this.m_Genotype18ResultCollection; }
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
                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

                this.m_PanelSetOrder.Finish(this.m_AccessionOrder);

                if (this.m_AccessionOrder.PanelSetOrderCollection.WomensHealthProfileExists() == true)
                {
                    this.m_AccessionOrder.PanelSetOrderCollection.GetWomensHealthProfile().SetExptectedFinalTime(this.m_AccessionOrder);
                }
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

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.HPV1618.HPV1618WordDocument report = new Business.Test.HPV1618.HPV1618WordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

        private void ComboBoxGenotype16Result_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxGenotype16Result.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxGenotype16Result.SelectedItem;
                this.m_PanelSetOrder.HPV16ResultCode = testResult.ResultCode;
            }
        }

        private void ComboBoxGenotype18Result_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxGenotype18Result.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxGenotype18Result.SelectedItem;
                this.m_PanelSetOrder.HPV18ResultCode = testResult.ResultCode;
            }
        }
    }
}
