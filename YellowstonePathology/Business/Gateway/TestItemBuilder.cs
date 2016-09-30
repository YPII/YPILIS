using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace YellowstonePathology.Business.Gateway
{
	public class TestItemBuilder : Domain.Persistence.IBuilder
	{
		private YellowstonePathology.Business.Test.Model.Test m_Test;

		public TestItemBuilder()
		{

		}

		public YellowstonePathology.Business.Test.Model.Test Test
		{
			get { return this.m_Test; }
		}

		public void Build(XElement testElement)
		{
            if (testElement != null)
            {
				YellowstonePathology.Business.Test.Model.Test test = new YellowstonePathology.Business.Test.Model.Test();
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(testElement, test);
				xmlPropertyWriter.Write();
				this.BuildResultItemCollection(test, testElement);
                this.m_Test = test;
            }
            else
            {
                this.m_Test = null;
            }
		}

		private void BuildResultItemCollection(YellowstonePathology.Business.Test.Model.Test test, XElement testElement)
		{
			List<XElement> resultElements = (from item in testElement.Elements("ResultItemCollection")
														 select item).ToList<XElement>();
			foreach (XElement resultElement in resultElements.Elements("ResultItem"))
			{
				YellowstonePathology.Test.Model.ResultItem result = new YellowstonePathology.Test.Model.ResultItem();
				Domain.Persistence.SqlXmlPropertyWriter xmlPropertyWriter = new Domain.Persistence.SqlXmlPropertyWriter(resultElement);
				result.WriteProperties(xmlPropertyWriter);
				test.ResultItemCollection.Add(result);
			}
		}
	}
}
