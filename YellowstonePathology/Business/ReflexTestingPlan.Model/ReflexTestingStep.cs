using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReflexTestingPlan.Model
{
    public class ReflexTestingStep
    {
        protected string m_StepName;
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;   
     
        protected bool m_HasPanelSetBeenOrdered;
        protected bool m_IsStepComplete;
        protected bool m_Stop;        
        protected ReflexTestingStep m_NextStep;
        protected YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;        

        public ReflexTestingStep()
        {
            this.m_Stop = false;
            this.m_IsStepComplete = false;
            this.m_HasPanelSetBeenOrdered = false;
        }

        public string StepName
        {
            get { return this.m_StepName; }
        }

        public bool HasPanelSetBeenOrdered
        {
            get { return this.m_HasPanelSetBeenOrdered; }
        }

        public bool IsStepComplete
        {
            get { return this.m_IsStepComplete; }
        }

        public bool Stop
        {
            get { return this.m_Stop; }
        }           

        public ReflexTestingStep NextStep
        {
            get { return this.m_NextStep; }
        }

        public YellowstonePathology.Business.PanelSet.Model.PanelSet PanelSet
        {
            get { return this.m_PanelSet; }
        }        

        public virtual void SetState(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;            
        }
    }
}
