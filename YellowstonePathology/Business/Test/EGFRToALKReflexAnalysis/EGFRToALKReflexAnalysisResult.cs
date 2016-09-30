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

        public EGFRToALKReflexAnalysisResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, 
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder)
        {
            this.m_EGFRToALKReflexAnalysisEGFRResult = new EGFRToALKReflexAnalysisEGFRResult(accessionOrder);
            this.m_EGFRToALKReflexAnalysisALKResult = new EGFRToALKReflexAnalysisALKResult(accessionOrder, this.m_EGFRToALKReflexAnalysisEGFRResult.EGFRMutationAnalysisResult, egfrToALKReflexAnalysisTestOrder);
            this.m_EGFRToALKReflexAnalysisROS1RResult = new EGFRToALKReflexAnalysisROS1Result(accessionOrder, this.m_EGFRToALKReflexAnalysisEGFRResult.EGFRMutationAnalysisResult, egfrToALKReflexAnalysisTestOrder);
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
    }
}
