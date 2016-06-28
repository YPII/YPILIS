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
	/// Interaction logic for PanelSetOrderSelectionPage.xaml
	/// </summary>
	public partial class TrichomonasResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder m_ReportOrderTrichomonas;
		private YellowstonePathology.Business.Test.Trichomonas.TrichomonasResultCollection m_ResultCollection;

        private string m_PageHeaderText;

        public TrichomonasResultPage(YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder trichomonasTestOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(trichomonasTestOrder, accessionOrder)
		{            
			this.m_AccessionOrder = accessionOrder;			
			this.m_SystemIdentity = systemIdentity;

			this.m_ReportOrderTrichomonas = trichomonasTestOrder;            
            this.m_PageHeaderText = "Trichomonas Results For: " + this.m_AccessionOrder.PatientDisplayName;
            this.m_ResultCollection = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasResultCollection();

			InitializeComponent();

			DataContext = this;
            Loaded += TrichomonasResultPage_Loaded;
            Unloaded += TrichomonasResultPage_Unloaded;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
		}

        private void TrichomonasResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxResult.SelectionChanged += ComboBoxResult_SelectionChanged;             
        }

        private void TrichomonasResultPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxResult.SelectionChanged -= ComboBoxResult_SelectionChanged;             
        }

        public YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder ReportOrder
        {
            get { return this.m_ReportOrderTrichomonas; }
        }

		public YellowstonePathology.Business.Test.Trichomonas.TrichomonasResultCollection ResultCollection
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

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{			
			YellowstonePathology.Business.Test.Trichomonas.TrichomonasWordDocument report = new YellowstonePathology.Business.Test.Trichomonas.TrichomonasWordDocument(this.m_AccessionOrder, this.m_ReportOrderTrichomonas, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_ReportOrderTrichomonas.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

        private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Rules.MethodResult methodResult =  this.m_ReportOrderTrichomonas.IsOkToFinalize();
			if(methodResult.Success == true)
			{
                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

				this.m_ReportOrderTrichomonas.Finish(this.m_AccessionOrder);

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
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_ReportOrderTrichomonas.IsOkToUnfinalize();
			if (methodResult.Success == true)
			{
				this.m_ReportOrderTrichomonas.Unfinalize();
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_ReportOrderTrichomonas.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_ReportOrderTrichomonas.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_ReportOrderTrichomonas.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_ReportOrderTrichomonas.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.Next != null) this.Next(this, new EventArgs());
		}

		private void ComboBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ComboBoxResult.SelectedItem != null)
			{
				YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxResult.SelectedItem;
				this.m_ReportOrderTrichomonas.ResultCode = testResult.ResultCode;
			}
		}
	}
}
