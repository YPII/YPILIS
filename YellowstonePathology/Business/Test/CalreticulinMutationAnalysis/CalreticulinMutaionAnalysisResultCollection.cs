using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CalreticulinMutationAnalysis
{
	public class CalreticulinMutationAnalysisResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
        public CalreticulinMutationAnalysisResultCollection()
		{
            this.Add(new CalreticulinMutationAnalysisResultDetected());
            this.Add(new CalreticulinMutationAnalysisResultNotDetected());
			this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
		}               
	}
}
