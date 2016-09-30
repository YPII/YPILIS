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
	public partial class EGFRResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_PageHeaderText;

        private YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder m_EGFRMutationAnalysisTestOrder;
        private string m_OrderedOnDescription;

        public EGFRResultPage(YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(egfrMutationAnalysisTestOrder, accessionOrder)
        {
            this.m_EGFRMutationAnalysisTestOrder = egfrMutationAnalysisTestOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_PageHeaderText = "EGFR Results For: " + this.m_AccessionOrder.PatientDisplayName;

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_EGFRMutationAnalysisTestOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_EGFRMutationAnalysisTestOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;
            if (aliquotOrder != null) this.m_OrderedOnDescription += ": " + aliquotOrder.Label;

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

        public YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder EGFRMutationAnalysisTestOrder
        {
            get { return this.m_EGFRMutationAnalysisTestOrder; }
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

        private void HyperLinkPositiveL858R_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            result.SetResult(this.m_EGFRMutationAnalysisTestOrder, "L858R");
        }

        private void HyperLinkPositiveExon19_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            result.SetResult(this.m_EGFRMutationAnalysisTestOrder, "exon 19 insertion/deletion");
        }

        private void HyperLinkPositiveE709X_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            result.SetResult(this.m_EGFRMutationAnalysisTestOrder, "E709X");
        }

        private void HyperLinkPositiveG719X_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            result.SetResult(this.m_EGFRMutationAnalysisTestOrder, "G719X");
        }

        private void HyperLinkPositiveS768I_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            result.SetResult(this.m_EGFRMutationAnalysisTestOrder, "S768I");
        }

        private void HyperLinkPositiveL861Q_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            result.SetResult(this.m_EGFRMutationAnalysisTestOrder, "L861Q");
        }

        private void HyperLinkResistantT790M_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResistanceResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResistanceResult();
            result.SetResult(this.m_EGFRMutationAnalysisTestOrder, "T790M");
        }

        private void HyperLinkResistantExon20_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResistanceResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResistanceResult();
            result.SetResult(this.m_EGFRMutationAnalysisTestOrder, "Resistant Exon 20");
        }

        private void HyperLinkUninterpretable_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisUninterpretableResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisUninterpretableResult();
            result.SetResult(this.m_EGFRMutationAnalysisTestOrder, null);
        }        

        private void HyperLinkNotDetected_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisNotDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisNotDetectedResult();
            result.SetResult(this.m_EGFRMutationAnalysisTestOrder, null);
        }

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisWordDocument report = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisWordDocument(this.m_AccessionOrder, this.m_EGFRMutationAnalysisTestOrder, Business.Document.ReportSaveModeEnum.Draft);
            report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_EGFRMutationAnalysisTestOrder.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Rules.MethodResult result = this.m_EGFRMutationAnalysisTestOrder.IsOkToFinalize();
            if (result.Success == true)
            {
                this.m_EGFRMutationAnalysisTestOrder.Finish(this.m_AccessionOrder);
            }
            else
            {
				MessageBox.Show(result.Message);
            }            
        }

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_EGFRMutationAnalysisTestOrder.IsOkToUnfinalize();
			if (result.Success == true)
            {
                this.m_EGFRMutationAnalysisTestOrder.Unfinalize();
            }
            else
            {
				MessageBox.Show(result.Message);
            } 
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_EGFRMutationAnalysisTestOrder.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_EGFRMutationAnalysisTestOrder.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_EGFRMutationAnalysisTestOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_EGFRMutationAnalysisTestOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

        private void HyperLinkCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {       
            if (this.Next != null) this.Next(this, new EventArgs());
        }        
	}
}
