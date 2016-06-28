using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.KRASStandard
{
    [PersistentClass("tblKRASStandardTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class KRASStandardTestOrder : YellowstonePathology.Business.Test.PanelSetOrder, YellowstonePathology.Business.Interface.ISolidTumorTesting
    {
        private string m_Result;
        private string m_Interpretation;
        private string m_Comment;
        private string m_Indication;
        private string m_IndicationComment;
        private string m_TumorNucleiPercentage;
        private string m_Method;
        private string m_References;
        private string m_ReportDisclaimer;
        private string m_MutationDetected;

        public KRASStandardTestOrder()
        {

        }

        public KRASStandardTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {

        }

        [PersistentProperty()]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        [PersistentProperty()]
        public string Interpretation
        {
            get { return this.m_Interpretation; }
            set
            {
                if (this.m_Interpretation != value)
                {
                    this.m_Interpretation = value;
                    this.NotifyPropertyChanged("Interpretation");
                }
            }
        }

        [PersistentProperty()]
        public string Comment
        {
            get { return this.m_Comment; }
            set
            {
                if (this.m_Comment != value)
                {
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
                }
            }
        }

        [PersistentProperty()]
        public string Indication
        {
            get { return this.m_Indication; }
            set
            {
                if (this.m_Indication != value)
                {
                    this.m_Indication = value;
                    this.NotifyPropertyChanged("Indication");
                }
            }
        }

        [PersistentProperty()]
        public string IndicationComment
        {
            get { return this.m_IndicationComment; }
            set
            {
                if (this.m_IndicationComment != value)
                {
                    this.m_IndicationComment = value;
                    this.NotifyPropertyChanged("IndicationComment");
                }
            }
        }

        [PersistentProperty()]
        public string TumorNucleiPercentage
        {
            get { return this.m_TumorNucleiPercentage; }
            set
            {
                if (this.m_TumorNucleiPercentage != value)
                {
                    this.m_TumorNucleiPercentage = value;
                    this.NotifyPropertyChanged("TumorNucleiPercentage");
                }
            }
        }

        [PersistentProperty()]
        public string Method
        {
            get { return this.m_Method; }
            set
            {
                if (this.m_Method != value)
                {
                    this.m_Method = value;
                    this.NotifyPropertyChanged("Method");
                }
            }
        }

        [PersistentProperty()]
        public string References
        {
            get { return this.m_References; }
            set
            {
                if (this.m_References != value)
                {
                    this.m_References = value;
                    this.NotifyPropertyChanged("References");
                }
            }
        }

        [PersistentProperty()]
        public string ReportDisclaimer
        {
            get { return this.m_ReportDisclaimer; }
            set
            {
                if (this.m_ReportDisclaimer != value)
                {
                    this.m_ReportDisclaimer = value;
                    this.NotifyPropertyChanged("ReportDisclaimer");
                }
            }
        }

        [PersistentProperty()]
        public string MutationDetected
        {
            get { return this.m_MutationDetected; }
            set
            {
                if (this.m_MutationDetected != value)
                {
                    this.m_MutationDetected = value;
                    this.NotifyPropertyChanged("MutationDetected");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();
            result.Append("Result: ");
            result.Append(this.m_Result);
            result.AppendLine(this.m_MutationDetected);
            result.Append("Indication: ");
            result.AppendLine(this.m_Indication);
            result.Append("Interpretation: ");
            result.AppendLine(this.m_Interpretation);
            result.Append("Comment: ");
            result.AppendLine(this.m_Comment);
            result.Append("Tumor Nuclei Percent: ");
            result.AppendLine(this.m_TumorNucleiPercentage);
            return result.ToString();
        }

        public YellowstonePathology.Business.Rules.MethodResult IsOkToSetResult()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            if (this.m_Accepted == true)
            {
                result.Success = false;
                result.Message = "The results cannot be set because the results have already been accepted.";
            }
            else if (string.IsNullOrEmpty(this.m_Indication) == true)
            {
                result.Success = false;
                result.Message = "The results cannot be set because the indication has not been set.";
            }
            return result;
        }

        public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
        {
            YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
            if (result.Success == true)
            {
                if (string.IsNullOrEmpty(this.m_ResultCode) == true)
                {
                    result.Success = false;
                    result.Message = "The results cannot be accepted because the Result is not set.";
                }
            }
            return result;
        }

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult auditResult = base.IsOkToFinalize(accessionOrder);
            if(auditResult.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_ResultCode) == true)
                {
                    auditResult.Status = Audit.Model.AuditStatusEnum.Failure;
                    auditResult.Message = "This case cannot be finalized because the results have not been set.";
                }
                else
                {
                    YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new KRASStandardReflex.KRASStandardReflexTest();
                    if (accessionOrder.PanelSetOrderCollection.Exists(krasStandardReflexTest.PanelSetId, this.OrderedOnId, true) == false)
                    {
                        YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings ypi = new YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings();
                        if (this.TechnicalComponentFacilityId == ypi.FacilityId)
                        {
                            if (string.IsNullOrEmpty(this.TumorNucleiPercentage) == true)
                            {
                                if (accessionOrder.PanelSetOrderCollection.Exists(krasStandardReflexTest.PanelSetId) == false)
                                {
                                    auditResult.Status = Audit.Model.AuditStatusEnum.Failure;
                                    auditResult.Message = "This case cannot be finalized because the Tumor Nuclei Percent is not set.";
                                }
                            }
                        }
                    }
                }
            }
            return auditResult;
        }
    }
}
