using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Patient.Model
{
    public class LinkingRuleMatch
    {
        private LinkingRuleMatchNameEnum m_LinkingRuleMatchName;
        private bool m_IsMatch;
        private bool m_IsLevensteinDistanceMatch;
        private bool m_IsSoundexMatch;
        private bool m_IsStartsWithMatch;

        public LinkingRuleMatch(LinkingRuleMatchNameEnum linkingRuleMatchName)
        {
            this.m_LinkingRuleMatchName = linkingRuleMatchName;
        }

        public LinkingRuleMatchNameEnum LinkingRuleMatchName
        {
            get { return this.LinkingRuleMatchName; }
            set { this.m_LinkingRuleMatchName = value; }
        }

        public bool IsMatch
        {
            get { return this.m_IsMatch; }
            set { this.m_IsMatch = value; }
        }

        public bool IsLevensteinDistanceMatch
        {
            get { return this.m_IsLevensteinDistanceMatch; }
            set { this.m_IsLevensteinDistanceMatch = value; }
        }

        public bool IsSoundexMatch
        {
            get { return this.m_IsSoundexMatch; }
            set { this.m_IsSoundexMatch = value; }
        }

        public bool IsStartsWithMatch
        {
            get { return this.m_IsStartsWithMatch; }
            set { this.m_IsStartsWithMatch = value; }
        }
    }
}
