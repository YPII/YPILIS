using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using YellowstonePathology.Business.Persistence;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain
{
	[PersistentClass("tblOrderCommentLog", "YPIDATA")]
	public class OrderCommentLog : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private int m_OrderCommentLogId;
		private int m_OrderCommentId;
		private int m_SpecimenLogId;
		private string m_MasterAccessionNo;
		private string m_AliquotOrderId;
		private string m_Category;
		private string m_Action;
		private DateTime m_LogDate;
		private int m_ClientId;
		private string m_Description;
		private string m_Comment;
		private int m_LoggedById;
		private string m_LoggedBy;
		private string m_ClientOrderId;
		private bool m_RequiresResponse;
		private bool m_RequiresNotification;
		private string m_Response;
		private string m_NotificationAddress;
		private string m_StationName;
		private string m_ContainerId;

		public OrderCommentLog()
		{
		}

		public OrderCommentLog(string objectId)
		{
			this.m_ObjectId = objectId;
		}

		public void SetDefaultValues(YellowstonePathology.Business.User.SystemUser systemUser)
        {
            this.LogDate = DateTime.Now;
            this.LoggedById = systemUser.UserId;
            this.LoggedBy = systemUser.DisplayName;            
        }

		public void FromEvent(YellowstonePathology.Business.Interface.IOrderComment labEvent)
        {            
            this.Action = labEvent.Action;
            this.Category = labEvent.Category;
            this.Description = labEvent.Description;
			this.OrderCommentId = labEvent.OrderCommentId;
			this.RequiresNotification = labEvent.RequiresNotification;
			this.RequiresResponse = labEvent.RequiresResponse;
			this.NotificationAddress = labEvent.NotificationAddress;
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public static void LogEvent(int specimenLogId, string masterAccessionNo, string comment,
			YellowstonePathology.Business.User.SystemUser systemUser, YellowstonePathology.Business.Domain.OrderCommentEnum eventEnum)		
        {
			OrderCommentLogCollection orderCommentLogCollection = new OrderCommentLogCollection();
			YellowstonePathology.Business.Interface.IOrderComment eventInfo = YellowstonePathology.Business.Domain.OrderCommentFactory.GetOrderComment(eventEnum);

			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			OrderCommentLog eventLog = new OrderCommentLog(objectId);
            eventLog.FromEvent(eventInfo);
            eventLog.SpecimenLogId = specimenLogId;
            eventLog.MasterAccessionNo = masterAccessionNo;
            eventLog.Comment = comment;
            eventLog.LoggedById = systemUser.UserId;
            eventLog.LoggedBy = systemUser.DisplayName;
            eventLog.LogDate = DateTime.Now;

			orderCommentLogCollection.Add(eventLog);
        }

		public static void LogEvent(string clientOrderId, string comment,
			YellowstonePathology.Business.User.SystemUser systemUser, YellowstonePathology.Business.Domain.OrderCommentEnum eventEnum)
		{
			OrderCommentLogCollection orderCommentLogCollection = new OrderCommentLogCollection();
			YellowstonePathology.Business.Interface.IOrderComment eventInfo = YellowstonePathology.Business.Domain.OrderCommentFactory.GetOrderComment(eventEnum);
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

			OrderCommentLog eventLog = new OrderCommentLog(objectId);
			eventLog.FromEvent(eventInfo);
			eventLog.ClientOrderId = clientOrderId;
			eventLog.Comment = comment;
			eventLog.LoggedById = systemUser.UserId;
			eventLog.LoggedBy = systemUser.DisplayName;
			eventLog.LogDate = DateTime.Now;

			orderCommentLogCollection.Add(eventLog);
		}

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			result.AppendLine("Log Date: " + this.m_LogDate.ToShortDateString() + " " + this.m_LogDate.ToShortTimeString());
			result.AppendLine("Logged By: " + this.m_LoggedBy);
			result.AppendLine("Category: " + this.m_Category);
			result.AppendLine("Action: " + this.m_Action);
			result.AppendLine("Description: " + this.m_Description);
			result.AppendLine("Lab Event Id:" + this.m_OrderCommentId.ToString());
			result.AppendLine("Specimen Log Id: " + this.m_SpecimenLogId.ToString());
			result.AppendLine("Master Accession No: " + this.m_MasterAccessionNo);
			result.AppendLine("Aliquot Order Id: " + this.m_AliquotOrderId.ToString());
			result.AppendLine("Client Id: " + this.m_ClientId.ToString());

			string comment = string.Empty;
			if (!string.IsNullOrEmpty(this.m_Comment))
			{
				comment = this.m_Comment;
			}
			result.AppendLine("Comment: " + comment);

			string clientOrderId = string.Empty;
			if (!string.IsNullOrEmpty(this.m_ClientOrderId))
			{
				clientOrderId = this.m_ClientOrderId;
			}
			result.AppendLine("Client Order Id: " + clientOrderId);

			result.AppendLine("Requires Response: " + (this.RequiresResponse == true ? "True" : "False"));

			string response = string.Empty;
			if (!string.IsNullOrEmpty(this.m_Response))
			{
				response = this.Response;
			}
			result.AppendLine("Response: " + response);

			return result.ToString();
		}

		public void SendNotification(YellowstonePathology.Business.User.SystemUser user)
		{
			string message = this.ToString();
			System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage("Administrator@ypii.com", this.NotificationAddress, user.DisplayName + " - Event Notification", message);
			System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("10.1.2.111");

            Uri uri = new Uri("http://tempuri.org/");
            System.Net.ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            System.Net.NetworkCredential credential = credentials.GetCredential(uri, "Basic");

            client.Credentials = credential;
			client.Send(mailMessage);
		}

		[PersistentDocumentIdProperty()]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");
				}
			}
		}

		[PersistentPrimaryKeyProperty(true)]
		public int OrderCommentLogId
		{
			get { return this.m_OrderCommentLogId; }
			set
			{
				if(this.m_OrderCommentLogId != value)
				{
					this.m_OrderCommentLogId = value;
					this.NotifyPropertyChanged("OrderCommentLogId");
				}
			}
		}

		[PersistentProperty()]
		public int OrderCommentId
		{
			get { return this.m_OrderCommentId; }
			set
			{
				if(this.m_OrderCommentId != value)
				{
					this.m_OrderCommentId = value;
					this.NotifyPropertyChanged("OrderCommentId");
				}
			}
		}

		[PersistentProperty()]
		public int SpecimenLogId
		{
			get { return this.m_SpecimenLogId; }
			set
			{
				if(this.m_SpecimenLogId != value)
				{
					this.m_SpecimenLogId = value;
					this.NotifyPropertyChanged("SpecimenLogId");
				}
			}
		}

		[PersistentProperty()]
		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set
			{
				if(this.m_MasterAccessionNo != value)
				{
					this.m_MasterAccessionNo = value;
					this.NotifyPropertyChanged("MasterAccessionNo");
				}
			}
		}

		[PersistentProperty()]
		public string AliquotOrderId
		{
			get { return this.m_AliquotOrderId; }
			set
			{
				if(this.m_AliquotOrderId != value)
				{
					this.m_AliquotOrderId = value;
					this.NotifyPropertyChanged("AliquotOrderId");
				}
			}
		}

		[PersistentProperty()]
		public string Category
		{
			get { return this.m_Category; }
			set
			{
				if(this.m_Category != value)
				{
					this.m_Category = value;
					this.NotifyPropertyChanged("Category");
				}
			}
		}

		[PersistentProperty()]
		public string Action
		{
			get { return this.m_Action; }
			set
			{
				if(this.m_Action != value)
				{
					this.m_Action = value;
					this.NotifyPropertyChanged("Action");
				}
			}
		}

		[PersistentProperty()]
		public DateTime LogDate
		{
			get { return this.m_LogDate; }
			set
			{
				if(this.m_LogDate != value)
				{
					this.m_LogDate = value;
					this.NotifyPropertyChanged("LogDate");
				}
			}
		}

		[PersistentProperty()]
		public int ClientId
		{
			get { return this.m_ClientId; }
			set
			{
				if(this.m_ClientId != value)
				{
					this.m_ClientId = value;
					this.NotifyPropertyChanged("ClientId");
				}
			}
		}

		[PersistentProperty()]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if(this.m_Description != value)
				{
					this.m_Description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}

		[PersistentProperty()]
		public string Comment
		{
			get { return this.m_Comment; }
			set
			{
				if(this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

		[PersistentProperty()]
		public int LoggedById
		{
			get { return this.m_LoggedById; }
			set
			{
				if(this.m_LoggedById != value)
				{
					this.m_LoggedById = value;
					this.NotifyPropertyChanged("LoggedById");
				}
			}
		}

		[PersistentProperty()]
		public string LoggedBy
		{
			get { return this.m_LoggedBy; }
			set
			{
				if(this.m_LoggedBy != value)
				{
					this.m_LoggedBy = value;
					this.NotifyPropertyChanged("LoggedBy");
				}
			}
		}

		[PersistentProperty()]
		public string ClientOrderId
		{
			get { return this.m_ClientOrderId; }
			set
			{
				if(this.m_ClientOrderId != value)
				{
					this.m_ClientOrderId = value;
					this.NotifyPropertyChanged("ClientOrderId");
				}
			}
		}

		[PersistentProperty()]
		public bool RequiresResponse
		{
			get { return this.m_RequiresResponse; }
			set
			{
				if(this.m_RequiresResponse != value)
				{
					this.m_RequiresResponse = value;
					this.NotifyPropertyChanged("RequiresResponse");
				}
			}
		}

		[PersistentProperty()]
		public bool RequiresNotification
		{
			get { return this.m_RequiresNotification; }
			set
			{
				if(this.m_RequiresNotification != value)
				{
					this.m_RequiresNotification = value;
					this.NotifyPropertyChanged("RequiresNotification");
				}
			}
		}

		[PersistentProperty()]
		public string Response
		{
			get { return this.m_Response; }
			set
			{
				if(this.m_Response != value)
				{
					this.m_Response = value;
					this.NotifyPropertyChanged("Response");
				}
			}
		}

		[PersistentProperty()]
		public string NotificationAddress
		{
			get { return this.m_NotificationAddress; }
			set
			{
				if(this.m_NotificationAddress != value)
				{
					this.m_NotificationAddress = value;
					this.NotifyPropertyChanged("NotificationAddress");
				}
			}
		}

		[PersistentProperty()]
		public string StationName
		{
			get { return this.m_StationName; }
			set
			{
				if(this.m_StationName != value)
				{
					this.m_StationName = value;
					this.NotifyPropertyChanged("StationName");
				}
			}
		}

		[PersistentProperty()]
		public string ContainerId
		{
			get { return this.m_ContainerId; }
			set
			{
				if(this.m_ContainerId != value)
				{
					this.m_ContainerId = value;
					this.NotifyPropertyChanged("ContainerId");
				}
			}
		}

		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_OrderCommentLogId = propertyWriter.WriteInt("OrderCommentLogId");
			this.m_OrderCommentId = propertyWriter.WriteInt("OrderCommentId");
			this.m_SpecimenLogId = propertyWriter.WriteInt("SpecimenLogId");
			this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
			this.m_AliquotOrderId = propertyWriter.WriteString("AliquotOrderId");
			this.m_Category = propertyWriter.WriteString("Category");
			this.m_Action = propertyWriter.WriteString("Action");
			this.m_LogDate = propertyWriter.WriteDateTime("LogDate");
			this.m_ClientId = propertyWriter.WriteInt("ClientId");
			this.m_Description = propertyWriter.WriteString("Description");
			this.m_Comment = propertyWriter.WriteString("Comment");
			this.m_LoggedById = propertyWriter.WriteInt("LoggedById");
			this.m_LoggedBy = propertyWriter.WriteString("LoggedBy");
			this.m_ClientOrderId = propertyWriter.WriteString("ClientOrderId");
			this.m_RequiresResponse = propertyWriter.WriteBoolean("RequiresResponse");
			this.m_RequiresNotification = propertyWriter.WriteBoolean("RequiresNotification");
			this.m_Response = propertyWriter.WriteString("Response");
			this.m_NotificationAddress = propertyWriter.WriteString("NotificationAddress");
			this.m_StationName = propertyWriter.WriteString("StationName");
			this.m_ContainerId = propertyWriter.WriteString("ContainerId");
			this.m_ObjectId = propertyWriter.WriteString("ObjectId");
		}

		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			propertyReader.ReadInt("OrderCommentLogId", OrderCommentLogId);
			propertyReader.ReadInt("OrderCommentId", OrderCommentId);
			propertyReader.ReadInt("SpecimenLogId", SpecimenLogId);
			propertyReader.ReadString("MasterAccessionNo", MasterAccessionNo);
			propertyReader.ReadString("AliquotOrderId", AliquotOrderId);
			propertyReader.ReadString("Category", Category);
			propertyReader.ReadString("Action", Action);
			propertyReader.ReadDateTime("LogDate", LogDate);
			propertyReader.ReadInt("ClientId", ClientId);
			propertyReader.ReadString("Description", Description);
			propertyReader.ReadString("Comment", Comment);
			propertyReader.ReadInt("LoggedById", LoggedById);
			propertyReader.ReadString("LoggedBy", LoggedBy);
			propertyReader.ReadString("ClientOrderId", ClientOrderId);
			propertyReader.ReadBoolean("RequiresResponse", RequiresResponse);
			propertyReader.ReadBoolean("RequiresNotification", RequiresNotification);
			propertyReader.ReadString("Response", Response);
			propertyReader.ReadString("NotificationAddress", NotificationAddress);
			propertyReader.ReadString("StationName", StationName);
			propertyReader.ReadString("ContainerId", ContainerId);
			propertyReader.ReadString("ObjectId", ObjectId);
		}
	}
}
