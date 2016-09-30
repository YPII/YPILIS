using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
    public class EGFRToALKReflexAnalysisROS1Result : EGFRToALKReflexAnalysisElementResult
    {
        public EGFRToALKReflexAnalysisROS1Result(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, 
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResult egfrMutationAnalysisResult,
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder)
            : base(accessionOrder, 204)
        {
            if (egfrToALKReflexAnalysisTestOrder.QNSForROS1 == true)
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

                        if(this.m_Ordered == true)
                        {
                            if (this.m_Final == true)
                            {
                                YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHResultCollection ros1ByFISHResultCollection = new YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHResultCollection();
                                YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder)this.m_PanelSetOrder;
                                this.m_ResultAbbreviation = ros1ByFISHResultCollection.GetByResultCode(ros1ByFISHTestOrder.ResultCode).ResultAbbreviation;
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
                        if (this.m_Final == true)
                        {
                        	YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHResultCollection ros1ByFISHResultCollection = new YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHResultCollection();
                            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder = (YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder)this.m_PanelSetOrder;
                            this.m_ResultAbbreviation = ros1ByFISHResultCollection.GetByResultCode(ros1ByFISHTestOrder.ResultCode).ResultAbbreviation;
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
