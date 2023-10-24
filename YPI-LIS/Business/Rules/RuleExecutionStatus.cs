using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules
{
	public class RuleExecutionStatus
	{
        RuleExecutionStatusList m_RuleExecutionStatusList;
        bool m_ExecutionHalted;
        bool m_ShowMessage;        

        public RuleExecutionStatus()
        {            
            this.m_RuleExecutionStatusList = new RuleExecutionStatusList();
            this.m_ShowMessage = true;
        }

        public void Reset()
        {            
            this.m_ExecutionHalted = false;
            this.m_ShowMessage = false;
            this.m_RuleExecutionStatusList.Clear();
        }

        public RuleExecutionStatusList RuleExecutionStatusList
        {
            get { return this.m_RuleExecutionStatusList; }
        }

        public bool ExecutionHalted
        {
            get { return this.RuleExecutionStatusList.WasExecutionHalted(); }
        }

        public bool ShowMessage
        {
            get { return this.m_ShowMessage; }
            set { this.m_ShowMessage = value; }
        }

        public void PopulateFromLinqExecutionStatus(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_ExecutionHalted = executionStatus.Halted;
            foreach (YellowstonePathology.Business.Rules.ExecutionMessage executionMessage in executionStatus.ExecutionMessages)
            {
                RuleExecutionStatusItem ruleExecutionstatusItem = new RuleExecutionStatusItem();
                ruleExecutionstatusItem.Description = executionMessage.Message;
                ruleExecutionstatusItem.ExecutionHalted = executionMessage.Halted;
                this.m_RuleExecutionStatusList.Add(ruleExecutionstatusItem);
            }
        }
	}
}
