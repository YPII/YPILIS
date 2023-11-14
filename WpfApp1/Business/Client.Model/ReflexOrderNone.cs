using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class ReflexOrderNone : ReflexOrder
    {
        public ReflexOrderNone()
        {
            this.m_ReflexOrderCode = "RFLXNONE";
            this.m_Description = "No Reflex Order";
            this.m_PanelSet = new YellowstonePathology.Business.PanelSet.Model.PanelSet();
            this.m_PanelSet.PanelSetName = "None";
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            return false;
        }
    }
}
