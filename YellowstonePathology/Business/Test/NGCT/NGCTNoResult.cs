using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTNoResult : NGCTResult
	{
		public NGCTNoResult()
		{
            this.m_NGResultCode = null;
            this.m_NeisseriaGonorrhoeaeResult = null;
            this.m_CTResultCode = null;
            this.m_ChlamydiaTrachomatisResult = null;
            this.m_Method = null;
			this.m_References = null;
			this.m_TestInformation = null;
		}
	}
}
