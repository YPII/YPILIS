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
		public static string BreastPrimaryIndication = "Breast Primary";
		public static string BreastMetastaticIndication = "Breast Metastatic";
		public static string GastricIndication = "Gastric/Other Adenocarcinoma";
		public static string EndometrialIndication = "Endometrial";

		public HER2AmplificationByISHIndicatorCollection()
		{
			this.Add(HER2AmplificationByISHIndicatorCollection.BreastIndication);
			this.Add(HER2AmplificationByISHIndicatorCollection.BreastPrimaryIndication);
			this.Add(HER2AmplificationByISHIndicatorCollection.BreastMetastaticIndication);
			this.Add(HER2AmplificationByISHIndicatorCollection.GastricIndication);
			this.Add(HER2AmplificationByISHIndicatorCollection.EndometrialIndication);
		}		
	}
}