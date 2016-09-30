using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Interface
{
	public interface IRule
	{
		void Execute(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus);
        void Execute();        
	}
}
