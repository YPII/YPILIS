using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class SimulationCollection : ObservableCollection<Simulation>
    {
        public SimulationCollection()
        {
            this.Add(new UnsatSimulation());
            this.Add(new UnsatWithCotestSimulation());
            this.Add(new NormalSimulation());
            this.Add(new NormalWithCotestSimulation());
            this.Add(new AbnormalSimulation());
            this.Add(new AbnormalWithCotestSimulation());
        }
    }
}
