using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class TelephoneNumberAudit : Audit
    {
        private string m_TelephoneNumber;
        private string m_RegexPattern = @"(\([2-9]\d\d\)|[2-9]\d\d) ?[-.,]? ?[2-9]\d\d ?[-.,]? ?\d{4}";

        public TelephoneNumberAudit(string telephoneNumber)
        {
            this.m_TelephoneNumber = telephoneNumber;
        }

        public override void Run()
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(this.m_RegexPattern);
            System.Text.RegularExpressions.Match match = regex.Match(this.m_TelephoneNumber);
            if (match.Success == true)
            {
                this.m_ActionRequired = false;
                this.m_Message = new StringBuilder();
            }
            else
            {
                this.m_ActionRequired = true;
                this.m_Message = new StringBuilder("The telephone number is not valid.");
            }
        }
    }
}
