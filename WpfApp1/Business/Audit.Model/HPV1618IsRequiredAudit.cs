﻿using System;
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
            this.IsRequiredByASCCPRules(this.m_AccessionOrder);
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

                if (woman.ReflexToHPVGenotypes == true)
                {
                    YellowstonePathology.Business.Test.HPV1618.HPV1618Test panelSetHPV1618 = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPV1618.PanelSetId) == false)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.AppendLine("An " + panelSetHPV1618.PanelSetName + " is required by ASCCP guidelines but is not ordered.");
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
				YellowstonePathology.Business.Client.Model.ReflexOrder reflexOrder = Business.Client.Model.ReflexOrderCollection.GetByReflexByOrderCode(womensHealthProfileTestOrder.HPV1618ReflexOrderCode);                

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
