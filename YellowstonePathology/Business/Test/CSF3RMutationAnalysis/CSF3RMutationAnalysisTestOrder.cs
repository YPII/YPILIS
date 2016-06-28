using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.CSF3RMutationAnalysis
{
    [PersistentClass("tblCSF3RMutationAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class CSF3RMutationAnalysisTestOrder : PanelSetOrder
    {
        private string m_Result;
        private string m_Interpretation;
        private string m_Comment;
        private string m_Method;
        private string m_References;
        private string m_ASR;

        public CSF3RMutationAnalysisTestOrder()
        {
            this.m_Method = "The CSF3R Mutation Analysis test is performed by a bi-directional sequencing method.  Exon 14 and exon 17 " +
                "mutation hot spots are analyzed using GenBank Acce# NG_016270 as reference.  This assay has a typical sensitivity of 10-15% " +
                "for detecting mutated CSF3R DNA in a wild-type background.  Nucleic acid can be isolated from plasma, peripheral blood " +
                "cells, bone marrow or fixed cyto pellets.  Various factors including quantity and quality of nucleic acid, sample preparation " +
                "and sample age can affect assay performance.";
            this.m_Interpretation = "The Colony Stimulating Factor 3 Receptor (CSF3R) gene is mutated in 59% of chronic neutrophilic leukemia " +
                "(CNL) or atypical chronic myeloid leukemia (aCML).  CSF3R mutation has been reported in 30 - 80 % of leukemia arising in " +
                "patients with severe congenital neutropenia.  It has also been reported in 4 % of colon cancer and 3 % of small cell / squamous " +
                "cell lung cancer.  The presence of CSF3R mutation is important for diagnosis, classification, and prognosis of chronic " +
                "myeloproliferative neoplasms.";
            this.m_References = "1.Maxson JE, Gotlib J, Pollyea DA, et al. Oncogenic CSF3R mutations in chronic neutrophilic leukemia and " +
                "atypical CML. N Engl J Med. 2013; 368(19):1781 - 90." + Environment.NewLine + Environment.NewLine +
                "2.Pardanani A, Lasho TL, Laborde RR, et al. CSF3R T618I is a highly prevalent and specific mutation in chronic neutrophilic " +
                "leukemia. Leukemia. 2013; 27(9):1873 - 3.";
            this.m_ASR = "The performance characteristics of this test have been determined by NeoGenomics Laboratories.  This test has not " +
                "been approved by the FDA. The FDA has determined such clearance or approval is not necessary.  This laboratory is CLIA " +
                "certified to perform high complexity clinical testing.";
        }

        public CSF3RMutationAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
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
        public string ASR
        {
            get { return this.m_ASR; }
            set
            {
                if (this.m_ASR != value)
                {
                    this.m_ASR = value;
                    this.NotifyPropertyChanged("ASR");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Result: " + this.m_Result);
            result.AppendLine();

            result.AppendLine("Interpretation: " + this.m_Interpretation);
            result.AppendLine();

            return result.ToString();
        }
    }
}
