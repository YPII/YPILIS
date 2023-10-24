using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRulePAPResultCollection : ObservableCollection<HPVRule>
    {
        public HPVRulePAPResultCollection()
        { }

        public static HPVRulePAPResultCollection GetAll()
        {
            HPVRulePAPResultCollection result = new Model.HPVRulePAPResultCollection();
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

        public bool ExistByResultDescription(string description)
        {
            HPVRule result = this.FirstOrDefault(x => x.Description == description);
            return result == null ? false : true;
        }

        public HPVRule GetByResultDescription(string description)
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
