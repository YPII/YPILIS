using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Task.Model
{
	public class TaskAcknowledgementType
	{
		public static string Daily = "Daily";
		public static string Immediate = "Immediate";

		public static List<string> GetAll()
		{
			List<string> result = new List<string>();
			result.Add(Immediate);
			result.Add(Daily);

			return result;
		}
	}
}
