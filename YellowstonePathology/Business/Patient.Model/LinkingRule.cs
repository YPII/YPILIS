using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Patient.Model
{
    public class LinkingRule
    {        
        public LinkingRule()
        {

        }
        
        protected void Match(LinkingRuleMatchNameEnum linkingRuleMatchName, Nullable<DateTime> date1, Nullable<DateTime> date2, int threshhold, LinkingRuleMatchCollection linkingRuleMatchCollection)
        {
            LinkingRuleMatch linkingRuleMatch = new LinkingRuleMatch(linkingRuleMatchName);
            if (date2.HasValue == true)
            {
                if (date1.Value == date2.Value)
                {
                    linkingRuleMatch.IsMatch = true;
                }
            }
            linkingRuleMatchCollection.Add(linkingRuleMatch);
        }         

        protected void Match(LinkingRuleMatchNameEnum linkingRuleMatchName, string word1, string word2, int threshhold, LinkingRuleMatchCollection linkingRuleMatchCollection)
        {
            LinkingRuleMatch linkingRuleMatch = new LinkingRuleMatch(linkingRuleMatchName);
            if (this.IsExactMatch(word1, word2) == true)
            {
                linkingRuleMatch.IsMatch = true;
            }            
            linkingRuleMatchCollection.Add(linkingRuleMatch);
        }

        private bool IsExactMatch(string word1, string word2)
        {
            bool result = false;
            if (string.IsNullOrEmpty(word1) == false)
            {
                if (word1.ToUpper() == word2.ToUpper())
                {
                    result = true;
                }
            }
            return result;
        }

        private bool IsLevensteinDistanceMatch(string word1, string word2, int threshhold)
        {
            bool result = false;
            if (string.IsNullOrEmpty(word1) == false)
            {
                int score = LevenshteinDistance.Run(word1.ToUpper(), word2.ToUpper());
                if (score <= threshhold)
                {
                    result = true;
                }
            }
			return result;
        }

        private bool IsSoundexMatch(string word1, string word2)
        {
            bool result = false;
            if (string.IsNullOrEmpty(word1) == false)
            {
                string value1 = SoundexGenerator.GetSoundex(word1.ToUpper());
                string value2 = SoundexGenerator.GetSoundex(word2.ToUpper());
                result = value1 == value2;
            }
			return result;
        }

        private bool IsStartsWithMatch(string word1, string word2)
        {
            bool result = false;
            if (string.IsNullOrEmpty(word1) == false)
            {
                if (word1.StartsWith(word2) == true) result = true;
                if (word2.StartsWith(word1) == true) result = true;
            }
            return result;
        }        
    }
}
