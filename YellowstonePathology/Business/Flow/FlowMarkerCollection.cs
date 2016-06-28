using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Flow
{
    public class FlowMarkerCollection : ObservableCollection<Flow.FlowMarkerItem>
    {
		public const string PREFIXID = "FM";

		public FlowMarkerCollection()
        {

        }

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string flowMarkerId = element.Element("FlowMarkerId").Value;
                    if (this[i].FlowMarkerId == flowMarkerId)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    this.RemoveItem(i);
                }
            }
        }

        public YellowstonePathology.Business.Flow.FlowMarkerItem GetNextItem(string reportNo, string name)
		{
			string flowMarkerId = this.GetNextId(reportNo);
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			FlowMarkerItem flowMarker = new FlowMarkerItem(flowMarkerId, objectId, reportNo, name);
			return flowMarker;
		}

        public YellowstonePathology.Business.Flow.FlowMarkerItem GetMarkerByName(string markerName)
        {
            YellowstonePathology.Business.Flow.FlowMarkerItem result = null;
            foreach (FlowMarkerItem flowMarker in this)
            {
                if (flowMarker.Name.ToUpper() == markerName.ToUpper())
                {
                    result = flowMarker;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string flowMarkerId)
        {
            bool result = false;
            foreach (FlowMarkerItem flowMarker in this)
            {
                if (flowMarker.FlowMarkerId == flowMarkerId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Flow.FlowMarkerItem Get(string flowMarkerId)
        {
            YellowstonePathology.Business.Flow.FlowMarkerItem result = null;
            foreach (FlowMarkerItem flowMarker in this)
            {
                if (flowMarker.FlowMarkerId == flowMarkerId)
                {
                    result = flowMarker;
                    break;
                }
            }
            return result;
        }

        //WHC MOVE TO ORDERIDPARSER
        public string GetNextId(string reportNo)
		{
			int largestId = 0;
			foreach (FlowMarkerItem flowMarker in this)
			{
				int thisId = this.ParseId(flowMarker.FlowMarkerId, PREFIXID);
				if (thisId > largestId)
				{
					largestId = thisId;
				}
			}
			return reportNo + "." + PREFIXID + (largestId + 1).ToString();
		}

		//WHC MOVE TO ORDERIDPARSER
		public int ParseId(string id, string prefix)
		{
			int startIndex = id.IndexOf(prefix) + prefix.Length;
			int result = Convert.ToInt32(id.Substring(startIndex));
			return result;
		}

		public void Add(string reportNo, MarkerItem item)
        {
			FlowMarkerItem flowMarker = this.GetNextItem(reportNo, item.MarkerName);
			this.Add(flowMarker);
        }

        public bool HasQuestionMarks()
        {
			bool result = false;
			foreach (FlowMarkerItem flowMarkerItem in this)
			{
				if (flowMarkerItem.HasQuestionMarks() == true)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public void Insert(FlowMarkerCollection panelMarkers, string reportNo)
		{
			foreach (FlowMarkerItem panelMarkerItem in panelMarkers)
			{
				FlowMarkerItem flowMarker = this.GetNextItem(reportNo, panelMarkerItem.Name);
				flowMarker.Intensity = panelMarkerItem.Intensity;
				flowMarker.Interpretation = panelMarkerItem.Interpretation;
				flowMarker.MarkerUsed = panelMarkerItem.MarkerUsed;
				this.Add(flowMarker);
			}
		}

		public int CountOfUsedMarkers()
		{
			int result = 0;
			foreach (FlowMarkerItem flowMarker in this)
			{
				if(flowMarker.MarkerUsed == true)
				{
					result++;
				}
			}
			return result;
		}
	}
}
