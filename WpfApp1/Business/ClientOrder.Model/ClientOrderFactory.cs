using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class ClientOrderFactory
    {
        public static ClientOrder GetClientOrder(Nullable<int> panelSetId)
        {
            ClientOrder result = null;
            if (panelSetId.HasValue == true)
            {
                switch (panelSetId)
                {
                    case 13:
                        result = new SurgicalClientOrder();                        
                        break;
                    case 15:
                        result = new CytologyClientOrder();
                        break;                    
                    default:
                        result = new ClientOrder();
                        break;
                }
            }
            else
            {
                result = new ClientOrder();
            }            
            return result;
        }

        public static ClientOrder GetClientOrder(SpecimenTypeEnum specimenType)
        {
            ClientOrder result = null;
            switch (specimenType)
            {
                case SpecimenTypeEnum.Surgical:
                    result = new SurgicalClientOrder();
                    break;
                case SpecimenTypeEnum.ThinPrepPap:
                    result = new CytologyClientOrder();
                    break;
                default:
                    result = new ClientOrder();
                    break;
            }            
            return result;
        }

        public static ClientOrder GetSpecificClientOrder(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
        {
            ClientOrder result = null;            
            switch (panelSet.PanelSetId)
            {
                case 13:
                    result = new SurgicalClientOrder();
                    break;
                case 15:
                    result = new CytologyClientOrder();
                    break;                
            }            
            return result;
        }
    }
}
