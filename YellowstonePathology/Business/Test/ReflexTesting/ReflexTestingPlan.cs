using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ReflexTesting
{
    public class ReflexTestingPlan : PanelSetOrder
    {
        protected ReflexTestingPlanStep m_FirstStep;
        protected ReflexTestingPlanStepCollection m_ReflexTestingPlanStepCollection;
		protected string m_StatusMessage;

		public ReflexTestingPlan() 
        {
            this.m_ReflexTestingPlanStepCollection = new ReflexTestingPlanStepCollection();            
        }

		public ReflexTestingPlan(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_ReflexTestingPlanStepCollection = new ReflexTestingPlanStepCollection();            
		}

        public virtual void OrderInitialTests(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
        {

        }
        
        public virtual void SetStatus(YellowstonePathology.Business.Test.PanelSetOrderCollection panelSetOrderCollection)
        {
            
        }

        public ReflexTestingPlanStepCollection ReflexTestingPlanStepCollection
        {
            get { return this.m_ReflexTestingPlanStepCollection; }
        }

		public string StatusMessage
		{
			get { return this.m_StatusMessage; }
			set
			{
                if (this.m_StatusMessage != value)
				{
                    this.m_StatusMessage = value;
                    this.NotifyPropertyChanged("StatusMessage");
				}
			}
		}       
    }
}
