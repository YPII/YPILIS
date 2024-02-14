using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules
{
	public class RuleSet
	{
        protected List<YellowstonePathology.Business.Interface.IRule> m_RuleList;
        protected ExecutionStatus m_ExecutionStatus;

        public RuleSet()
        {
            this.m_RuleList = new List<YellowstonePathology.Business.Interface.IRule>();
        }

        public RuleSet(ExecutionStatus executionStatus)
        {
            this.m_RuleList = new List<YellowstonePathology.Business.Interface.IRule>();
            this.m_ExecutionStatus = executionStatus;
        }

        public List<YellowstonePathology.Business.Interface.IRule> RuleList
        {
            get { return this.m_RuleList; }
            set { this.m_RuleList = value; }
        }

        public void Execute()
        {
            foreach (YellowstonePathology.Business.Interface.IRule rule in this.m_RuleList)
            {
                if (this.m_ExecutionStatus != null)
                {
                    rule.Execute(this.m_ExecutionStatus);
                }
                else
                {
                    rule.Execute();
                }
            }
        }
	}
}
