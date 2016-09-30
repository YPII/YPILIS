using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
    public class EGFRToALKReflexAnalysisEGFRResult : EGFRToALKReflexAnalysisElementResult
    {
        YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResult m_EGFRMutationAnalysisResult;

        public EGFRToALKReflexAnalysisEGFRResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder, 60)
        {
            if (this.m_Final == true)
            {                
                YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)this.m_PanelSetOrder;
                this.m_EGFRMutationAnalysisResult = YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResultFactory.GetResult(egfrMutationAnalysisTestOrder.ResultCode);
                this.m_ResultAbbreviation = this.m_EGFRMutationAnalysisResult.ResultAbbreviation;                
            }           
        }

        public YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResult EGFRMutationAnalysisResult
        {
            get { return this.m_EGFRMutationAnalysisResult; }
        }
    }
}
