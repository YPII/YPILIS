using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class HPVStandingOrderHandler : StandingOrderHandler
    {
        private YellowstonePathology.Business.Domain.Physician m_Physician;

        public HPVStandingOrderHandler(YellowstonePathology.Business.Domain.Physician physician, YellowstonePathology.Business.Test.PanelSetOrderCollection panelSetOrderCollection)
            : base(panelSetOrderCollection)
        {
            this.m_Physician = physician;
            this.Initialize();
        }

        protected override void Initialize()
        {
            if (this.HasUnhandledStandingOrders() == true)
            {
				this.m_PanelSet = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
                this.m_StandingOrderMessage = "This physician has a standing order to reflex HPV. A Womens Health Profile should by ordered. ";
            }
        }

        public override bool HasUnhandledStandingOrders()
        {
            bool result = false;
            if (this.m_Physician.HPVStandingOrderCode != "0")
            {
                if (this.m_PanelSetOrderCollection.HasPanelSetBeenOrdered(116) == false)
                {
                    result = true;
                }
            }
            return result;
        }

        public override void Refresh()
        {
            this.Initialize();
        }
    }
}
