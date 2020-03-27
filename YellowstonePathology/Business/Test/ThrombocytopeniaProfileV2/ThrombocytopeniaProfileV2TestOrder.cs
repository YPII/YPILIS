using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ThrombocytopeniaProfileV2
{
    [PersistentClass("tblThrombocytopeniaProfileV2TestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class ThrombocytopeniaProfileV2TestOrder : YellowstonePathology.Business.Test.PanelSetOrder
   {
        private string m_AntiPlateletAntibodyIgG;
        private string m_AntiPlateletAntibodyIgM;
        private string m_ReticulatedPlateletAnalysis;
        private string m_AntiPlateletAntibodyIgGReference;
        private string m_AntiPlateletAntibodyIgMReference;
        private string m_ReticulatedPlateletAnalysisReference;
        private string m_Interpretation;
        private string m_Method;
        private string m_ASRComment;

        public ThrombocytopeniaProfileV2TestOrder()
        { }

        public ThrombocytopeniaProfileV2TestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_AntiPlateletAntibodyIgGReference = "Negative";
            this.m_AntiPlateletAntibodyIgMReference = "Negative";
            this.m_ReticulatedPlateletAnalysisReference = "0-0.37%";
            this.m_Interpretation = "* Negative:  IgG and/or IgM values are not elevated.  There is no indication that immune mechanisms " +
                "are involved in the thrombocytopenia.  Other etiologies should be considered." + Environment.NewLine + Environment.NewLine +
                "* Weakly Positive: The moderately elevated IgG and / or IgM value suggests that immune mechanisms could be involved in the " +
                "thrombocytopenia.  Other etiologies should also be considered." + Environment.NewLine + Environment.NewLine +
                "*Positive: The elevated IgG and/ or IgM value suggests that immune mechanisms are involved in the thrombocytopenia." + 
                Environment.NewLine + Environment.NewLine +
                "*Strongly Positive: The IgG and / or IgM value is greatly elevated and indicates that immune mechanisms are involved in " +
                "the thrombocytopenia.";
            this.m_Method = "Reticulated Platelets: Quantitative Flow Cytometry" + Environment.NewLine +
                "Platelet Associated Antibodies: Qualitative Flow Cytometry";
            this.m_ASRComment = "Tests utilizing Analytic Specific Reagents (ASR's) were developed and performance characteristics " +
                "determined by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug " +
                "Administration.  The FDA has determined that such clearance or approval is not necessary.  ASR's may be used for clinical " +
                "purposes and should not be regarded as investigational or for research.  This laboratory is certified under the Clinical " +
                "Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
        }

        [PersistentProperty()]
        public string AntiPlateletAntibodyIgG
        {
            get { return this.m_AntiPlateletAntibodyIgG; }
            set
            {
                if (this.m_AntiPlateletAntibodyIgG != value)
                {
                    this.m_AntiPlateletAntibodyIgG = value;
                    NotifyPropertyChanged("AntiPlateletAntibodyIgG");
                }
            }
        }

        [PersistentProperty()]
        public string AntiPlateletAntibodyIgM
        {
            get { return this.m_AntiPlateletAntibodyIgM; }
            set
            {
                if (this.m_AntiPlateletAntibodyIgM != value)
                {
                    this.m_AntiPlateletAntibodyIgM = value;
                    NotifyPropertyChanged("AntiPlateletAntibodyIgM");
                }
            }
        }

        [PersistentProperty()]
        public string ReticulatedPlateletAnalysis
        {
            get { return this.m_ReticulatedPlateletAnalysis; }
            set
            {
                if (this.m_ReticulatedPlateletAnalysis != value)
                {
                    this.m_ReticulatedPlateletAnalysis = value;
                    NotifyPropertyChanged("ReticulatedPlateletAnalysis");
                }
            }
        }

        [PersistentProperty()]
        public string AntiPlateletAntibodyIgGReference
        {
            get { return this.m_AntiPlateletAntibodyIgGReference; }
            set
            {
                if (this.m_AntiPlateletAntibodyIgGReference != value)
                {
                    this.m_AntiPlateletAntibodyIgGReference = value;
                    NotifyPropertyChanged("AntiPlateletAntibodyIgGReference");
                }
            }
        }

        [PersistentProperty()]
        public string AntiPlateletAntibodyIgMReference
        {
            get { return this.m_AntiPlateletAntibodyIgMReference; }
            set
            {
                if (this.m_AntiPlateletAntibodyIgMReference != value)
                {
                    this.m_AntiPlateletAntibodyIgMReference = value;
                    NotifyPropertyChanged("AntiPlateletAntibodyIgMReference");
                }
            }
        }

        [PersistentProperty()]
        public string ReticulatedPlateletAnalysisReference
        {
            get { return this.m_ReticulatedPlateletAnalysisReference; }
            set
            {
                if (this.m_ReticulatedPlateletAnalysisReference != value)
                {
                    this.m_ReticulatedPlateletAnalysisReference = value;
                    NotifyPropertyChanged("ReticulatedPlateletAnalysisReference");
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

            result.AppendLine("Anti-Platelet Antibody - IgG: " + this.m_AntiPlateletAntibodyIgG);
            result.AppendLine();

            result.AppendLine("Anti-Platelet Antibody - IgM: " + this.m_AntiPlateletAntibodyIgM);
            result.AppendLine();

            result.AppendLine("Reticulated Platelet Analysis: " + this.m_ReticulatedPlateletAnalysis);
            result.AppendLine();

            return result.ToString();
        }

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToAccept(accessionOrder);

            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_AntiPlateletAntibodyIgG) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "Anti - Platelet Antibody - IgG" + Environment.NewLine;
                }
                if (string.IsNullOrEmpty(this.m_AntiPlateletAntibodyIgM) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "Anti-Platelet Antibody - IgM" + Environment.NewLine;
                }
            }

            if (result.Status == AuditStatusEnum.Failure)
            {
                result.Message = "Unable to accept as the following result/s are not set: " + Environment.NewLine + result.Message;
            }

            return result;
        }
    }
}
