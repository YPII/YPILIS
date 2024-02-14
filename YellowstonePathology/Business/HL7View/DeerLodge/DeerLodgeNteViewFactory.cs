using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.DeerLodge
{
    public class DeerLodgeNteViewFactory
    {
        public static DeerLodgeNteView GetNteView(int panelSetId, Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            DeerLodgeNteView view = null;
            switch (panelSetId)
            {
                case 13:
                    view = new Business.Test.Surgical.SurgicalDeerLodgeNteView(accessionOrder, reportNo);
                    break;                
            }
            return view;
        }        
    }
}
