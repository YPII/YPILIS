using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View
{
    public class HolyRosaryHealthCare : Hl7Client
    {
        public HolyRosaryHealthCare()
            : base("BIGSKYDERMATOLOGY", "ECW")
        {

        }
    }
}
