using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public class PhoneNumber
	{
        public static string Format(string faxNumber)
        {
            string result = string.Empty;
            if (faxNumber.Length == 10)
            {
                result = "(" + faxNumber.Substring(0, 3) + ")" + faxNumber.Substring(3, 3) + "-" + faxNumber.Substring(6, 4);
            }            
            return result;
        }
	}
}
