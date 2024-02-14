using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.API2MALT1ByPCR
{
    public class API2MALT1ByPCRResultCollection : YellowstonePathology.Business.Test.TestResultCollection
    {
        public API2MALT1ByPCRResultCollection()
        {
            this.Add(new API2MALT1ByPCRNegativeResult());
            this.Add(new API2MALT1ByPCRPositiveResult());
            this.Add(new YellowstonePathology.Business.Test.TestResultNoResult());
        }
    }
}
