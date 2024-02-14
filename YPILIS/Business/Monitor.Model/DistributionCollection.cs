using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Monitor.Model
{
    public class DistributionCollection : ObservableCollection<Distribution>
    {

        public DistributionCollection()
        {

        }

        public void SetState()
        {
            foreach (Distribution distribution in this)
            {
                distribution.SetState();             
            }
        }        
    }
}
