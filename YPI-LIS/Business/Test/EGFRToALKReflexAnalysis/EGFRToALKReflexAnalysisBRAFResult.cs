using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis
{
    public class EGFRToALKReflexAnalysisBRAFResult : EGFRToALKReflexAnalysisElementResult
    {
        public EGFRToALKReflexAnalysisBRAFResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            : base(accessionOrder, 274)
            { }
    }
}
