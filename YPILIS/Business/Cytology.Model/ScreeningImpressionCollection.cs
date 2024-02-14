using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Cytology.Model
{
	public class ScreeningImpressionCollection : ObservableCollection<ScreeningImpression>
	{
		public ScreeningImpressionCollection()
		{

		}

        public ScreeningImpression Get(string fullResultCode)
        {
            ScreeningImpression result = null;
            foreach(ScreeningImpression screeningImpression in this)
            {
                if(fullResultCode.Substring(3,2) == screeningImpression.ResultCode)
                {
                    result = screeningImpression;
                    break;
                }
            }
            return result;
        }
	}
}
