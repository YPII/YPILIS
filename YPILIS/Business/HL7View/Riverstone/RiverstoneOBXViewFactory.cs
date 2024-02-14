using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.Riverstone
{
    public class RiverstoneOBXViewFactory
    {
        public static RiverstoneOBXView GetOBXView(int panelSetId, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
        {
            RiverstoneOBXView view = null;
            switch (panelSetId)
            {                
                case 13:
					view = new YellowstonePathology.Business.Test.Surgical.SurgicalRiverstoneOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 116:
                    view = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileRiverstoneOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 3:
                    view = new YellowstonePathology.Business.Test.NGCT.NGCTRiverstoneOBXView(accessionOrder, reportNo, obxCount);
                    break;
                case 14:
                    view = new YellowstonePathology.Business.Test.HPV.HPVRiverstoneObxView(accessionOrder, reportNo, obxCount);
                    break;
                case 400:
                    view = new YellowstonePathology.Business.Test.SARSCoV2.SARSCoV2RiverstoneOBXView(accessionOrder, reportNo, obxCount);
                    break;
            }
            return view;
        }        
    }
}
