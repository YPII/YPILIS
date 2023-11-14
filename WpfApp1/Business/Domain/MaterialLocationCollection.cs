using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public class MaterialLocationCollection :ObservableCollection<MaterialLocation>
	{
		public MaterialLocationCollection()
		{
		}
	}
}
