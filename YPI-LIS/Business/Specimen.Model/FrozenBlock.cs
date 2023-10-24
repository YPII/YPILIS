using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class FrozenBlock : Aliquot
    {
        public FrozenBlock(string identificationType) : base("Frozen Block", "FrozenBlock", identificationType)
        {
            
        }
    }
}
