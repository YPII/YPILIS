using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.ECW
{
    public class ECWHl7Client : Hl7Client
    {
        public ECWHl7Client()
            : base("ECW", "ECW")
        {

        }
    }
}
