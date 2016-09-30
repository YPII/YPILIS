using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class SpecimenAliquot : Aliquot
    {
        public SpecimenAliquot(string identificationType)
            : base("Specimen", "Specimen", identificationType)
        {
            
        }
    }
}
