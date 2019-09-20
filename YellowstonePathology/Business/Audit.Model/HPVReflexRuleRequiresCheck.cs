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
                YellowstonePathology.Business.Client.Model.ReflexOrder oldReflexOrder = null;

                switch (womensHealthProfileTestOrder.HPVReflexOrderCode)
                {
                    case "RFLXHPVRL17":
                        oldReflexOrder = new Client.Model.HPVReflexOrderRule14();
                        break;
                    case "RFLXHPVRL18":
                        oldReflexOrder = new Client.Model.HPVReflexOrderRule2();
                        break;
                    case "RFLXHPVRL19":
                        oldReflexOrder = new Client.Model.HPVReflexOrderRule14();
                        break;
                }
                if (oldReflexOrder != null)
                {
                    YellowstonePathology.Business.Audit.Model.HPVIsRequiredAudit hpvAudit = new HPVIsRequiredAudit(this.m_AccessionOrder);
                    hpvAudit.Run();
                    if (hpvAudit.ActionRequired == false)
                    {
                        if (oldReflexOrder.IsRequired(this.m_AccessionOrder) == true)
                        {
                            this.ActionRequired = true;
                        }
                    }
                }
            }
        }
    }
}
