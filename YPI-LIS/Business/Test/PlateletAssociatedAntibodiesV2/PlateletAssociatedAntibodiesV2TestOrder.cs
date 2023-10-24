using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.PlateletAssociatedAntibodiesV2
{
    [PersistentClass("tblPlateletAssociatedAntibodiesV2TestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class PlateletAssociatedAntibodiesV2TestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_IgGResult;
        private string m_IgGReference;
        private string m_IgMResult;
        private string m_IgMReference;
        private string m_Interpretation;
        private string m_Method;
        private string m_ASRComment;

        public PlateletAssociatedAntibodiesV2TestOrder()
        { }



        public PlateletAssociatedAntibodiesV2TestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_IgGReference = "Negative";
            this.m_IgMReference = "Negative";
            this.m_Method = "Quantitative Flow Cytometry";
            this.m_Interpretation = "* Negative:  IgG and/or IgM values are not elevated.  There is no indication that immune mechanisms " +
                "are involved in the thrombocytopenia.  Other etiologies should be considered." + Environment.NewLine + Environment.NewLine +
                "* Weakly Positive:  The moderately elevated IgG and / or IgM value suggests that immune mechanisms could be involved in the " +
                "thrombocytopenia.  Other etiologies should also be considered." + Environment.NewLine + Environment.NewLine +
                "*Positive: The elevated IgG and/ or IgM value suggests that immune mechanisms are involved in the thrombocytopenia." + 
                Environment.NewLine + Environment.NewLine +
                "* Strongly Positive: The IgG and / or IgM value is greatly elevated and indicates that immune mechanisms are involved in the " +
                "thrombocytopenia.";
            this.m_ASRComment = "Tests utilizing Analytic Specific Reagents (ASR’s) were developed and performance characteristics " +
                "determined by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug " +
                "Administration.  The FDA has determined that such clearance or approval is not necessary.  ASR’s may be used for clinical " +
                "purposes and should not be regarded as investigational or for research.  This laboratory is certified under the Clinical " +
                "Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
        }

        [PersistentProperty()]
        public string IgGResult
        {
            get { return this.m_IgGResult; }
            set
            {
                if (this.m_IgGResult != value)
                {
                    this.m_IgGResult = value;
                    NotifyPropertyChanged("IgGResult");
                }
            }
        }

        [PersistentProperty()]
        public string IgMResult
        {
            get { return this.m_IgMResult; }
            set
            {
                if (this.m_IgMResult != value)
                {
                    this.m_IgMResult = value;
                    NotifyPropertyChanged("IgMResult");
                }
            }
        }

        [PersistentProperty()]
        public string IgGReference
        {
            get { return this.m_IgGReference; }
            set
            {
                if (this.m_IgGReference != value)
                {
                    this.m_IgGReference = value;
                    NotifyPropertyChanged("IgGReference");
                }
            }
        }

        [PersistentProperty()]
        public string IgMReference
        {
            get { return this.m_IgMReference; }
            set
            {
                if (this.m_IgMReference != value)
                {
                    this.m_IgMReference = value;
                    NotifyPropertyChanged("IgMReference");
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
                    NotifyPropertyChanged("Interpretation");
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

            result.AppendLine("Anti Platelet Antibody - IgG: " + this.m_IgGResult);
            result.AppendLine();

            result.AppendLine("Anti Platelet Antibody - IgM: " + this.m_IgMResult);
            result.AppendLine();

            return result.ToString();
        }

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToAccept(accessionOrder);

            if (result.Status == AuditStatusEnum.OK)
            {
                string message = string.Empty;
                if (string.IsNullOrEmpty(this.m_IgGResult) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    message = "Anti Platelet Antibody - IgG" + Environment.NewLine;
                }

                if (string.IsNullOrEmpty(this.m_IgGReference) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    message += "IgG Reference" + Environment.NewLine;
                }
                if (string.IsNullOrEmpty(this.m_IgMResult) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    message += "Anti Platelet Antibody - IgM" + Environment.NewLine;
                }

                if (string.IsNullOrEmpty(this.m_IgMReference) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    message += "IgM Reference";
                }

                if(result.Status == AuditStatusEnum.Failure)
                {
                    result.Message = "Unable to accept as the following are not set:" + Environment.NewLine + message;
                }
            }

            return result;
        }

        public override AuditResult IsOkToFinalize(AccessionOrder accessionOrder)
        {
            AuditResult result = new Audit.Model.AuditResult();
            result.Status = AuditStatusEnum.OK;

            YellowstonePathology.Business.Rules.MethodResult methodResult = IsOkToFinalize();
            if (methodResult.Success == false)
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
