using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICCodyClient : Hl7Client
    {
        public EPICCodyClient() : base("EPIC", "CRH")
        {

        }
    }
}
