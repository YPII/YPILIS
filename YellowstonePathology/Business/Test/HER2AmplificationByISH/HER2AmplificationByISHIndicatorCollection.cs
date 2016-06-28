using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHIndicatorCollection : ObservableCollection<string>
	{
		public static string BreastIndication = "Breast";
		public static string GastricIndication = "Gastric";

		public HER2AmplificationByISHIndicatorCollection()
		{
			this.Add(HER2AmplificationByISHIndicatorCollection.BreastIndication);
			this.Add(HER2AmplificationByISHIndicatorCollection.GastricIndication);
		}
	}
}