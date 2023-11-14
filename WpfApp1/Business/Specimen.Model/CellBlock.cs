using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class CellBlock : Aliquot
    {
        public CellBlock(string identificationType) : base("Cell Block", "CellBlock", identificationType)
        {
            
        }
    }
}
