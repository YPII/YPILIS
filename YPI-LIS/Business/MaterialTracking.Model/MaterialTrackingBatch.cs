using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.MaterialTracking.Model
{
	[PersistentClass("tblMaterialTrackingBatch", "YPIDATA")]
    public class MaterialTrackingBatch : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_MaterialTrackingBatchId;
        private DateTime m_BatchDate;
		private string m_Description;		
		private string m_FromFacilityId;
		private string m_FromFacilityName;
        private string m_FromLocation;
        private string m_ToFacilityId;
        private string m_ToFacilityName;
        private string m_ToLocation;
		private DateTime m_OpenDate;
		private DateTime? m_ClosedDate;
		private bool m_IsOpen;
        private string m_TrackingInformation;
        private string m_MasterAccessionNo;

        private MaterialTrackingLogCollection m_MaterialTrackingLogCollection;

        public MaterialTrackingBatch()
        {
            this.m_MaterialTrackingLogCollection = new Model.MaterialTrackingLogCollection();
        }

        public MaterialTrackingBatch(string objectId, string description, YellowstonePathology.Business.Facility.Model.Facility fromFacility, string fromLocation,
            YellowstonePathology.Business.Facility.Model.Facility toFacility, string toLocation, string masterAccessionNo)
        {
            this.m_MaterialTrackingBatchId = Guid.NewGuid().ToString();
			this.m_ObjectId = objectId;
            this.m_Description = description;

            this.m_BatchDate = DateTime.Now;
			this.m_OpenDate = DateTime.Now;
            this.m_FromFacilityId = fromFacility.FacilityId;
            this.m_FromFacilityName = fromFacility.FacilityName;
            this.m_FromLocation = fromLocation;
            this.m_ToFacilityId = toFacility.FacilityId;
            this.m_ToFacilityName = toFacility.FacilityName;
            this.m_ToLocation = toLocation;
			this.m_IsOpen = true;
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_MaterialTrackingLogCollection = new Model.MaterialTrackingLogCollection();
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
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public DateTime BatchDate
        {
            get { return this.m_BatchDate; }
            set
            {
                if (this.m_BatchDate != value)
                {
                    this.m_BatchDate = value;
                    this.NotifyPropertyChanged("BatchDate");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string FromFacilityId
        {
            get { return this.m_FromFacilityId; }
            set
            {
                if (this.m_FromFacilityId != value)
                {
                    this.m_FromFacilityId = value;
                    this.NotifyPropertyChanged("FromFacilityId");
                }
            }
        }


        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string FromFacilityName
        {
            get { return this.m_FromFacilityName; }
            set
            {
                if (this.m_FromFacilityName != value)
                {
                    this.m_FromFacilityName = value;
                    this.NotifyPropertyChanged("FromFacilityName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string FromLocation
		{
			get { return this.m_FromLocation; }
			set
			{
				if(this.m_FromLocation != value)
				{
					this.m_FromLocation = value;
                    this.NotifyPropertyChanged("FromLocation");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ToFacilityId
        {
            get { return this.m_ToFacilityId; }
            set
            {
                if (this.m_ToFacilityId != value)
                {
                    this.m_ToFacilityId = value;
                    this.NotifyPropertyChanged("ToFacilityId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string ToFacilityName
        {
            get { return this.m_ToFacilityName; }
            set
            {
                if (this.m_ToFacilityName != value)
                {
                    this.m_ToFacilityName = value;
                    this.NotifyPropertyChanged("ToFacilityName");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string ToLocation
        {
            get { return this.m_ToLocation; }
            set
            {
                if (this.m_ToLocation != value)
                {
                    this.m_ToLocation = value;
                    this.NotifyPropertyChanged("ToLocation");
                }
            }
        }

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public DateTime OpenDate
		{
			get { return this.m_OpenDate; }
			set
			{
				if (this.m_OpenDate != value)
				{
					this.m_OpenDate = value;
					this.NotifyPropertyChanged("OpenDate");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "3", "null", "datetime")]
		public DateTime? ClosedDate
		{
			get { return this.m_ClosedDate; }
			set
			{
				if (this.m_ClosedDate != value)
				{
					this.m_ClosedDate = value;
					this.NotifyPropertyChanged("ClosedDate");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1", "null", "tinyint")]
		public bool IsOpen
		{
			get { return this.m_IsOpen; }
			set
			{
				if (this.m_IsOpen != value)
				{
					this.m_IsOpen = value;
					this.NotifyPropertyChanged("IsOpen");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string TrackingInformation
        {
            get { return this.m_TrackingInformation; }
            set
            {
                if (this.m_TrackingInformation != value)
                {
                    this.m_TrackingInformation = value;
                    this.NotifyPropertyChanged("TrackingInformation");
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

        [PersistentCollection()]
        public MaterialTrackingLogCollection MaterialTrackingLogCollection
        {
            get { return this.m_MaterialTrackingLogCollection; }
            set
            {
                this.m_MaterialTrackingLogCollection = value;
                this.NotifyPropertyChanged("MaterialTrackingLogCollection");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		public void Close()
		{			
			this.m_IsOpen = false;
			this.m_ClosedDate = DateTime.Now;
		}
	}
}
