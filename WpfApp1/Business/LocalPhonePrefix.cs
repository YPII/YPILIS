using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class LocalPhonePrefix : List<string>
    {
        public LocalPhonePrefix()
        {
            this.Add("200");
            this.Add("206");
            this.Add("208");
            this.Add("237");
            this.Add("238");
            this.Add("245");
            this.Add("247");
            this.Add("248");
            this.Add("252");
            this.Add("254");
            this.Add("255");
            this.Add("256");
            this.Add("259");
            this.Add("272");
            this.Add("281");
            this.Add("294");
            this.Add("318");
            this.Add("325");
            this.Add("371");
            this.Add("373");
            this.Add("384");
            this.Add("412");
            this.Add("413");
            this.Add("435");
            this.Add("534");
            this.Add("545");
            this.Add("561");
            this.Add("591");
            this.Add("598");
            this.Add("601");
            this.Add("606");
            this.Add("620");
            this.Add("647");
            this.Add("651");
            this.Add("652");
            this.Add("655");
            this.Add("656");
            this.Add("657");
            this.Add("661");
            this.Add("670");
            this.Add("671");
            this.Add("672");
            this.Add("690");
            this.Add("694");
            this.Add("696");
            this.Add("697");
            this.Add("698");
            this.Add("702");
            this.Add("717");
            this.Add("794");
            this.Add("831");
            this.Add("839");
            this.Add("850");
            this.Add("855");
            this.Add("860");
            this.Add("861");
            this.Add("867");
            this.Add("869");
            this.Add("876");
            this.Add("894");
            this.Add("896");
            this.Add("927");
            this.Add("969");
            this.Add("970");
            this.Add("998");
            this.Add("341");
            this.Add("645");
            this.Add("290");
            this.Add("298");
            this.Add("321");
            this.Add("322");
            this.Add("326");
            this.Add("328");
            this.Add("343");
            this.Add("348");
            this.Add("425");
            this.Add("426");
            this.Add("445");
            this.Add("446");
            this.Add("520");
            this.Add("530");
            this.Add("623");
            this.Add("628");
            this.Add("629");
            this.Add("633");
            this.Add("638");
            this.Add("639");
            this.Add("662");
            this.Add("663");
            this.Add("664");
            this.Add("665");
            this.Add("666");
            this.Add("667");
            this.Add("668");
            this.Add("669");
            this.Add("679");
            this.Add("699");
            this.Add("743");
            this.Add("780");
            this.Add("800");
            this.Add("812");
            this.Add("818");
            this.Add("875");
            this.Add("905");
            this.Add("940");
            this.Add("947");
            this.Add("953");
            this.Add("962");
            this.Add("967");
            this.Add("969");
        }

        public string HandleLongDistance(string phoneNumber)
        {
            string result = phoneNumber;
            string prefix = phoneNumber.Substring(3, 3);
            string area = phoneNumber.Substring(0, 3);
            if(area != "406")
            {
                result = "1" + phoneNumber;
            }
            else if(this.Exists(element => element == prefix) == false)
            {
                result = "1" + phoneNumber;
            }
            return result;
        }
    }
}
