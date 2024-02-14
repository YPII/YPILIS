using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Patient.Model
{
    public class BirthdateLastFirstRule : LinkingRule
    {
        private const int LevensteinDistanceThreshhold = 1;
        private YellowstonePathology.Business.Patient.Model.PatientLinkingListItem m_ItemToLink;
        private YellowstonePathology.Business.Patient.Model.PatientLinkingListItem m_ItemToMatch;

        public BirthdateLastFirstRule(YellowstonePathology.Business.Patient.Model.PatientLinkingListItem itemToLink, YellowstonePathology.Business.Patient.Model.PatientLinkingListItem itemToMatch)
        {
            this.m_ItemToLink = itemToLink;
            this.m_ItemToMatch = itemToMatch;
        }

        public LinkingRuleMatchCollection Match()
        {
            LinkingRuleMatchCollection linkingRuleMatchCollection = new LinkingRuleMatchCollection();                        
            base.Match(LinkingRuleMatchNameEnum.Birthdate, this.m_ItemToLink.PBirthdate, this.m_ItemToMatch.PBirthdate, LevensteinDistanceThreshhold, linkingRuleMatchCollection);
            base.Match(LinkingRuleMatchNameEnum.LastName, this.m_ItemToLink.PLastName, this.m_ItemToMatch.PLastName, LevensteinDistanceThreshhold, linkingRuleMatchCollection);
            base.Match(LinkingRuleMatchNameEnum.FirstName, this.m_ItemToLink.PFirstName, this.m_ItemToMatch.PFirstName, LevensteinDistanceThreshhold, linkingRuleMatchCollection);           

            return linkingRuleMatchCollection;
        }
    }
}
