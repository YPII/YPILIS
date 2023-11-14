using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
    public class EGFRToALKReflexAnalysisROS1Result : EGFRToALKReflexAnalysisElementResult
    {
        public EGFRToALKReflexAnalysisROS1Result(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            : base(accessionOrder, 204)
        {
            
        }        
    }
}
