using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.View
{
	public class AccessionSlideOrderViewCollection : ObservableCollection<AccessionSlideOrderView>
	{
		public AccessionSlideOrderViewCollection()
		{

		}

		public bool Exists(string slideOrderId)
        {
            bool result = false;
			if (this.Count(n => n.SlideOrder.SlideOrderId == slideOrderId) > 0)
            {
                result = true;
            }            
            return result;
        }

		public AccessionSlideOrderViewCollection GetMissing(AccessionSlideOrderViewCollection matchCollection)
		{
			AccessionSlideOrderViewCollection result = new AccessionSlideOrderViewCollection();

			foreach (AccessionSlideOrderView accessionSlideOrderView in matchCollection)
			{
				if(!Exists(accessionSlideOrderView.SlideOrder.SlideOrderId))
				{
					result.Add(accessionSlideOrderView);
				}
			}
			return result;
		}

		public string MasterAccessionNoString()
		{
			StringBuilder result = new StringBuilder();
			foreach (AccessionSlideOrderView accessionSlideOrderView in this)
			{
				string masterAccessionNoString = "'" + accessionSlideOrderView.MasterAccessionNo.ToString() + "'";
				if(!result.ToString().Contains(masterAccessionNoString))
				{
					result.Append(masterAccessionNoString + ",");
				}
			}
			if (result.Length > 1)
			{
				result.Remove(result.Length - 1, 1);
			}

			return result.ToString();
		}

		public XElement ToXml()
		{
			XElement result = new XElement("AccessionSlideOrderCollection");
			foreach(AccessionSlideOrderView accessionSlideOrderView in this)
			{
				XElement viewElement = accessionSlideOrderView.ToXml();
				XElement slideOrderElement = accessionSlideOrderView.SlideOrder.ToXml();
				viewElement.Add(slideOrderElement);
				result.Add(viewElement);
			}

			return result;
		}
	}
}
