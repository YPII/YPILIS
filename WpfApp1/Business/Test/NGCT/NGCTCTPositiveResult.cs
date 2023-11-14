using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTCTPositiveResult : NGCTResult
	{
		public NGCTCTPositiveResult()
		{
			this.m_ResultCode = NGCTResult.CTPositiveResultCode;
			this.m_Result = NGCTResult.PositiveResult;
		}
	}
}
