using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Helper
{
	public class EnumDiscriptionValuePair
	{
		public string Key { get; set; }

		public string Name { get; set; }

		public int Value { get; set; }

		public static List<EnumDiscriptionValuePair> ListFrom<T>()
		{
			var array = (T[])(Enum.GetValues(typeof(T)).Cast<T>());
			return array
			  .Select(p => new EnumDiscriptionValuePair
				{
					Key = p.ToString(),
					Name = p.ToString().SplitCapitalizedWords(),
					Value = Convert.ToInt32(p)
				})
				.OrderBy(edvp => edvp.Name)
			   .ToList();
		}
	}
}
