using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace YellowstonePathology.Business.Validation
{
    [Serializable()]
    public class BrokenRuleCollection : ObservableCollection<BrokenRuleItem>
    {     
        public BrokenRuleCollection()
        {
        
        }        
    }
}
