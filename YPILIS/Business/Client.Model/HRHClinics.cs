using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HRHClinics : List<int>
    {
        public HRHClinics()
        {
            this.Add(1684);
            this.Add(650);
            this.Add(649);
            this.Add(1421);
        }

        public bool Exists(int clientId)
        {
            bool result = false;
            foreach (int id in this)
            {
                if(id == clientId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
