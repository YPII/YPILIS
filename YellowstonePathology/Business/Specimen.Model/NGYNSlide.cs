using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class NGYNSlide : Aliquot
    {
        public NGYNSlide(string identificationType)
            : base("Non-GYN Slide", "NGYNSLD", identificationType)
        {
            
        }
    }
}
