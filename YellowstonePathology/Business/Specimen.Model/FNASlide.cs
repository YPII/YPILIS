using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class FNASlide : Aliquot
    {
        public FNASlide(string identificationType)
            : base("FNA Slide", "FNASLD", identificationType)
        {
            
        }
    }
}
