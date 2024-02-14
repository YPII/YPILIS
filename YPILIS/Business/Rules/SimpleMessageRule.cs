using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules
{
	public class SimpleMessageRule
	{
        protected List<Action> m_ActionList;				
        protected YellowstonePathology.Business.Rules.ExecutionMessage m_ExecutionMessage;

        public SimpleMessageRule(YellowstonePathology.Business.Rules.ExecutionMessage executionMessage)
		{
            this.m_ExecutionMessage = executionMessage;
			this.m_ActionList = new List<Action>();
		}        

        public List<Action> ActionList
        {
            get { return this.m_ActionList; }
            set { this.m_ActionList = value; }
        }

        public YellowstonePathology.Business.Rules.ExecutionMessage ExecutionMessage
        {
            get { return this.m_ExecutionMessage; }            
        }				        
              		
		public void Execute()
		{            
			foreach (Action action in this.m_ActionList)
			{
				action.Invoke();
                if (this.m_ExecutionMessage.Halted == true)
				{
                    if (this.m_ExecutionMessage.ContinueExecutionOnHalt == false)
                    {
                        break;
                    }
				}
			}
		}		
	}
}
