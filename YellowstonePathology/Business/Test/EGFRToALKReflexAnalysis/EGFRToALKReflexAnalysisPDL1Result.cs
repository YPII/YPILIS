/*
 * Created by SharpDevelop.
 * User: William.Copland
 * Date: 12/14/2015
 * Time: 12:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
	/// <summary>
	/// Description of EGFRToALKReflexAnalysisPDL1Result.
	/// </summary>
	public class EGFRToALKReflexAnalysisPDL1Result: EGFRToALKReflexAnalysisElementResult
    {
        public EGFRToALKReflexAnalysisPDL1Result(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, 
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResult egfrMutationAnalysisResult,
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder)
            : base(accessionOrder, 215)
        {
            if(egfrToALKReflexAnalysisTestOrder.DoNotPerformPDL1 == true)
            {
                this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.NotGoingToPerform;
            }
            else if (egfrToALKReflexAnalysisTestOrder.QNSForPDL1 == true)
            {
                this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.QNS;
            }
            else
            {
                if (egfrMutationAnalysisResult != null)
                {
                    YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult egfrMutationAnalysisDetectedResult = new EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
                    if (egfrMutationAnalysisResult.ResultCode == egfrMutationAnalysisDetectedResult.ResultCode)
                    {
                        this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.NotGoingToPerform; //NotIndicated;
                    }
                    else
                    {
                        if (this.m_Ordered == false)
                        {
                            this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.OrderRequired;
                        }
                        if (this.m_Final == true)
                        {                        	
                            YellowstonePathology.Business.Test.PDL1.PDL1TestOrder pdl1TestOrder = (YellowstonePathology.Business.Test.PDL1.PDL1TestOrder)this.m_PanelSetOrder;
                            this.m_ResultAbbreviation = pdl1TestOrder.StainPercent;
                        }
                    }
                }
                else
                {
                    this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.Pending;
                }
            }
        }
	}
}
