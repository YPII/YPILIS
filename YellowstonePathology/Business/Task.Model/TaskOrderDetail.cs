using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Task.Model
{
	[PersistentClass("tblTaskOrderDetail", "YPIDATA")]
	public class TaskOrderDetail : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_TaskOrderDetailId;
		private string m_TaskOrderId;
		private string m_TaskId;
		private string m_Description;
		private string m_Comment;
        private string m_AssignedTo;
        private bool m_Acknowledged;
        private int? m_AcknowledgedById;
        private string m_AcknowledgedByInitials;
        private DateTime? m_AcknowledgedDate;
        private DateTime? m_ActionDate;

		public TaskOrderDetail()
		{

		}

		public TaskOrderDetail(string taskOrderDetailId, string taskOrderId, string objectId, Task task)
		{
			this.m_TaskOrderDetailId = taskOrderDetailId;
			this.m_TaskOrderId = taskOrderId;
			this.m_ObjectId = objectId;
			this.m_TaskId = task.TaskId;
			this.m_Description = task.Description;
            this.m_AssignedTo = task.AssignedTo;
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
		
		[PersistentPrimaryKeyProperty(false)]
		public string TaskOrderDetailId
		{
			get { return this.m_TaskOrderDetailId; }
			set
			{
				if (this.m_TaskOrderDetailId != value)
				{
					this.m_TaskOrderDetailId = value;
					this.NotifyPropertyChanged("TaskOrderDetailId");
				}
			}
		}

		[PersistentProperty()]
		public string TaskOrderId
		{
			get { return this.m_TaskOrderId; }
			set
			{
				if (this.m_TaskOrderId != value)
				{
					this.m_TaskOrderId = value;
					this.NotifyPropertyChanged("TaskOrderId");
				}
			}
		}

		[PersistentProperty()]
		public string TaskId
		{
			get { return this.m_TaskId; }
			set
			{
				if (this.m_TaskId != value)
				{
					this.m_TaskId = value;
					this.NotifyPropertyChanged("TaskId");
				}
			}
		}

		[PersistentProperty()]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if (this.m_Description != value)
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
				if (this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

        [PersistentProperty()]
        public string AssignedTo
        {
            get { return this.m_AssignedTo; }
            set
            {
                if (this.m_AssignedTo != value)
                {
                    this.m_AssignedTo = value;
                    this.NotifyPropertyChanged("AssignedTo");
                }
            }
        }

        [PersistentProperty()]
        public bool Acknowledged
        {
            get { return this.m_Acknowledged; }
            set
            {
                if (this.m_Acknowledged != value)
                {
                    this.m_Acknowledged = value;
                    this.NotifyPropertyChanged("Acknowledged");
                }
            }
        }

        [PersistentProperty()]
        public int? AcknowledgedById
        {
            get { return this.m_AcknowledgedById; }
            set
            {
                if (this.m_AcknowledgedById != value)
                {
                    this.m_AcknowledgedById = value;
                    this.NotifyPropertyChanged("AcknowledgedById");
                }
            }
        }

        [PersistentProperty()]
        public string AcknowledgedByInitials
        {
            get { return this.m_AcknowledgedByInitials; }
            set
            {
                if (this.m_AcknowledgedByInitials != value)
                {
                    this.m_AcknowledgedByInitials = value;
                    this.NotifyPropertyChanged("AcknowledgedByInitials");
                }
            }
        }

        [PersistentProperty()]
        public DateTime? AcknowledgedDate
        {
            get { return this.m_AcknowledgedDate; }
            set
            {
                if (this.m_AcknowledgedDate != value)
                {
                    this.m_AcknowledgedDate = value;
                    this.NotifyPropertyChanged("AcknowledgedDate");
                }
            }
        }

        [PersistentProperty()]
        public DateTime? ActionDate
        {
            get { return this.m_ActionDate; }
            set
            {
                if (this.m_ActionDate != value)
                {
                    this.m_ActionDate = value;
                    this.NotifyPropertyChanged("ActionDate");
                }
            }
        }

        public void AppendComment(string comment)
        {
            if (string.IsNullOrEmpty(this.m_Comment) == true)
            {
                this.Comment = comment;
            }
            else
            {
                this.Comment += Environment.NewLine + comment;
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
