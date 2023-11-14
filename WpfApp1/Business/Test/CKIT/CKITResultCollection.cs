using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CKIT
{
	public class CKITResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{

		public CKITResultCollection()
		{
			this.Add(new CKITDetectedResult());
			this.Add(new CKITNotDetectedResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}
	}
}
