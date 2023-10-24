using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.FetalHemoglobin
{
    [PersistentClass("tblFetalHemoglobinTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class FetalHemoglobinTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_HbFResult;
        private string m_ReferenceRange;
        private string m_ReportComment;
        private string m_ASRComment;

        public FetalHemoglobinTestOrder()
        { }

        public FetalHemoglobinTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_ReferenceRange = "Less than or equal to 0.04%";
            this.m_ASRComment = "Tests utilizing Analytic Specific Reagents (ASR’s) were developed and performance characteristics determined " +
                "by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The " +
                "FDA has determined that such clearance or approval is not necessary.  ASR’s may be used for clinical purposes and should not " +
                "be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement " +
                "Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
        }

        [PersistentProperty()]
        public string HbFResult
        {
            get { return this.m_HbFResult; }
            set
            {
                if (this.m_HbFResult != value)
                {
                    this.m_HbFResult = value;
                    NotifyPropertyChanged("HbFResult");
                }
            }
        }

        [PersistentProperty()]
        public string ReportComment
        {
            get { return this.m_ReportComment; }
            set
            {
                if (this.m_ReportComment != value)
                {
                    this.m_ReportComment = value;
                    NotifyPropertyChanged("ReportComment");
                }
            }
        }

        [PersistentProperty()]
        public string ReferenceRange
        {
            get { return this.m_ReferenceRange; }
            set
            {
                if (this.m_ReferenceRange != value)
                {
                    this.m_ReferenceRange = value;
                    NotifyPropertyChanged("ReferenceRange");
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

            result.AppendLine("Hb-F: " + this.m_HbFResult);
            result.AppendLine();

            return result.ToString();
        }

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToAccept(accessionOrder);

            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_HbFResult) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "Unable to accept as the Hb-F result is not set: ";
                }
            }
            return result;
        }
    }
}
