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
	/// Interaction logic for MPNExtendedReflexPage.xaml
	/// </summary>
	public partial class MPNExtendedReflexPage : ResultControl, INotifyPropertyChanged
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void FinishEventHandler(object sender, EventArgs e);
		public event FinishEventHandler Finish;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		public delegate void OrderTestEventHandler(object sender, CustomEventArgs.TestOrderInfoEventArgs panelSetReturnEventArgs);
		public event OrderTestEventHandler OrderTest;
		

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexResult m_MPNExtendedReflexResult;
		private string m_PageHeaderText;
		private string m_OrderedOnDescription;

		public MPNExtendedReflexPage(YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(testOrder, accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
			this.m_MPNExtendedReflexResult = new Business.Test.MPNExtendedReflex.MPNExtendedReflexResult(this.m_AccessionOrder);
			this.m_OrderedOnDescription = this.m_MPNExtendedReflexResult.SpecimenOrder.Description;

            this.m_PageHeaderText = this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex.PanelSetName + " for: " + this.m_AccessionOrder.PatientDisplayName;

			InitializeComponent();

			this.DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonBack);
            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonFinish);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex PanelSetOrder
		{
			get { return this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex; }
		}

		public YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexResult MPNExtendedReflexResult
		{
			get { return this.m_MPNExtendedReflexResult; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public string OrderedOnDescription
		{
			get { return this.m_OrderedOnDescription; }
		}
		
        private void HyperLinkOrderMLP_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Test.MPL.MPLTest panelSet = new Business.Test.MPL.MPLTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet.PanelSetId) == false)
            {
                YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex.OrderedOnId);
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(panelSet, orderTarget, false);                
                this.OrderTest(this, new CustomEventArgs.TestOrderInfoEventArgs(testOrderInfo));
				this.m_MPNExtendedReflexResult = new Business.Test.MPNExtendedReflex.MPNExtendedReflexResult(this.m_AccessionOrder);
                this.NotifyPropertyChanged("");
            }
            else
            {
                MessageBox.Show("MPL has already been ordered.", "Order exists");
            }
        }

		private void HyperLinkOrderCalreticulinMutationAnalysis_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest panelSet = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet.PanelSetId) == false)
			{
				YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex.OrderedOnId);
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(panelSet, orderTarget, false);                
                this.OrderTest(this, new CustomEventArgs.TestOrderInfoEventArgs(testOrderInfo));
				this.m_MPNExtendedReflexResult = new Business.Test.MPNExtendedReflex.MPNExtendedReflexResult(this.m_AccessionOrder);
                this.NotifyPropertyChanged("");
			}
			else
			{
				MessageBox.Show("Calreticulin Mutation Analysis has already been ordered.", "Order exists");
			}
		}

		private void HyperLinkSetResult_Click(object sender, RoutedEventArgs e)
		{
			this.m_MPNExtendedReflexResult.SetResults();
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexWordDocument report = new Business.Test.MPNExtendedReflex.MPNExtendedReflexWordDocument(this.m_AccessionOrder, this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
		{
            Business.Audit.Model.AuditResult auditResult = this.m_MPNExtendedReflexResult.IsOkToFinalize();
            if(auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
			{
				this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Finish(this.m_AccessionOrder);
			}
			else
			{
                MessageBox.Show(auditResult.Message);
            }
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Final == true)
			{
				this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Unfinalize();
			}
			else
			{
				MessageBox.Show("This case cannot be unfinalized because it is not final.");
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{			
			YellowstonePathology.Business.Rules.MethodResult result = this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}		
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_MPNExtendedReflexResult.PanelSetOrderMPNExtendedReflex.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void ButtonFinish_Click(object sender, RoutedEventArgs e)
		{
            if (this.m_MPNExtendedReflexResult.AuditCollection.ActionRequired == true)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(this.m_MPNExtendedReflexResult.AuditCollection.Message + " Are you sure you want to continue.", "Continue?", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    if (this.Finish != null) this.Finish(this, new EventArgs());
                }
            }
            else
            {
                if (this.Finish != null) this.Finish(this, new EventArgs());
            }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        
	}
}
