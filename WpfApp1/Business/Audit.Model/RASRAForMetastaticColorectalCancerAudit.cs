using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class RASRAForMetastaticColorectalCancerAudit : Audit
    {
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_InitialKeyWords;
        private YellowstonePathology.Business.Surgical.KeyWordCollection m_SecondaryKeyWords;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public RASRAForMetastaticColorectalCancerAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_InitialKeyWords = new Surgical.KeyWordCollection { "metastatic" };
            this.m_SecondaryKeyWords = new Surgical.KeyWordCollection { "carcinoma", "adenocarcinoma", "colon", "colorectal", "origin", "primary" };
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            YellowstonePathology.Business.Test.RASRAFPanel.RASRAFPanelTest rasRAFPanelTest = new Test.RASRAFPanel.RASRAFPanelTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(rasRAFPanelTest.PanelSetId) == false)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                if (this.IndicatorExists(surgicalTestOrder.Comment))
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.Append(rasRAFPanelTest.PanelSetName);
                }
                else
                {
                    foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
                    {
                        if (this.IndicatorExists(surgicalSpecimen.Diagnosis) == true)
                        {
                            this.m_Status = AuditStatusEnum.Failure;
                            this.m_Message.Append(rasRAFPanelTest.PanelSetName);
                            break;
                        }
                    }
                }
            }
        }

        private bool IndicatorExists(string searchString)
        {
            bool result = false;

            if (this.m_InitialKeyWords.WordsExistIn(searchString) == true)
            {
                if (this.m_SecondaryKeyWords.WordsExistIn(searchString) == true)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
