using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Xml.Linq;
using System.ComponentModel;

namespace YellowstonePathology.Business.Flow
{
    public class FlowMarkerCollection : ObservableCollection<Flow.FlowMarkerItem>, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        public const string PREFIXID = "FM";
        public CellPopulationsOfInterest m_CellPopulationsOfInterest;
        private Flow.FlowMarkerCollection m_CurrentMarkerPanel;

        public FlowMarkerCollection()
        {
            //this.m_CellPopulationsOfInterest = new CellPopulationsOfInterest();
        }

        public void RemovePanel(int cellPopulationId)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                if(this[i].CellPopulationId == cellPopulationId)
                {
                    this.Remove(this[i]);
                }
            }

            this.SetCellPopulationsOfInterest();
            this.SetFirstMarkerPanelIfExists();            
        }

        public FlowMarkerCollection CurrentMarkerPanel
        {
            get { return this.m_CurrentMarkerPanel; }
        }

        public CellPopulationsOfInterest CellPopulationsOfInterest
        {
            get { return this.m_CellPopulationsOfInterest; }
        }

        public void SetCurrentMarkerPanel(int cellPopulationId)
        {
            this.m_CurrentMarkerPanel = new FlowMarkerCollection();
            foreach (FlowMarkerItem item in this)
            {
                if(item.CellPopulationId == cellPopulationId)
                {
                    this.m_CurrentMarkerPanel.Add(item);
                }
            }
            this.NotifyPropertyChanged("CurrentMarkerPanel");
        }

        public FlowMarkerCollection GetMarkerPanel(int cellPopulationId)
        {
            FlowMarkerCollection result = new FlowMarkerCollection();
            foreach (FlowMarkerItem item in this)
            {
                if (item.CellPopulationId == cellPopulationId)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public void SetFirstMarkerPanelIfExists()
        {
            this.m_CurrentMarkerPanel = new FlowMarkerCollection();
            Nullable<int> firstId = this.m_CellPopulationsOfInterest.GetFirstId();
            if(firstId.HasValue == true)
            {
                foreach (FlowMarkerItem item in this)
                {
                    if (item.CellPopulationId == firstId)
                    {
                        this.m_CurrentMarkerPanel.Add(item);
                    }
                }
            }            
            this.NotifyPropertyChanged("CurrentMarkerPanel");
        }

        public void SetCellPopulationsOfInterest()
        {
            this.m_CellPopulationsOfInterest = new CellPopulationsOfInterest();
            foreach(Flow.FlowMarkerItem item in this)
            {
                if(this.m_CellPopulationsOfInterest.Exists(item.CellPopulationId) == false)
                {
                    this.m_CellPopulationsOfInterest.Add(new CellPopulationOfInterest(item.CellPopulationId, item.CellPopulationOfInterest, item.PanelName));
                }
            }
            this.NotifyPropertyChanged("CellPopulationsOfInterest");
        }

        public int GetNextCellPopulationId()
        {
            int highestCellPopulationId = 0;
            foreach(Flow.FlowMarkerItem item in this)
            {
                if (item.CellPopulationId > highestCellPopulationId) highestCellPopulationId = item.CellPopulationId;
            }
            return highestCellPopulationId + 1;
        }

        public void ClearCellPopulation(int cellPopulationId)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                if (this[i].CellPopulationId == cellPopulationId)
                {
                    this.Remove(this[i]);
                }
            }
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

        public YellowstonePathology.Business.Flow.FlowMarkerItem GetNextItem(string reportNo, string name, int cellPopulationId, string cellPopulationOfInterest, string panelName)
		{
			string flowMarkerId = this.GetNextId(reportNo);
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			FlowMarkerItem flowMarker = new FlowMarkerItem(flowMarkerId, objectId, reportNo, name, cellPopulationId, cellPopulationOfInterest, panelName);
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
		
		public int ParseId(string id, string prefix)
		{
			int startIndex = id.IndexOf(prefix) + prefix.Length;
			int result = Convert.ToInt32(id.Substring(startIndex));
			return result;
		}

		public void Add(string reportNo, MarkerItem item, int cellPopulationId, string cellPopulationOfInterest, string panelName)
        {
			FlowMarkerItem flowMarker = this.GetNextItem(reportNo, item.MarkerName, cellPopulationId, cellPopulationOfInterest, panelName);
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

		public void Insert(FlowMarkerCollection panelMarkers, string reportNo, int cellPopulationId, string cellPopulationOfInterest, string panelName)
		{
			foreach (FlowMarkerItem panelMarkerItem in panelMarkers)
			{
				FlowMarkerItem flowMarker = this.GetNextItem(reportNo, panelMarkerItem.Name, cellPopulationId, cellPopulationOfInterest, panelName);
				flowMarker.Intensity = panelMarkerItem.Intensity;
				flowMarker.Interpretation = panelMarkerItem.Interpretation;
				flowMarker.MarkerUsed = panelMarkerItem.MarkerUsed;
                flowMarker.CellPopulationId = cellPopulationId;
                flowMarker.CellPopulationOfInterest = cellPopulationOfInterest;
                flowMarker.PanelName = panelName;
                this.Add(flowMarker);
			}
            this.SetCellPopulationsOfInterest();
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

        public void Sync(DataTable dataTable, string reportNo)
        {
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string flowMarkerId = dataTableReader["FlowMarkerId"].ToString();
                string markerReportNo = dataTableReader["ReportNo"].ToString();

                FlowMarkerItem flowMarkerItem = null;

                if (this.Exists(flowMarkerId) == true)
                {
                    flowMarkerItem = this.Get(flowMarkerId);
                }
                else if (reportNo == markerReportNo)
                {
                    flowMarkerItem = new FlowMarkerItem();
                    this.Add(flowMarkerItem);
                }

                if (flowMarkerItem != null)
                {
                    YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(flowMarkerItem, dataTableReader);
                    sqlDataTableReaderPropertyWriter.WriteProperties();
                }
            }
        }

        public void RemoveDeleted(DataTable dataTable)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                for (int idx = 0; idx < dataTable.Rows.Count; idx++)
                {
                    string flowMarkerId = dataTable.Rows[idx]["FlowMarkerId"].ToString();
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
