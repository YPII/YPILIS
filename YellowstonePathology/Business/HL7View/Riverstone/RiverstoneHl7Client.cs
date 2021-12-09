using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.Riverstone
{
    public class RiverstoneHl7Client : Hl7Client
    {
        public RiverstoneHl7Client()
            : base("RVST", "RVST")
        {

        }
    }
}
