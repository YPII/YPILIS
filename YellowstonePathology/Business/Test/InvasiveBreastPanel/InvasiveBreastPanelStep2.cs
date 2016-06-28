using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.InvasiveBreastPanel
{
	public class InvasiveBreastPanelStep2 : YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlanStepTest
    {
        public InvasiveBreastPanelStep2()
            : base("ER", "Step 2: Estrogen Receptor, Semi-quantitative", new YellowstonePathology.Business.Test.Model.EstrogenReceptorSemiquant())
        {
            
        }

		public override void Walk(PanelSetOrderCollection panelSetOrderCollection, YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan reflexTestingPlan)
        {
            reflexTestingPlan.ReflexTestingPlanStepCollection.Add(this);
            this.SetStatus(panelSetOrderCollection);

            reflexTestingPlan.StatusMessage += System.Environment.NewLine;
            if (panelSetOrderCollection.HasTestBeenOrdered(this.m_Test.TestId) == true)
            {
                reflexTestingPlan.StatusMessage += "Estrogen Receptor, Semi-quantitative has been ordered.";
            }
            else
            {
                reflexTestingPlan.StatusMessage += "Estrogen Receptor, Semi-quantitative has not been ordered.";
            }
        }
    }
}
