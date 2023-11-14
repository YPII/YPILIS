using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YellowstonePathology.Business.Helper
{
    public class IdHelper
    {
        public static string ToIdString(List<int> intList)
        {
            string result = string.Empty;
            for (int i = 0; i < intList.Count; i++)
            {
                result = result + (intList[i]);
                if (i != intList.Count - 1)
                {
                    result = result + (", ");
                }
            }
            return result;
        }

        public static string ToIdString(List<string> stringList)
        {
            string result = string.Empty;
            for (int i = 0; i < stringList.Count; i++)
            {
                if (i == 0)
                {
                    result = "'";
                }
                result = result + (stringList[i]);
                if (i == stringList.Count - 1)
                {
                    result += "'";
                }
                if (i != stringList.Count - 1)
                {
                    result = result + ("', '");
                }
            }
            return result;
        }

        public static string CapitalConsonantsInString(string source)
        {
            string upper = source.ToUpper();
            string pattern = @"[^BCDFGHJ-NP-TVWXZ]";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(upper, "");
            return result;
        }
    }
}
