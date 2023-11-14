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

        protected string m_Method = "DNA was isolated from cells or microdissection-enriched FFPE tissue.  Formalin-fixed, paraffin-embedded " +
            "tumor tissue sections were deparaffinized and DNA was isolated.  EGFR tyrosine kinase domain mutations were evaluated in the " +
            "entirety of exons 18 to 21.  The patient’s sequence is compared to the EGFR sequence database NM_005228.  This assay is by Sanger " +
            "sequencing method with Locked Nucleic Acid (LNA) for T790M.  The sensitivity for detecting the T790M mutation in exon 20 is at " +
            "least 3% with the remaining mutations having a sensitivity of 10 to 15% for detecting mutated EGFR DNA in a wild-type background.  " +
            "Various factors including quantity and quality of nucleic acid, sample preparation and sample age can affect assay performance.";

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
            egfrMutationAnalysisTestOrder.ReportReferences = null;
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
            egfrMutationAnalysisTestOrder.ReportReferences = this.m_References;
            egfrMutationAnalysisTestOrder.Interpretation = this.m_Interpretation;
            egfrMutationAnalysisTestOrder.ResultCode = this.m_ResultCode;            
        }        
    }
}
