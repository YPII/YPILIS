using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTResultCollection : YellowstonePathology.Business.Test.TestResultCollection
	{
		public NGCTResultCollection()
		{
		}

		public static NGCTResultCollection GetAll()
		{
			NGCTResultCollection result = new NGCTResultCollection();
			result.Add(new NGCTCTNegativeResult());
			result.Add(new NGCTCTPositiveResult());
			result.Add(new NGCTNGPositiveResult());
			result.Add(new NGCTNGNegativeResult());
            result.Add(new NGCTInvalidResult());
			result.Add(new NGCTNoResult());

			return result;
		}

		public static NGCTResultCollection GetNGResultCollection()
		{
			NGCTResultCollection result = new NGCTResultCollection();
			result.Add(new NGCTNGNegativeResult());
			result.Add(new NGCTNGPositiveResult());
            result.Add(new NGCTInvalidResult());
            result.Add(new NGCTNoResult());

			return result;
		}

		public static NGCTResultCollection GetCTResultCollection()
		{
			NGCTResultCollection result = new NGCTResultCollection();
			result.Add(new NGCTCTNegativeResult());
			result.Add(new NGCTCTPositiveResult());
            result.Add(new NGCTInvalidResult());
            result.Add(new NGCTNoResult());

			return result;
		}

		public NGCTResult GetNGResult(NGCTTestOrder testOrder)
		{
			NGCTResult result = null;
			foreach (NGCTResult ngctResult in this)
			{
				if (ngctResult.ResultCode == testOrder.NGResultCode)
				{
					result = ngctResult;
					break;
				}
			}
			return result;
		}

		public NGCTResult GetCTResult(NGCTTestOrder testOrder)
		{
			NGCTResult result = null;
			foreach (NGCTResult ngctResult in this)
			{
				if (ngctResult.ResultCode == testOrder.CTResultCode)
				{
					result = ngctResult;
					break;
				}
			}
			return result;
		}
	}
}
