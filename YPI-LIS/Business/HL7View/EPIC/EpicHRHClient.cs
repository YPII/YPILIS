using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICHRHClient : Hl7Client
    {
        public EPICHRHClient() : base("EPIC", "HRH")
        {

        }
    }
}
