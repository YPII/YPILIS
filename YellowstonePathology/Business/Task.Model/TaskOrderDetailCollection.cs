using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Data;

namespace YellowstonePathology.Business.Task.Model
{
	public class TaskOrderDetailCollection : ObservableCollection<TaskOrderDetail>
	{
		public const string PREFIXID = "TSKOD";

		public TaskOrderDetailCollection()
		{

		}

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string taskOrderDetailId = element.Element("TaskOrderDetailId").Value;
                    if (this[i].TaskOrderDetailId == taskOrderDetailId)
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

        public bool HasUnacknowledgeItems()
        {
            bool result = false;
            foreach (TaskOrderDetail taskOrderDetail in this)
            {
                if (taskOrderDetail.Acknowledged == false)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasAcknowledgeItems()
        {
            bool result = false;
            foreach (TaskOrderDetail taskOrderDetail in this)
            {
                if (taskOrderDetail.Acknowledged == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

		public bool Exists(string taskOrderDetailId)
		{
			bool result = false;
			foreach (TaskOrderDetail taskOrderDetail in this)
			{
				if (taskOrderDetail.TaskOrderDetailId == taskOrderDetailId)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public TaskOrderDetail Get(string taskOrderDetailId)
		{
			TaskOrderDetail result = null;
			foreach (TaskOrderDetail taskOrderDetail in this)
			{
				if (taskOrderDetail.TaskOrderDetailId == taskOrderDetailId)
				{
					result = taskOrderDetail;
					break;
				}
			}
			return result;
		}

        public TaskOrderDetailFedexShipment GetFedexShipment()
        {
            TaskOrderDetailFedexShipment result = null;
            foreach (TaskOrderDetail taskOrderDetail in this)
            {
                if (taskOrderDetail is TaskOrderDetailFedexShipment)
                {
                    result = taskOrderDetail as TaskOrderDetailFedexShipment;
                    break;
                }
            }
            return result;
        }

        public bool FedexShipmentExists()
        {
            bool result = false;
            foreach (TaskOrderDetail taskOrderDetail in this)
            {
                if (taskOrderDetail is TaskOrderDetailFedexShipment)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public void Sync(DataTable dataTable, string taskOrderId)
        {
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string taskOrderDetailId = dataTableReader["TaskOrderDetailId"].ToString();
                string taskOrderDetailTaskOrderId = dataTableReader["TaskOrderId"].ToString();
                string taskId = dataTableReader["TaskId"].ToString();

                TaskOrderDetail taskOrderDetail = null;

                if (this.Exists(taskOrderDetailId) == true)
                {
                    taskOrderDetail = this.Get(taskOrderDetailId);
                }
                else if (taskOrderId == taskOrderDetailTaskOrderId)
                {
                    taskOrderDetail = TaskOrderDetailFactory.GetTaskOrderDetail(taskId);
                    this.Add(taskOrderDetail);
                }

                if (taskOrderDetail != null)
                {
                    YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(taskOrderDetail, dataTableReader);
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
                    string taskOrderDetailId = dataTable.Rows[idx]["TaskOrderDetailId"].ToString();
                    if (this[i].TaskOrderDetailId == taskOrderDetailId)
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
    }
}
