using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
    public class MetastaticStatusCollection : ObservableCollection<MetastaticStatusResult>
    {
        public MetastaticStatusCollection()
        {
            this.Add(new MetastaticStatusResult("Negative", "Negative", "0", "Her2 Negative"));
            this.Add(new MetastaticStatusResult("Negative", "Low Positive", "1+", "Her2 Low Expression"));
            this.Add(new MetastaticStatusResult("Negative", "Low Positive", "2+", "Her2 Low Expression"));            
            this.Add(new MetastaticStatusResult("Equivocal", "Low Positive", "1+", "Her2 Low Expression"));
            this.Add(new MetastaticStatusResult("Equivocal", "Low Positive", "2+", "Her2 Low Expression"));
            this.Add(new MetastaticStatusResult("Positive", "Any", "Any", "Her2 Positive"));
        }

        public MetastaticStatusResult GetResult(string ishResult, string ihcResult, string ihcScore)
        {            
            MetastaticStatusResult result = null;
            foreach(MetastaticStatusResult msr in this)
            {
                if(msr.ISHResult == ishResult && msr.IHCResult == ihcResult && msr.IHCScore == ihcScore)
                {
                    result = msr;
                }
            }
            if(result == null)
            {
                if(ishResult == "Positive")
                {
                    result = this.First(r => r.ISHResult == "Positive");
                }
            }
            return result;
        }
    }
}
