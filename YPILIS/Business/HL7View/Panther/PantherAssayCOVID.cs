using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.Panther
{
    public class PantherAssayCOVID : PantherAssay
    {
        public PantherAssayCOVID() 
            : base("SARSCoV2", "SARSCoV2", 415)
        {            
            this.AnalyteList.Add("SARSCoV2");
        }

        public override Business.Rules.MethodResult CanSendOrder(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder)
        {
            Business.Rules.MethodResult result = new Rules.MethodResult();
            result.Success = true;
            return result;
        }

        public override string GetOrderedOnId(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder)
        {
            return panelSetOrder.OrderedOnId;
        }
    }
}
