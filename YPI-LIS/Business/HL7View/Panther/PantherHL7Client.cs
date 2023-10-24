using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.Panther
{
    public class PantherHL7Client : Hl7Client
    {
        public PantherHL7Client()
            : base("Panther", "Hologic")
        {

        }
    }
}
