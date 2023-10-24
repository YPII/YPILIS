using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Helper
{
    public class IdListHelper
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
    }
}
