using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class CSSlide : Aliquot
    {
        public CSSlide(string identificationType) : base("CS Slide", "CSSLD", identificationType)
        {
            
        }
    }
}
