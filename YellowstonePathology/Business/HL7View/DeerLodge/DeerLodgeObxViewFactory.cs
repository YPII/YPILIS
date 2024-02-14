using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.DeerLodge
{
    public class DeerLodgeObxViewFactory
    {
        public static DeerLodgeObxView GetObxView(int panelSetId, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount, bool unsolicted, bool beakerTesting)
        {
            DeerLodgeObxView view = null;
            switch (panelSetId)
            {
                case 13:
                    view = new Business.Test.Surgical.SurgicalDeerLodgeObxView(accessionOrder, reportNo, obxCount);
                    break;                            
            }
            return view;
        }
    }
}
