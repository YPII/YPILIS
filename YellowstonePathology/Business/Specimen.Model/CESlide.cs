using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class CESlide : Aliquot
    {
        public CESlide(string identificationType)
            : base("CE Slide", "CESLD", identificationType)
        {
            
        }
    }
}
