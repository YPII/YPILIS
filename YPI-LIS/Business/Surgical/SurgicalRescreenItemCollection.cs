using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Surgical
{
	public class SurgicalRescreenItemCollection : ObservableCollection<SurgicalRescreenItem>
	{
		public SurgicalRescreenItemCollection()
		{
		}
	}
}
