using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHProbeSignalIntensityCollection : ObservableCollection<string>
	{
		public HER2AmplificationByISHProbeSignalIntensityCollection()
		{
			this.Add("Adequate - High");
			this.Add("Adequate - Moderate");
			this.Add("Adequate - Low");			
		}
	}
}
