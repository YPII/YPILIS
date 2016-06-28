using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MPNExtendedReflex
{
    public class MPNExtendedReflexCALRResult : YellowstonePathology.Business.Audit.Model.Audit
    {
        public const string NotDetectedResult = "Not Detected";
        public const string DetectedResult = "Detected";

        public MPNExtendedReflexCALRResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
			YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest panelSetCalreticulinMutationAnalysis = new YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTest();            
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetCalreticulinMutationAnalysis.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder panelSetOrderCalreticulinMutationAnalysis = (YellowstonePathology.Business.Test.CalreticulinMutationAnalysis.CalreticulinMutationAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetCalreticulinMutationAnalysis.PanelSetId);
                if (panelSetOrderCalreticulinMutationAnalysis.Final == true)
                {
                    string calrResult = panelSetOrderCalreticulinMutationAnalysis.Result;
                    if (panelSetOrderCalreticulinMutationAnalysis.Result == DetectedResult)
                    {
                        calrResult = calrResult + "(" + panelSetOrderCalreticulinMutationAnalysis.Mutations + ")";
                    }
                    this.m_Message = new StringBuilder(calrResult);
                }
                else
                {
                    this.m_Message = new StringBuilder(MPNExtendedReflexResult.PendingResult);
                }
            }
            else
            {
				YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest panelSetJAK2V617F = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTest();
                if (accessionOrder.PanelSetOrderCollection.Exists(panelSetJAK2V617F.PanelSetId) == true)
                {
					YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder panelSetOrderJAK2V617F = (YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetJAK2V617F.PanelSetId);
                    if (panelSetOrderJAK2V617F.Final == true)
                    {
						YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FNotDetectedResult jak2V617NotDetectedResult = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FNotDetectedResult();
						YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FDetectedResult jak2V617DetectedResult = new YellowstonePathology.Business.Test.JAK2V617F.JAK2V617FDetectedResult();

                        if (panelSetOrderJAK2V617F.ResultCode == jak2V617NotDetectedResult.ResultCode)
                        {
                            this.m_ActionRequired = true;
                            this.m_Message = new StringBuilder(MPNExtendedReflexResult.PleaseOrder);
                        }
                        else if (panelSetOrderJAK2V617F.ResultCode == jak2V617DetectedResult.ResultCode)
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

        public string Result
        {
            get { return this.m_Message.ToString(); }
        }
    }
}
