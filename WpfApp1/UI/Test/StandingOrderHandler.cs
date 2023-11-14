using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class StandingOrderHandler
    {
        
        protected YellowstonePathology.Business.Test.PanelSetOrderCollection m_PanelSetOrderCollection;
        protected string m_StandingOrderMessage;
        protected YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;        

        public StandingOrderHandler(YellowstonePathology.Business.Test.PanelSetOrderCollection panelSetOrderCollection)
        {            
            this.m_PanelSetOrderCollection = panelSetOrderCollection;            
        }        

        public string StandingOrderMessage
        {
            get { return this.m_StandingOrderMessage; }
            set { this.m_StandingOrderMessage = value; }
        }

        public YellowstonePathology.Business.PanelSet.Model.PanelSet PanelSet
        {
            get { return this.m_PanelSet; }
            set { this.m_PanelSet = value; }
        }

        protected virtual void Initialize()
        {
            throw new Exception("Not implemented here");
        }

        public virtual bool HasUnhandledStandingOrders()
        {
            return false;
        }

        public virtual void Refresh()
        {
            throw new Exception("Not implemented here");
        }
    }
}
