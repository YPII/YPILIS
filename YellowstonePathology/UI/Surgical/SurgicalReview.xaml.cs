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

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for SurgicalReview.xaml
    /// </summary>
    public partial class SurgicalReview : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private PathologistUI m_PathologistUI;
        private string m_CancerCaseSummaryVisibility;
        private YellowstonePathology.UI.TypingShortcutUserControl m_TypingShortcutUserControl;
        private YellowstonePathology.Business.View.BillingSpecimenViewCollection m_BillingSpecimenViewCollection;

        public SurgicalReview(YellowstonePathology.UI.TypingShortcutUserControl typingShortcutUserControl, PathologistUI pathologistUI)
        {
            this.m_TypingShortcutUserControl = typingShortcutUserControl;
            this.m_PathologistUI = pathologistUI;

            this.m_BillingSpecimenViewCollection = new Business.View.BillingSpecimenViewCollection();
            this.RefreshBillingSpecimenViewCollection();

            this.Loaded += SurgicalReview_Loaded;
            this.PreviewLostKeyboardFocus += SurgicalReview_PreviewLostKeyboardFocus;

            this.KeyDown += SurgicalReview_KeyDown;
            InitializeComponent();
            this.DataContext = this;
        }

        private void SurgicalReview_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.G)
            {
                this.TextBoxGross.Focus();
                //this.TextBoxGross.SelectionStart = 0;
                this.TextBoxGross.SelectionLength = 0;
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.K)
            {
                this.textBoxComment.Focus();
                //this.textBoxComment.SelectionStart = 0;
                this.textBoxComment.SelectionLength = 0;
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.M)
            {
                this.TextBoxMicroscopic.Focus();
                //this.TextBoxMicroscopic.SelectionStart = 0;
                this.TextBoxMicroscopic.SelectionLength = 0;
            }
            

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D1)
            {                
                if (this.ItemsControlSpecimen.Items.Count >= 1) SetFocusOnDiagnosis(0);                    
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D2)
            {
                if (this.ItemsControlSpecimen.Items.Count >= 2) SetFocusOnDiagnosis(1);
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D3)
            {
                if (this.ItemsControlSpecimen.Items.Count >= 3) SetFocusOnDiagnosis(2);
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D4)
            {
                if (this.ItemsControlSpecimen.Items.Count >= 4) SetFocusOnDiagnosis(3);
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D5)
            {
                if (this.ItemsControlSpecimen.Items.Count >= 5) SetFocusOnDiagnosis(4);
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D6)
            {
                if (this.ItemsControlSpecimen.Items.Count >= 6) SetFocusOnDiagnosis(5);
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D7)
            {
                if (this.ItemsControlSpecimen.Items.Count >= 7) SetFocusOnDiagnosis(6);
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D8)
            {
                if (this.ItemsControlSpecimen.Items.Count >= 8) SetFocusOnDiagnosis(7);
            }

            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.D9)
            {
                if (this.ItemsControlSpecimen.Items.Count >= 9) SetFocusOnDiagnosis(8);
            }
        }
        
        private void SetFocusOnDiagnosis(int itemIndex)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {                                
                UIElement uiElement = (UIElement)this.ItemsControlSpecimen.ItemContainerGenerator.ContainerFromItem(this.ItemsControlSpecimen.Items[itemIndex]);
                IEnumerable<TextBox> textBoxList = FindVisualChildren<TextBox>(uiElement);
                foreach (TextBox textBox in textBoxList)
                {
                    if (textBox.Name == "TextBoxDiagnosis")
                    {
                        textBox.Focus();
                        textBox.SelectionLength = 0;
                        textBox.BringIntoView();
                        break;
                    }
                }                
            }
            ));
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null) return childOfChild;
                }
            }
            return null;
        }

        private void SurgicalReview_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_PathologistUI.RunWorkspaceEnableRules();
            this.m_PathologistUI.RunPathologistEnableRules();
            //this.SetFocusOnDiagnosis();
        }

       
        public void SetFocusOnDiagnosis()
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                for (int i = 0; i < this.ItemsControlSpecimen.Items.Count; i++)
                {
                    IEnumerable<TextBox> textBoxList = FindVisualChildren<TextBox>(this.ItemsControlSpecimen);
                    foreach (TextBox textBox in textBoxList)
                    {
                        if (textBox.Name == "TextBoxDiagnosis")
                        {
                            bool tag = (bool)textBox.Tag;
                            if (tag == true)
                            {
                                textBox.Focus();
                                textBox.SelectionLength = 0;
                                break;
                            }
                        }
                    }
                }
            }
            ));
        }       

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }

        private void SurgicalReview_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            MainWindow.UpdateFocusedBindingSource(this);
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public PathologistUI PathologistUI
        {
            get { return this.m_PathologistUI; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_PathologistUI.AccessionOrder; }
        }

        public YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder PanelSetOrderSurgical
        {
            get { return (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_PathologistUI.PanelSetOrder; }
        }

        public YellowstonePathology.Business.Amendment.Model.AmendmentCollection AmendmentCollection
        {
            get
            {
                return this.m_PathologistUI.AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.PanelSetOrderSurgical.ReportNo);
            }
        }

        public YellowstonePathology.Business.Common.FieldEnabler FieldEnabler
        {
            get { return this.m_PathologistUI.FieldEnabler; }
        }

        public YellowstonePathology.Business.User.SystemUserCollection AmendmentUsers
        {
            get { return this.m_PathologistUI.AmendmentUsers; }
        }

        public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
        {
            get { return this.m_PathologistUI.PathologistUsers; }
        }

        private void Any_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            e.Handled = true;
        }

        private void HyperLinkFS_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult = ((Hyperlink)sender).Tag as YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult;
            Test.FrozenFinalDialog frozenFinalDialog = new Test.FrozenFinalDialog(this.AccessionOrder, intraoperativeConsultationResult, this.m_TypingShortcutUserControl);
            frozenFinalDialog.ShowDialog();
        }

        private void HyperlinkAcceptPeerReview_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink ctrl = (Hyperlink)sender;
            YellowstonePathology.Business.Test.PanelOrder panelOrder = (YellowstonePathology.Business.Test.PanelOrder)ctrl.Tag;

            if (panelOrder.Accepted == true)
            {
                panelOrder.Accepted = false;
                panelOrder.AcceptedDate = null;
                panelOrder.AcceptedTime = null;
                panelOrder.AcceptedById = 0;
            }
            else
            {
                panelOrder.Accepted = true;
                panelOrder.AcceptedDate = DateTime.Today;
                panelOrder.AcceptedTime = DateTime.Now;
                panelOrder.AcceptedById = Business.User.SystemIdentity.Instance.User.UserId;
            }
        }

        private void ButtonAddCptCode_Click(object sender, RoutedEventArgs args)
        {
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen)((Button)sender).Tag;
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderById(surgicalSpecimen.SpecimenOrderId);
            YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = this.PanelSetOrderSurgical.PanelSetOrderCPTCodeCollection.GetNextItem(this.PanelSetOrderSurgical.ReportNo);
            panelSetOrderCPTCode.Quantity = 1;
            panelSetOrderCPTCode.CPTCode = null;
            panelSetOrderCPTCode.Modifier = null;
            panelSetOrderCPTCode.CodeableDescription = "Specimen " + specimenOrder.SpecimenNumber + ": " + this.PanelSetOrderSurgical.PanelSetName;
            panelSetOrderCPTCode.CodeableType = "Surgical Diagnosis";
            panelSetOrderCPTCode.EntryType = Business.Billing.Model.PanelSetOrderCPTCodeEntryType.ManualEntry;
            panelSetOrderCPTCode.SpecimenOrderId = specimenOrder.SpecimenOrderId;
            panelSetOrderCPTCode.ClientId = this.AccessionOrder.ClientId;
            panelSetOrderCPTCode.MedicalRecord = this.AccessionOrder.SvhMedicalRecord;
            this.PanelSetOrderSurgical.PanelSetOrderCPTCodeCollection.Add(panelSetOrderCPTCode);
            this.RefreshBillingSpecimenViewCollection();
        }

        private void ButtonDeleteCptCode_Click(object sender, RoutedEventArgs args)
        {
            YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode = (YellowstonePathology.Business.Test.PanelSetOrderCPTCode)((Button)sender).Tag;
            this.PanelSetOrderSurgical.PanelSetOrderCPTCodeCollection.Remove(panelSetOrderCPTCode);
            this.RefreshBillingSpecimenViewCollection();
        }

        private void ButtonAddIcd9Code_Click(object sender, RoutedEventArgs args)
        {
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen)((Button)sender).Tag;
            YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd9BillingCode = this.AccessionOrder.ICD9BillingCodeCollection.GetNextItem(this.PanelSetOrderSurgical.ReportNo,
                this.AccessionOrder.MasterAccessionNo, surgicalSpecimen.SpecimenOrderId, null, 1);
            icd9BillingCode.SurgicalSpecimenId = surgicalSpecimen.SurgicalSpecimenId;
            this.AccessionOrder.ICD9BillingCodeCollection.Add(icd9BillingCode);
            this.RefreshBillingSpecimenViewCollection();
        }

        private void ButtonDeleteIcd9Code_Click(object sender, RoutedEventArgs args)
        {
            string icd9BillingId = ((YellowstonePathology.Business.Billing.Model.ICD9BillingCode)((Button)sender).Tag).Icd9BillingId;
            YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd9BillingCode = this.AccessionOrder.ICD9BillingCodeCollection.GetCurrent(icd9BillingId);
            this.AccessionOrder.ICD9BillingCodeCollection.Remove(icd9BillingCode);
            this.RefreshBillingSpecimenViewCollection();
        }

        private void ButtonCorrelation_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult = ((Button)sender).Tag as YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult;
            ImmediateExamCorrelationDialog immediateExamCorrelationDialog = new ImmediateExamCorrelationDialog(intraoperativeConsultationResult);
            immediateExamCorrelationDialog.ShowDialog();
        }

        private void ButtonSignature_Click(object sender, RoutedEventArgs args)
        {
            if (this.PanelSetOrderSurgical.Final == false)
            {
                this.Signout();
            }
            else
            {
                if (this.PanelSetOrderSurgical.TestOrderReportDistributionCollection.HasDistributedItems() == false)
                {
                    this.PanelSetOrderSurgical.Unfinalize();
                }
                this.PanelSetOrderSurgical.TestOrderReportDistributionCollection.UnscheduleDistributions();
            }

            this.m_PathologistUI.RunWorkspaceEnableRules();
            this.m_PathologistUI.RunPathologistEnableRules();
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonSignAmendment_Click(object sender, RoutedEventArgs args)
        {
            Button button = (Button)sender;
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = (YellowstonePathology.Business.Amendment.Model.Amendment)button.Tag;

            if (amendment.Final == false)
            {                
                YellowstonePathology.Business.Test.OkToFinalizeResult okToFinalizeResult = amendment.IsOkToFinalize(this.m_PathologistUI.AccessionOrder);
                if (okToFinalizeResult.OK == true)
                {
                    bool canFinal = true;
                    if (okToFinalizeResult.ShowWarningMessage == true)
                    {
                        MessageBoxResult messageBoxResult = MessageBox.Show(okToFinalizeResult.Message, "Issue with the amendment", MessageBoxButton.YesNo, MessageBoxImage.Exclamation, MessageBoxResult.No);
                        if (messageBoxResult == MessageBoxResult.No)
                        {
                            canFinal = false;
                        }
                    }

                    if (canFinal == true)
                    {
                        this.HandleBigSkyDermWarning();
                        amendment.Finish();
                    }
                }
                else
                {
                    MessageBox.Show(okToFinalizeResult.Message);
                }
            }
            else
            {
                YellowstonePathology.Business.Test.OkToUnfinalizeResult okToUnfinalResult = amendment.IsOkToUnfinalize(this.m_PathologistUI.PanelSetOrder);
                if (okToUnfinalResult.OK == true)
                {
                    if (okToUnfinalResult.ShowWarningMessage == true)
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(okToUnfinalResult.Message, "Warning", MessageBoxButton.YesNo);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            amendment.Unfinalize();
                        }
                    }
                    else
                    {
                        amendment.Unfinalize();
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show(okToUnfinalResult.Message);
                }
            }

            YellowstonePathology.Business.Rules.Surgical.SetAmendmentSignatureText setAmendmentSignatureText = new Business.Rules.Surgical.SetAmendmentSignatureText();
            setAmendmentSignatureText.Execute(this.m_PathologistUI.AccessionOrder, this.m_PathologistUI.PanelSetOrder, amendment);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void HandleBigSkyDermWarning()
        {
            if(this.m_PathologistUI.AccessionOrder.ClientId == 1203)
            {
                Business.Logging.EmailExceptionHandler.HandleException("A Big Sky Dermatology Amendment was just signed: " + this.m_PathologistUI.AccessionOrder.MasterAccessionNo);
                MessageBox.Show("Warning!!! Big Sky Dermatology has a problem with their system.  When amendments are sent across the interface the providers are not notified of the new report.  We are working with E-Clinical Works to resolve this issue.  If the changes in this amendment will affect how a patient is going to be treated, it is best that you contact the provider and communicate the changes directly with them.");                
                Business.Test.PanelSetOrder psoSurgical = this.m_PathologistUI.AccessionOrder.PanelSetOrderCollection.GetSurgical();
                Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = psoSurgical.TestOrderReportDistributionCollection.GetFirstEclinical();
                if (testOrderReportDistribution != null)
                {
                    testOrderReportDistribution.DistributionType = "Fax";
                }
            }
        }

        private void ButtonAddAmendment_Click(object sender, RoutedEventArgs args)
        {
            this.m_PathologistUI.AddAmentment();
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonDeleteAmendment_Click(object sender, RoutedEventArgs args)
        {
            Button button = (Button)sender;
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = (YellowstonePathology.Business.Amendment.Model.Amendment)button.Tag;
            this.m_PathologistUI.DeleteAmendment(amendment);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonSetAncillaryStudiesCommentsToGood_Click(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(this.PanelSetOrderSurgical.ReportNo) == false)
            {
                this.PanelSetOrderSurgical.SetProcedureCommentsToGood();
            }
        }

        private void ButtonReassignCase_Click(object sender, RoutedEventArgs e)
        {
            if (this.PanelSetOrderSurgical != null)
            {
                YellowstonePathology.Business.Rules.PanelSetOrder.ReassignCase reassignCase = new YellowstonePathology.Business.Rules.PanelSetOrder.ReassignCase();
                YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
                if (this.PanelSetOrderSurgical.Final == false)
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("An amendment will be created as a result of reasigning this case.  Are you sure you want to proceed with reasignment?", "Proceed?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        reassignCase.Execute(executionStatus, this.m_PathologistUI.AccessionOrder, this.PanelSetOrderSurgical, true, Business.User.SystemIdentity.Instance);
                    }
                }
                else
                {
                    reassignCase.Execute(executionStatus, this.m_PathologistUI.AccessionOrder, this.PanelSetOrderSurgical, false, Business.User.SystemIdentity.Instance);
                    if (executionStatus.Halted == false)
                    {
                        MessageBox.Show("The case has been reassigned");
                    }
                }
            }
        }

        private void ButtonPapAccession_Click(object sender, RoutedEventArgs e)
        {
            Common.HistroicalAccessionDialog dlg = new YellowstonePathology.UI.Common.HistroicalAccessionDialog(this.PanelSetOrderSurgical.ReportNo);

            if (dlg.ShowDialog().Value == true)
            {
                this.PanelSetOrderSurgical.PapCorrelationAccessionNo = dlg.CorrelatedAccessionNo;
            }
        }

        private void ButtonCancerCaseSummary_Click(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(this.PanelSetOrderSurgical.ReportNo) == false)
            {
                if (this.CancerCaseSummaryVisibility == "Visible")
                {
                    this.CancerCaseSummaryVisibility = "Collapsed";
                }
                else
                {
                    this.CancerCaseSummaryVisibility = "Visible";
                }
            }
        }

        private void ButtonCAPLink_Click(object sender, RoutedEventArgs args)
        {
            System.Diagnostics.Process.Start(@"https://www.cap.org/protocols-and-guidelines/cancer-reporting-tools/cancer-protocol-templates");
        }

        private void CheckPendingStudies(bool isFinal)
        {
            if (!isFinal)
            {
                YellowstonePathology.Business.Rules.Amendment.PendingStudies pendingStudies = new YellowstonePathology.Business.Rules.Amendment.PendingStudies();
                pendingStudies.Execute(this.PanelSetOrderSurgical);
            }
        }

        private void ComboBoxAmendmentUsers_SelectionChanged(object sender, RoutedEventArgs args)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem != null)
            {
                YellowstonePathology.Business.Amendment.Model.Amendment amendment = (YellowstonePathology.Business.Amendment.Model.Amendment)comboBox.Tag;
                YellowstonePathology.Business.User.SystemUser systemUserItem = (YellowstonePathology.Business.User.SystemUser)comboBox.SelectedItem;
                amendment.PathologistSignature = systemUserItem.Signature;
            }
        }

        private void CheckBoxRevisedDiagnosis_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = (YellowstonePathology.Business.Amendment.Model.Amendment)checkBox.Tag;
            amendment.ShowPreviousDiagnosis = true;
        }

        private void CheckBoxRevisedDiagnosis_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = (YellowstonePathology.Business.Amendment.Model.Amendment)checkBox.Tag;
            amendment.ShowPreviousDiagnosis = false;
        }

        public string CancerCaseSummaryVisibility
        {
            get
            {
                string result = "Collapsed";
                if (this.m_CancerCaseSummaryVisibility == "Visible")
                {
                    result = "Visible";
                }
                else
                {
                    if (this.PanelSetOrderSurgical.PanelSetId == 13)
                    {
                        if (!string.IsNullOrEmpty(this.PanelSetOrderSurgical.CancerSummary))
                        {
                            result = "Visible";
                        }
                    }
                }
                return result;
            }
            set
            {
                this.m_CancerCaseSummaryVisibility = value;
                this.NotifyPropertyChanged("CancerCaseSummaryVisibility");
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs args)
        {
            if (args.Key == Key.Space)
            {
                TextBox textBox = (TextBox)args.Source;
                this.m_TypingShortcutUserControl.SetShortcut(textBox);
            }
        }

        public YellowstonePathology.Business.View.BillingSpecimenViewCollection BillingSpecimenViewCollection
        {
            get { return this.m_BillingSpecimenViewCollection; }
        }

        public void RefreshBillingSpecimenViewCollection()
        {
            this.m_BillingSpecimenViewCollection.Refresh(this.PanelSetOrderSurgical.SurgicalSpecimenCollection,
                    this.m_PathologistUI.AccessionOrder.SpecimenOrderCollection, this.PanelSetOrderSurgical.PanelSetOrderCPTCodeCollection,
                    this.m_PathologistUI.AccessionOrder.ICD9BillingCodeCollection);
        }

        private void HyperLinkReplaceSpecialCharacters_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder initialNonASCIICharacters = this.FindNonASCIICharacters();
            if (initialNonASCIICharacters.Length != 0)
            {
                this.ReplaceNonASCIICharacters();
                StringBuilder subsequentNonASCIICharacters = this.FindNonASCIICharacters();
                if (subsequentNonASCIICharacters.Length != 0)
                {
                    MessageBox.Show("The following non-ASCII characters were found but not replaced: " + Environment.NewLine + subsequentNonASCIICharacters.ToString());
                }
                else
                {
                    MessageBox.Show("The following non-ASCII characters have been replaced: " + Environment.NewLine + initialNonASCIICharacters.ToString());
                }
            }
            else
            {
                MessageBox.Show("No non-ASCII characters were found.");
            }
        }

        private void ReplaceNonASCIICharacters()
        {
            StringBuilder result = new StringBuilder();
            string text = this.PanelSetOrderSurgical.CancerSummary;

            Dictionary<int, string> mapDictionary = new Dictionary<int, string>();
            mapDictionary.Add(8804, "<=");
            mapDictionary.Add(8805, ">=");
            mapDictionary.Add(8220, "\"");
            mapDictionary.Add(8221, "\"");

            foreach (KeyValuePair<int, string> map in mapDictionary)
            {
                char nonASCIIChar = (char)map.Key;
                text = text.Replace(nonASCIIChar.ToString(), map.Value);
            }

            this.PanelSetOrderSurgical.CancerSummary = text;

            //Remove this line
            this.ReplaceNonASCIICharactersInAudits();
        }

        //Remove this method
        private void ReplaceNonASCIICharactersInAudits()
        {
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit in this.PanelSetOrderSurgical.SurgicalAuditCollection)
            {
                StringBuilder result = new StringBuilder();
                string text = surgicalAudit.CancerSummary;

                Dictionary<int, string> mapDictionary = new Dictionary<int, string>();
                mapDictionary.Add(8804, "<=");
                mapDictionary.Add(8805, ">=");
                mapDictionary.Add(8220, "\"");
                mapDictionary.Add(8221, "\"");

                foreach (KeyValuePair<int, string> map in mapDictionary)
                {
                    char nonASCIIChar = (char)map.Key;
                    text = text.Replace(nonASCIIChar.ToString(), map.Value);
                }

                surgicalAudit.CancerSummary = text;
            }
        }

        private StringBuilder FindNonASCIICharacters()
        {
            StringBuilder result = new StringBuilder();
            if (string.IsNullOrEmpty(this.PanelSetOrderSurgical.CancerSummary) == false)
            {
                string text = this.PanelSetOrderSurgical.CancerSummary;
                for (int i = 0; i < text.Length; ++i)
                {
                    char c = text[i];
                    if (((int)c) > 127)
                    {
                        result.AppendLine("Character: " + text[i].ToString() + ", Code: " + ((int)c).ToString());
                    }
                }
            }
            return result;
        }

        private void Signout()
        {
            MainWindow.MoveKeyboardFocusNextThenBack();
            if (this.PanelSetOrderSurgical.Final == false)
            {
                YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.PanelSetOrderSurgical.IsOkToFinalize(this.m_PathologistUI.AccessionOrder);
                if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure || auditResult.Status == Business.Audit.Model.AuditStatusEnum.Warning)
                {
                    PathologistSignoutPath pathologistSignoutPath = new PathologistSignoutPath(this.m_PathologistUI.AccessionOrder, this.PanelSetOrderSurgical);
                    pathologistSignoutPath.Start();
                    this.RefreshBillingSpecimenViewCollection();
                    auditResult = pathologistSignoutPath.IsPathologistSignoutAuditSuccessful();
                }

                if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
                {
                    this.PanelSetOrderSurgical.Finish(this.m_PathologistUI.AccessionOrder);
                    if (this.PanelSetOrderSurgical.Accepted == false)
                    {
                        this.PanelSetOrderSurgical.Accept();
                    }
                }
            }
        }

        private void ContextMenuAddInteropativeConsultation_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.Business.Test.Model.IntraoperativeConsultation intraoperativeConsultation = new Business.Test.Model.IntraoperativeConsultation();
            //MenuItem menuItem = (MenuItem)sender;
            //YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen)menuItem.Tag;
            //YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(this.m_PathologistUI.PanelSetOrder.ReportNo, intraoperativeConsultation, null, null, false, )
        }

        private void ButtonSetCPTCodes_Click(object sender, RoutedEventArgs e)
        {
            this.m_PathologistUI.PanelSetOrder.PanelSetOrderCPTCodeCollection.SetCPTCodes(this.m_PathologistUI.AccessionOrder.SpecimenOrderCollection, this.m_PathologistUI.PanelSetOrder.ReportNo, this.m_PathologistUI.AccessionOrder.ClientId, this.m_PathologistUI.AccessionOrder.SvhMedicalRecord, this.m_PathologistUI.AccessionOrder.SvhAccount);
            this.RefreshBillingSpecimenViewCollection();            
        }

        private void CheckBoxGlobal_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = (YellowstonePathology.Business.Amendment.Model.Amendment)checkBox.Tag;
            amendment.MasterAccessionNo = this.m_PathologistUI.AccessionOrder.MasterAccessionNo;
        }

        private void CheckBoxGlobal_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = (YellowstonePathology.Business.Amendment.Model.Amendment)checkBox.Tag;
            amendment.MasterAccessionNo = null;
        }
    }
}
