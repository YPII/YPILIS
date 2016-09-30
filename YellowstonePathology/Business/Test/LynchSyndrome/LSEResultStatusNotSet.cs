using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEResultStatusNotSet : LSEResultStatus
	{
		public LSEResultStatusNotSet(LSEResult lseResult, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string orderedOnId)
			: base(lseResult, accessionOrder, orderedOnId)
		{
			this.m_IsOrdered = false;
			this.m_Status = "The LSE Type must be set before a status can be set.";
		}

		public override bool IsMatch()
		{
			bool result = true;
			return result;
		}
	}
}
