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
	/// Interaction logic for TCellClonalityByPCRREsultPage.xaml
	/// </summary>
	public partial class TCellRecepterBetaGeneRearrangementResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_PageHeaderText;

		private YellowstonePathology.Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementTestOrder m_PanelSetOrder;
        private string m_OrderedOnDescription;        

		public TCellRecepterBetaGeneRearrangementResultPage(YellowstonePathology.Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementTestOrder tCellRecepterBetaGeneRearrangementTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(tCellRecepterBetaGeneRearrangementTestOrder, accessionOrder)
		{
			this.m_PanelSetOrder = tCellRecepterBetaGeneRearrangementTestOrder;
			this.m_AccessionOrder = accessionOrder;			
			this.m_SystemIdentity = systemIdentity;

			this.m_PageHeaderText = "T-Cell Recepter Beta Gene Rearrangement Results For: " + this.m_AccessionOrder.PatientDisplayName;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
			YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;
            if(aliquotOrder != null) this.m_OrderedOnDescription += ": " + aliquotOrder.Label;

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

		public YellowstonePathology.Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementTestOrder PanelSetOrder
        {
			get { return this.m_PanelSetOrder; }
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

        private void HyperLinkPositive_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This result has not been implemented.");
        }

        private void HyperLinkNegative_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementNegativeResult result = new Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementNegativeResult();
			result.SetResults(this.m_PanelSetOrder);
			this.NotifyPropertyChanged("PanelSetOrder");
        }

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementWordDocument report = new Business.Test.TCellRecepterBetaGeneRearrangement.TCellRecepterBetaGeneRearrangementWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_PanelSetOrder.Final == false)
			{
				this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
			}
			else
			{
				MessageBox.Show("This case cannot be finalized because it is already final.");
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_PanelSetOrder.Final == true)
			{
				this.m_PanelSetOrder.Unfinalize();
			}
			else
			{
				MessageBox.Show("This case cannot be unfinalized because it is not final.");
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{			
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrder.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}		
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrder.Unaccept();
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
