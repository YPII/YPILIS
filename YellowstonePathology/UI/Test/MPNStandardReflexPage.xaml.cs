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
	/// Interaction logic for MPNStandardResultPage.xaml
	/// </summary>
	public partial class MPNStandardReflexPage : ResultControl, INotifyPropertyChanged
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void FinishEventHandler(object sender, EventArgs e);
		public event FinishEventHandler Finish;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		public delegate void OrderTestEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.TestOrderInfoEventArgs eventArgs);
		public event OrderTestEventHandler OrderTest;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex m_PanelSetOrderMPNStandardReflex;
        private YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder m_PanelSetOrderJAK2V617F;
		private YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214TestOrder m_PanelSetOrderJAK2Exon1214;
        private string m_JAK2V617FResult;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		
		private string m_PageHeaderText;


		public MPNStandardReflexPage(YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(testOrder, accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;            
			this.m_SystemIdentity = systemIdentity;

			YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexTest panelSetMPNStandardReflex = new YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexTest();
			YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest panelSetJAK2V617F = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest();
			YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214Test panelSetJAK2Exon1214 = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214Test();

			this.m_PanelSetOrderMPNStandardReflex = (YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMPNStandardReflex.PanelSetId);
			this.m_PanelSetOrderJAK2V617F = (YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetJAK2V617F.PanelSetId);
			this.m_JAK2V617FResult = this.m_PanelSetOrderJAK2V617F.Result;

			this.m_PanelSetOrderJAK2Exon1214 = (YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetJAK2Exon1214.PanelSetId);
            this.m_SpecimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrderMPNStandardReflex.OrderedOnId);

			this.m_PageHeaderText =  this.m_PanelSetOrderMPNStandardReflex.PanelSetName + " for: " + this.m_AccessionOrder.PatientDisplayName;

			InitializeComponent();

			this.DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonBack);
            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonFinish);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }

		public YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex PanelSetOrderMPNStandardReflex
        {
            get { return this.m_PanelSetOrderMPNStandardReflex; }
        }

        public string JAK2V617FResult
        {
            get { return this.m_JAK2V617FResult; }
        }

		public YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214TestOrder PanelSetOrderJAK2Exon1214
        {
			get { return this.m_PanelSetOrderJAK2Exon1214; }
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}		

		private void HyperLinkOrderJak2Exon1214_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214Test panelSet = new YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214Test();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet.PanelSetId) == false)
			{
                YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrderMPNStandardReflex.OrderedOnId);
                YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(panelSet, orderTarget, false);                
                this.OrderTest(this, new CustomEventArgs.TestOrderInfoEventArgs(testOrderInfo));
			}
			else
			{
				MessageBox.Show("Jak2 Exon 12-14 has already been ordered.", "Order exists");
			}
		}

        private void HyperLinkSetResult_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexResult mpnStandardReflexResult = new Business.Test.MPNStandardReflex.MPNStandardReflexResult(this.m_AccessionOrder);
            mpnStandardReflexResult.SetResults(this.m_PanelSetOrderMPNStandardReflex);
        }

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{			
			YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexWordDocument report = new Business.Test.MPNStandardReflex.MPNStandardReflexWordDocument(this.m_AccessionOrder, this.m_PanelSetOrderMPNStandardReflex, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrderMPNStandardReflex.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_PanelSetOrderMPNStandardReflex.Final == false)
			{
				this.m_PanelSetOrderMPNStandardReflex.Finish(this.m_AccessionOrder);
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_PanelSetOrderMPNStandardReflex.Final == true)
			{
				this.m_PanelSetOrderMPNStandardReflex.Unfinalize();
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{			
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrderMPNStandardReflex.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrderMPNStandardReflex.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}		
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrderMPNStandardReflex.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrderMPNStandardReflex.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void ButtonFinish_Click(object sender, RoutedEventArgs e)
		{
			if (this.Finish != null) this.Finish(this, new EventArgs());
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
