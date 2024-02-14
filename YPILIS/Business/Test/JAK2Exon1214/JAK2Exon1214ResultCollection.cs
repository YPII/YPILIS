using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.JAK2Exon1214
{
	public class JAK2Exon1214ResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
        public JAK2Exon1214ResultCollection()
		{
            this.Add(new JAK2Exon1214DetectedResult());
            this.Add(new JAK2Exon1214NotDetectedResult());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());	
		}		
	}
}
