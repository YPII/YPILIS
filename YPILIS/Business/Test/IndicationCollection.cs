using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
	public class IndicationCollection : ObservableCollection<Indication>
	{		
        public IndicationCollection()
		{

		}

        public Indication GetIndication(string indicationCode)
        {
            Indication result = null;
            foreach (Indication indication in this)
            {
                if (indication.IndicationCode == indicationCode)
                {
                    result = indication;
                }
            }
            return result;
        }

        public static IndicationCollection GetAll()
		{
            IndicationCollection result = new IndicationCollection();
            result.Add(new IndicationColorectalCancer());
            result.Add(new IndicationLungCancer());
			result.Add(new IndicationLynchSymdrome());
            result.Add(new IndicationMetastaticMelanoma());
            result.Add(new IndicationPapillaryThyroid());
            result.Add(new IndicationUnknownPrimary());
			return result;
		}
	}
}
