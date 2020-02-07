using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class ASCCPAudit : AccessionOrderAudit
    {
        private Business.ASCCPRule.Woman m_Woman;
        private bool m_IsOkToRunAudit;

        public ASCCPAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            : base(accessionOrder)
        {

        }

        public override void Run()
        {
            Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womansHealthProfileTestOrder = (Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetWomensHealthProfile();
            if(womansHealthProfileTestOrder.Final == true)
            {
                this.m_IsOkToRunAudit = true;
                Business.Cytology.Model.OrderTypeCollection orderTypeCollection = new Cytology.Model.OrderTypeCollection();
                this.m_Woman = new ASCCPRule.Woman();
                this.m_Woman.FromAccessionOrder(this.m_AccessionOrder);

                if(this.m_Woman.OrderType != null)
                {
                    Business.ASCCPRule.BaseRule baseRule = Business.ASCCPRule.RuleFactory.GetUnsatRule(this.m_Woman);
                    baseRule.FinalizePap(this.m_Woman);
                    baseRule.FinalizeHPV(this.m_Woman);
                    baseRule.FinalizeGenotyping(this.m_Woman);
                }
                else
                {
                    this.m_Woman.RuleIsMatch = false;
                }
            }
            else
            {
                this.m_IsOkToRunAudit = false;
            }
        }

        public bool IsOkToRunAudit
        {
            get { return this.m_IsOkToRunAudit; }
        }

        public Business.ASCCPRule.Woman Woman
        {
            get { return this.m_Woman; }
        }
    }
}
