using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test
{
    public class FinalizeTestResult
    {
        private bool m_BoneMarrowSummaryIsSuggested;

        public FinalizeTestResult()
        { }

        public bool BoneMarrowSummaryIsSuggested
        {
            get { return this.m_BoneMarrowSummaryIsSuggested; }
            set { this.m_BoneMarrowSummaryIsSuggested = value; }
        }
    }
}
