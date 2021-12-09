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
	public partial class APTIMASARSCoV2ResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private Business.User.SystemIdentity m_SystemIdentity;
		private Business.Test.AccessionOrder m_AccessionOrder;                
		private string m_PageHeaderText;        
        private Business.Test.APTIMASARSCoV2.APTIMASARSCoV2TestOrder m_SARSCoV2TestOrder;
		private UI.Navigation.PageNavigator m_PageNavigator;
        private Business.Test.APTIMASARSCoV2.APTIMASARSCoV2ResultCollection m_ResultCollection;

        public APTIMASARSCoV2ResultPage(YellowstonePathology.Business.Test.APTIMASARSCoV2.APTIMASARSCoV2TestOrder sarsCoV2TestOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,            
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) : base (sarsCoV2TestOrder, accessionOrder)
		{
			this.m_SARSCoV2TestOrder = sarsCoV2TestOrder;			
			this.m_AccessionOrder = accessionOrder;                        
			this.m_SystemIdentity = systemIdentity;
			this.m_PageNavigator = pageNavigator;

            this.m_ResultCollection = Business.Test.APTIMASARSCoV2.APTIMASARSCoV2ResultCollection.GetAllResults();
            this.m_PageHeaderText = "SARSCo-V2 Results For: " + this.m_AccessionOrder.PatientDisplayName + "  (" + this.m_SARSCoV2TestOrder.ReportNo + ")";          

            InitializeComponent();

			DataContext = this;
            Loaded += ResultPage_Loaded;
            Unloaded += ResultPage_Unloaded;
            
            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);            
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);            
        }

        private void ResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxResult.SelectionChanged += ComboBoxResult_SelectionChanged;
             
        }

        private void ResultPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ComboBoxResult.SelectionChanged -= ComboBoxResult_SelectionChanged;
             
        }

        public Business.Test.APTIMASARSCoV2.APTIMASARSCoV2ResultCollection ResultCollection
        {
            get { return this.m_ResultCollection; }
        }

        public Business.Test.APTIMASARSCoV2.APTIMASARSCoV2TestOrder PanelSetOrder
		{
			get { return this.m_SARSCoV2TestOrder; }
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
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_SARSCoV2TestOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                this.m_SARSCoV2TestOrder.Finish(this.m_AccessionOrder);

                Business.Client.Model.ClientGroupClientCollection clientGroupClientCollection = Business.Gateway.PhysicianClientGateway.GetClientGroupClientCollectionByClientGroupId("1");
                if (clientGroupClientCollection.ClientIdExists(this.m_AccessionOrder.ClientId) == true)
                {
                    if (this.m_SARSCoV2TestOrder.Result == "DETECTED")
                    {
                        string faxNumber = "4062378098";
                        if (m_SARSCoV2TestOrder.TestOrderReportDistributionCollection.FaxNumberExists(faxNumber) == false)
                        {
                            string testOrderReportDistributionId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                            YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution =
                                new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution(testOrderReportDistributionId, testOrderReportDistributionId, this.m_SARSCoV2TestOrder.ReportNo, 0, null, 558, "St. Vincent Healthcare",
                                    YellowstonePathology.Business.Client.Model.FaxPhysicianClientDistribution.FAX, faxNumber);
                            this.m_SARSCoV2TestOrder.TestOrderReportDistributionCollection.Add(testOrderReportDistribution);
                        }
                    }
                }                
            }
            else
            {
                MessageBox.Show(auditResult.Message);
            }
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_SARSCoV2TestOrder.IsOkToUnfinalize();
            if (methodResult.Success == true)
            {
                this.m_SARSCoV2TestOrder.Unfinalize();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }        

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_SARSCoV2TestOrder.IsOkToUnaccept();
            if (methodResult.Success == true)
            {
                this.m_SARSCoV2TestOrder.Unaccept();
            }
            else
            {            
                MessageBox.Show(methodResult.Message);
            }
        }

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_SARSCoV2TestOrder.IsOkToAccept();
            if (methodResult.Success == true)
            {                                
                this.m_SARSCoV2TestOrder.Accept();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }        

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Test.APTIMASARSCoV2.APTIMASARSCoV2WordDocument report = new YellowstonePathology.Business.Test.APTIMASARSCoV2.APTIMASARSCoV2WordDocument(this.m_AccessionOrder, this.m_SARSCoV2TestOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWord(report.SaveFileName);
        }        

        private void ComboBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }        
    }
}
