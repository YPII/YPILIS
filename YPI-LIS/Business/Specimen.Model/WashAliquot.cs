using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class WashAliquot : Aliquot
    {
        public WashAliquot(string identificationType)
            : base("Wash", "Wash", identificationType)
        {
            
        }
    }
}
