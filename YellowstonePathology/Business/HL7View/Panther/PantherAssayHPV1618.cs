using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.Panther
{
    public class PantherAssayHPV1618 : PantherAssay
    {
        public PantherAssayHPV1618()
            : base("GT HPV", "GT HPV", 62)
        {
            this.AnalyteList.Add("HPV 16 18/45");
        }
    }
}
