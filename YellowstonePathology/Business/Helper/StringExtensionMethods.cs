using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YellowstonePathology.Business.Helper
{
    public static class StringExtensionMethods
    {
        public static Boolean IsValidGUID(this String str)
        {
            bool result = false;

			if (IsNotBlank(str) == false) result = true;
			if (IsGUIDLengthCorrect(str) == false) result = true;
			if (IsGUIDFormatValid(str) == false) result = true;
			
            return result;
        }

		private static Boolean IsNotBlank(string str)
		{
			bool result = true;
			if (string.IsNullOrEmpty(str) == true)
			{
				result = false;
			}

			return result;
		}

		private static Boolean IsGUIDLengthCorrect(string str)
		{
			bool result = true;
			int correctLength = 36;
			if (str.Length != correctLength)
			{
				result = false;
			}

			return result;
		}

		private static Boolean IsGUIDFormatValid(string str)
		{
			bool result = false;
			Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);

			if (isGuid.IsMatch(str))
			{
				result = true;
			}

			return result;
		}

		public static Boolean IsValidContainerId(this String containerId)
		{
			bool result = true;
			if (string.IsNullOrEmpty(containerId) == false && containerId.Length == 40)
			{
				string prefix = containerId.Substring(0, 4).ToUpper();
				string guidString = containerId.Substring(4);
				if (prefix != "CTNR")
				{
					result = false;
				}

				if (guidString.IsValidGUID() == false)
				{
					result = false;
				}
			}
			else
			{
				result = false;
			}

			return result;
		}

		public static string SplitCapitalizedWords(this String source)
		{
			if (string.IsNullOrEmpty(source)) return String.Empty;
			StringBuilder newText = new StringBuilder(source.Length * 2);
			newText.Append(source[0]);
			for (int i = 1; i < source.Length; i++)
			{
				if (char.IsUpper(source[i]))
					newText.Append(' ');
				newText.Append(source[i]);
			}
			return newText.ToString();
		}

		public static string FormattedTelephoneNumber(this String source)
		{
			string result = source;
            if (string.IsNullOrEmpty(source) == false && source.Length == 10)
            {
                result =  "(" + source.Substring(0, 3) + ") " + source.Substring(3, 3) + "-" + source.Substring(6, 4);
            }
			return result;
		}
		
		public static string StringAsPercent(this String source)
		{
			string result = "0%";
			if(string.IsNullOrEmpty(source) == false) result = source + "%";
			return result;
		}
    }
}
