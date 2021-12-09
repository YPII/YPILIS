using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTCTNotPerformedResult : NGCTResult
	{
		public NGCTCTNotPerformedResult()
		{
			this.m_ResultCode = NGCTResult.NGNotPerformedResultCode;
			this.m_Result = NGCTResult.NotPerformedResult;
		}
	}
}
