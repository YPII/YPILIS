using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ReflexTesting
{
    public class ReflexTestingPlanStep
    {
        protected string m_StepId;
        protected string m_StepDescription;        
        protected bool m_Ordered;
        protected Nullable<DateTime> m_OrderDate;
        protected bool m_ResultIsFinal;
        protected Nullable<DateTime> m_ResultFinalDate;
        protected string m_StatusMessage;        

        public ReflexTestingPlanStep(string stepId, string stepDescription)
        {
            this.m_StepId = stepId;
            this.m_StepDescription = stepDescription;            
        }

        public string StepId
        {
            get { return this.m_StepId; }
        }

        public string StepDescription
        {
            get { return this.m_StepDescription; }
        }        

        public bool Ordered
        {
            get { return this.m_Ordered; }
            set { this.m_Ordered = value; }
        }

        public Nullable<DateTime> OrderDate
        {
            get { return this.m_OrderDate; }
            set { this.m_OrderDate = value; }
        }

        public bool ResultIsFinal
        {
            get { return this.m_ResultIsFinal; }
            set { this.m_ResultIsFinal = value; }
        }

        public Nullable<DateTime> ResultFinalDate
        {
            get { return this.m_ResultFinalDate; }
            set { this.m_ResultFinalDate = value; }
        }

        public string StatusMessage
        {
            get { return this.m_StatusMessage; }
            set { this.m_StatusMessage = value; }
        }

        public virtual void SetStatus(PanelSetOrderCollection panelSetOrderCollection)
        {
            throw new Exception("Not implemented here.");
        }

        public virtual bool IsOkToOrder()
        {
            throw new Exception("Not implemented here.");
        }        

        public virtual void Walk(PanelSetOrderCollection panelSetOrderCollection, ReflexTestingPlan reflexTestingPlan)
        {
            throw new Exception("Not implemented here.");
        }
    }
}
