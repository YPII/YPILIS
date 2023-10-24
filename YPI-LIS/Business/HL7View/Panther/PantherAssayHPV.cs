using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.Panther
{
    public class PantherAssayHPV : PantherAssay
    {
        public PantherAssayHPV() 
            : base("HPV", "HPV", 14)
        {

        }
    }
}
