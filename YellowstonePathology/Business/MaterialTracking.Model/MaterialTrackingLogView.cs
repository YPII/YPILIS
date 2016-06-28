using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.MaterialTracking.Model
{	
    public class MaterialTrackingLogView : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_MaterialTrackingBatchId;
        private string m_MaterialTrackingLogId;
        private string m_MasterAccessionNo;
        private string m_PLastName;
        private string m_PFirstName;
        private string m_MaterialType;
        private string m_MaterialId;
        private string m_MaterialLabel;
        private DateTime m_LogDate;
        private string m_LoggedBy;

        public MaterialTrackingLogView()
        {
            
        }

        [PersistentProperty()]
		public string MaterialTrackingBatchId
		{
			get { return this.m_MaterialTrackingBatchId; }
			set
			{
				if(this.m_MaterialTrackingBatchId != value)
				{
					this.m_MaterialTrackingBatchId = value;
					this.NotifyPropertyChanged("MaterialTrackingBatchId");					
				}
			}
		}

        [PersistentProperty()]
        public string MaterialTrackingLogId
        {
            get { return this.m_MaterialTrackingLogId; }
            set
            {
                if (this.m_MaterialTrackingLogId != value)
                {
                    this.m_MaterialTrackingLogId = value;
                    this.NotifyPropertyChanged("MaterialTrackingLogId");
                }
            }
        }

        [PersistentProperty()]
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
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set
            {
                if (this.m_PFirstName != value)
                {
                    this.m_PFirstName = value;
                    this.NotifyPropertyChanged("PFirstName");
                }
            }
        }

        [PersistentProperty()]
        public string MaterialType
        {
            get { return this.m_MaterialType; }
            set
            {
                if (this.m_MaterialType != value)
                {
                    this.m_MaterialType = value;
                    this.NotifyPropertyChanged("MaterialType");
                }
            }
        }

        [PersistentProperty()]
        public string MaterialId
        {
            get { return this.m_MaterialId; }
            set
            {
                if (this.m_MaterialId != value)
                {
                    this.m_MaterialId = value;
                    this.NotifyPropertyChanged("MaterialId");
                }
            }
        }

        [PersistentProperty()]
        public string MaterialLabel
        {
            get { return this.m_MaterialLabel; }
            set
            {
                if (this.m_MaterialLabel != value)
                {
                    this.m_MaterialLabel = value;
                    this.NotifyPropertyChanged("MaterialLabel");
                }
            }
        }

        [PersistentProperty()]
        public DateTime LogDate
        {
            get { return this.m_LogDate; }
            set
            {
                if (this.m_LogDate != value)
                {
                    this.m_LogDate = value;
                    this.NotifyPropertyChanged("LogDate");
                }
            }
        }

        [PersistentProperty()]
        public string LoggedBy
        {
            get { return this.m_LoggedBy; }
            set
            {
                if (this.m_LoggedBy != value)
                {
                    this.m_LoggedBy = value;
                    this.NotifyPropertyChanged("LoggedBy");
                }
            }
        }

        public void FromScannedItemView(MaterialTrackingScannedItemView materialTrackingScannedItemView, MaterialTrackingLog materialTackingLog)
        {
            this.m_MaterialTrackingLogId = materialTackingLog.MaterialTrackingLogId;
            this.m_MaterialTrackingBatchId = materialTackingLog.MaterialTrackingBatchId;
            this.m_MaterialId = materialTrackingScannedItemView.MaterialId;
            this.m_MaterialType = materialTrackingScannedItemView.MaterialType;
            this.m_MasterAccessionNo = materialTrackingScannedItemView.MasterAccessionNo;
            this.m_PLastName = materialTrackingScannedItemView.PLastName;
            this.m_PFirstName = materialTrackingScannedItemView.PFirstName;
            this.m_MaterialLabel = materialTrackingScannedItemView.MaterialLabel;
            this.m_LogDate = materialTackingLog.LogDate;
            this.m_LoggedBy = materialTackingLog.LoggedBy;
        }

        public void FromScannedItemView(MaterialTrackingLog materialTackingLog)
        {
            this.m_MaterialTrackingLogId = materialTackingLog.MaterialTrackingLogId;
            this.m_MaterialTrackingBatchId = materialTackingLog.MaterialTrackingBatchId;
            this.m_MaterialId = materialTackingLog.MaterialId;
            this.m_MaterialType = materialTackingLog.MaterialType;            
            this.m_LogDate = materialTackingLog.LogDate;
            this.m_LoggedBy = materialTackingLog.LoggedBy;
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
