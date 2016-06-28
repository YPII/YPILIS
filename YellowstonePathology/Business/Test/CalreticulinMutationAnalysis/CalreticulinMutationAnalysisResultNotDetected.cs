using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.CalreticulinMutationAnalysis
{
	public class CalreticulinMutationAnalysisResultNotDetected : YellowstonePathology.Business.Test.TestResult
    {            
        public CalreticulinMutationAnalysisResultNotDetected()
        {
            this.m_ResultCode = "CALMTTNRNTDTCTD";
            this.m_Result = "Not Detected";            
        }                
    }
}
