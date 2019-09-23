using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class HPVReflexRuleRequiresCheck : AccessionOrderAudit
    {
        public HPVReflexRuleRequiresCheck(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            : base(accessionOrder)
        { }

        public override void Run()
        {
            this.DoesHPVAuditRequireHPVOrder();
        }

        private void DoesHPVAuditRequireHPVOrder()
        {
            this.ActionRequired = false;
            YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
                YellowstonePathology.Business.Audit.Model.HPVIsRequiredAudit hpvAudit = new HPVIsRequiredAudit(this.m_AccessionOrder);
                hpvAudit.Run();
                if (hpvAudit.ActionRequired == false)
                {
                    YellowstonePathology.Business.Client.Model.ReflexOrder reflexOrder = YellowstonePathology.Business.Client.Model.ReflexOrderCollection.GetByReflexByOrderCode(womensHealthProfileTestOrder.HPVReflexOrderCode);
                    if (reflexOrder.MeetsBaseRequirements(this.m_AccessionOrder) == true)
                    {
                        if (reflexOrder.HasNoPositiveHPVInLastYear(this.m_AccessionOrder) == false)
                        {
                            YellowstonePathology.Business.Test.HPV.HPVTest hpvTest = new Test.HPV.HPVTest();
                            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(hpvTest.PanelSetId) == false)
                            {
                                this.m_ActionRequired = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
