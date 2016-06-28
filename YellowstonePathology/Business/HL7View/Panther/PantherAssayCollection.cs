using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.Panther
{
    public class PantherAssayCollection : ObservableCollection<PantherAssay>
    {
        public PantherAssayCollection()
        {            
            PantherAssayHPV hpv = new PantherAssayHPV();
            this.Add(hpv);

            PantherAssay ngct = new PantherAssayNGCT();            
            this.Add(ngct);

            PantherAssay hpv1618 = new PantherAssayHPV1618();            
            this.Add(hpv1618);

            PantherAssay trich = new PantherAssayTrich();
            this.Add(trich);
        }
    }
}
