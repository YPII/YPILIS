using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardResultSelectionCollection : ObservableCollection<string>
	{
		public KRASStandardResultSelectionCollection()
		{
			KRASStandardResultCollection krasStandardResultCollection = KRASStandardResultCollection.GetAll();
			foreach (KRASStandardResult result in krasStandardResultCollection)
			{
				if(this.Contains(result.Result) == false)
				{
					this.Add(result.Result);
				}
			}
		}
	}
}
