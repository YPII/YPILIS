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
        private string m_FromLocationId;
        private string m_FromLocationName;
        private string m_ToFacilityId;
        private string m_ToFacilityName;
        private string m_ToLocationId;
        private string m_ToLocationName;
		private DateTime m_OpenDate;
		private DateTime? m_ClosedDate;
		private bool m_IsOpen;
        private string m_TrackingInformation;
        private string m_MasterAccessionNo;

        public MaterialTrackingBatch()
        {
            
        }

        public MaterialTrackingBatch(string objectId, string description, YellowstonePathology.Business.Facility.Model.Facility fromFacility, YellowstonePathology.Business.Facility.Model.Location fromLocation,
            YellowstonePathology.Business.Facility.Model.Facility toFacility, YellowstonePathology.Business.Facility.Model.Location toLocation, string masterAccessionNo)
        {
            this.m_MaterialTrackingBatchId = Guid.NewGuid().ToString();
			this.m_ObjectId = objectId;
            this.m_Description = description;

            this.m_BatchDate = DateTime.Now;
			this.m_OpenDate = DateTime.Now;
            this.m_FromFacilityId = fromFacility.FacilityId;
            this.m_FromFacilityName = fromFacility.FacilityName;
            this.m_FromLocationId = fromLocation.LocationId;
            this.m_FromLocationName = fromLocation.Description;
            this.m_ToFacilityId = toFacility.FacilityId;
            this.m_ToFacilityName = toFacility.FacilityName;
            this.m_ToLocationId = toLocation.LocationId;
            this.m_ToLocationName = toLocation.Description;
			this.m_IsOpen = true;
            this.m_MasterAccessionNo = masterAccessionNo;
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
		public string FromLocationId
		{
			get { return this.m_FromLocationId; }
			set
			{
				if(this.m_FromLocationId != value)
				{
					this.m_FromLocationId = value;
                    this.NotifyPropertyChanged("FromLocationId");
				}
			}
		}

        [PersistentProperty()]
		public string FromLocationName
		{
			get { return this.m_FromLocationName; }
			set
			{
				if(this.m_FromLocationName != value)
				{
					this.m_FromLocationName = value;
					this.NotifyPropertyChanged("FromLocationName");
				}
			}
		}

        [PersistentProperty()]
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
        public string ToLocationId
        {
            get { return this.m_ToLocationId; }
            set
            {
                if (this.m_ToLocationId != value)
                {
                    this.m_ToLocationId = value;
                    this.NotifyPropertyChanged("ToLocationId");
                }
            }
        }

        [PersistentProperty()]
        public string ToLocationName
        {
            get { return this.m_ToLocationName; }
            set
            {
                if (this.m_ToLocationName != value)
                {
                    this.m_ToLocationName = value;
                    this.NotifyPropertyChanged("ToLocationName");
                }
            }
        }

		[PersistentProperty()]
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
