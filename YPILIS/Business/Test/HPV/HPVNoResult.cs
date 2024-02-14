using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV
{
    public class HPVNoResult : HPVResult
    {
        public HPVNoResult()
        {
            this.m_ResultCode = null;
            this.m_Result = null;
        }
    }
}
