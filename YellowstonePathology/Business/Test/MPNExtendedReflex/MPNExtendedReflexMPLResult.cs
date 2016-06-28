using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
    public class MPNExtendedReflexMPLResult : YellowstonePathology.Business.Audit.Model.Audit
    {               
        public MPNExtendedReflexMPLResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            bool messageIsSet = false;
            YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest jak2V617FTest = new JAK2V617F.JAK2V617FTest();
            YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder panelSetOrderJAK2V617F = (YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(jak2V617FTest.PanelSetId);
            if (panelSetOrderJAK2V617F.Final == true)
            {
                YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FDetectedResult jak2V617DetectedResult = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FDetectedResult();

                if (panelSetOrderJAK2V617F.ResultCode == jak2V617DetectedResult.ResultCode)
                {
                    this.m_Message = new StringBuilder(MPNExtendedReflexResult.NotClinicallyIndicated);
                    messageIsSet = true;
                }
            }

            if (messageIsSet == false)
            {
                YellowstonePathology.Business.Test.MPL.MPLTest panelSetMPL = new YellowstonePathology.Business.Test.MPL.MPLTest();
                if (accessionOrder.PanelSetOrderCollection.Exists(panelSetMPL.PanelSetId) == true)
                {
                    YellowstonePathology.Business.Test.MPL.PanelSetOrderMPL panelSetOrderMPL = (YellowstonePathology.Business.Test.MPL.PanelSetOrderMPL)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMPL.PanelSetId);
                    if (panelSetOrderMPL.Final == true)
                    {
                        this.m_Message = new StringBuilder(panelSetOrderMPL.Result);
                    }
                    else
                    {
                        this.m_Message = new StringBuilder(MPNExtendedReflexResult.PendingResult);
                    }
                }
                else
                {
                    YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest panelSetCalreticulinMutationAnalysis = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetCalreticulinMutationAnalysis.PanelSetId) == true)
                    {
                        YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder reportOrderCalreticulinMutationAnalysis = (YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetCalreticulinMutationAnalysis.PanelSetId);
                        if (reportOrderCalreticulinMutationAnalysis.Final == true)
                        {
                            YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisResultNotDetected calreticulinMutationAnalysisResultNotDetected = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisResultNotDetected();
                            YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisResultDetected calreticulinMutationAnalysisResultDetected = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisResultDetected();

                            if (reportOrderCalreticulinMutationAnalysis.ResultCode == calreticulinMutationAnalysisResultNotDetected.ResultCode)
                            {
                                this.m_ActionRequired = true;
                                this.m_Message = new StringBuilder(MPNExtendedReflexResult.PleaseOrder);
                            }
                            else if (reportOrderCalreticulinMutationAnalysis.ResultCode == calreticulinMutationAnalysisResultDetected.ResultCode)
                            {
                                this.m_Message = new StringBuilder(MPNExtendedReflexResult.NotClinicallyIndicated);
                            }
                        }
                        else
                        {
                            this.m_Message = new StringBuilder(MPNExtendedReflexResult.NotOrdered);
                        }
                    }
                    else
                    {
                        this.m_Message = new StringBuilder(MPNExtendedReflexResult.UnknownState);
                    }
                }
            }
        }

        public string Result
        {
            get { return this.m_Message.ToString(); }
        }
    }
}
