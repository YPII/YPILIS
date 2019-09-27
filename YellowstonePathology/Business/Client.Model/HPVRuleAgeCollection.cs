using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVRuleAgeCollection : ObservableCollection<HPVRule>
    {
        public HPVRuleAgeCollection()
        { }

        public static HPVRuleAgeCollection GetAll()
        {
            HPVRuleAgeCollection result = new Model.HPVRuleAgeCollection();
            result.Add(new Model.HPVRuleAge1());
            result.Add(new Model.HPVRuleAge2());
            result.Add(new Model.HPVRuleAge3());
            result.Add(new Model.HPVRuleAge4());
            result.Add(new Model.HPVRuleAge5());
            result.Add(new Model.HPVRuleAge6());
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
