using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Linq;

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

	}
}
