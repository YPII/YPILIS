using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHRareResult : PNHResult
	{        
        public PNHRareResult()
		{
			this.m_Result = "Rare cells with PNH phenotype";
            this.m_Comment = "Rare cells with PNH phenotype";
            this.m_ResultCode = "PNHRR";            
        }
	}
}
