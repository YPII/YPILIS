using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Task.Model
{
	[PersistentClass("tblTaskOrder", "YPIDATA")]
	public class TaskOrder : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

		protected TaskOrderDetailCollection m_TaskOrderDetailCollection;

		protected string m_ObjectId;
        protected string m_TaskOrderId;
        protected string m_MasterAccessionNo;
        protected string m_ReportNo;
        protected string m_PanelSetName;
        protected string m_TaskName;
        protected string m_TargetType;
        protected string m_TargetId;
        protected string m_TargetDescription;
        protected DateTime? m_OrderDate;
        protected int m_OrderedById;
        protected string m_OrderedByInitials;
        protected bool m_Acknowledged;
        protected DateTime? m_AcknowledgedDate;
		protected int? m_AcknowledgedById;
		protected string m_AcknowledgedByInitials;
        protected bool m_Final;
        protected Nullable<int> m_FinaledById;
        protected string m_FinaledByInitials;
        protected Nullable<DateTime> m_FinalDate;
		protected string m_AcknowledgementType;
		protected DateTime? m_TaskDate;
		protected string m_TaskId;
       
        public TaskOrder()
        {
			this.m_TaskOrderDetailCollection = new TaskOrderDetailCollection();
		}

        public TaskOrder(string taskOrderId, string objectId, string masterAccessionNo, string reportNo, Task task, YellowstonePathology.Business.Interface.IOrderTarget orderTarget, 
            string panelSetName, string acknowledgementType)
        {
            this.m_TaskOrderId = taskOrderId;
			this.m_ObjectId = objectId;
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_ReportNo = reportNo;
            this.m_TaskName = task.TaskName;                                    
            this.m_OrderedById = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserId;
			this.m_OrderedByInitials = YellowstonePathology.Business.User.SystemIdentity.Instance.User.Initials;
			this.m_TargetId = orderTarget.GetId();
            this.m_TargetType = orderTarget.GetOrderedOnType();
            this.m_TargetDescription = orderTarget.GetDescription();
            this.m_OrderDate = DateTime.Now;
            this.m_PanelSetName = panelSetName;
			this.m_AcknowledgementType = acknowledgementType;

			this.m_TaskOrderDetailCollection = new TaskOrderDetailCollection();
        }

        public virtual void Acknowledge(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {

        }

        public virtual void Acknowledge(TaskOrderDetail taskOrderDetail, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            taskOrderDetail.Acknowledged = true;
            taskOrderDetail.AcknowledgedById = systemIdentity.User.UserId;
            taskOrderDetail.AcknowledgedDate = DateTime.Now;
            taskOrderDetail.AcknowledgedByInitials = systemIdentity.User.Initials;

            this.Acknowledge(accessionOrder, systemIdentity);
        }        

		[PersistentCollection()]
		public TaskOrderDetailCollection TaskOrderDetailCollection
		{
			get { return this.m_TaskOrderDetailCollection; }
			set { this.m_TaskOrderDetailCollection = value; }
		}

		[PersistentDocumentIdProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
        [PersistentDataColumnProperty(false, "50", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "20", "null", "varchar")]
		public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "250", "null", "varchar")]
        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set
            {
                if (this.m_PanelSetName != value)
                {
                    this.m_PanelSetName = value;
                    this.NotifyPropertyChanged("PanelSetName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string TaskName
        {
            get { return this.m_TaskName; }
            set
            {
                if (this.m_TaskName != value)
                {
                    this.m_TaskName = value;
                    this.NotifyPropertyChanged("TaskName");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string TargetType
        {
            get { return this.m_TargetType; }
            set
            {
                if (this.m_TargetType != value)
                {
                    this.m_TargetType = value;
                    this.NotifyPropertyChanged("TargetType");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string TargetId
        {
            get { return this.m_TargetId; }
            set
            {
                if (this.m_TargetId != value)
                {
                    this.m_TargetId = value;
                    this.NotifyPropertyChanged("TargetId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string TargetDescription
        {
            get { return this.m_TargetDescription; }
            set
            {
                if (this.m_TargetDescription != value)
                {
                    this.m_TargetDescription = value;
                    this.NotifyPropertyChanged("TargetDescription");
                }
            }
        }        

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public DateTime? OrderDate
        {
            get { return this.m_OrderDate; }
            set
            {
                if (this.m_OrderDate != value)
                {
                    this.m_OrderDate = value;
                    this.NotifyPropertyChanged("OrderDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public int OrderedById
        {
            get { return this.m_OrderedById; }
            set
            {
                if (this.m_OrderedById != value)
                {
                    this.m_OrderedById = value;
                    this.NotifyPropertyChanged("OrderedById");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string OrderedByInitials
        {
            get { return this.m_OrderedByInitials; }
            set
            {
                if (this.m_OrderedByInitials != value)
                {
                    this.m_OrderedByInitials = value;
                    this.NotifyPropertyChanged("OrderedByInitials");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
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
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
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
		[PersistentDataColumnProperty(true, "11", "null", "int")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "1", "null", "bit")]
        public bool Final
        {
            get { return this.m_Final; }
            set
            {
                if (this.m_Final != value)
                {
                    this.m_Final = value;
                    this.NotifyPropertyChanged("Final");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public Nullable<int> FinaledById
        {
            get { return this.m_FinaledById; }
            set
            {
                if (this.m_FinaledById != value)
                {
                    this.m_FinaledById = value;
                    this.NotifyPropertyChanged("FinaledById");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string FinaledByInitials
        {
            get { return this.m_FinaledByInitials; }
            set
            {
                if (this.m_FinaledByInitials != value)
                {
                    this.m_FinaledByInitials = value;
                    this.NotifyPropertyChanged("FinaledByInitials");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public DateTime? FinalDate
        {
            get { return this.m_FinalDate; }
            set
            {
                if (this.m_FinalDate != value)
                {
                    this.m_FinalDate = value;
                    this.NotifyPropertyChanged("FinalDate");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string AcknowledgementType
		{
			get { return this.m_AcknowledgementType; }
			set
			{
				if (this.m_AcknowledgementType != value)
				{
					this.m_AcknowledgementType = value;
					this.NotifyPropertyChanged("AcknowledgementType");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public DateTime? TaskDate
		{
			get { return this.m_TaskDate; }
			set
			{
				if (this.m_TaskDate != value)
				{
					this.m_TaskDate = value;
					this.NotifyPropertyChanged("TaskDate");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
