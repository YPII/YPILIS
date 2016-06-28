using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules
{
	public class SimpleRule
	{
        protected List<Action> m_ActionList;				
        protected YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;                

		public SimpleRule(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
            this.m_ExecutionStatus = executionStatus;
			this.m_ActionList = new List<Action>();
		}        

        public List<Action> ActionList
        {
            get { return this.m_ActionList; }
            set { this.m_ActionList = value; }
        }

        public YellowstonePathology.Business.Rules.ExecutionStatus ExecutionStatus
        {
            get { return this.m_ExecutionStatus; }            
        }				        
              		
		public void Execute()
		{            
			foreach (Action action in this.m_ActionList)
			{
				action.Invoke();
				if (this.m_ExecutionStatus.IsExecutionHalted())
				{					                                        
                    if (this.m_ExecutionStatus.ContinueExecutionOnHalt == false)
                    {
                        break;
                    }
				}
			}
		}		
	}
}
