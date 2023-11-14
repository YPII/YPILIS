using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.KRASStandard
{
	public class KRASStandardMutationCollection : ObservableCollection<string>
	{
		public KRASStandardMutationCollection()
		{
			KRASStandardResultCollection krasStandardResultCollection = KRASStandardResultCollection.GetAll();
			this.Add(string.Empty);
			foreach (KRASStandardResult result in krasStandardResultCollection)
			{
				if (string.IsNullOrEmpty(result.ResultDescription) == false)
				{
					this.Add(result.ResultDescription);
				}
			}
		}
	}
}
