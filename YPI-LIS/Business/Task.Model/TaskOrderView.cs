using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskOrderView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_TaskOrderId;
        protected string m_MasterAccessionNo;
        protected string m_ReportNo;
        protected string m_TargetDescription;
        protected string m_PanelSetName;
        protected DateTime? m_OrderDate;
        protected string m_OrderedBy;
        protected DateTime? m_AcknowledgedDate;
        protected string m_TrackingNumber;
        protected string m_PLastName;

        public TaskOrderView()
        {
        
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "20", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "20", "null", "varchar")]
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
        public string PLastName
        {
            get { return this.m_PLastName; }
            set
            {
                if (this.m_PLastName != value)
                {
                    this.m_PLastName = value;
                    this.NotifyPropertyChanged("PLastName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "250", "null", "varchar")]
        public string TrackingNumber
        {
            get { return this.m_TrackingNumber; }
            set
            {
                if (this.m_TrackingNumber != value)
                {
                    this.m_TrackingNumber = value;
                    this.NotifyPropertyChanged("TrackingNumber");
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
        [PersistentDataColumnProperty(true, "250", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string OrderedBy
        {
            get { return this.m_OrderedBy; }
            set
            {
                if (this.m_OrderedBy != value)
                {
                    this.m_OrderedBy = value;
                    this.NotifyPropertyChanged("OrderedBy");
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
