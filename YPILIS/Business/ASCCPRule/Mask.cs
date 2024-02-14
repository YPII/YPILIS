using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class Mask
    {
        public const int COTESTSTART = 0;
        public const int COTESTLEN = 1;

        public const int AGESTART = 1;
        public const int AGELEN = 2;

        public const int SASTART = 3;
        public const int SALEN = 1;

        public const int SISTART = 3;
        public const int SILEN = 1;

        public const int ECTZSTART = 4;
        public const int ECTZLEN = 1;

        public static Woman MakeWoman(string mask)
        {
            Woman result = new Woman();
            //result.Cotesting = mask.Substring(COTESTSTART, COTESTLEN);
            return result;
        }        
    }
}
