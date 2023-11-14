using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules
{
	public class RuleExecutionStatusItem
	{
        string m_Description;
        bool m_ExecutionHalted;

        public RuleExecutionStatusItem()
        {

        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        public bool ExecutionHalted
        {
            get { return this.m_ExecutionHalted; }
            set { this.m_ExecutionHalted = value; }
        }
	}
}
