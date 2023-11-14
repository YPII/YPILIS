using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class LocalPhonePrefix
    {
        public static bool IsLongDistance(string prefix)
        {
            bool result = true;

            List<string> localPrefixList = new List<string>();
            localPrefixList.Add("662");
            localPrefixList.Add("332");
            localPrefixList.Add("668");
            localPrefixList.Add("665");
            localPrefixList.Add("962");
            localPrefixList.Add("628");
            localPrefixList.Add("633");
            localPrefixList.Add("445");
            localPrefixList.Add("446");
            localPrefixList.Add("237");
            localPrefixList.Add("238");
            localPrefixList.Add("245");
            localPrefixList.Add("247");
            localPrefixList.Add("248");
            localPrefixList.Add("252");
            localPrefixList.Add("254");
            localPrefixList.Add("255");
            localPrefixList.Add("256");
            localPrefixList.Add("259");
            localPrefixList.Add("373");
            localPrefixList.Add("651");
            localPrefixList.Add("652");
            localPrefixList.Add("655");
            localPrefixList.Add("656");
            localPrefixList.Add("657");
            localPrefixList.Add("896");

            foreach (string item in localPrefixList)
            {
                if (item == prefix)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
