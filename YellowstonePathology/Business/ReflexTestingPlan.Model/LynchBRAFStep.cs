using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReflexTestingPlan.Model
{
    public class LynchBRAFStep : ReflexTestingStep
    {
        YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest m_BRAFV600EKTest;        

        public LynchBRAFStep()            
        {
            this.m_BRAFV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
        }

        public override void SetState(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            base.SetState(accessionOrder);

            YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest();
            this.m_BRAFV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();

            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_BRAFV600EKTest.PanelSetId) == true) // || this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasStandardReflexTest.PanelSetId) == true)
            {
                this.m_HasPanelSetBeenOrdered = true;
				YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetBrafPanelSetOrder();
                if (panelSetOrderBraf.Final == true)
                {
                    this.m_IsStepComplete = true;
					if (panelSetOrderBraf.Result == YellowstonePathology.Business.Test.BRAFV600EK.BRAFResult.Detected)
                    {
                        this.m_Stop = true;
                        this.m_IsStepComplete = true;
                    }
					else if (panelSetOrderBraf.Result == YellowstonePathology.Business.Test.BRAFV600EK.BRAFResult.NotDetected)
                    {
                        this.m_NextStep = new LynchMLH1ByPCRStep();
                    }
                }                
            }            
        }
    }
}
