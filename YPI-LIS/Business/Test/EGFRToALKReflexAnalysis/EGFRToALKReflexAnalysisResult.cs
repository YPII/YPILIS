using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
    public class EGFRToALKReflexAnalysisResult
    {
        private EGFRToALKReflexAnalysisEGFRResult m_EGFRToALKReflexAnalysisEGFRResult;
        private EGFRToALKReflexAnalysisALKResult m_EGFRToALKReflexAnalysisALKResult;
        private EGFRToALKReflexAnalysisROS1Result m_EGFRToALKReflexAnalysisROS1RResult;
        private EGFRToALKReflexAnalysisPDL1SP142Result m_EGFRToALKReflexAnalysisPDL1SP142Result;
        private EGFRToALKReflexAnalysisPDL122C3Result m_EGFRToALKReflexAnalysisPDL122C3Result;
        private EGFRToALKReflexAnalysisBRAFResult m_EGFRToALKReflexAnalysisBRAFResult;

        public EGFRToALKReflexAnalysisResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, 
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder)
        {
            this.m_EGFRToALKReflexAnalysisEGFRResult = new EGFRToALKReflexAnalysisEGFRResult(accessionOrder);
            this.m_EGFRToALKReflexAnalysisALKResult = new EGFRToALKReflexAnalysisALKResult(accessionOrder);
            this.m_EGFRToALKReflexAnalysisROS1RResult = new EGFRToALKReflexAnalysisROS1Result(accessionOrder);
            this.m_EGFRToALKReflexAnalysisPDL1SP142Result = new EGFRToALKReflexAnalysisPDL1SP142Result(accessionOrder);
            this.m_EGFRToALKReflexAnalysisPDL122C3Result = new EGFRToALKReflexAnalysisPDL122C3Result(accessionOrder);
            this.m_EGFRToALKReflexAnalysisBRAFResult = new EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisBRAFResult(accessionOrder);
        }

        public EGFRToALKReflexAnalysisEGFRResult EGFRToALKReflexAnalysisEGFRResult
        {
            get { return this.m_EGFRToALKReflexAnalysisEGFRResult; }
        }

        public EGFRToALKReflexAnalysisALKResult EGFRToALKReflexAnalysisALKResult
        {
            get { return this.m_EGFRToALKReflexAnalysisALKResult; }
        }

        public EGFRToALKReflexAnalysisROS1Result EGFRToALKReflexAnalysisROS1Result
        {
            get { return this.m_EGFRToALKReflexAnalysisROS1RResult; }
        }

        public EGFRToALKReflexAnalysisPDL1SP142Result EGFRToALKReflexAnalysisPDL1SP142Result
        {
            get { return this.m_EGFRToALKReflexAnalysisPDL1SP142Result; }
        }

        public EGFRToALKReflexAnalysisPDL122C3Result EGFRToALKReflexAnalysisPDL122C3Result
        {
            get { return this.m_EGFRToALKReflexAnalysisPDL122C3Result; }
        }

        public EGFRToALKReflexAnalysisBRAFResult EGFRToALKReflexAnalysisBRAFResult
        {
            get { return this.m_EGFRToALKReflexAnalysisBRAFResult; }
        }
    }
}
