using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHSampleAdequacyCollection : ObservableCollection<string>
	{
		public HER2AmplificationByISHSampleAdequacyCollection()
		{
			this.Add("Adequate number of invasive tumor cells present");
			this.Add("Limited by low number of tumor cells present");			
		}
	}
}
