using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSETypeCollection : ObservableCollection<string>
    {
        public LSETypeCollection()
        {
            this.Add(LSEType.NOTSET);
            this.Add(LSEType.COLON);
            this.Add(LSEType.GYN);
        }
    }
}
