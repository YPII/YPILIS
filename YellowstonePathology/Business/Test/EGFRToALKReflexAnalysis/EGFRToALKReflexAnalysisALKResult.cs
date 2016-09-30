using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
    public class EGFRToALKReflexAnalysisALKResult : EGFRToALKReflexAnalysisElementResult
    {
        public EGFRToALKReflexAnalysisALKResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResult egfrMutationAnalysisResult,
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder)
            : base(accessionOrder, 131)
        {
            if (egfrToALKReflexAnalysisTestOrder.QNSForALK == true)
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

                        if (this.m_Ordered == true)
                        {
                            if (this.m_Final == true)
                            {
                                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHResultCollection alkForNSCLCByFISHResultCollection = new ALKForNSCLCByFISH.ALKForNSCLCByFISHResultCollection();
                                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkForNSCLCByFISHTestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)this.m_PanelSetOrder;
                                this.m_ResultAbbreviation = alkForNSCLCByFISHResultCollection.GetByResultCode(alkForNSCLCByFISHTestOrder.ResultCode).ResultAbbreviation;
                                this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.Final;
                            }
                            else
                            {
                                this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.Pending;
                            }
                        }
                        else
                        {
                            this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.NotIndicated;
                        }

                        //this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.NotIndicated;
                    }
                    else
                    {
                        if (this.m_Ordered == false)
                        {
                            this.m_Status = EGFRToALKReflexAnalysisElementStatusEnum.OrderRequired;
                        }
                        else if (this.m_Final == true)
                        {
                            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHResultCollection alkForNSCLCByFISHResultCollection = new ALKForNSCLCByFISH.ALKForNSCLCByFISHResultCollection();
                            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkForNSCLCByFISHTestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)this.m_PanelSetOrder;
                            this.m_ResultAbbreviation = alkForNSCLCByFISHResultCollection.GetByResultCode(alkForNSCLCByFISHTestOrder.ResultCode).ResultAbbreviation;
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
