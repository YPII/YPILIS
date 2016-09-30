using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;

namespace YellowstonePathology.Business.Domain.Persistence.Schema.AccessionOrder
{
	public class RecentAccessionViewSchema
	{
		private static XDocument instance;
		private const string SchemaPath = "YellowstonePathology.Business.Domain.Persistence.Schema.AccessionOrder.RecentAccessionViewSchema.xsd";

		public static XDocument Instance
		{
			get
			{
				if (instance == null)
				{
					Assembly assembly = Assembly.GetExecutingAssembly();
					XmlReader xmlReader = XmlReader.Create(assembly.GetManifestResourceStream(SchemaPath));
					instance = XDocument.Load(xmlReader);
				}
				return instance;
			}
		}
	}
}
