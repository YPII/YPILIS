using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

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
