using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Test.EGFRMutationAnalysis
{
    public class EGFRMutationAnalysisResult
    {        
        public static int PanelSetId = 60;
        public static string PanelSetName = "EGFR Mutation Analysis";        

        protected string m_Indication = "Lung adenocarcinoma";

        protected string m_Method = "DNA was isolated from the specimen using an automated DNA extraction system.  EGFR allele-specific PCR amplification of " +
            "exons 18 – 21 was then performed followed by an amplification using primers designed to detect both mutant and wild-type forms of the EGFR gene.  Utilizing " +
            "the Shifted Termination Assay, the presence of an EGFR point mutation causes termination of DNA chain synthesis during amplification. Thus, DNA strands are " +
            "formed with lengths that are specific to the particular EGFR point mutation present.  Deletions and insertions of exons 19 and 20 are also detected by fragment " +
            "analysis using capillary electrophoresis.  Mutations detected: E709X, G719X, S768I, T790M, L858R, L861Q, exon 19 deletions, and exon 20 insertions.  The assay " + 
            "sensitivity is 1% tumor involvement.";

        protected string m_References = "1. Janne PA, et al. Epidermal growth factor receptor mutations in non-small-cell lung cancer: implications for treatment and tumor biology. J Clin Oncol. 2005; 23:3227-34. \r\n" +
            "2. Lynch TJ, et al. Activating mutations in the epidermal growth factor receptor underlying responsiveness of non-small-cell lung cancer to gefitinib. N Engl J Med. 2004; 350:2129-39. \r\n" +
            "3. Sequist LV, et al. Response to treatment and survival of patients with non-small cell lung cancer undergoing somatic EGFR mutation testing. Oncologist. 2007;12:90-8.";        

        protected string m_Result;
        protected string m_ResultAbbreviation;
        protected string m_Interpretation;
        protected string m_Comment;
        protected bool m_ReflexToALKIsIndicated;
        protected string m_ReflexToALKComment;
        protected string m_ResultCode;

        public EGFRMutationAnalysisResult()
        {

        }

        public EGFRMutationAnalysisResult(string resultCode)
        {

        }

        public string Result
        {
            get { return this.m_Result; }
        }

        public string ResultAbbreviation
        {
            get { return this.m_ResultAbbreviation; }
        } 

        public string Indication
        {
            get { return this.m_Indication; }
        }

        public string Comment
        {
            get { return this.m_Comment; }
        }

        public string Method
        {
            get { return this.m_Method; }
        }

        public string References
        {
            get { return this.m_References; }
        }

        public bool ReflexToALKIsIndicated
        {
            get { return this.m_ReflexToALKIsIndicated; }
        }

        public string ReflexToALKComment
        {
            get { return this.m_ReflexToALKComment; }
        }

        public string ResultCode
        {
            get { return this.m_ResultCode; }
        }

        public void Clear(YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder)
        {
            egfrMutationAnalysisTestOrder.Result = null;
            egfrMutationAnalysisTestOrder.Comment = null;
            egfrMutationAnalysisTestOrder.Indication = null;
            egfrMutationAnalysisTestOrder.Method = null;
            egfrMutationAnalysisTestOrder.References = null;
            egfrMutationAnalysisTestOrder.TumorNucleiPercentage = null;
            egfrMutationAnalysisTestOrder.MicrodissectionPerformed = false;
            egfrMutationAnalysisTestOrder.Interpretation = null;
            egfrMutationAnalysisTestOrder.Mutation = null;
            egfrMutationAnalysisTestOrder.ResultCode = null;            
        }

        public virtual void SetResult(YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder, string mutation)
        {
            egfrMutationAnalysisTestOrder.Result = this.m_Result;
            egfrMutationAnalysisTestOrder.Comment = this.m_Comment;
            egfrMutationAnalysisTestOrder.Indication = this.m_Indication;
            egfrMutationAnalysisTestOrder.Method = this.m_Method;
            egfrMutationAnalysisTestOrder.References = this.m_References;
            egfrMutationAnalysisTestOrder.Interpretation = this.m_Interpretation;
            egfrMutationAnalysisTestOrder.ResultCode = this.m_ResultCode;            
        }        
    }
}
