using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class GISTAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_GISTKeyWords;

        public GISTAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_GISTKeyWords = new Surgical.KeyWordCollection { "gastrointestinal stromal tumor", "GIST" };
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            Business.Test.PDGFRaMutaionAnlaysis.PDGFRaMutaionAnlaysisTest pdgfraMutaionAnlaysisTest = new Test.PDGFRaMutaionAnlaysis.PDGFRaMutaionAnlaysisTest();
            Business.Test.CKIT.CKITTest ckitTest = new Test.CKIT.CKITTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(pdgfraMutaionAnlaysisTest.PanelSetId) == false || this.m_AccessionOrder.PanelSetOrderCollection.Exists(ckitTest.PanelSetId) == false)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
                {
                    if (this.m_GISTKeyWords.WordsExistIn(surgicalSpecimen.Diagnosis) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.AppendLine(pdgfraMutaionAnlaysisTest.PanelSetName);
                        this.m_Message.AppendLine(ckitTest.PanelSetName);
                        break;
                    }
                }
            }
        }
    }
}
