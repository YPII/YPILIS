using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHGeneticHeterogeneityCollection : ObservableCollection<string>
	{
		public static string GeneticHeterogeneityPresentInCells = "Present in scattered individual cells";
		public static string GeneticHeterogeneityPresentInClusters = "Present in small cell clusters";

		public HER2AmplificationByISHGeneticHeterogeneityCollection()
		{
			this.Add("Not Present");
			this.Add(HER2AmplificationByISHGeneticHeterogeneityCollection.GeneticHeterogeneityPresentInCells);
			this.Add(HER2AmplificationByISHGeneticHeterogeneityCollection.GeneticHeterogeneityPresentInClusters);
		}
	}
}
