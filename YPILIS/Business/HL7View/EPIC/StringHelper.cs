using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class StringHelper
    {
        public static string ReplaceSpecialCharacters(string fieldValue)
        {
            string result = fieldValue;
            if (string.IsNullOrEmpty(fieldValue) == false)
            {
                result = result.Trim();
                result = result.Replace("\r\n", @"\.br\").Replace("\n", @"\.br\").Replace("\r", @"\.br\");
                result = result.Replace("&", @"\T\");
                result = result.Replace("~", @"\R\");
                result = result.Replace("^", @"\S\");
            }
            return result;
        }
    }
}
