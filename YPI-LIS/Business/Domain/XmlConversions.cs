using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain
{
	public static class XmlConversions
	{
		public static Nullable<int> NullableIntFromElement(XElement source)
		{
			Nullable<int> returnValue = null;
			if(source.Value.Length > 0)
			{
				returnValue = Convert.ToInt32(source.Value);
			} 
			return returnValue;
		}

		public static string ValueFromNullableInt(Nullable<int> source)
		{
			return source.HasValue ? source.Value.ToString() : string.Empty;
		}
	}
}
