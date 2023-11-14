using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Flow
{
    public class CytoplasmicMarkerMappingCollection : ObservableCollection<CytoplasmicMarkerMapping>
    {
        public CytoplasmicMarkerMappingCollection()
        {
            this.Add(new CytoplasmicMarkerMapping("CD79a", "49", "28"));
            this.Add(new CytoplasmicMarkerMapping("CD3", "4", "69"));
            this.Add(new CytoplasmicMarkerMapping("Mu", "70", "71"));
            this.Add(new CytoplasmicMarkerMapping("CD22", "17", "72"));
            this.Add(new CytoplasmicMarkerMapping("CD4", "5", "74"));
            this.Add(new CytoplasmicMarkerMapping("Kappa", "32", "79"));
            this.Add(new CytoplasmicMarkerMapping("Lambda", "31", "80"));
        }

        public int CountOfCytoplasmicDups(FlowMarkerCollection flowMarkerCollection)
        {
            int count = 0;
            foreach(CytoplasmicMarkerMapping mapping in this)
            {
                if(flowMarkerCollection.Exists(mapping.RegularMarkerId) && flowMarkerCollection.Exists(mapping.CytoplasmicMarkerId) == true)
                {
                    count += 1;
                }
            }
            return count;
        }
    }
}
