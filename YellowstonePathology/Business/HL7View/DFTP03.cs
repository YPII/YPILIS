using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.HL7View
{
    public class DFTP03 : Hl7MessageType
    {
        public DFTP03() : base("DFT", "P03", string.Empty) { }
    }
}
