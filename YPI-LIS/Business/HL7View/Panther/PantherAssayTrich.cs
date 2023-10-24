using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.Panther
{
    public class PantherAssayTrich : PantherAssay
    {
        public PantherAssayTrich()
            : base("Aptima TV", "TRICH", 61)
        {
            this.AnalyteList.Add("TRICH");
        }
    }
}
