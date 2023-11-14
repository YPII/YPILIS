using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Helper
{
	public class SsnHelper
	{
		public static string FormatWithoutDashes(string ssn)
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(ssn))
			{
				result = ssn.Replace("-", "");
			}
			return result;
		}
	}
}
