using System;
using System.ComponentModel;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test
{
	[PersistentClass("tblPanelOrder", "YPIDATA")]
	public class PanelOrder : Interface.IPanelOrder, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.Test.Model.TestOrderCollection m_TestOrderCollection;		

		protected string m_ObjectId;
		protected string m_PanelOrderId;
        protected string m_ReportNo;
        protected int m_PanelId;
        protected string m_PanelName;
        protected string m_ResultCode;
        protected int m_PanelOrderBatchId;
        protected bool m_Acknowledged;
        protected Nullable<DateTime> m_AcknowledgedDate;
        protected Nullable<DateTime> m_AcknowledgedTime;
        protected int m_AcceptedById;
        protected bool m_Accepted;
        protected Nullable<DateTime> m_AcceptedDate;
        protected Nullable<DateTime> m_AcceptedTime;
        protected int m_AcknowledgedById;
        protected int m_OrderedById;
        protected Nullable<DateTime> m_OrderDate;
        protected Nullable<DateTime> m_OrderTime;
        protected string m_Comment;
        protected int m_AssignedToId;        

        public PanelOrder()
        {            
			this.m_TestOrderCollection = new YellowstonePathology.Business.Test.Model.TestOrderCollection();
		}

		public PanelOrder(string reportNo, string objectId, string panelOrderId, YellowstonePathology.Business.Panel.Model.Panel panel, int orderedById)
		{
            this.m_ReportNo = reportNo;
			this.m_ObjectId = objectId;
            this.m_PanelOrderId = panelOrderId;
            this.m_PanelId = panel.PanelId;
            this.m_PanelName = panel.PanelName;
            this.m_OrderedById = orderedById;
            this.m_OrderDate = DateTime.Today;
			this.m_OrderTime = DateTime.Now;
            this.m_ResultCode = panel.ResultCode;

            if (panel.AcknowledgeOnOrder == true)
            {
                this.m_Acknowledged = true;
                this.m_AcknowledgedById = orderedById;
                this.m_AcknowledgedDate = DateTime.Today;
                this.m_AcknowledgedTime = DateTime.Now;
            }

			this.m_TestOrderCollection = new YellowstonePathology.Business.Test.Model.TestOrderCollection();
            this.NotifyPropertyChanged(string.Empty);
		}

		public virtual void AcceptResults(Rules.RuleExecutionStatus ruleExecutionStatus, Test.AccessionOrder accessionOrder, Business.User.SystemUser acceptingUser)
		{
			System.Windows.MessageBox.Show("Accept Results is not implemented.");
		}

		public virtual void AcceptResults()
		{
			if (this.Accepted == false)
			{
				this.Accepted = true;
				this.AcceptedById = Business.User.SystemIdentity.Instance.User.UserId;
				this.AcceptedDate = DateTime.Today;
				this.AcceptedTime = DateTime.Now;
			}
		}                      		     

        public void AutoAcceptResults()
        {
            if (this.Accepted == false)
            {
                this.Accepted = true;
                this.AcceptedById = 5051;
                this.AcceptedDate = DateTime.Today;
                this.AcceptedTime = DateTime.Now;
            }
        }

        public void AutoAssign()
        {
            if (this.AssignedToId == 0)
            {
                this.AssignedToId = 5051;
            }
        }

		public virtual void UnacceptResults()
		{
			this.Accepted = false;
			this.AcceptedById = 0;
			this.AcceptedDate = null;
			this.AcceptedTime = null;
		}

		public virtual bool CanAcceptResults()
		{
			bool result = true;
			foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in this.TestOrderCollection)
			{
				if (string.IsNullOrEmpty(testOrder.Result) == true)
				{
					result = false;
					break;
				}
			}
			return result;
		}        

		public virtual void SetResultsAsNormal(AccessionOrder accessionOrder, YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus)
		{
			System.Windows.MessageBox.Show("Set Results As Normal is not implemented.");
		}

		public virtual void MarkAsUnassigned()
        {
            if (this.Accepted != true)
            {
				this.PanelOrderBatchId = 0;
            }
        }

		[PersistentCollection()]
		public YellowstonePathology.Business.Test.Model.TestOrderCollection TestOrderCollection
		{
			get { return this.m_TestOrderCollection; }
            set { this.m_TestOrderCollection = value; }
		}

		public string DisplayAcknowledge
		{
			get
			{
				string display = this.PanelName;
				if ((this.PanelId == 19 || this.PanelId == 21) && this.Acknowledged)
				{
					display += " - " + this.AcknowledgedTime.Value.ToShortDateString() + " " + this.AcknowledgedTime.Value.ToShortTimeString();
				}
				return display;
			}

			set { }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public virtual void FromXml(XElement xml)
		{
			throw new NotImplementedException("FromXml not implemented in PanelOrder");
		}

		public virtual XElement ToXml()
		{
			throw new NotImplementedException("ToXml not implemented in PanelOrder");
		}

		public string OrderedBy
		{
			get
			{
				string result = string.Empty;
				if (this.m_OrderedById > 0)
				{
					result = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(this.m_OrderedById).Initials;
				}
				return result;
			}
		}

		public string AcceptedBy
		{
			get
			{
				string result = string.Empty;
				if (this.m_AcceptedById > 0)
				{
					result = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(this.m_AcceptedById).Initials;
				}
				return result;
			}
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
		public string PanelOrderId
		{
			get { return this.m_PanelOrderId; }
			set
			{
				if (this.m_PanelOrderId != value)
				{
					this.m_PanelOrderId = value;
					this.NotifyPropertyChanged("PanelOrderId");
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

		[PersistentProperty(true)]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int PanelId
		{
			get { return this.m_PanelId; }
			set
			{
				if (this.m_PanelId != value)
				{
					this.m_PanelId = value;
					this.NotifyPropertyChanged("PanelId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "100", "null", "varchar")]
		public string PanelName
		{
			get { return this.m_PanelName; }
			set
			{
				if (this.m_PanelName != value)
				{
					this.m_PanelName = value;
					this.NotifyPropertyChanged("PanelName");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ResultCode
        {
            get { return this.m_ResultCode; }
            set
            {
                if (this.m_ResultCode != value)
                {
                    this.m_ResultCode = value;
                    this.NotifyPropertyChanged("ResultCode");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "11", "0", "int")]
		public int PanelOrderBatchId
		{
			get { return this.m_PanelOrderBatchId; }
			set
			{
				if (this.m_PanelOrderBatchId != value)
				{
					this.m_PanelOrderBatchId = value;
					this.NotifyPropertyChanged("PanelOrderBatchId");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
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
		public Nullable<DateTime> AcknowledgedDate
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
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> AcknowledgedTime
		{
			get { return this.m_AcknowledgedTime; }
			set
			{
				if (this.m_AcknowledgedTime != value)
				{
					this.m_AcknowledgedTime = value;
					this.NotifyPropertyChanged("AcknowledgedTime");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int AcceptedById
		{
			get { return this.m_AcceptedById; }
			set
			{
				if (this.m_AcceptedById != value)
				{
					this.m_AcceptedById = value;
					this.NotifyPropertyChanged("AcceptedById");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "1", "0", "tinyint")]
		public bool Accepted
		{
			get { return this.m_Accepted; }
			set
			{
				if (this.m_Accepted != value)
				{
					this.m_Accepted = value;
					this.NotifyPropertyChanged("Accepted");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> AcceptedDate
		{
			get { return this.m_AcceptedDate; }
			set
			{
				if (this.m_AcceptedDate != value)
				{
					this.m_AcceptedDate = value;
					this.NotifyPropertyChanged("AcceptedDate");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> AcceptedTime
		{
			get { return this.m_AcceptedTime; }
			set
			{
				if (this.m_AcceptedTime != value)
				{
					this.m_AcceptedTime = value;
					this.NotifyPropertyChanged("AcceptedTime");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int AcknowledgedById
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
		[PersistentDataColumnProperty(false, "11", "0", "int")]
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
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> OrderDate
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
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public Nullable<DateTime> OrderTime
		{
			get { return this.m_OrderTime; }
			set
			{
				if (this.m_OrderTime != value)
				{
					this.m_OrderTime = value;
					this.NotifyPropertyChanged("OrderTime");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		[PersistentDataColumnProperty(false, "11", "0", "int")]
		public int AssignedToId
		{
			get { return this.m_AssignedToId; }
			set
			{
				if (this.m_AssignedToId != value)
				{
					this.m_AssignedToId = value;
					this.NotifyPropertyChanged("AssignedToId");
				}
			}
		}            

        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
        }        
	}
}
