using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardResultCollection : List<KRASStandardResult>
	{
		public KRASStandardResultCollection()
		{
		}

		public KRASStandardResult GetResult(string resultCode)
		{
			KRASStandardResult result = new KRASStandardResult();
			foreach (KRASStandardResult krasResult in this)
			{
				if (krasResult.ResultCode == resultCode)
				{
					result = krasResult;
					break;
				}
			}
			return result;
		}

		public KRASStandardResult GetResult(string resultString, string resultDescription)
		{
			KRASStandardResult result = new KRASStandardResult();
			foreach (KRASStandardResult krasResult in this)
			{
				if (krasResult.Result == resultString)
				{
					if (string.IsNullOrEmpty(resultDescription) == false)
					{
						if (krasResult.ResultDescription == resultDescription)
						{
							result = krasResult;
							break;
						}
					}
					else if (string.IsNullOrEmpty(krasResult.ResultDescription) == true)
					{
						result = krasResult;
						break;
					}
				}
			}
			return result;
		}

		public bool IsValid(string resultString, string resultDescription)
		{
			bool result = false;
			foreach (KRASStandardResult krasResult in this)
			{
				if (krasResult.Result == resultString)
				{
					if (string.IsNullOrEmpty(resultDescription) == false)
					{
						if (krasResult.ResultDescription == resultDescription)
						{
							result = true;
							break;
						}
					}
					else if (string.IsNullOrEmpty(krasResult.ResultDescription) == true)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		public static KRASStandardResultCollection GetAll()
		{
			KRASStandardResultCollection result = new KRASStandardResultCollection();
			result.Add(new KRASStandardNotDetectedResult());
			result.Add(new KRASStandardC34GTG12CDetectedResult());
			result.Add(new KRASStandardC34GCG12RDetectedResult());
			result.Add(new KRASStandardC34GAG12SDetectedResult());
			result.Add(new KRASStandardC35GCG12ADetectedResult());
			result.Add(new KRASStandardC35GAG12DDetectedResult());
			result.Add(new KRASStandardC35GTG12VDetectedResult());
			result.Add(new KRASStandardC37GTG13CDetectedResult());
			result.Add(new KRASStandardC37GCG13RDetectedResult());
			result.Add(new KRASStandardC37GAG13SDetectedResult());
			result.Add(new KRASStandardC38GCG13ADetectedResult());
			result.Add(new KRASStandardC38GAG13DDetectedResult());
			result.Add(new KRASStandardC38GTG13VDetectedResult());
			result.Add(new KRASStandardIndeterminateResult());
			result.Add(new KRASStandardInsufficientResult());

			return result;
		}
	}
}
