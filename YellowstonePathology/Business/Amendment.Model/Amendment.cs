 using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Amendment.Model
{
    [PersistentClass("tblAmendment", "YPIDATA")]
    public class Amendment : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        string m_SignatureButtonText;
        bool m_SignatureButtonIsEnabled;
        bool m_DeleteButtonIsEnabled;

        private string m_ObjectId;
        private string m_AmendmentId;
        private string m_ReportNo;
        private bool m_RequirePathologistSignature;
        private bool m_ShowPreviousDiagnosis;
        private bool m_Final;
        private bool m_IsDistributed;
        private bool m_DistributeOnFinal;
        private bool m_ShowAmendmentOnReport;
        private bool m_RevisedDiagnosis;
        private Nullable<DateTime> m_AmendmentDate;
        private Nullable<DateTime> m_AmendmentTime;
        private Nullable<DateTime> m_FinalDate;
        private Nullable<DateTime> m_FinalTime;
        private int m_UserId;
        private string m_Text;
        private string m_AmendmentType;
        private string m_PathologistSignature;
        private string m_ReferenceReportNo;
        private int? m_AcceptedById;
        private bool m_Accepted;
        private Nullable<DateTime> m_AcceptedDate;
        private Nullable<DateTime> m_AcceptedTime;
        private string m_AcceptedBy;
        private bool m_SystemGenerated;

        public Amendment()
        {

        }

        public Amendment(string reportNo, string objectId, string amendmentId)
        {
            this.m_ReportNo = reportNo;
            this.m_ObjectId = objectId;
            this.AmendmentId = amendmentId;
            this.SetDefaultValues(reportNo);
        }

        public string SignatureButtonText
        {
            get { return this.m_SignatureButtonText; }
            set
            {
                this.m_SignatureButtonText = value;
                this.NotifyPropertyChanged("SignatureButtonText");
            }
        }

        public bool SignatureButtonIsEnabled
        {
            get { return this.m_SignatureButtonIsEnabled; }
            set
            {
                this.m_SignatureButtonIsEnabled = value;
                this.NotifyPropertyChanged("SignatureButtonIsEnabled");
            }
        }

        public bool DeleteButtonIsEnabled
        {
            get { return this.m_DeleteButtonIsEnabled; }
            set
            {
                this.m_DeleteButtonIsEnabled = value;
                this.NotifyPropertyChanged("DeleteButtonIsEnabled");
            }
        }

        public string ShortAmendment
        {
            get
            {
                string text = string.Empty;
                if (this.Text.Length > 100)
                {
                    text = this.Text.Substring(0, 100);
                }
                else
                {
                    text = this.Text;
                }
                text = text.Replace('\n', ' ');
                text = text.Replace('\r', ' ');
                return text;
            }
        }

        public void TestResultAmendmentFill(string reportNo, int assignedToId, string amendmentText)
        {
            this.ReportNo = reportNo;
            this.RequirePathologistSignature = true;
            this.AmendmentDate = DateTime.Today;
            this.AmendmentTime = DateTime.Now;
            this.Text = amendmentText;
            this.AmendmentType = "Addendum";
            this.ShowPreviousDiagnosis = false;
            this.Final = false;
            this.IsDistributed = false;
            this.DistributeOnFinal = true;
            this.UserId = assignedToId;
            this.ShowAmendmentOnReport = true;
            this.RevisedDiagnosis = false;
        }

        public void SetDefaultValues(string reportNo)
        {
            this.ReportNo = reportNo;
            this.RequirePathologistSignature = true;
            this.Text = "???";
            this.AmendmentType = "Addendum";
            this.ShowPreviousDiagnosis = false;
            this.Final = false;
            this.IsDistributed = false;
            this.DistributeOnFinal = true;
            this.ShowAmendmentOnReport = true;
            this.RevisedDiagnosis = false;
            this.AmendmentDate = DateTime.Today;
            this.AmendmentTime = DateTime.Now;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        [PersistentDocumentIdProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ObjectId
        {
            get { return this.m_ObjectId; }
            set
            {
                if (this.m_ObjectId != value)
                {
                    this.m_ObjectId = value;
                    this.NotifyPropertyChanged("ObjectId");
                }
            }
        }

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
        public string AmendmentId
        {
            get { return this.m_AmendmentId; }
            set
            {
                if (this.m_AmendmentId != value)
                {
                    this.m_AmendmentId = value;
                    this.NotifyPropertyChanged("AmendmentId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "1", "bit")]
        public bool RequirePathologistSignature
        {
            get { return this.m_RequirePathologistSignature; }
            set
            {
                if (this.m_RequirePathologistSignature != value)
                {
                    this.m_RequirePathologistSignature = value;
                    this.NotifyPropertyChanged("RequirePathologistSignature");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
        public bool ShowPreviousDiagnosis
        {
            get { return this.m_ShowPreviousDiagnosis; }
            set
            {
                if (this.m_ShowPreviousDiagnosis != value)
                {
                    this.m_ShowPreviousDiagnosis = value;
                    this.NotifyPropertyChanged("ShowPreviousDiagnosis");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
        public bool Final
        {
            get { return this.m_Final; }
            set
            {
                if (this.m_Final != value)
                {
                    this.m_Final = value;
                    this.NotifyPropertyChanged("Final");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
        public bool IsDistributed
        {
            get { return this.m_IsDistributed; }
            set
            {
                if (this.m_IsDistributed != value)
                {
                    this.m_IsDistributed = value;
                    this.NotifyPropertyChanged("IsDistributed");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
        public bool DistributeOnFinal
        {
            get { return this.m_DistributeOnFinal; }
            set
            {
                if (this.m_DistributeOnFinal != value)
                {
                    this.m_DistributeOnFinal = value;
                    this.NotifyPropertyChanged("DistributeOnFinal");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "1", "bit")]
        public bool ShowAmendmentOnReport
        {
            get { return this.m_ShowAmendmentOnReport; }
            set
            {
                if (this.m_ShowAmendmentOnReport != value)
                {
                    this.m_ShowAmendmentOnReport = value;
                    this.NotifyPropertyChanged("ShowAmendmentOnReport");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
        public bool RevisedDiagnosis
        {
            get { return this.m_RevisedDiagnosis; }
            set
            {
                if (this.m_RevisedDiagnosis != value)
                {
                    this.m_RevisedDiagnosis = value;
                    this.NotifyPropertyChanged("RevisedDiagnosis");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> AmendmentDate
        {
            get { return this.m_AmendmentDate; }
            set
            {
                if (this.m_AmendmentDate != value)
                {
                    this.m_AmendmentDate = value;
                    this.NotifyPropertyChanged("AmendmentDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> AmendmentTime
        {
            get { return this.m_AmendmentTime; }
            set
            {
                if (this.m_AmendmentTime != value)
                {
                    this.m_AmendmentTime = value;
                    this.NotifyPropertyChanged("AmendmentTime");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> FinalDate
        {
            get { return this.m_FinalDate; }
            set
            {
                if (this.m_FinalDate != value)
                {
                    this.m_FinalDate = value;
                    this.NotifyPropertyChanged("FinalDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> FinalTime
        {
            get { return this.m_FinalTime; }
            set
            {
                if (this.m_FinalTime != value)
                {
                    this.m_FinalTime = value;
                    this.NotifyPropertyChanged("FinalTime");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int UserId
        {
            get { return this.m_UserId; }
            set
            {
                if (this.m_UserId != value)
                {
                    this.m_UserId = value;
                    this.NotifyPropertyChanged("UserId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "20", "null", "varchar")]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "'???'", "varchar")]
        public string Text
        {
            get { return this.m_Text; }
            set
            {
                if (this.m_Text != value)
                {
                    this.m_Text = value;
                    this.NotifyPropertyChanged("Text");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string AmendmentType
        {
            get { return this.m_AmendmentType; }
            set
            {
                if (this.m_AmendmentType != value)
                {
                    this.m_AmendmentType = value;
                    this.NotifyPropertyChanged("AmendmentType");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "250", "null", "varchar")]
        public string PathologistSignature
        {
            get { return this.m_PathologistSignature; }
            set
            {
                if (this.m_PathologistSignature != value)
                {
                    this.m_PathologistSignature = value;
                    this.NotifyPropertyChanged("PathologistSignature");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "20", "null", "varchar")]
        public string ReferenceReportNo
        {
            get { return this.m_ReferenceReportNo; }
            set
            {
                if (this.m_ReferenceReportNo != value)
                {
                    this.m_ReferenceReportNo = value;
                    this.NotifyPropertyChanged("ReferenceReportNo");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public int? AcceptedById
        {
            get { return this.m_AcceptedById; }
            set
            {
                if (this.m_AcceptedById != value)
                {
                    this.m_AcceptedById = value;
                    this.NotifyPropertyChanged("AcceptedById");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "null", "bit")]
        public bool Accepted
        {
            get { return this.m_Accepted; }
            set
            {
                if (this.m_Accepted != value)
                {
                    this.m_Accepted = value;
                    this.NotifyPropertyChanged("Accepted");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> AcceptedDate
        {
            get { return this.m_AcceptedDate; }
            set
            {
                if (this.m_AcceptedDate != value)
                {
                    this.m_AcceptedDate = value;
                    this.NotifyPropertyChanged("AcceptedDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> AcceptedTime
        {
            get { return this.m_AcceptedTime; }
            set
            {
                if (this.m_AcceptedTime != value)
                {
                    this.m_AcceptedTime = value;
                    this.NotifyPropertyChanged("AcceptedTime");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string AcceptedBy
        {
            get { return this.m_AcceptedBy; }
            set
            {
                if (this.m_AcceptedBy != value)
                {
                    this.m_AcceptedBy = value;
                    this.NotifyPropertyChanged("AcceptedBy");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "null", "bit")]
        public bool SystemGenerated
        {
            get { return this.m_SystemGenerated; }
            set
            {
                if (this.m_SystemGenerated != value)
                {
                    this.m_SystemGenerated = value;
                    this.NotifyPropertyChanged("SystemGenerated");
                }
            }
        }

        public virtual YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            if (this.Accepted == true)
            {
                result.Success = false;
                result.Message = "The amendment cannot be accepted because it is already accepted.";
            }
            else if (this.m_Text.Contains("???"))
            {
                result.Success = false;
                result.Message = "The amendment cannot be accepted because the text contains ???.";
            }
            return result;
        }

        public virtual YellowstonePathology.Business.Rules.MethodResult IsOkToUnaccept()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            if (this.Final == true)
            {
                result.Success = false;
                result.Message = "The amendment cannot be unaccepted because the amendment is final.";
            }
            else if (this.Accepted == false)
            {
                result.Success = false;
                result.Message = "The amendment cannot be unaccepted because it has not accepted.";
            }
            return result;
        }

        public YellowstonePathology.Business.Test.OkToUnfinalizeResult IsOkToUnfinalize(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            YellowstonePathology.Business.Test.OkToUnfinalizeResult result = new Test.OkToUnfinalizeResult();
            result.OK = true;

            if (this.Final == false)
            {
                result.OK = false;
                result.Message = "Cannot unfinalize this amendment because it is not final.";
            }
            else if (panelSetOrder.TestOrderReportDistributionCollection.HasDistributedItemsAfter(this.FinalTime.Value) == true)
            {
                result.OK = false;
                result.ShowWarningMessage = true;
                result.Message = "Warning. This amendment has been sent out since it was finaled.  Are you sure you want to unfinal it?";
            }
            return result;
        }

        public YellowstonePathology.Business.Test.OkToFinalizeResult IsOkToFinalize(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Test.OkToFinalizeResult okToFinalizeResult = new Test.OkToFinalizeResult();
            okToFinalizeResult.OK = true;

            if (this.m_Text.Contains("???"))
            {
                okToFinalizeResult.OK = false;
                okToFinalizeResult.Message = "The amendment cannot be finalized because the text contains ???.";
            }
            else if (this.m_Final == true)
            {
                okToFinalizeResult.OK = false;
                okToFinalizeResult.Message = "The amendment is already final.";
            }
            else
            {
                bool textMatches = this.SystemGeneratedTextMatchesCurrent(accessionOrder);
                if (textMatches == false)
                {
                    okToFinalizeResult.Message = "The amendment text may not accurately reflect the results of the test for which the amendment was created." +
                        Environment.NewLine + Environment.NewLine + "Do you wish to continue?";
                    okToFinalizeResult.ShowWarningMessage = true;
                }
            }
            return okToFinalizeResult;
        }

        public YellowstonePathology.Business.Rules.MethodResult IsOkToResetText(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
            if (this.Final == true)
            {
                result.Success = false;
                result.Message = "The amendment cannot be reset because the amendment is final.";
            }
            else
            {
                if (this.HasAbilityToResetText(accessionOrder) == false)
                {
                    result.Success = false;
                    result.Message = "The amendment cannot be reset because the text is not generated.";
                }
            }
            return result;
        }

        public void Accept()
        {
            this.m_Accepted = true;
            this.m_AcceptedById = Business.User.SystemIdentity.Instance.User.UserId;
            this.m_AcceptedBy = Business.User.SystemIdentity.Instance.User.DisplayName;
            this.m_AcceptedDate = DateTime.Today;
            this.m_AcceptedTime = DateTime.Now;
            this.NotifyPropertyChanged(string.Empty);
        }

        public void Unaccept()
        {
            this.m_Accepted = false;
            this.m_AcceptedById = 0;
            this.m_AcceptedBy = null;
            this.m_AcceptedDate = null;
            this.m_AcceptedTime = null;
            this.NotifyPropertyChanged(string.Empty);
        }

        public void Finish()
        {
            if (this.Accepted == false)
            {
                this.Accept();
            }

            this.m_PathologistSignature = Business.User.SystemIdentity.Instance.User.Signature;
            this.m_UserId = Business.User.SystemIdentity.Instance.User.UserId;
            this.m_Final = true;
            this.m_FinalDate = DateTime.Today;
            this.m_FinalTime = DateTime.Now;
            this.NotifyPropertyChanged(string.Empty);
        }

        public void Unfinalize()
        {
            this.m_PathologistSignature = null;
            this.m_UserId = 0;
            this.m_Final = false;
            this.m_FinalDate = null;
            this.m_FinalTime = null;
            this.NotifyPropertyChanged(string.Empty);
        }

        private bool SystemGeneratedTextMatchesCurrent(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = true;
            if (this.m_SystemGenerated == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReferenceReportNo);
                YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest her2AmplificationByISHTest = new Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
                if (panelSetOrder.PanelSetId == her2AmplificationByISHTest.PanelSetId)
                {
                    string generatedText = YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHSystemGeneratedAmendmentText.AmendmentText((YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)panelSetOrder);
                    result = this.m_Text.Contains(generatedText);
                }
            }
            return result;
        }

        private bool HasAbilityToResetText(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            if (this.m_SystemGenerated == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReferenceReportNo);
                YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest her2AmplificationByISHTest = new Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
                if (panelSetOrder.PanelSetId == her2AmplificationByISHTest.PanelSetId)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
