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

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex m_PanelSetOrder;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		
		private string m_PageHeaderText;


		public MPNStandardReflexPage(YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(testOrder, accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;            
			this.m_SystemIdentity = systemIdentity;

			this.m_PanelSetOrder = testOrder;
            this.m_SpecimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);

			this.m_PageHeaderText =  this.m_PanelSetOrder.PanelSetName + " for: " + this.m_AccessionOrder.PatientDisplayName;

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

		public YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }        		

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}		

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{			
			YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexWordDocument report = new Business.Test.MPNStandardReflex.MPNStandardReflexWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(report.SaveFileName);
		}

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                this.HandleFinalizeTestResult(finalizeTestResult);
                if (this.m_PanelSetOrder.Accepted == false)
                {
                    this.m_PanelSetOrder.Accept();
                }
            }
            else
            {
                MessageBox.Show(auditResult.Message);
            }
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_PanelSetOrder.Final == true)
			{
				this.m_PanelSetOrder.Unfinalize();
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{			
			YellowstonePathology.Business.Audit.Model.AuditResult result = this.m_PanelSetOrder.IsOkToAccept(this.m_AccessionOrder);
            if (result.Status == Business.Audit.Model.AuditStatusEnum.OK)
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

        private void HyperLinkPreviousResults_Click(object sender, RoutedEventArgs e)
        {
            UI.Test.PreviousResultDialog dlg = new UI.Test.PreviousResultDialog(this.m_PanelSetOrder, this.m_AccessionOrder);
            dlg.ShowDialog();
        }
    }
}
