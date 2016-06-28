using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.MPL
{
	public class MPLResultCollection : TestResultCollection
	{
        public MPLResultCollection()
		{
            this.Add(new MPLResultDetected());
            this.Add(new MPLResultNotDetected());                        
            this.Add(new TestResultNoResult());
		}               
	}
}
