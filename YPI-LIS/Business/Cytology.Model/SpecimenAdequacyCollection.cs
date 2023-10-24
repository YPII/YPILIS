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

        public SpecimenAdequacy GetFromPAPResultCode(string papResultCode)
        {
            SpecimenAdequacy result = null;
            foreach (SpecimenAdequacy specimenAdequacy in this)
            {
                if (specimenAdequacy.ResultCode == papResultCode.Substring(1, 2))
                {
                    result = specimenAdequacy;
                    break;
                }
            }
            return result;
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
