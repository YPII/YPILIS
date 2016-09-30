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
	/// Interaction logic for HPVResultPage.xaml
	/// </summary>
	public partial class HPVResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;                
		private string m_PageHeaderText;
        private YellowstonePathology.Business.Test.HPV.HPVResultCollection m_ResultCollection;
        private YellowstonePathology.Business.Test.HPV.HPVTestOrder m_HPVTestOrder;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private string m_AdditionalTestingComment;

        public HPVResultPage(YellowstonePathology.Business.Test.HPV.HPVTestOrder hpvTestOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,            
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) : base (hpvTestOrder, accessionOrder)
		{
			this.m_HPVTestOrder = hpvTestOrder;			
			this.m_AccessionOrder = accessionOrder;                        
			this.m_SystemIdentity = systemIdentity;
			this.m_PageNavigator = pageNavigator;

            this.m_ResultCollection = Business.Test.HPV.HPVResultCollection.GetAllResults();
			this.m_PageHeaderText = "HPV Results For: " + this.m_AccessionOrder.PatientDisplayName + "  (" + this.m_HPVTestOrder.ReportNo + ")";

            bool hpv1618HasBeenOrdered = this.m_AccessionOrder.PanelSetOrderCollection.Exists(62);            
            if (hpv1618HasBeenOrdered == true)
            {
                this.m_AdditionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.HPV1618HasBeenOrderedComment;
            }
            else
            {
                this.m_AdditionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.NoAdditionalTestingOrderedComment;
            }

            InitializeComponent();

			DataContext = this;
            Loaded += HPVResultPage_Loaded;
            Unloaded += HPVResultPage_Unloaded;
            
            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        private void HPVResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxResult.SelectionChanged += ComboBoxResult_SelectionChanged;
             
        }

        private void HPVResultPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxResult.SelectionChanged -= ComboBoxResult_SelectionChanged;
             
        }

        public string AdditionalTestingComment
        {
            get { return this.m_AdditionalTestingComment; }
        }           

		public YellowstonePathology.Business.Test.HPV.HPVTestOrder PanelSetOrder
		{
			get { return this.m_HPVTestOrder; }
		}

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.Business.Test.HPV.HPVResultCollection ResultCollection
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

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }		

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_HPVTestOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                this.m_HPVTestOrder.Finish(this.m_AccessionOrder);

                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

                if (this.m_AccessionOrder.PanelSetOrderCollection.WomensHealthProfileExists() == true)
                {
                    this.m_AccessionOrder.PanelSetOrderCollection.GetWomensHealthProfile().SetExptectedFinalTime(this.m_AccessionOrder);
                }
            }
            else
            {
                MessageBox.Show(auditResult.Message);
            }            
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_HPVTestOrder.IsOkToUnfinalize();
            if (methodResult.Success == true)
            {
                this.m_HPVTestOrder.Unfinalize();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }        

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_HPVTestOrder.IsOkToUnaccept();
            if (methodResult.Success == true)
            {
                this.m_HPVTestOrder.Unaccept();
            }
            else
            {            
                MessageBox.Show(methodResult.Message);
            }
        }

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_HPVTestOrder.IsOkToAccept();
            if (methodResult.Success == true)
            {                                
                this.m_HPVTestOrder.Accept();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }        

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Test.HPV.HPVWordDocument report = new Business.Test.HPV.HPVWordDocument(this.m_AccessionOrder, this.m_HPVTestOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_HPVTestOrder.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
        }        

        private void ComboBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxResult.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxResult.SelectedItem;
                this.m_HPVTestOrder.ResultCode = testResult.ResultCode;
            }
        }
    }
}
