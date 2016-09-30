using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Data;

namespace YellowstonePathology.Business.Task.Model
{
	public class TaskOrderCollection : ObservableCollection<TaskOrder>
	{
		public const string PREFIXID = "TSKO";

		public TaskOrderCollection()
		{

		}

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string taskOrderId = element.Element("TaskOrderId").Value;
                    if (this[i].TaskOrderId == taskOrderId)
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

        public TaskOrderCollection TaskOrderCollectionForReport(string reportNo)
		{
			TaskOrderCollection result = new TaskOrderCollection();
			foreach (TaskOrder taskOrder in this)
			{
				if (taskOrder.ReportNo == reportNo) result.Add(taskOrder);
			}

			return result;
		}

		public bool Exists(string taskOrderId)
		{
			bool result = false;
			foreach (TaskOrder taskOrder in this)
			{
				if (taskOrder.TaskOrderId == taskOrderId)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public TaskOrder GetTaskOrder(string taskOrderId)
		{
			TaskOrder result = null;
			foreach (TaskOrder taskOrder in this)
			{
				if (taskOrder.TaskOrderId == taskOrderId)
				{
					result = taskOrder;
					break;
				}
			}
			return result;
		}

        public TaskOrder GetTaskOrderByReportNo(string reportNo)
        {
            TaskOrder result = null;
            foreach (TaskOrder taskOrder in this)
            {
                if (taskOrder.ReportNo == reportNo)
                {
                    result = taskOrder;
                    break;
                }
            }
            return result;
        }

		public TaskOrder GetTaskOrderByName(string taskName)
		{
			TaskOrder result = null;
			foreach (TaskOrder taskOrder in this)
			{
				if (taskOrder.TaskName == taskName)
				{
					result = taskOrder;
					break;
				}
			}
			return result;
		}

		public static YellowstonePathology.Business.Rules.MethodResult AddDailyTaskOrderCytologySlideDisposal(int days)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
			YellowstonePathology.Business.Task.Model.TaskCytologySlideDisposal task = new TaskCytologySlideDisposal();
			DateTime actionDate = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetNewestDailyTaskOrderTaskDate(task.TaskId);
			YellowstonePathology.Business.Task.Model.TaskOrderCollection taskOrderCollection = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;

			DateTime finalDate = actionDate.AddDays(days);

			while (actionDate < finalDate)
			{
				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

				YellowstonePathology.Business.Task.Model.TaskOrderCytologySlideDisposal taskOrder = new YellowstonePathology.Business.Task.Model.TaskOrderCytologySlideDisposal(objectId, actionDate, objectId, systemIdentity);
				taskOrderCollection.Add(taskOrder);
				actionDate = actionDate.AddDays(1);
			}

            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(taskOrderCollection, false);			

			actionDate = actionDate.AddDays(-1);
			result.Message = "Daily Task Order Cytology Slide Disposal have been added through " + actionDate.ToString("MM/dd/yyyy");
			return result;
		}

		public static YellowstonePathology.Business.Rules.MethodResult AddDailyTaskOrderSurgicalSpecimenDisposal(int days)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
			YellowstonePathology.Business.Task.Model.TaskSurgicalSpecimenDisposal task = new TaskSurgicalSpecimenDisposal();
			DateTime actionDate = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetNewestDailyTaskOrderTaskDate(task.TaskId);
			YellowstonePathology.Business.Task.Model.TaskOrderCollection taskOrderCollection = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;

			DateTime finalDate = actionDate.AddDays(days);

			while (actionDate < finalDate)
			{
				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

				YellowstonePathology.Business.Task.Model.TaskOrderSurgicalSpecimenDisposal taskOrderSurgical = new YellowstonePathology.Business.Task.Model.TaskOrderSurgicalSpecimenDisposal(objectId, actionDate, objectId, systemIdentity);
				taskOrderCollection.Add(taskOrderSurgical);
				actionDate = actionDate.AddDays(1);
			}

            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(taskOrderCollection, true);			
			actionDate = actionDate.AddDays(-1);
			result.Message = "Daily Task Order Surgical Specimen Disposal have been added through " + actionDate.ToString("MM/dd/yyyy");
			return result;
		}

		public static YellowstonePathology.Business.Rules.MethodResult AddDailyTaskOrderPOCReport(int weeks)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();
			YellowstonePathology.Business.Task.Model.TaskPOCReport task = new TaskPOCReport();
			DateTime actionDate = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetNewestWeeklyTaskOrderTaskDate(task.TaskId);
			YellowstonePathology.Business.Task.Model.TaskOrderCollection taskOrderCollection = new YellowstonePathology.Business.Task.Model.TaskOrderCollection();			
			YellowstonePathology.Business.User.SystemIdentity systemIdentity = Business.User.SystemIdentity.Instance;

			DateTime finalDate = actionDate.AddDays(weeks * 7);

			while (actionDate < finalDate)
			{
				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

				YellowstonePathology.Business.Task.Model.TaskOrderPOCReport taskOrder = new YellowstonePathology.Business.Task.Model.TaskOrderPOCReport(objectId, actionDate, objectId, systemIdentity);
				taskOrderCollection.Add(taskOrder);

				actionDate = actionDate.AddDays(7);
			}

            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(taskOrderCollection, true);
            actionDate = actionDate.AddDays(-7);
			result.Message = "Daily Task Order POC Report have been added through " + actionDate.ToString("MM/dd/yyyy");
			return result;
		}

        public void Sync(DataTable dataTable)
        {
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string taskOrderId = dataTableReader["TaskOrderId"].ToString();

                TaskOrder taskOrder = null;

                if (this.Exists(taskOrderId) == true)
                {
                    taskOrder = this.GetTaskOrder(taskOrderId);
                }
                else
                {
                    taskOrder = new TaskOrder();
                    this.Add(taskOrder);
                }

                YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(taskOrder, dataTableReader);
                sqlDataTableReaderPropertyWriter.WriteProperties();
            }
        }

        public void RemoveDeleted(DataTable dataTable)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                for (int idx = 0; idx < dataTable.Rows.Count; idx++)
                {
                    string taskOrderId = dataTable.Rows[idx]["TaskOrderId"].ToString();
                    if (this[i].TaskOrderId == taskOrderId)
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
