using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Trichomonas
{
    public class TrichomonasInvalidResult : TrichomonasResult
    {
        public TrichomonasInvalidResult()
        {
            this.m_ResultCode = "TRCHMNSNVLD";
            this.m_Result = "Invalid";
        }
    }
}
