using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.HL7View.Panther
{
    public class PantherAssayFactory
    {
        public static PantherAssay GetPantherAssay(Business.Test.PanelSetOrder panelSetOrder)
        {
            PantherAssay result = null;
            switch (panelSetOrder.PanelSetId)
            {
                case 14:
                    result = new Business.HL7View.Panther.PantherAssayHPV();
                    break;
                case 3:
                    result = new Business.HL7View.Panther.PantherAssayNGCT();
                    break;
                case 62:
                case 269:
                    result = new Business.HL7View.Panther.PantherAssayHPV1618();
                    break;
                case 61:
                    result = new Business.HL7View.Panther.PantherAssayTrich();
                    break;
                case 415:
                    result = new Business.HL7View.Panther.PantherAssayCOVID();
                    break;
                default:
                    throw new Exception(panelSetOrder.PanelSetName + " is mot implemented yet.");
            }
            return result;
        }
    }
}
