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
	/// Interaction logic for InvasiveBreastPanelPage.xaml
	/// </summary>
	public partial class InvasiveBreastPanelPage : ResultControl, INotifyPropertyChanged
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		public delegate void OrderHER2byFISHEventHandler(object sender, EventArgs e);
		public event OrderHER2byFISHEventHandler OrderHER2byFISH;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		private YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel m_InvasiveBreastPanel;
		private YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelResult m_InvasiveBreastPanelResult;
		private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder m_PanelSetOrderHer2ByIsh;
		private YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTestOrder m_PanelSetOrderErPrSemiQuantitative;

        private string m_PageHeaderText;

		public InvasiveBreastPanelPage(YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(testOrder, accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;

			this.m_InvasiveBreastPanel = testOrder;
			this.m_InvasiveBreastPanel.SetStatus(this.m_AccessionOrder.PanelSetOrderCollection);

			this.m_InvasiveBreastPanelResult = new Business.Test.InvasiveBreastPanel.InvasiveBreastPanelResult(this.m_AccessionOrder);

            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest panelSetHer2ByIsh = new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
			this.m_PanelSetOrderHer2ByIsh = (YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetHer2ByIsh.PanelSetId);

            YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest erPrSemiQuantitativeTest = new YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTest();
			this.m_PanelSetOrderErPrSemiQuantitative = (YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(erPrSemiQuantitativeTest.PanelSetId);

			this.m_PageHeaderText = "Invasive Breast Panel for: " + this.m_InvasiveBreastPanel.ReportNo;

			InitializeComponent();

			this.DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
		}        

        public YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel InvasiveBreastPanel
		{
			get { return this.m_InvasiveBreastPanel; }
		}

		public YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelResult InvasiveBreastPanelResult
		{
			get { return this.m_InvasiveBreastPanelResult; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}		

		public string Her2Result
		{
			get { return this.m_PanelSetOrderHer2ByIsh.ToResultString(this.m_AccessionOrder); }
		}

		public string ErPrResult
		{
			get { return this.m_PanelSetOrderErPrSemiQuantitative.ToResultString(this.m_AccessionOrder); }
		}

		public string PageHeaderText
		{
            get { return this.m_PageHeaderText; }
		}		

		private void HyperLinkOrderFISH_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.Her2AmplificationByFish.Her2AmplificationByFishTest panelSet = new Business.Test.Her2AmplificationByFish.Her2AmplificationByFishTest();
            YellowstonePathology.Business.PanelSet.Model.PanelSetHer2AmplificationByFishRetired3 panelSet3 = new Business.PanelSet.Model.PanelSetHer2AmplificationByFishRetired3();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet.PanelSetId) == false)
			{
				if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet3.PanelSetId) == false)
				{
					this.OrderHER2byFISH(this, new EventArgs());
				}
			}
			else MessageBox.Show("HER2 Amplification by Fish has already been ordered.", "Order exists");
		}		

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{			
			YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelWordDocument invasiveBreastPanel = new Business.Test.InvasiveBreastPanel.InvasiveBreastPanelWordDocument(this.m_AccessionOrder, this.m_InvasiveBreastPanel, Business.Document.ReportSaveModeEnum.Draft);
			invasiveBreastPanel.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_InvasiveBreastPanel.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_InvasiveBreastPanel.Final == false)
			{
				this.m_InvasiveBreastPanel.Finish(this.m_AccessionOrder);				
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_InvasiveBreastPanel.Final == true)
			{
				this.m_InvasiveBreastPanel.Unfinalize();				
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{			
			YellowstonePathology.Business.Rules.MethodResult result = this.m_InvasiveBreastPanel.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_InvasiveBreastPanel.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}		
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_InvasiveBreastPanel.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_InvasiveBreastPanel.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.Next != null)
			{
				if(this.HandleHER2ByFISHRequired() == true) this.Next(this, new EventArgs());
			}
		}

		private bool HandleHER2ByFISHRequired()
		{
			bool result = true;
			if (this.m_InvasiveBreastPanelResult.IsHER2ByFISHRequired == true && this.m_InvasiveBreastPanelResult.HER2ByFISHHasBeenOrdered == false)
			{
				MessageBoxResult messageBoxResult = MessageBox.Show("HER2 by FISH is required and has not been ordered.  Are you sure you want to continue?", "HER2 by FISH required", MessageBoxButton.YesNo);
				if (messageBoxResult == MessageBoxResult.No) result = false;
			}
			return result;
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
