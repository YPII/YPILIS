using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class HPV1618IsRequiredAudit : AccessionOrderAudit
    {
        public HPV1618IsRequiredAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {

        }

        public override void Run()
        {            
			this.IsMarkedOnWomensHealthProfile(this.m_AccessionOrder);
            this.IsRequiredByReflexOrder(this.m_AccessionOrder);
            this.IsRequiredByStandingOrder(this.m_AccessionOrder);
        }

		private void IsMarkedOnWomensHealthProfile(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
			YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
			if (accessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
				if (womensHealthProfileTestOrder.OrderHPV1618 == true)
                {
					YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == false)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.AppendLine("An " + panelSetHPV1618.PanelSetName + " is required but not ordered.");
                    }
                }                            
            }
        }

        private void IsRequiredByStandingOrder(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
			YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
			if (accessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
				YellowstonePathology.Business.Client.Model.StandingOrder standingOrder = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetByStandingOrderCode(womensHealthProfileTestOrder.HPV1618StandingOrderCode);                
                if (standingOrder.IsRequired(accessionOrder) == true)
                {
					YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == false)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.AppendLine("An " + panelSetHPV1618.PanelSetName + " is required but not ordered.");
                    }
                }                
            }
        }

        private void IsRequiredByReflexOrder(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
			YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
			if (accessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
				YellowstonePathology.Business.Client.Model.ReflexOrder reflexOrder = YellowstonePathology.Business.Client.Model.ReflexOrderCollection.GetByReflexByOrderCode(womensHealthProfileTestOrder.HPV1618ReflexOrderCode);                

                if (reflexOrder.IsRequired(accessionOrder) == true)
                {
					YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == false)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.AppendLine("An " + panelSetHPV1618.PanelSetName + " is required but not ordered.");
                    }
                }                
            }
        }
    }
}
