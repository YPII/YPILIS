using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReflexTestingPlan.Model
{
    public class ReflexTestingPlan
    {
        protected string m_Name;        
        protected ReflexTestingStep m_CurrentStep;
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected bool m_IsComplete;        
        protected bool m_HasCurrentStepPanelSetBeenOrdered;

        public ReflexTestingPlan(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, ReflexTestingStep currentStep)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_CurrentStep = currentStep;
            this.SetState();
        }

        private void SetState()
        {
            this.m_CurrentStep.SetState(this.m_AccessionOrder);
            this.m_HasCurrentStepPanelSetBeenOrdered = this.m_CurrentStep.HasPanelSetBeenOrdered;
            this.m_IsComplete = this.m_CurrentStep.Stop;            

            if (this.m_IsComplete == false)
            {
                if (this.m_CurrentStep.IsStepComplete == true)
                {
                    this.m_CurrentStep = this.m_CurrentStep.NextStep;
                    this.SetState();
                }
            }            
        }        

        public string Name
        {
            get { return this.m_Name; }
        }

        public ReflexTestingStep CurrentStep
        {
            get { return this.m_CurrentStep; }
        }        

        public bool IsComplete
        {
            get { return this.m_IsComplete; }
        }

        public bool HasCurrentStepPanelSetBeenOrdered
        {
            get { return this.m_HasCurrentStepPanelSetBeenOrdered; }
        }        
    }
}
