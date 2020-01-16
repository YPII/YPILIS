using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.ASCCPRule
{
    public class WomanCollection : ObservableCollection<Woman>
    {
        public WomanCollection()
        {
            
        }

        public void AddWoman(int startAge, int endAge, Woman woman)
        {
            for (int i = 28; i <= 32; i++)
            {
                woman.Age = i;
                this.Add(woman);
            }
        }
    }
}
