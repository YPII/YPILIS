using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class Block : Aliquot
    {
        public Block(string identificationType) : base("Block", "Block", identificationType)
        {
            
        }
    }
}
