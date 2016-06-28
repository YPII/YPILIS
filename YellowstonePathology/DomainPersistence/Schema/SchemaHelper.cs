using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain.Persistence.Schema
{
	public class SchemaHelper
	{
		public static XDocument SpecificElements(XDocument largeSchema, List<string> elementNames)
		{
			XDocument specificSchema = new XDocument(largeSchema);
			specificSchema.Root.RemoveNodes();

			List<XElement> specificElements = (from e in largeSchema.Root.Elements()
											   from n in elementNames											   
											   where e.Attribute("name").Value == n || e.Attribute("name").Value == n + "Type"
											   select e).ToList<XElement>();
			foreach(XElement e in specificElements)
			{
				specificSchema.Root.Add(e);
			}
			return specificSchema;
		}
	}
}
