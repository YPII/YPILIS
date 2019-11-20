using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for AmendmentSuggestedPage.xaml
    /// </summary>
    public partial class AmendmentSuggestedPage : UserControl
    {
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private string m_PageHeaderText;
        private string m_AmdendmentDesiredText;

        public AmendmentSuggestedPage(Business.Test.PanelSetOrder testOrder, 
            Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = testOrder;
            this.m_PageHeaderText = "Amendment to surgical suggested For: " + this.m_AccessionOrder.PatientDisplayName;

            this.m_AmdendmentDesiredText = "If the diagnosis for this case has changed, an amendment to the surgical should be created.  " +
                "To add an amendment to the surgical click on the link in the left panel.";
            InitializeComponent();

            DataContext = this;
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public string AmendmentDesiredText
        {
            get { return this.m_AmdendmentDesiredText; }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        private void HyperLinkAddAmendment_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.Surgical.SurgicalTest panelSetSurgical = new YellowstonePathology.Business.Test.Surgical.SurgicalTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetSurgical.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrder surgicalPanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetSurgical.PanelSetId);
                YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(surgicalPanelSetOrder.ReportNo);
                if (amendmentCollection.HasAmendmentForReport(this.m_PanelSetOrder.ReportNo) == false)
                {
                    YellowstonePathology.Business.Amendment.Model.Amendment amendment = this.m_AccessionOrder.AddAmendment(surgicalPanelSetOrder.ReportNo);
                    amendment.TestResultAmendmentFill(surgicalPanelSetOrder.ReportNo, surgicalPanelSetOrder.AssignedToId, "Bone Marrow Summary:" + Environment.NewLine);
                    amendment.ReferenceReportNo = this.m_PanelSetOrder.ReportNo;
                    amendment.SystemGenerated = true;
                    amendment.RevisedDiagnosis = true;
                }
            }
            this.Next(this, new EventArgs());
        }
    }
}
