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
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for AcidWashResultDialog.xaml
    /// </summary>
    public partial class AcidWashResultDialog : Window
    {
        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.Test.ThinPrepPap.PanelOrderAcidWash m_PanelOrderAcidWash;
        private string m_HeaderText;        

        public AcidWashResultDialog(string masterAccessionNo, string reportNo)
        {            
            this.m_AccessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
            Business.Test.ThinPrepPap.ThinPrepPapAcidWashPanel thinPrepPapAcidWashPanel = new Business.Test.ThinPrepPap.ThinPrepPapAcidWashPanel();
            this.m_PanelOrderAcidWash = (Business.Test.ThinPrepPap.PanelOrderAcidWash)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo).PanelOrderCollection.GetPanelByPanelId(thinPrepPapAcidWashPanel.PanelId);
            this.m_HeaderText = "Results for " + this.m_AccessionOrder.PatientDisplayName;            

            InitializeComponent();

            DataContext = this;
            this.Closing += AcidWashResultDialog_Closing;
        }

        private void AcidWashResultDialog_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this);
        }

        public string HeaderText
        {
            get { return this.m_HeaderText; }
        }

        public Business.Test.ThinPrepPap.PanelOrderAcidWash PanelOrderAcidWash
        {
            get { return this.m_PanelOrderAcidWash; }
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelOrderAcidWash.IsOkToAccept();
            if (result.Success == true)
            {
                this.m_PanelOrderAcidWash.AcceptResults();
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            this.m_PanelOrderAcidWash.UnacceptResults();
        }        

        
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
            this.Close();
        }

        private void HyperLinkPrintLabel_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPAP();
            YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(panelSetOrder.ReportNo);
            string dummyReportNo = (orderIdParser.ReportNoYear + 50).ToString() + "-" + orderIdParser.MasterAccessionNoNumber + "." + orderIdParser.ReportNoLetter;

            YellowstonePathology.UI.Login.CytologySlideLabelDocument cytologySlideLabelDocument = new Login.CytologySlideLabelDocument(dummyReportNo, this.m_AccessionOrder.PLastName, false);
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();

            System.Printing.PrintServer printServer = new System.Printing.LocalPrintServer();
            System.Printing.PrintQueue printQueue = printServer.GetPrintQueue(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.CytologySlideLabelPrinter);

            printDialog.PrintQueue = printQueue;            
            printDialog.PrintDocument(cytologySlideLabelDocument.DocumentPaginator, "Slide Labels");
        }

        private void HyperLinkQNS_Click(object sender, RoutedEventArgs e)
        {
            this.m_PanelOrderAcidWash.Comment = "QNS";
        }
    }
}
