using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.DeerLodge
{
    public class DeerLodgeHl7Client : Hl7Client
    {
        public DeerLodgeHl7Client() : base("EPIC", "DRLDG")
        {

        }
    }
}
