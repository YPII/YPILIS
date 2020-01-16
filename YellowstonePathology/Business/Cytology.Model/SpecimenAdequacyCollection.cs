using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Cytology.Model
{
	public class SpecimenAdequacyCollection : ObservableCollection<SpecimenAdequacy>
	{
		public SpecimenAdequacyCollection()
		{

		}

        public SpecimenAdequacy Get(string code)
        {
            SpecimenAdequacy result = null;
            foreach(SpecimenAdequacy specimenAdequacy in this)
            {
                if(specimenAdequacy.ResultCode == code)
                {
                    result = specimenAdequacy;
                    break;
                }
            }
            return result;
        }
	}
}
