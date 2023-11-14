using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Patient.Model
{
    public class LinkingRuleMatchCollection : ObservableCollection<LinkingRuleMatch>
    {
        public LinkingRuleMatchCollection()
        {

        }

        public bool IsMatch()
        {
            bool result = true;
            foreach (LinkingRuleMatch linkingRuleMatch in this)
            {
                if (linkingRuleMatch.IsMatch == false)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
