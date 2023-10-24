using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.EMA
{
    public class EMAHl7Client : Hl7Client
    {
        public EMAHl7Client()
            : base("MOD7513", "MOD7513")
        {

        }
    }
}
