using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class Slide : Aliquot
    {
        public Slide(string identificationType) : base("Slide", "Slide", identificationType)
        {

        }
    }
}
