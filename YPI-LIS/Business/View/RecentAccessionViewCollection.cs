using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.View
{
	public class RecentAccessionViewCollection : ObservableCollection<RecentAccessionView>
	{
		public RecentAccessionViewCollection()
		{
		}
	}
}
