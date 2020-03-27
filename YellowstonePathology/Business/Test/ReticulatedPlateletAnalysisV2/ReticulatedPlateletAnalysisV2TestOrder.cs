using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ReticulatedPlateletAnalysisV2
{
    [PersistentClass("tblReticulatedPlateletAnalysisV2TestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class ReticulatedPlateletAnalysisV2TestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Result;
        private string m_ResultReference;
        private string m_Method;
        private string m_ASRComment;

        public ReticulatedPlateletAnalysisV2TestOrder()
        { }

        public ReticulatedPlateletAnalysisV2TestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_ResultReference = "0-0.37%";
            this.m_Method = "Quantitative Flow Cytometry";
            this.m_ASRComment = "Tests utilizing Analytic Specific Reagents (ASR's) were developed and performance characteristics " +
                "determined by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug " +
                "Administration.  The FDA has determined that such clearance or approval is not necessary.  ASR's may be used for clinical " +
                "purposes and should not be regarded as investigational or for research.  This laboratory is certified under the Clinical " +
                "Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
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
                    NotifyPropertyChanged("Result");
                }
            }
        }

        [PersistentProperty()]
        public string ResultReference
        {
            get { return this.m_ResultReference; }
            set
            {
                if (this.m_ResultReference != value)
                {
                    this.m_ResultReference = value;
                    NotifyPropertyChanged("ResultReference");
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
                    NotifyPropertyChanged("Method");
                }
            }
        }

        [PersistentProperty()]
        public string ASRComment
        {
            get { return this.m_ASRComment; }
            set
            {
                if (this.m_ASRComment != value)
                {
                    this.m_ASRComment = value;
                    NotifyPropertyChanged("ASRComment");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Reticulated Platelet Analysis: " + this.m_Result);
            result.AppendLine();

            return result.ToString();
        }

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToAccept(accessionOrder);

            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_Result) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "Unable to accept as the Reticulated Platelet Analysis is not set.";
                }
            }

            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_ResultReference) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = "Unable to accept as the Reference is not set.";
                }
            }

            return result;
        }

        public override AuditResult IsOkToFinalize(AccessionOrder accessionOrder)
        {
            AuditResult result = new Audit.Model.AuditResult();
            result.Status = AuditStatusEnum.OK;

            YellowstonePathology.Business.Rules.MethodResult methodResult = IsOkToFinalize();
            if(methodResult.Success == false)
            {
                result.Status = AuditStatusEnum.Failure;
                result.Message = methodResult.Message;
            }

            if (result.Status == AuditStatusEnum.OK)
            {
                result = base.IsOkToFinalize(accessionOrder);
            }

            return result;
        }
    }
}
