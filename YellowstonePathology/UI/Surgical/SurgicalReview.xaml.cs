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
            this.PreviewLostKeyboardFocus += SurgicalReview_PreviewLostKeyboardFocus;

            InitializeComponent();                    
            this.DataContext = this;            
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
			panelSetOrderCPTCode.EntryType = YellowstonePathology.Business.Billing.Model.PanelSetOrderCPTCodeEntryType.ManualEntry;
			panelSetOrderCPTCode.SpecimenOrderId = specimenOrder.SpecimenOrderId;
			panelSetOrderCPTCode.ClientId = this.AccessionOrder.ClientId;
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
				this.AccessionOrder.MasterAccessionNo, surgicalSpecimen.SpecimenOrderId, null, null, 1);
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
            /*
            if ((Business.User.SystemIdentity.Instance.User.UserId == 5102 ||
                Business.User.SystemIdentity.Instance.User.UserId == 5111 ||
                Business.User.SystemIdentity.Instance.User.UserId == 5129 ||
                Business.User.SystemIdentity.Instance.User.UserId == 5088 ||
                Business.User.SystemIdentity.Instance.User.UserId == 5091) && this.PanelSetOrderSurgical.Final == false) //Dr. Durden only and only if signing
            {
                this.TestSignout();
            }
            */

            this.TestSignout();

            /*
            else
            {
                if (this.FindNonASCIICharacters().Length == 0)
                {
                    this.CheckPendingStudies(this.PanelSetOrderSurgical.Final);

                    if (this.PanelSetOrderSurgical.Final == false)
                    {
                        PQRSMeasurePath pQRSMeasurePath = new PQRSMeasurePath(this.AccessionOrder, this.PanelSetOrderSurgical);
                        pQRSMeasurePath.HandlePQRS();
                        this.RefreshBillingSpecimenViewCollection();
                    }

                    YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
                    YellowstonePathology.Business.Rules.Surgical.PathologistFinalRules rules = new YellowstonePathology.Business.Rules.Surgical.PathologistFinalRules();
                    rules.Execute(this.AccessionOrder, this.PanelSetOrderSurgical, Business.User.SystemIdentity.Instance, executionStatus);

                    if (executionStatus.Halted == true && string.IsNullOrEmpty(executionStatus.ExecutionMessagesString) == false)
                    {
                        MessageBox.Show(executionStatus.ExecutionMessagesString, "Unable to final");
                    }

                    this.m_PathologistUI.SetSignatureButtonProperties();
                    this.NotifyPropertyChanged(string.Empty);

                    this.m_PathologistUI.RunWorkspaceEnableRules();
                    this.m_PathologistUI.RunPathologistEnableRules();
                }
                else
                {
                    MessageBox.Show("The cancer case summary contains special characters.  These must be removed before signing.");
                }
            }
            */
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

                    if(canFinal == true)
                    {
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
				Common.ReassignCaseDialog reassignCaseDialog = new Common.ReassignCaseDialog(this.PanelSetOrderSurgical, Business.User.SystemIdentity.Instance);
                reassignCaseDialog.ShowDialog();                
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
            System.Diagnostics.Process.Start(@"http://www.cap.org/web/home/resources/cancer-reporting-tools/cancer-protocol-templates?_afrLoop=290486100518829#%40%3F_afrLoop%3D290486100518829%26_adf.ctrl-state%3Ddqakeocns_55");
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

        private void TestSignout()
        {
            MainWindow.MoveKeyboardFocusNextThenBack();
            if (this.PanelSetOrderSurgical.Final == false)
            {
                YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.PanelSetOrderSurgical.IsOkToFinalize(this.m_PathologistUI.AccessionOrder);
                if(auditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
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

                this.m_PathologistUI.SetSignatureButtonProperties();
                this.NotifyPropertyChanged(string.Empty);                
            }
        }

        private void ContextMenuAddInteropativeConsultation_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.Business.Test.Model.IntraoperativeConsultation intraoperativeConsultation = new Business.Test.Model.IntraoperativeConsultation();
            //MenuItem menuItem = (MenuItem)sender;
            //YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen)menuItem.Tag;
            //YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(this.m_PathologistUI.PanelSetOrder.ReportNo, intraoperativeConsultation, null, null, false, )
        }        
    }
}
