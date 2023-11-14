using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.CMMC
{
    public class CMMCHl7Client : Hl7Client
    {
        public CMMCHl7Client() : base("CMMC", "AthenaHealth")
        {

        }
    }
}
