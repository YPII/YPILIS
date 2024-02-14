using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICHl7Client : Hl7Client
    {
        public EPICHl7Client() : base("COPATH", "SVH")
        {

        }
    }
}
