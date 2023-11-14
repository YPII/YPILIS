using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.KRASStandardReflex
{
    public class KRASStandardReflexResultFactory
    {
        public static KRASStandardReflexResult GetResult(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            KRASStandardReflexResult result = null;

            YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder krasStandardReflexTestOrder = (YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

            YellowstonePathology.Business.Test.KRASStandard.KRASStandardTest krasStandardTest = new KRASStandard.KRASStandardTest();
            YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder krasStandardTestOrder = (YellowstonePathology.Business.Test.KRASStandard.KRASStandardTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasStandardTest.PanelSetId, krasStandardReflexTestOrder.OrderedOnId, true);

            YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new BRAFV600EK.BRAFV600EKTest();

            if (accessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, krasStandardReflexTestOrder.OrderedOnId, true) == false)
            {               
                result = new KRASStandardReflexKRASOnlyResult(reportNo, accessionOrder);
            }
            else
            {
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder brafV600EKTestOrder = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, krasStandardReflexTestOrder.OrderedOnId, true);
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKNotDetectedResult brafV600EKNotDetectedResult = new BRAFV600EK.BRAFV600EKNotDetectedResult();
                YellowstonePathology.Business.Test.KRASStandard.KRASStandardNotDetectedResult krasStandardNotDetectedResult = new KRASStandard.KRASStandardNotDetectedResult();
                YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKDetectedResult brafV600EKDetectedResult = new BRAFV600EK.BRAFV600EKDetectedResult();

                if (krasStandardTestOrder.ResultCode == krasStandardNotDetectedResult.ResultCode &&
                    brafV600EKTestOrder.ResultCode == brafV600EKNotDetectedResult.ResultCode)
                {
                    result = new KRASStandardReflexBothNotDetectedResult(reportNo, accessionOrder);
                }
                else if (krasStandardTestOrder.ResultCode == krasStandardNotDetectedResult.ResultCode &&
                    brafV600EKTestOrder.ResultCode == brafV600EKDetectedResult.ResultCode)
                {
                    result = new KRASStandardReflexKRASNotDetecedBRAFDetectedResult(reportNo, accessionOrder);
                }
                else
                {
                    result = new KRASStandardReflexKRASWithBRAFResult(reportNo, accessionOrder);
                }
            }
            return result;
        }
    }
}
