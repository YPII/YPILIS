using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
    public class SpecimenAdequacyTypes : ObservableCollection<string>
    {
        public SpecimenAdequacyTypes()
        {
            this.Add("Adequate");
            this.Add("Paucicellular");
            this.Add("Limited - Clotted");
            this.Add("Limited - Hemodilute");
            this.Add("Limited - Hemolyzed");
            this.Add("Poor Viability");            
        }
    }
}
