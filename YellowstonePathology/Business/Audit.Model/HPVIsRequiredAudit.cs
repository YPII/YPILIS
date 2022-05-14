using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class HPVIsRequiredAudit : AccessionOrderAudit
    {
        public HPVIsRequiredAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {

        }

        public override void Run()
        {            
			this.IsMarkedOnWomensHealthProfile(this.m_AccessionOrder);
            this.IsRequiredByReflexOrder(this.m_AccessionOrder);
            this.IsRequiredByASCCPRules(this.m_AccessionOrder);
        }

		private void IsMarkedOnWomensHealthProfile(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
			YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
			if (accessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
				if (womensHealthProfileTestOrder.OrderHPV == true)
                {
					YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == false)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.AppendLine("The order indicates that an " + panelSetHPV.PanelSetName + " is required but it has not ordered.");
                    }
                }
            }
        }

        private void IsRequiredByASCCPRules(Business.Test.AccessionOrder accessionOrder)
        {
            Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new Business.Test.WomensHealthProfile.WomensHealthProfileTest();
            YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);

            if (womensHealthProfileTestOrder.ManagePerASCCP == true || womensHealthProfileTestOrder.ManagePerASCCPWithCotest == true)
            {
                Business.ASCCPRule.Woman woman = new ASCCPRule.Woman();
                woman.FromAccessionOrder(accessionOrder);

                Business.ASCCPRule.RuleCollection ruleCollection = new Business.ASCCPRule.RuleCollection();
                Business.ASCCPRule.BaseRule matchingRule = ruleCollection.GetMatchingRule(woman);
                
                matchingRule.Finalize(woman);
                if (woman.PerformHPV == true)
                {
                    YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == false)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.AppendLine("A " + panelSetHPV.PanelSetName + " is required by ASCCP guidelines but is not ordered.");
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
				YellowstonePathology.Business.Client.Model.ReflexOrder reflexOrder = Business.Client.Model.ReflexOrderCollection.GetByReflexByOrderCode(womensHealthProfileTestOrder.HPVReflexOrderCode);

                if (reflexOrder.IsRequired(accessionOrder) == true)
                {
					YellowstonePathology.Business.Test.HPV.HPVTest panelSetHPV = new YellowstonePathology.Business.Test.HPV.HPVTest();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV.PanelSetId) == false)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.AppendLine("An " + panelSetHPV.PanelSetName + " is required but not ordered.");
                    }
                }
            }
        }
    }
}
