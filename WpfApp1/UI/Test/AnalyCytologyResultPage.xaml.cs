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
using System.ComponentModel;

namespace YellowstonePathology.UI.Test
{    
    public partial class AnalCytologyResultPage : ResultControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        public delegate void ShowPublishedDocumentEventHandler(object sender, EventArgs e);
        public event ShowPublishedDocumentEventHandler ShowPublishedDocument;

        private Business.User.SystemIdentity m_SystemIdentity;
        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.Test.AnalCytology.AnalCytologyTestOrder m_PanelSetOrder;
        private string m_PageHeaderText;
        private string m_OrderedOnDescription;

        private List<string> m_ScreeningImpressionList;
        private List<string> m_SpecimenAdequacyList;
        private List<string> m_OtherConditionList;

        public AnalCytologyResultPage(Business.Test.AnalCytology.AnalCytologyTestOrder testOrder,
            Business.Test.AccessionOrder accessionOrder,
            Business.User.SystemIdentity systemIdentity) : base(testOrder, accessionOrder)
        {
            this.m_PanelSetOrder = testOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_PageHeaderText = "Androgen Receptor By IHC For: " + this.m_AccessionOrder.PatientDisplayName;

            this.m_ScreeningImpressionList = new List<string>();
            this.m_ScreeningImpressionList.Add("No Impression.");
            this.m_ScreeningImpressionList.Add("Negative for intraepithelial lesion or malignancy.");
            this.m_ScreeningImpressionList.Add("Negative for intraepithelial lesion or malignancy.Reactive cellular changes.");
            this.m_ScreeningImpressionList.Add("Atypical squamous cells - uncertain significance(ASCUS).");
            this.m_ScreeningImpressionList.Add("Atypical squamous cells - cannot exclude high grade dysplasia(ASC - H).");
            this.m_ScreeningImpressionList.Add("Low grade squamous intraepithelial lesion(LSIL).");
            this.m_ScreeningImpressionList.Add("High grade squamous intraepithelial lesion(HSIL).");
            this.m_ScreeningImpressionList.Add("Atypical glandular cells.");
            this.m_ScreeningImpressionList.Add("Endocervical adenocarcinoma -in-situ.");
            this.m_ScreeningImpressionList.Add("Squamous cell carcinoma.");
            this.m_ScreeningImpressionList.Add("Adenocarcinoma.");
            this.m_ScreeningImpressionList.Add("Other malignant neoplasm.");

            this.m_SpecimenAdequacyList = new List<string>();
            this.m_SpecimenAdequacyList.Add("Satisfactory for evaluation, transformation zone component absent.");
            this.m_SpecimenAdequacyList.Add("Satisfactory for evaluation, transformation zone component present.");
            this.m_SpecimenAdequacyList.Add("Satisfactory for evaluation.");
            this.m_SpecimenAdequacyList.Add("Unsatisfactory for evaluation due to -");
            this.m_SpecimenAdequacyList.Add("No Evaluation.");

            this.m_OtherConditionList = new List<string>();
            this.m_OtherConditionList.Add("Atrophy.");
            this.m_OtherConditionList.Add("Bacteria morphologically consistent with Actinomyces species.");
            this.m_OtherConditionList.Add("Cellular changes consistent with Herpes simplex virus.");
            this.m_OtherConditionList.Add("Endometrial cells(in a woman 45 years of age or greater).");
            this.m_OtherConditionList.Add("Fungal organisms consistent with Candida species.");
            this.m_OtherConditionList.Add("Inflammatory cells.");
            this.m_OtherConditionList.Add("Shift in vaginal flora suggestive of bacterial vaginosis.");
            this.m_OtherConditionList.Add("Trichomonas vaginalis organisms.");                       

            Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
            Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
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

        public Business.Test.AnalCytology.AnalCytologyTestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public List<string> ScreeningImpressionList
        {
            get { return this.m_ScreeningImpressionList; }
            set { this.m_ScreeningImpressionList = value; }
        }

        public List<string> SpecimenAdequacyList
        {
            get { return this.m_SpecimenAdequacyList; }
            set { this.m_SpecimenAdequacyList = value; }
        }

        public List<string> OtherConditionList
        {
            get { return this.m_OtherConditionList; }
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

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {            
            Business.Test.AnalCytology.AnalCytologyWordDocument report = new YellowstonePathology.Business.Test.AnalCytology.AnalCytologyWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
            report.Render();
            Business.Document.CaseDocument.OpenWordDocumentWithWord(report.SaveFileName);            
        }

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                this.HandleFinalizeTestResult(finalizeTestResult);
            }
            else
            {
                MessageBox.Show(auditResult.Message);
            }
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnfinalize();
            if (result.Success == true)
            {
                this.m_PanelSetOrder.Unfinalize();
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            Business.Audit.Model.AuditResult result = this.m_PanelSetOrder.IsOkToAccept(this.m_AccessionOrder);
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
            Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnaccept();
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
