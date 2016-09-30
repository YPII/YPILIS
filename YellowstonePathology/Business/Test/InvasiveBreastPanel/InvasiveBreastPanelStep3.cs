using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.InvasiveBreastPanel
{
	public class InvasiveBreastPanelStep3 : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlanStepTest
    {
        public InvasiveBreastPanelStep3()
            : base("ER", "Step 3: Progesterone Receptor, Semi-quantitative", new YellowstonePathology.Business.Test.Model.ProgesteroneReceptorSemiquant())
        {
            
        }

		public override void Walk(PanelSetOrderCollection panelSetOrderCollection, YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan reflexTestingPlan)
        {
            reflexTestingPlan.ReflexTestingPlanStepCollection.Add(this);
            this.SetStatus(panelSetOrderCollection);

            reflexTestingPlan.StatusMessage += System.Environment.NewLine;
            if (panelSetOrderCollection.HasTestBeenOrdered(this.m_Test.TestId) == true)
            {
                reflexTestingPlan.StatusMessage += "Progesterone Receptor, Semi-quantitative has been ordered.";
            }
            else
            {
                reflexTestingPlan.StatusMessage += "Progesterone Receptor, Semi-quantitative has not been ordered.";
            }         
        }
    }
}
