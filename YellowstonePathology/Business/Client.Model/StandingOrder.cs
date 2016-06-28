using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class StandingOrder
    {
        protected string m_StandingOrderCode;
        protected string m_Description;
        protected string m_PanelSetName;
        protected ReflexOrder m_ReflexOrder;

        public StandingOrder()
        {
            
        }        

        public virtual bool IsRequired(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            return this.m_ReflexOrder.IsRequired(accessionOrder);
        }


        public string StandingOrderCode
        {
            get { return this.m_StandingOrderCode; }
            set { this.m_StandingOrderCode = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        public string PanelSetName
        {
            get { return this.m_ReflexOrder.PanelSet.PanelSetName; }
        }

        public override string ToString()
        {
            return this.m_ReflexOrder.RuleNumber.ToString() + ".) " +  this.m_Description;
        }
    }
}
