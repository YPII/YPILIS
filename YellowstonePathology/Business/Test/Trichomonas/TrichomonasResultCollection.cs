using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Trichomonas
{
	public class TrichomonasResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public TrichomonasResultCollection()
		{
			this.Add(new TrichomonasNegativeResult());
			this.Add(new TrichomonasPositiveResult());
            this.Add(new TrichomonasInvalidResult());
			this.Add(new Business.Test.TestResultNoResult());
		}
	}
}
