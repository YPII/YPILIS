using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.Orders
{
    public class ARUPMSH : MSH
    {
        public ARUPMSH()
            : base("LAB", "10549", "T")
        {

        }
    }
}
