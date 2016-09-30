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
	/// Interaction logic for KRASExon4MutationResultPage.xaml
	/// </summary>
	public partial class KRASExon4MutationResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;

		private YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationTestOrder m_KRASExon4MutationTestOrder;
		private string m_OrderedOnDescription;

		public KRASExon4MutationResultPage(YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationTestOrder krasExon4MutationTestOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(krasExon4MutationTestOrder, accessionOrder)

        {
			this.m_KRASExon4MutationTestOrder = krasExon4MutationTestOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;

			this.m_PageHeaderText = "KRAS Exon 4 Mutation Analysis Result For: " + this.m_AccessionOrder.PatientDisplayName;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_KRASExon4MutationTestOrder.OrderedOn, this.m_KRASExon4MutationTestOrder.OrderedOnId);
			this.m_OrderedOnDescription = specimenOrder.Description;

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

		public YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationTestOrder PanelSetOrder
		{
			get { return this.m_KRASExon4MutationTestOrder; }
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

		private void HyperLinkNotDetected_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationResult result = new YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationResult();
            result.SetResults(this.m_KRASExon4MutationTestOrder);
			this.NotifyPropertyChanged("PanelSetOrder");
		}

		private void HyperLinkDetected_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationResult result = new YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationResult();
            result.SetResults(this.m_KRASExon4MutationTestOrder);
			this.NotifyPropertyChanged("PanelSetOrder");
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.KRASExon4Mutation.KRASExon4MutationWordDocument report = new Business.Test.KRASExon4Mutation.KRASExon4MutationWordDocument(this.m_AccessionOrder, this.m_KRASExon4MutationTestOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_KRASExon4MutationTestOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_KRASExon4MutationTestOrder.Final == false)
			{
				this.m_KRASExon4MutationTestOrder.Finish(this.m_AccessionOrder);    
			}
			else
			{
				MessageBox.Show("This case cannot be finalized because it is already final.");
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_KRASExon4MutationTestOrder.Final == true)
			{
				this.m_KRASExon4MutationTestOrder.Unfinalize();   
			}
			else
			{
				MessageBox.Show("This case cannot be unfinalized because it is not final.");
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_KRASExon4MutationTestOrder.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_KRASExon4MutationTestOrder.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_KRASExon4MutationTestOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_KRASExon4MutationTestOrder.Unaccept();
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
