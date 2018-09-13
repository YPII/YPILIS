using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YellowstonePathology.Business.Helper
{
    public class PhoneNumberHelper
    {
        public static string FormatWithDashes(string phoneNumber)
        {
            string result = string.Empty;
            if (phoneNumber.Length == 10)
            {
                result = "(" + phoneNumber.Substring(0, 3) + ") " + phoneNumber.Substring(3, 3) + "-" + phoneNumber.Substring(6, 4);
            }
            return result;
        }

        public static string CorrectPhoneNumber(string numberIn)
        {
            if (string.IsNullOrEmpty(numberIn) == true) return numberIn;

            string result = Regex.Replace(numberIn, @"[^\d]", String.Empty);

            if (result.Length == 10 || result.Length == 7) return result;
            if (result.Length == 11 && result[0] == '1') return result.Remove(0, 1);
            if (result.Length == 11 && result[0] == '9') return result.Remove(0, 1);
            if (result.Length == 12 && result[0] == '9' && result[1] == '1') return result.Remove(0, 2);

            return result;
        }
    }
}
