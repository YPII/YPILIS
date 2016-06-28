using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules
{
	public class RuleExecutionStatusList : ObservableCollection<RuleExecutionStatusItem>
	{
        public RuleExecutionStatusList()
        {

        }        

        public bool WasExecutionHalted()
        {
            bool halted = false;
            foreach (RuleExecutionStatusItem item in this)
            {
                if (item.ExecutionHalted == true)
                {
                    halted = true;
                    break;
                }
            }
            return halted;
        }
	}
}
