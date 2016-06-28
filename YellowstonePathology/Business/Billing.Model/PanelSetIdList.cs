using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class PanelSetIdList : List<int>
    {
        public PanelSetIdList()
        {

        }

        public bool Exists(int panelSetId)
        {
            bool result = false;
            foreach (int i in this)
            {
                if (i == panelSetId)
                {
                    result = true;                    
                    break;
                }
            }
            return result;
        }
    }
}
