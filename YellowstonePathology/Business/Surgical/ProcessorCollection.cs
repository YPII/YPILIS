using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class ProcessorCollection : ObservableCollection<Processor>
    {
        public ProcessorCollection()
        {
            this.Add(new Cheech());
            this.Add(new Chong());            
        }
    }
}
