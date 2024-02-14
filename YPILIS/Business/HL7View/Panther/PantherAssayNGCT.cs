using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.Panther
{
    public class PantherAssayNGCT : PantherAssay
    {
        public PantherAssayNGCT()
            : base("AptimaCombo2", "CT/GC", 3)
        {
            this.AnalyteList.Add("CT");
            this.AnalyteList.Add("GC");
        }
    }
}
