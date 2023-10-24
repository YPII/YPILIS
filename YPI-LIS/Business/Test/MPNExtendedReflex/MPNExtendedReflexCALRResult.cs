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
                this.m_Message = new StringBuilder(MPNExtendedReflexResult.NotOrdered);
            }
        }

        public string Result
        {
            get { return this.m_Message.ToString(); }
        }
    }
}
