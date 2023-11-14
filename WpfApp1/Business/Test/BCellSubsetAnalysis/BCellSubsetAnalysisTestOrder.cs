using System;
using System.Text;
using YellowstonePathology.Business.Persistence;


namespace YellowstonePathology.Business.Test.BCellSubsetAnalysis
{
    [PersistentClass("tblBCellSubsetAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class BCellSubsetAnalysisTestOrder : PanelSetOrder
    {
        private string m_Method;        
        private string m_ASRComment;
        private string m_MatureBCellsPlusPercent;
        private string m_MatureBCellsMinusPercent;
        private string m_MemoryBCellPercent;
        private string m_NonSwitchedMemoryBCellPercent;
        private string m_MarginalZoneBCellPercent;
        private string m_ClassSwitchedMemoryBCellPercent;
        private string m_NaiveBCellPercent;
        private string m_TransitionalBCellPercent;
        private string m_PlasmaBlastsPercent;        
        private string m_TotalNucleatedPercent;
        private string m_TotalLymphocytesPercent;

        public BCellSubsetAnalysisTestOrder()
        {
        }

        public BCellSubsetAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_ASRComment = "Tests utilizing Analytic Specific Reagents (ASR's) were developed and performance characteristics determined by " +
                "Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The FDA has " +
                "determined that such clearance or approval is not necessary.  ASR's may be used for clinical purposes and should not be regarded as " +
                "investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) " +
                "as qualified to perform high complexity clinical laboratory testing.";
            this.m_Method = "Quantitative Flow Cytometry.";
            this.m_ReportReferences = "A. S. Bradley, B Ford, A.S. Bansal.  Altered functional B cell subset populations in patients with chronic fatigue syndrome compared to healthy controls.  British Society for Immunology, Clinical and Experimental Immunology, 172: 73-80. " + Environment.NewLine +
                "F.Mensah, A Bansal, S. Berkovitz, A.Sharma, V Reddy, M J.Leandro and G.Cambridge.Extended B cell phenotype in patients with myalgic encephalomyelitis/ chronic fatigue syndrome: a cross-sectional study.British Society for Immunology, Clinical and Experimental Immunology, 184: 237 - 247." + Environment.NewLine +
                "Ramos S, Brenu E, Nuyen T, Ng J, Staines D, et al.Characterization of B cell Subsets and Receptors in Chronic Fatigue Syndrome Patients. J Clin Cell Immunol 6: 288.";            
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string ASRComment
        {
            get { return this.m_ASRComment; }
            set
            {
                if (this.m_ASRComment != value)
                {
                    this.m_ASRComment = value;
                    this.NotifyPropertyChanged("ASRComment");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "10", "null", "varchar")]
        public string MatureBCellsPlusPercent
        {
            get { return this.m_MatureBCellsPlusPercent; }
            set
            {
                if (this.m_MatureBCellsPlusPercent != value)
                {
                    this.m_MatureBCellsPlusPercent = value;
                    this.NotifyPropertyChanged("MatureBCellsPlusPercent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "10", "null", "varchar")]
        public string MatureBCellsMinusPercent

        {
            get { return this.m_MatureBCellsMinusPercent; }
            set
            {
                if (this.m_MatureBCellsMinusPercent != value)
                {
                    this.m_MatureBCellsMinusPercent = value;
                    this.NotifyPropertyChanged("MatureBCellsMinusPercent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "10", "null", "varchar")]
        public string MemoryBCellPercent
        {
            get { return this.m_MemoryBCellPercent; }
            set
            {
                if (this.m_MemoryBCellPercent != value)
                {
                    this.m_MemoryBCellPercent = value;
                    this.NotifyPropertyChanged("MemoryBCellPercent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "10", "null", "varchar")]
        public string NonSwitchedMemoryBCellPercent
        {
            get { return this.m_NonSwitchedMemoryBCellPercent; }
            set
            {
                if (this.m_NonSwitchedMemoryBCellPercent != value)
                {
                    this.m_NonSwitchedMemoryBCellPercent = value;
                    this.NotifyPropertyChanged("NonSwitchedMemoryBCellPercent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "10", "null", "varchar")]
        public string MarginalZoneBCellPercent
        {
            get { return this.m_MarginalZoneBCellPercent; }
            set
            {
                if (this.m_MarginalZoneBCellPercent != value)
                {
                    this.m_MarginalZoneBCellPercent = value;
                    this.NotifyPropertyChanged("MarginalZoneBCellPercent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "10", "null", "varchar")]
        public string ClassSwitchedMemoryBCellPercent
        {
            get { return this.m_ClassSwitchedMemoryBCellPercent; }
            set
            {
                if (this.m_ClassSwitchedMemoryBCellPercent != value)
                {
                    this.m_ClassSwitchedMemoryBCellPercent = value;
                    this.NotifyPropertyChanged("ClassSwitchedMemoryBCellPercent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "10", "null", "varchar")]
        public string NaiveBCellPercent
        {
            get { return this.m_NaiveBCellPercent; }
            set
            {
                if (this.m_NaiveBCellPercent != value)
                {
                    this.m_NaiveBCellPercent = value;
                    this.NotifyPropertyChanged("NaiveBCellPercent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "10", "null", "varchar")]
        public string TransitionalBCellPercent
        {
            get { return this.m_TransitionalBCellPercent; }
            set
            {
                if (this.m_TransitionalBCellPercent != value)
                {
                    this.m_TransitionalBCellPercent = value;
                    this.NotifyPropertyChanged("TransitionalBCellPercent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "10", "null", "varchar")]
        public string PlasmaBlastsPercent
        {
            get { return this.m_PlasmaBlastsPercent; }
            set
            {
                if (this.m_PlasmaBlastsPercent != value)
                {
                    this.m_PlasmaBlastsPercent = value;
                    this.NotifyPropertyChanged("PlasmaBlastsPercent");
                }
            }
        }        

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "10", "null", "varchar")]
        public string TotalNucleatedPercent
        {
            get { return this.m_TotalNucleatedPercent; }
            set
            {
                if (this.m_TotalNucleatedPercent != value)
                {
                    this.m_TotalNucleatedPercent = value;
                    this.NotifyPropertyChanged("TotalNucleatedPercent");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "10", "null", "varchar")]
        public string TotalLymphocytesPercent
        {
            get { return this.m_TotalLymphocytesPercent; }
            set
            {
                if (this.m_TotalLymphocytesPercent != value)
                {
                    this.m_TotalLymphocytesPercent = value;
                    this.NotifyPropertyChanged("TotalLymphocytesPercent");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Mature B-Cells Percent: " + this.m_MatureBCellsPlusPercent);
            result.AppendLine("21+ 38-: " + this.m_MatureBCellsMinusPercent);
            result.AppendLine("Memory B-Cell Percent: " + this.m_MemoryBCellPercent);
            result.AppendLine("Non-Switched Memory B-Cell Percent: " + this.m_NonSwitchedMemoryBCellPercent);
            result.AppendLine("Marginal ZoneB-Cell Percent: " + this.m_MarginalZoneBCellPercent);
            result.AppendLine("Class Switched Memory B-Cell Percent: " + this.m_ClassSwitchedMemoryBCellPercent);
            result.AppendLine("Naive B-Cell Percent: " + this.m_NaiveBCellPercent);
            result.AppendLine("Transitional B-Cell Percent: " + this.m_TransitionalBCellPercent);
            result.AppendLine("Plasmablasts Percent: " + this.m_PlasmaBlastsPercent);            
            result.AppendLine("Total Lymphs % of Nucleated Cells: " + this.m_TotalNucleatedPercent);
            result.AppendLine("B-Cell % of Total Lymphocytes: " + this.m_TotalLymphocytesPercent);

            return result.ToString();
        }
    }
}
