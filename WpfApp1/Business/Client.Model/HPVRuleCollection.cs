using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRuleCollection : ObservableCollection<HPVRule>
    {
        public HPVRuleCollection()
        { }

        public static HPVRuleCollection GetHPVRuleAgeCollection()
        {
            HPVRuleCollection result = new Model.HPVRuleCollection();
            result.Add(new Model.HPVRuleAge1());
            result.Add(new Model.HPVRuleAge2());
            result.Add(new Model.HPVRuleAge3());
            result.Add(new Model.HPVRuleAge4());
            result.Add(new Model.HPVRuleAge5());
            result.Add(new Model.HPVRuleAge6());
            return result;
        }

        public static HPVRuleCollection GetHPVRulePAPResultCollection()
        {
            HPVRuleCollection result = new Model.HPVRuleCollection();
            result.Add(new HPVRulePAPResult1());
            result.Add(new HPVRulePAPResult2());
            result.Add(new HPVRulePAPResult3());
            result.Add(new HPVRulePAPResult4());
            result.Add(new HPVRulePAPResult5());
            result.Add(new HPVRulePAPResult6());
            result.Add(new HPVRulePAPResult7());
            result.Add(new HPVRulePAPResult8());
            result.Add(new HPVRulePAPResult9());
            return result;
        }

        public static HPVRuleCollection GetHPVRulePreviousTestingCollection()
        {
            HPVRuleCollection result = new Model.HPVRuleCollection();
            result.Add(new HPVRulePreviousTesting1());
            result.Add(new HPVRulePreviousTesting2());
            return result;
        }

        public static HPVRuleCollection GetHPVRuleEndocervicalCollection()
        {
            HPVRuleCollection result = new Model.HPVRuleCollection();
            result.Add(new HPVRuleEndoCervical1());
            result.Add(new HPVRuleEndoCervical2());
            return result;
        }

        public bool ExixtsByDescription(string description)
        {
            HPVRule result = this.FirstOrDefault(x => x.Description == description);
            return result == null ? false : true;
        }

        public HPVRule GetByDescription(string description)
        {
            HPVRule result = this.FirstOrDefault(x => x.Description == description);
            return result;
        }

        public HPVRuleAgeCollection SatisfiesCondition(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            HPVRuleAgeCollection result = new Model.HPVRuleAgeCollection();
            foreach (HPVRule rule in this)
            {
                if (rule.SatisfiesCondition(accessionOrder) == true)
                {
                    result.Add(rule);
                }
            }
            return result;
        }
    }
}
