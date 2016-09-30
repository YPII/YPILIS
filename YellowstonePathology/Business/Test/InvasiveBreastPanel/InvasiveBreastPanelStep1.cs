using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.InvasiveBreastPanel
{
	public class InvasiveBreastPanelStep1 : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlanStepPanelSet
    {
        public InvasiveBreastPanelStep1()
			: base("HER2", "Step 1: HER2 Amplification by ISH", new YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest())
        {
            
        }

		public override void Walk(PanelSetOrderCollection panelSetOrderCollection, YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan reflexTestingPlan)
        {
            reflexTestingPlan.ReflexTestingPlanStepCollection.Add(this);
            this.SetStatus(panelSetOrderCollection);

            if (this.Ordered == true)
            {
                if (this.ResultIsFinal == true)
                {
                    reflexTestingPlan.StatusMessage = "HER2 is finalized.";
                }
                else
                {
                    reflexTestingPlan.StatusMessage = "HER2 is not finalized.";
                }
            }
            else
            {
                reflexTestingPlan.StatusMessage = "HER2 has not been ordered.";
            }
        }
    }
}
