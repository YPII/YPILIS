using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PapCorrelationIsRequiredAudit : Audit
    {
        private YellowstonePathology.Business.Rules.Surgical.WordSearchList m_PapCorrelationWordList;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public PapCorrelationIsRequiredAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;

            this.m_PapCorrelationWordList = new Business.Rules.Surgical.WordSearchList();
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("ECC", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("CERVIX", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("CERVICAL", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("VAGINAL", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("ENDOCERVICAL", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("BLADDER", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("THYROID", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("BREAST", true, string.Empty));
            this.m_PapCorrelationWordList.Add(new Business.Rules.Surgical.WordSearchListItem("GALLBLADDER", false, string.Empty));
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            if (this.m_AccessionOrder.SpecimenOrderCollection.FindWordsInDescription(this.m_PapCorrelationWordList) == true)
            {
                    this.m_Status = AuditStatusEnum.Failure;
            }
        }
    }
}
