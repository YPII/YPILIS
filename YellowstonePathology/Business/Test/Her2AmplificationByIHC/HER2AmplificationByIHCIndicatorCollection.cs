using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.Her2AmplificationByIHC
{
	public class HER2AmplificationByIHCIndicatorCollection : ObservableCollection<string>
	{
		public static string BreastIndication = "Breast";
		public static string BreastPrimaryIndication = "Breast Primary";
		public static string BreastMetastaticIndication = "Breast Metastatic";		

		public HER2AmplificationByIHCIndicatorCollection()
		{
			this.Add(HER2AmplificationByIHCIndicatorCollection.BreastIndication);
			this.Add(HER2AmplificationByIHCIndicatorCollection.BreastPrimaryIndication);
			this.Add(HER2AmplificationByIHCIndicatorCollection.BreastMetastaticIndication);			
		}
	}
}