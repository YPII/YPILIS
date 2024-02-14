using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.CysticFibrosis
{
	public class CysticFibrosisEthnicGroupCollection : ObservableCollection<CysticFibrosisEthnicGroup>
	{
		public CysticFibrosisEthnicGroupCollection()
		{
			this.Add(new CysticFibrosisEthnicGroupAfricanAmerican());
			this.Add(new CysticFibrosisEthnicGroupAshkenaziJewish());
			this.Add(new CysticFibrosisEthnicGroupAsianAmerican());
			this.Add(new CysticFibrosisEthnicGroupEuropeanCaucasian());
			this.Add(new CysticFibrosisEthnicGroupHispanicAmerican());
			this.Add(new CysticFibrosisEthnicGroupNativeAmerican());
			this.Add(new CysticFibrosisEthnicGroupUnknown());
			this.Add(new CysticFibrosisEthnicGroupNull());
		}

        public CysticFibrosisEthnicGroup GetCysticFibrosisEthnicGroup(string cysticFibrosisEthnicGroupId)
        {
            CysticFibrosisEthnicGroup result = null;            
            foreach (CysticFibrosisEthnicGroup cysticFibrosisEthnicGroup in this)
            {
                if (cysticFibrosisEthnicGroup.EthnicGroupId == cysticFibrosisEthnicGroupId)
                {
                    result = cysticFibrosisEthnicGroup;
                }
            }            
            return result;
        }
	}
}
