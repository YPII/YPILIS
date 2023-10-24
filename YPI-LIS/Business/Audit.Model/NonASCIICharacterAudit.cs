using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class NonASCIICharacterAudit : Audit
    {
        YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;

        public NonASCIICharacterAudit(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            this.m_SurgicalTestOrder = surgicalTestOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            if (string.IsNullOrEmpty(this.m_SurgicalTestOrder.CancerSummary) == false)
            {
                StringBuilder nonASCIICharacters = new StringBuilder();
                string text = this.m_SurgicalTestOrder.CancerSummary;
                for (int i = 0; i < text.Length; ++i)
                {
                    char c = text[i];
                    if (((int)c) > 127)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        nonASCIICharacters.AppendLine("Character: " + text[i].ToString() + ", Code: " + ((int)c).ToString());
                    }
                }

                if(this.m_Status == AuditStatusEnum.Failure)
                {
                    this.m_Message.AppendLine("These Non ASCII characters were found:");
                    this.m_Message.AppendLine(nonASCIICharacters.ToString());
                }
            }
        }
    }
}
