using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CalreticulinMutationAnalysis
{
	public class CalreticulinMutationAnalysisResultDetected : YellowstonePathology.Business.Test.TestResult
    {            
        public CalreticulinMutationAnalysisResultDetected()
        {
            this.m_ResultCode = "CALMTTNRDTCTD";
            this.m_Result = "Detected";
        }                
    }
}
