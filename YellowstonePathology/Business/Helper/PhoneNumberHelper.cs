using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
