using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ReflexTesting
{
    public class ReflexTestingPlanStepCollection :ObservableCollection<ReflexTestingPlanStep>
    {
        public ReflexTestingPlanStepCollection()
        {

        }

        public ReflexTestingPlanStep GetStep(string stepId)
        {
            ReflexTestingPlanStep result = null;
            foreach (ReflexTestingPlanStep step in this)
            {                
                if(step.StepId == stepId)
                {
                    result = step;
                }
            }
            return result;
        }
    }
}
