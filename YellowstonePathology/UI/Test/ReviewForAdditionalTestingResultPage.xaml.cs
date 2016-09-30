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
	/// Interaction logic for ReviewForAdditionalTestingResultPage.xaml
	/// </summary>
	public partial class ReviewForAdditionalTestingResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTestOrder m_TestOrder;

        private string m_OrderedOnDescription;
        private string m_PageHeaderText;

		public ReviewForAdditionalTestingResultPage(YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTestOrder testOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(testOrder, accessionOrder)
		{            
			this.m_AccessionOrder = accessionOrder;			
			this.m_SystemIdentity = systemIdentity;

            this.m_TestOrder = testOrder;
			this.m_PageHeaderText = "Review For Additional Testing Results For: " + this.m_AccessionOrder.PatientDisplayName;

            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_TestOrder.OrderedOnId);
            if(orderTarget != null) this.m_OrderedOnDescription = orderTarget.GetDescription();

			InitializeComponent();

			DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

		public YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTestOrder TestOrder
        {
            get { return this.m_TestOrder; }
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

		private void HyperLinkSufficientTissue_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingResult.IsOkToSetResults(this.m_TestOrder);
			if (methodResult.Success == true)
			{
                YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingSufficientTissueResult result = new YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingSufficientTissueResult();
				result.SetResults(this.m_TestOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkInsufficientTissue_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingResult.IsOkToSetResults(this.m_TestOrder);
			if (methodResult.Success == true)
			{
                YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingInsufficientTissueResult result = new YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingInsufficientTissueResult();
				result.SetResults(this.m_TestOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingWordDocument report = new YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingWordDocument(this.m_AccessionOrder, this.m_TestOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_TestOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

        private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Rules.MethodResult methodResult =  this.m_TestOrder.IsOkToFinalize();
			if(methodResult.Success == true)
			{
				this.m_TestOrder.Finish(this.m_AccessionOrder);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
			}
        }

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_TestOrder.IsOkToUnfinalize();
			if (methodResult.Success == true)
			{
				this.m_TestOrder.Unfinalize();
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}		

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_TestOrder.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_TestOrder.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_TestOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_TestOrder.Unaccept();
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
	}
}
