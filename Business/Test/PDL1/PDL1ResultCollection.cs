using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PDL1
{
    public class PDL1ResultCollection : TestResultCollection
    {
        public PDL1ResultCollection()
        {
            this.Add(new PDL1NegativeResult());
            this.Add(new PDL1PositiveResult());
            this.Add(new TestResultNoResult());
        }
    }
}
