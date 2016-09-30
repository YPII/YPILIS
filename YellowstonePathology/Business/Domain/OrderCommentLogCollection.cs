using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public class OrderCommentLogCollection : ObservableCollection<YellowstonePathology.Business.Domain.OrderCommentLog>
	{
		public OrderCommentLogCollection()
		{

		}

		public OrderCommentLog LogOrderComment(int specimenLogId, string masterAccessionNo, string comment, YellowstonePathology.Business.User.SystemIdentity systemIdentity, YellowstonePathology.Business.Domain.OrderComment orderComment)
		{
			YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog = new OrderCommentLog();
			orderCommentLog.OrderCommentId = orderComment.OrderCommentId;
			orderCommentLog.SpecimenLogId = specimenLogId;
			orderCommentLog.MasterAccessionNo = masterAccessionNo;
			orderCommentLog.Comment = comment;
			orderCommentLog.Description = orderComment.Description;
			orderCommentLog.LoggedById = systemIdentity.User.UserId;
			orderCommentLog.LoggedBy = systemIdentity.User.DisplayName;
			orderCommentLog.StationName = System.Environment.MachineName;
			orderCommentLog.LogDate = DateTime.Now;
			orderCommentLog.ObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			this.Add(orderCommentLog);
			return orderCommentLog;
		}

		public bool OrderCommentExists(int orderCommentId)
		{
			bool result = false;
			foreach (OrderCommentLog orderCommentLog in this)
			{
				if (orderCommentLog.OrderCommentId == orderCommentId)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public void Add(YellowstonePathology.Business.Domain.OrderCommentEnum orderCommentEnum, YellowstonePathology.Business.User.SystemUser eventUser, string masterAccessionNo, int clientId, string clientOrderId, string comment)
		{
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog = new Business.Domain.OrderCommentLog(objectId);
			YellowstonePathology.Business.Interface.IOrderComment orderComment = YellowstonePathology.Business.Domain.OrderCommentFactory.GetOrderComment(orderCommentEnum);
			orderCommentLog.SetDefaultValues(eventUser);
			orderCommentLog.FromEvent(orderComment);
			orderCommentLog.MasterAccessionNo = masterAccessionNo;
			orderCommentLog.ClientId = clientId;
			orderCommentLog.Description = orderComment.Description;
			orderCommentLog.ClientOrderId = clientOrderId;
			this.Add(orderCommentLog);
		}

		public void SendNotifications(YellowstonePathology.Business.User.SystemUser user)
		{
			foreach (YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog in this)
			{
				if (orderCommentLog.RequiresNotification)
				{
					orderCommentLog.SendNotification(user);
				}
			}
		}

		public void AddUnique(OrderCommentLog attemptOrderCommentLog)
		{
			foreach (OrderCommentLog orderCommentLog in this)
			{
				if (attemptOrderCommentLog.OrderCommentLogId == orderCommentLog.OrderCommentLogId)
				{
					return;
				}
			}
			this.Add(attemptOrderCommentLog);
		}

		public void UpdateKeys(string masterAccessionNo)
		{
			foreach(OrderCommentLog orderCommentLog in this)
			{
				orderCommentLog.MasterAccessionNo = masterAccessionNo;
			}
		}
	}
}
