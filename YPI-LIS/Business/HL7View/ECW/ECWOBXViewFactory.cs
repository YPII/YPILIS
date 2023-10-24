using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.ECW
{
    public class ECWOBXViewFactory
    {
        public static ECWOBXView GetOBXView(int panelSetId, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount)
        {
            ECWOBXView view = null;
            switch (panelSetId)
            {                
                case 13:
					view = new YellowstonePathology.Business.Test.Surgical.SurgicalECWOBXView(accessionOrder, reportNo, obxCount);
                    break;                
			}
            return view;
        }        
    }
}
