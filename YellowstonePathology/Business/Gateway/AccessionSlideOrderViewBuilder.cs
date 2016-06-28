using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace YellowstonePathology.Business.Gateway
{
	public class AccessionSlideOrderViewBuilder : Domain.Persistence.IBuilder
	{
		public YellowstonePathology.Business.View.AccessionSlideOrderView m_AccessionSlideOrderView;

		public AccessionSlideOrderViewBuilder()
		{
		}

		public void Build(XElement sourceElement)
		{
			if (sourceElement != null)
			{
				YellowstonePathology.Business.View.AccessionSlideOrderView accessionSlideOrderView = new View.AccessionSlideOrderView();
				Domain.Persistence.SqlXmlPropertyWriter xmlPropertyWriter = new Domain.Persistence.SqlXmlPropertyWriter(sourceElement);
				accessionSlideOrderView.WriteProperties(xmlPropertyWriter);
				BuildSlideOrder(accessionSlideOrderView, sourceElement);				
				this.m_AccessionSlideOrderView = accessionSlideOrderView;
			}
			else
			{
				this.m_AccessionSlideOrderView = null;
			}
		}

		public YellowstonePathology.Business.View.AccessionSlideOrderView AccessionSlideOrderView
		{
			get { return this.m_AccessionSlideOrderView; }
		}

		private void BuildSlideOrder(YellowstonePathology.Business.View.AccessionSlideOrderView accessionSlideOrderView, XElement sourceElement)
		{
			XElement slideOrderElement = sourceElement.Element("SlideOrder");
			if( slideOrderElement != null)
			{
				accessionSlideOrderView.SlideOrder = new YellowstonePathology.Business.Slide.Model.SlideOrder();
				accessionSlideOrderView.SlideOrder.FromXml(slideOrderElement);
			}
		}		
	}
}
