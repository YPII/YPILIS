using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Specimen.Model
{
	[PersistentClass("tblSpecimenOrder", "YPIDATA")]
	public partial class SpecimenOrder : INotifyPropertyChanged, YellowstonePathology.Business.Interface.IOrderTarget
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private string m_SpecimenOrderId;
        private string m_SpecimenId;
        private string m_SpecimenType;
        private Nullable<DateTime> m_CollectionDate;
        private Nullable<DateTime> m_CollectionTime;
        private string m_SpecimenSite;
        private string m_FixationType;        
        
        private string m_Description;
        private int m_AliquotRequestCount;
        private Nullable<DateTime> m_AccessionTime;
        private string m_ClientFixation;
        private string m_LabFixation;
        private bool m_CollectionTimeUnknown;        
        private string m_MasterAccessionNo;
        
        private int m_SpecimenNumber;        
        private Nullable<int> m_ClientSpecimenNumber;
        private bool m_ClientAccessioned;
        private bool m_Accepted;
        private string m_SpecialInstructions;
        private string m_ContainerId;
        private bool m_Verified;
        private int m_VerifiedById;
        private Nullable<DateTime> m_VerifiedDate;
        private bool m_RequiresBlindVerification;
        private string m_SystemInitiatingOrder;
        private string m_SpecimenSource;
        private Nullable<DateTime> m_DateReceived;
        private string m_SpecimenAdequacy;
        private string m_LocationFacilityId;
        private string m_OwnerFacilityId;
        private bool m_RequiresGrossExamination;
        private string m_LocationId;
        private string m_FacilityId;
        
        private string m_ProcessorRun;
        private string m_ProcessorRunId;
        private Nullable<DateTime> m_ProcessorStartTime;
        private Nullable<int> m_ProcessorFixationTime;        
        private bool m_FixationStartTimeManuallyEntered;
        private Nullable<int> m_TimeToFixation;
        private Nullable<DateTime> m_FixationStartTime;
        private Nullable<DateTime> m_FixationEndTime;
        private Nullable<int> m_FixationDuration;
        private string m_FixationComment;
        private string m_TimeToFixationHourString;

		private YellowstonePathology.Business.Test.AliquotOrderCollection m_AliquotOrderCollection;		
		private List<YellowstonePathology.Business.Test.Model.TestOrder> m_PeerReviewTestOrders;		
		private bool m_DeletePending = false;
		private bool m_ApplyFixation = true;

		public SpecimenOrder()
		{
			this.m_AliquotOrderCollection = new YellowstonePathology.Business.Test.AliquotOrderCollection();			
			this.m_PeerReviewTestOrders = new List<YellowstonePathology.Business.Test.Model.TestOrder>();			
		}

		public SpecimenOrder(string specimenOrderId, string objectId, string masterAccessionNo)
		{
			this.m_SpecimenOrderId = specimenOrderId;
			this.m_ObjectId = objectId;
			this.m_MasterAccessionNo = masterAccessionNo;
			this.AccessionTime = DateTime.Now;
			this.Accepted = true;

			this.m_AliquotOrderCollection = new YellowstonePathology.Business.Test.AliquotOrderCollection();			
			this.m_PeerReviewTestOrders = new List<YellowstonePathology.Business.Test.Model.TestOrder>();
		}        

		public void FromClientOrderDetail(YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
		{
			this.m_AliquotRequestCount = 1;

            this.m_ClientAccessioned = clientOrderDetail.ClientAccessioned;
			this.m_ContainerId = clientOrderDetail.ContainerId;
			this.m_Description = clientOrderDetail.DescriptionToAccession;
			this.m_SpecialInstructions = clientOrderDetail.SpecialInstructions;

			YellowstonePathology.Business.Helper.DateTimeSplitter dateTimeSplitter = new YellowstonePathology.Business.Helper.DateTimeSplitter(clientOrderDetail.CollectionDate);
            this.m_CollectionDate = dateTimeSplitter.GetDate();
            this.m_CollectionTime = dateTimeSplitter.GetDateWithTime();

            this.m_FixationStartTimeManuallyEntered = clientOrderDetail.FixationStartTimeManuallyEntered;
            this.m_DateReceived = clientOrderDetail.DateReceived;            
			this.m_ClientFixation = clientOrderDetail.ClientFixation;
            this.m_FixationStartTime = clientOrderDetail.FixationStartTime;
            this.m_TimeToFixation = clientOrderDetail.TimeToFixation;
            this.m_TimeToFixationHourString = clientOrderDetail.TimeToFixationHourString;
            this.m_FixationComment = clientOrderDetail.FixationComment;            
			this.m_LabFixation = clientOrderDetail.LabFixation;
            this.m_SpecimenSource = clientOrderDetail.SpecimenSource;
            this.m_RequiresGrossExamination = clientOrderDetail.RequiresGrossExamination;

            this.SetTimeToFixation();
		}

		public void SetAliquotRequestCount()
		{
			this.AliquotRequestCount = this.AliquotOrderCollection.Count;
		}

		public void AcceptResults()
		{

		}

		public void AccessionNGCTFromCytology()
		{

		}

		public string GetCollectionDateTimeString()
		{
			string result = string.Empty;
			if (this.m_CollectionDate.HasValue == true)
			{
				if (this.m_CollectionTime.HasValue == true)
				{
					result = this.m_CollectionDate.Value.ToShortDateString() + " " + this.m_CollectionTime.Value.ToString("HH:mm");
				}
				else
				{
					result = this.m_CollectionDate.Value.ToShortDateString() + " : Time not given";
				}
			}
			return result;
		}		

        public string GetGrossSubmittedInString()
        {
            string result = null;
            if (this.m_AliquotOrderCollection.Count != 0)
            {
                result = "entirely submitted into ";
                if (this.m_AliquotOrderCollection.Count == 1)
                {
                	result += "cassette \"" + this.m_AliquotOrderCollection[0].Label + "\"";
                }
                else
                {
                    result += "cassettes \"" + this.m_AliquotOrderCollection[0].Label + "\" - \"" + this.m_AliquotOrderCollection[this.m_AliquotOrderCollection.Count - 1].Label + "\"";
                }                                              
            }
            return result;
        }

        public string GetSummarySubmissionString()
        { 
            StringBuilder result = new StringBuilder("CASSETTE SUMMARY: ");
            result.AppendLine();
            for(int i=0; i<this.m_AliquotOrderCollection.Count; i++)
            {
                string text = "\"" + this.m_AliquotOrderCollection[i].Label + "\" - [???]";
                if(i < this.m_AliquotOrderCollection.Count - 1)
                {
                    result.AppendLine(text);
                }
                else
                {
                    result.Append(text);                    
                }                
            }                        
            return result.ToString();
        }

        public string GetRepresentativeSectionsSubmittedIn()
        {
            string result = null;
            if (this.m_AliquotOrderCollection.Count != 0)
            {
                result = "Representative sections are submitted to ";               
                if (this.m_AliquotOrderCollection.Count == 1)
                {
                    result += "cassette \"" + this.m_AliquotOrderCollection[0].Label + "\"";
                }
                else
                {
                    result += "cassettes \"" + this.m_AliquotOrderCollection[0].Label + "\" - \"" + this.m_AliquotOrderCollection[this.m_AliquotOrderCollection.Count - 1].Label + "\"";
                }               
            }
            return result;
        }

        public string GetGrossRemainderSubmittedInString()
        {
            string result = null;
            if (this.m_AliquotOrderCollection.Count != 0)
            {
                result = "entirely submitted into ";
                if (this.m_AliquotOrderCollection.Count == 2)
                {
                    result += "cassette \"" + this.m_AliquotOrderCollection[1].Label + "\"";
                }
                else if (this.m_AliquotOrderCollection.Count > 2)
                {
                    result += "cassettes \"" + this.m_AliquotOrderCollection[1].Label + "\" - \"" + this.m_AliquotOrderCollection[this.m_AliquotOrderCollection.Count - 1].Label + "\"";
                }                
            }
            return result;
        }

        public string GetGrossMiddleCassettesSubmittedInString()
        {
            string result = null;
            if (this.m_AliquotOrderCollection.Count == 3)
            {
                result += " \"" + this.m_AliquotOrderCollection[1].Label + "\" ";
            }
            else if (this.m_AliquotOrderCollection.Count > 3)
            {                                
                result += " \"" + this.m_AliquotOrderCollection[1].Label + "\" - \"" + this.m_AliquotOrderCollection[this.m_AliquotOrderCollection.Count - 2].Label + "\" ";                
            }
            return result;
        }

        public string GetSpecimenDescriptionString()
		{
			string result = this.SpecimenNumber.ToString() + ".) " + this.Description;
			return result;
		}

        public string SpecimenDescriptionString
        {
            get 
            {
                return this.SpecimenNumber.ToString() + ".) " + this.Description;
            }            
        }               				

		[PersistentCollection()]
		public YellowstonePathology.Business.Test.AliquotOrderCollection AliquotOrderCollection
		{
			get { return m_AliquotOrderCollection; }
            set { this.m_AliquotOrderCollection = value; }
		}		

		public bool DeletePending
		{
			get { return m_DeletePending; }
			set
			{
				m_DeletePending = value;
				NotifyPropertyChanged("DeletePending");
			}
		}

		public bool ApplyFixation
		{
			get { return m_ApplyFixation; }
			set { m_ApplyFixation = value; }
		}        

		public string DescriptionWithCount
		{
			get
			{
				string retval = Description + "  ";
				string firstDisplay = string.Empty;
				string lastDisplay = string.Empty;
				int count = 0;
				foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in this.AliquotOrderCollection)
				{
					if (aliquotOrder.AliquotType == "Block" || aliquotOrder.AliquotType == "FrozenBlock" || aliquotOrder.AliquotType == "CellBlock")
					{
						count++;
						if (firstDisplay.Length == 0)
						{
							firstDisplay = aliquotOrder.Display;
						}
						if (count > 1)
						{
							lastDisplay = aliquotOrder.Display;
						}
					}
				}

				retval += firstDisplay;

				if (count > 1)
				{
					retval += " - " + lastDisplay;
				}
				retval += "  (" + count.ToString() + ")";
				return retval;
			}
		}		

        public string GetId()
        {
            return this.m_SpecimenOrderId;
        }

        public YellowstonePathology.Business.Interface.IOrderTargetType GetTargetType()
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenCollection specimenCollection = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.GetAll();
            return specimenCollection.GetSpecimen(this.m_SpecimenId);
        }

        public string GetOrderedOnType()
        {
            return YellowstonePathology.Business.OrderedOn.Specimen;
        }

        public string GetDescription()
        {
            return this.m_Description;
        }

		public bool ReceivedFresh
		{
			get { return this.m_ClientFixation == "Fresh"; }
		}

		public YellowstonePathology.Business.Test.Model.TestOrderCollection GetTestOrders(YellowstonePathology.Business.Test.Model.TestOrderCollection testForPanelSetCollection)
		{
			YellowstonePathology.Business.Test.Model.TestOrderCollection result = new YellowstonePathology.Business.Test.Model.TestOrderCollection();
			foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in testForPanelSetCollection)
			{				
                if (testOrder.AliquotOrder.SpecimenOrderId == this.SpecimenOrderId)
                {
                    result.Add(testOrder);
                }
			}

			return result;
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
		public string SpecimenOrderId
		{
			get { return this.m_SpecimenOrderId; }
			set
			{
				if (this.m_SpecimenOrderId != value)
				{
					this.m_SpecimenOrderId = value;
					this.NotifyPropertyChanged("SpecimenOrderId");
				}
			}
		}

        [PersistentProperty()]
        public string SpecimenId
        {
            get { return this.m_SpecimenId; }
            set
            {
                if (this.m_SpecimenId != value)
                {
                    this.m_SpecimenId = value;
                    this.NotifyPropertyChanged("SpecimenId");
                }
            }
        }

		[PersistentProperty()]
		public string SpecimenType
		{
			get { return this.m_SpecimenType; }
			set
			{
				if (this.m_SpecimenType != value)
				{
					this.m_SpecimenType = value;
					this.NotifyPropertyChanged("SpecimenType");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> CollectionDate
		{
			get { return this.m_CollectionDate; }
			set
			{
				if (this.m_CollectionDate != value)
				{
					this.m_CollectionDate = value;
					this.NotifyPropertyChanged("CollectionDate");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> CollectionTime
		{
			get { return this.m_CollectionTime; }
			set
			{
				if (this.m_CollectionTime != value)
				{
					this.m_CollectionTime = value;
					this.NotifyPropertyChanged("CollectionTime");
				}
			}
		}

		[PersistentProperty()]
		public string SpecimenSite
		{
			get { return this.m_SpecimenSite; }
			set
			{
				if (this.m_SpecimenSite != value)
				{
					this.m_SpecimenSite = value;
					this.NotifyPropertyChanged("SpecimenSite");
				}
			}
		}

		[PersistentProperty()]
		public string FixationType
		{
			get { return this.m_FixationType; }
			set
			{
				if (this.m_FixationType != value)
				{
					this.m_FixationType = value;
					this.NotifyPropertyChanged("FixationType");
				}
			}
		}

        [PersistentProperty()]
        public Nullable<int> FixationDuration
        {
            get { return this.m_FixationDuration; }
            set
            {
                if (this.m_FixationDuration != value)
                {
                    this.m_FixationDuration = value;
                    this.NotifyPropertyChanged("FixationDuration");
                }
            }
        }

        public void SetFixationDuration()
        {
            if (this.m_FixationEndTime.HasValue == true && this.m_FixationStartTime.HasValue == true)
            {
                TimeSpan timespan = new TimeSpan(this.m_FixationEndTime.Value.Ticks - this.m_FixationStartTime.Value.Ticks);
                this.m_FixationDuration = Convert.ToInt32(timespan.TotalHours);
            }
            else
            {
                this.m_FixationDuration = null;
            }
            this.NotifyPropertyChanged("FixationDuration");
        }
        
        public string FixationDurationString
        {
            get 
            {
                string result = null;
                if (this.FixationDuration.HasValue == true)
                {
                    if (this.FixationDuration != 0)
                    {
                        result = "~" + this.FixationDuration + " hrs";
                    }
                    else
                    {
                        result = "Unknown";
                    }
                }
                else
                {
                    result = "Unknown";
                }
                return result;
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
		public int AliquotRequestCount
		{
			get { return this.m_AliquotRequestCount; }
			set
			{
				if (this.m_AliquotRequestCount != value)
				{
					this.m_AliquotRequestCount = value;
					this.NotifyPropertyChanged("AliquotRequestCount");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> AccessionTime
		{
			get { return this.m_AccessionTime; }
			set
			{
				if (this.m_AccessionTime != value)
				{
					this.m_AccessionTime = value;
					this.NotifyPropertyChanged("AccessionTime");
				}
			}
		}

		[PersistentProperty()]
		public string ClientFixation
		{
			get { return this.m_ClientFixation; }
			set
			{
				if (this.m_ClientFixation != value)
				{
					this.m_ClientFixation = value;
					this.NotifyPropertyChanged("ClientFixation");
				}
			}
		}

		[PersistentProperty()]
		public string LabFixation
		{
			get { return this.m_LabFixation; }
			set
			{
				if (this.m_LabFixation != value)
				{
					this.m_LabFixation = value;
					this.NotifyPropertyChanged("LabFixation");
				}
			}
		}

		[PersistentProperty()]
		public bool CollectionTimeUnknown
		{
			get { return this.m_CollectionTimeUnknown; }
			set
			{
				if (this.m_CollectionTimeUnknown != value)
				{
					this.m_CollectionTimeUnknown = value;
					this.NotifyPropertyChanged("CollectionTimeUnknown");
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
		public string FixationComment
		{
			get { return this.m_FixationComment; }
			set
			{
				if (this.m_FixationComment != value)
				{
					this.m_FixationComment = value;
					this.NotifyPropertyChanged("FixationComment");
				}
			}
		}

		[PersistentProperty()]
		public int SpecimenNumber
		{
			get { return this.m_SpecimenNumber; }
			set
			{
				if (this.m_SpecimenNumber != value)
				{
					this.m_SpecimenNumber = value;
					this.NotifyPropertyChanged("SpecimenNumber");
				}
			}
		}

        [PersistentProperty()]
        public Nullable<int> ClientSpecimenNumber
        {
            get { return this.m_ClientSpecimenNumber; }
            set
            {
                if (this.m_ClientSpecimenNumber != value)
                {
                    this.m_ClientSpecimenNumber = value;
                    this.NotifyPropertyChanged("ClientSpecimenNumber");
                }
            }
        }

        [PersistentProperty()]
        public bool ClientAccessioned
        {
            get { return this.m_ClientAccessioned; }
            set
            {
                if (this.m_ClientAccessioned != value)
                {
                    this.m_ClientAccessioned = value;
                    this.NotifyPropertyChanged("ClientAccessioned");
                }
            }
        }

		[PersistentProperty()]
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
		public string SpecialInstructions
		{
			get { return this.m_SpecialInstructions; }
			set
			{
				if (this.m_SpecialInstructions != value)
				{
					this.m_SpecialInstructions = value;
					this.NotifyPropertyChanged("SpecialInstructions");
				}
			}
		}

		[PersistentProperty()]
		public string ContainerId
		{
			get { return this.m_ContainerId; }
			set
			{
				if (this.m_ContainerId != value)
				{
					this.m_ContainerId = value;
					this.NotifyPropertyChanged("ContainerId");
				}
			}
		}

		[PersistentProperty()]
		public bool Verified
		{
			get { return this.m_Verified; }
			set
			{
				if (this.m_Verified != value)
				{
					this.m_Verified = value;
					this.NotifyPropertyChanged("Verified");
				}
			}
		}

		[PersistentProperty()]
		public int VerifiedById
		{
			get { return this.m_VerifiedById; }
			set
			{
				if (this.m_VerifiedById != value)
				{
					this.m_VerifiedById = value;
					this.NotifyPropertyChanged("VerifiedById");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> VerifiedDate
		{
			get { return this.m_VerifiedDate; }
			set
			{
				if (this.m_VerifiedDate != value)
				{
					this.m_VerifiedDate = value;
					this.NotifyPropertyChanged("VerifiedDate");
				}
			}
		}

		[PersistentProperty()]
		public bool RequiresBlindVerification
		{
			get { return this.m_RequiresBlindVerification; }
			set
			{
				if (this.m_RequiresBlindVerification != value)
				{
					this.m_RequiresBlindVerification = value;
					this.NotifyPropertyChanged("RequiresBlindVerification");
				}
			}
		}

		[PersistentProperty()]
		public string SystemInitiatingOrder
		{
			get { return this.m_SystemInitiatingOrder; }
			set
			{
				if (this.m_SystemInitiatingOrder != value)
				{
					this.m_SystemInitiatingOrder = value;
					this.NotifyPropertyChanged("SystemInitiatingOrder");
				}
			}
		}

		[PersistentProperty()]
		public string SpecimenSource
		{
			get { return this.m_SpecimenSource; }
			set
			{
				if (this.m_SpecimenSource != value)
				{
					this.m_SpecimenSource = value;
					this.NotifyPropertyChanged("SpecimenSource");
				}
			}
		}

		[PersistentProperty()]
		public Nullable<DateTime> DateReceived
		{
			get { return this.m_DateReceived; }
			set
			{
				if (this.m_DateReceived != value)
				{
					this.m_DateReceived = value;
					this.NotifyPropertyChanged("DateReceived");
				}
			}
		}

		[PersistentProperty()]
		public string SpecimenAdequacy
		{
			get { return this.m_SpecimenAdequacy; }
			set
			{
				if (this.m_SpecimenAdequacy != value)
				{
					this.m_SpecimenAdequacy = value;
					this.NotifyPropertyChanged("SpecimenAdequacy");
				}
			}
		}

        [PersistentProperty()]
        public string LocationFacilityId
        {
            get { return this.m_LocationFacilityId; }
            set
            {
                if (this.m_LocationFacilityId != value)
                {
                    this.m_LocationFacilityId = value;
                    this.NotifyPropertyChanged("LocationFacilityId");
                }
            }
        }

        [PersistentProperty()]
        public string OwnerFacilityId
        {
            get { return this.m_OwnerFacilityId; }
            set
            {
                if (this.m_OwnerFacilityId != value)
                {
                    this.m_OwnerFacilityId = value;
                    this.NotifyPropertyChanged("OwnerFacilityId");
                }
            }
        }

        [PersistentProperty()]
        public bool RequiresGrossExamination
        {
            get { return this.m_RequiresGrossExamination; }
            set
            {
                if (this.m_RequiresGrossExamination != value)
                {
                    this.m_RequiresGrossExamination = value;
                    this.NotifyPropertyChanged("RequiresGrossExamination");
                }
            }
        }

        [PersistentProperty()]
        public string LocationId
        {
            get { return this.m_LocationId; }
            set
            {
                if (this.m_LocationId != value)
                {
                    this.m_LocationId = value;
                    this.NotifyPropertyChanged("LocationId");
                }
            }
        }

        [PersistentProperty()]
        public string FacilityId
        {
            get { return this.m_FacilityId; }
            set
            {
                if (this.m_FacilityId != value)
                {
                    this.m_FacilityId = value;
                    this.NotifyPropertyChanged("FacilityId");
                }
            }
        }

        [PersistentProperty()]
        public string ProcessorRun
        {
            get { return this.m_ProcessorRun; }
            set
            {
                if (this.m_ProcessorRun != value)
                {
                    this.m_ProcessorRun = value;
                    this.NotifyPropertyChanged("ProcessorRun");
                }
            }
        }

        [PersistentProperty()]
        public string ProcessorRunId
        {
            get { return this.m_ProcessorRunId; }
            set
            {
                if (this.m_ProcessorRunId != value)
                {
                    this.m_ProcessorRunId = value;
                    this.NotifyPropertyChanged("ProcessorRunId");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> ProcessorStartTime
        {
            get { return this.m_ProcessorStartTime; }
            set
            {
                if (this.m_ProcessorStartTime != value)
                {
                    this.m_ProcessorStartTime = value;
                    this.NotifyPropertyChanged("ProcessorStartTime");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<int> ProcessorFixationTime
        {
            get { return this.m_ProcessorFixationTime; }
            set
            {
                if (this.m_ProcessorFixationTime != value)
                {
                    this.m_ProcessorFixationTime = value;
                    this.NotifyPropertyChanged("ProcessorFixationTime");
                }
            }
        }        

        [PersistentProperty()]
        public bool FixationStartTimeManuallyEntered
        {
            get { return this.m_FixationStartTimeManuallyEntered; }
            set
            {
                if (this.m_FixationStartTimeManuallyEntered != value)
                {
                    this.m_FixationStartTimeManuallyEntered = value;
                    this.NotifyPropertyChanged("FixationStartTimeManuallyEntered");
                }
            }
        }        

        [PersistentProperty()]
        public Nullable<DateTime> FixationStartTime
        {
            get { return this.m_FixationStartTime; }
            set
            {
                if (this.m_FixationStartTime != value)
                {
                    this.m_FixationStartTime = value;
                    this.NotifyPropertyChanged("FixationStartTime");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> FixationEndTime
        {
            get { return this.m_FixationEndTime; }
            set
            {
                if (this.m_FixationEndTime != value)
                {
                    this.m_FixationEndTime = value;
                    this.NotifyPropertyChanged("FixationEndTime");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<int> TimeToFixation
        {
            get { return this.m_TimeToFixation; }
            set
            {
                if (this.m_TimeToFixation != value)
                {
                    this.m_TimeToFixation = value;
                    this.NotifyPropertyChanged("TimeToFixation");
                }
            }
        }

        [PersistentProperty()]
        public string TimeToFixationHourString
        {
            get { return this.m_TimeToFixationHourString; }
            set
            {
                if (this.m_TimeToFixationHourString != value)
                {
                    this.m_TimeToFixationHourString = value;
                    this.NotifyPropertyChanged("TimeToFixationHourString");
                }
            }
        }

        public void SetFixationStartTime()
        {
            if (this.m_ClientAccessioned == false)
            {
                if (this.m_FixationStartTimeManuallyEntered == false)
                {
                    if (this.m_ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.Fresh)
                    {
                        this.m_FixationStartTime = this.m_DateReceived;
                    }
                    else if (this.m_ClientFixation != YellowstonePathology.Business.Specimen.Model.FixationType.NotApplicable)
                    {
                        this.m_FixationStartTime = this.m_CollectionTime;
                    }
                }
            }
            else
            {
                this.m_FixationStartTime = null;                
            }
            this.NotifyPropertyChanged("FixationStartTime");
        }

        public void SetFixationEndTime()
        {
            if (this.m_FixationStartTime.HasValue == true && this.m_ProcessorStartTime.HasValue == true)
            {
                TimeSpan processorFixationTime = new TimeSpan(0, this.m_ProcessorFixationTime.Value, 0);
                this.m_FixationEndTime = this.m_ProcessorStartTime.Value + processorFixationTime;
                this.NotifyPropertyChanged("FixationEndTime");
            }
        }

        public void SetTimeToFixation()
        {            
            if (this.m_CollectionTime.HasValue == true && this.m_FixationStartTime.HasValue == true)
            {
                TimeSpan timeSpan = new TimeSpan(this.m_FixationStartTime.Value.Ticks - this.m_CollectionTime.Value.Ticks);
                this.m_TimeToFixation = Convert.ToInt32(timeSpan.TotalMinutes);
                
                double ttf = Math.Round(timeSpan.TotalMinutes, 0);
                if (ttf <= 60) this.m_TimeToFixationHourString = YellowstonePathology.Business.Specimen.Model.TimeToFixationType.LessThanOneHour;
                else if (ttf > 60) this.m_TimeToFixationHourString = YellowstonePathology.Business.Specimen.Model.TimeToFixationType.GreaterThanOneHour;
            }
            else
            {
                this.m_TimeToFixation = null;
				this.m_TimeToFixationHourString = "Unknown";
            }  
          
            this.NotifyPropertyChanged("TimeToFixation");
            this.NotifyPropertyChanged("TimeToFixationString");
            this.NotifyPropertyChanged("TimeToFixationHourString");            
        }

        public void SetProcessor(YellowstonePathology.Business.Surgical.ProcessorRun processorRun)
        {            
            this.m_ProcessorRun = processorRun.Name;
            this.m_ProcessorRunId = processorRun.ProcessorRunId;
            this.m_ProcessorFixationTime = Convert.ToInt32(processorRun.FixationTime.TotalMinutes);
            this.m_ProcessorStartTime = processorRun.GetProcessorStartTime(this.m_DateReceived);

            this.SetFixationEndTime();
            this.SetFixationDuration();
            this.NotifyPropertyChanged(string.Empty);            
        }

        public string TimeToFixationString
        {
            get
            {
                string result = null;
                if (this.m_TimeToFixation.HasValue == true)
                {
                    TimeSpan timeSpan = new TimeSpan(0, this.m_TimeToFixation.Value, 0);
                    if (timeSpan.TotalMinutes == 0)
                    {
                        result = "Immediate";
                    }
                    else if (timeSpan.TotalMinutes < 60)
                    {
                        result = timeSpan.TotalMinutes.ToString() + "min";
                    }
                    else
                    {
                        result = timeSpan.Hours + "hrs ";
                        if (timeSpan.Minutes > 0)
                        {
                            result += timeSpan.Minutes + "min";
                        }
                    }
                }
                else
                {
                    if (this.m_ClientAccessioned == false)
                    {
                        result = "Unknown";
                    }                    
                }
                return result;
            }                   
        }        

        public string ProcessorFixationTimeString
        {
            get
            {
                string result = null;
                if (this.m_ProcessorFixationTime.HasValue == true)
                {
                    TimeSpan timeSpan = new TimeSpan(0, this.m_ProcessorFixationTime.Value, 0);
                    if (timeSpan.TotalMinutes == 0)
                    {
                        result = "Unknown";
                    }
                    else if (timeSpan.TotalMinutes < 60)
                    {
                        result = timeSpan.TotalMinutes.ToString() + "min";
                    }
                    else
                    {
                        result = timeSpan.Hours + "hrs ";
                        if (timeSpan.Minutes > 0)
                        {
                            result += timeSpan.Minutes + "min";
                        }
                    }
                }
                else
                {
                    result = "Unknown";
                }
                return result;
            }
        }

		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_SpecimenOrderId = propertyWriter.WriteString("SpecimenOrderId");
			this.m_SpecimenType = propertyWriter.WriteString("SpecimenType");
            this.m_SpecimenId = propertyWriter.WriteString("SpecimenId");
			this.m_CollectionDate = propertyWriter.WriteNullableDateTime("CollectionDate");
			this.m_CollectionTime = propertyWriter.WriteNullableDateTime("CollectionTime");
			this.m_SpecimenSite = propertyWriter.WriteString("SpecimenSite");
			this.m_FixationType = propertyWriter.WriteString("FixationType");			
			this.m_Description = propertyWriter.WriteString("Description");
			this.m_AliquotRequestCount = propertyWriter.WriteInt("AliquotRequestCount");
			this.m_AccessionTime = propertyWriter.WriteNullableDateTime("AccessionTime");
			this.m_ClientFixation = propertyWriter.WriteString("ClientFixation");
			this.m_LabFixation = propertyWriter.WriteString("LabFixation");
			this.m_CollectionTimeUnknown = propertyWriter.WriteBoolean("CollectionTimeUnknown");			
			this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
			this.m_FixationComment = propertyWriter.WriteString("FixationComment");
			this.m_SpecimenNumber = propertyWriter.WriteInt("SpecimenNumber");
            this.m_ClientSpecimenNumber = propertyWriter.WriteNullableInt("ClientSpecimenNumber");
			this.m_Accepted = propertyWriter.WriteBoolean("Accepted");
			this.m_SpecialInstructions = propertyWriter.WriteString("SpecialInstructions");
			this.m_ContainerId = propertyWriter.WriteString("ContainerId");
			this.m_Verified = propertyWriter.WriteBoolean("Verified");
			this.m_VerifiedById = propertyWriter.WriteInt("VerifiedById");
			this.m_VerifiedDate = propertyWriter.WriteNullableDateTime("VerifiedDate");
			this.m_RequiresBlindVerification = propertyWriter.WriteBoolean("RequiresBlindVerification");
			this.m_SystemInitiatingOrder = propertyWriter.WriteString("SystemInitiatingOrder");
			this.m_SpecimenSource = propertyWriter.WriteString("SpecimenSource");
			this.m_DateReceived = propertyWriter.WriteNullableDateTime("DateReceived");
			this.m_SpecimenAdequacy = propertyWriter.WriteString("SpecimenAdequacy");
            this.m_LocationFacilityId = propertyWriter.WriteString("LocationFacilityId");
            this.m_OwnerFacilityId = propertyWriter.WriteString("OwnerFacilityId");
			this.m_ObjectId = propertyWriter.WriteString("ObjectId");
            this.m_RequiresGrossExamination = propertyWriter.WriteBoolean("RequiresGrossExamination");
            this.m_ClientAccessioned = propertyWriter.WriteBoolean("ClientAccessioned");
            this.m_FacilityId = propertyWriter.WriteString("FacilityId");
            this.m_LocationId = propertyWriter.WriteString("LocationId");
            this.m_ProcessorRun = propertyWriter.WriteString("ProcessorRun");
            this.m_ProcessorRunId = propertyWriter.WriteString("ProcessorRunId");
            this.m_ProcessorStartTime = propertyWriter.WriteNullableDateTime("ProcessorStartTime");
            this.m_ProcessorFixationTime = propertyWriter.WriteNullableInt("ProcessorFixationTime");
            this.m_TimeToFixation = propertyWriter.WriteNullableInt("TimeToFixation");
            this.m_FixationDuration = propertyWriter.WriteNullableInt("FixationDuration");
            this.m_FixationStartTimeManuallyEntered = propertyWriter.WriteBoolean("FixationStartTimeManuallyEntered");
            this.m_FixationStartTime = propertyWriter.WriteNullableDateTime("FixationStartTime");
            this.m_FixationEndTime = propertyWriter.WriteNullableDateTime("FixationEndTime");
            this.m_TimeToFixationHourString = propertyWriter.WriteString("TimeToFixationHourString");
		}
		
		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			propertyReader.ReadString("SpecimenOrderId", SpecimenOrderId);
            propertyReader.ReadString("SpecimenId", SpecimenId);
			propertyReader.ReadString("SpecimenType", SpecimenType);
			propertyReader.ReadNullableDateTime("CollectionDate", CollectionDate);
			propertyReader.ReadNullableDateTime("CollectionTime", CollectionTime);
			propertyReader.ReadString("SpecimenSite", SpecimenSite);
			propertyReader.ReadString("FixationType", FixationType);			
			propertyReader.ReadString("Description", Description);
			propertyReader.ReadInt("AliquotRequestCount", AliquotRequestCount);
			propertyReader.ReadNullableDateTime("AccessionTime", AccessionTime);
			propertyReader.ReadString("ClientFixation", ClientFixation);
			propertyReader.ReadString("LabFixation", LabFixation);
			propertyReader.ReadBoolean("CollectionTimeUnknown", CollectionTimeUnknown);			
			propertyReader.ReadString("MasterAccessionNo", MasterAccessionNo);
			propertyReader.ReadString("FixationComment", FixationComment);
			propertyReader.ReadInt("SpecimenNumber", SpecimenNumber);
            propertyReader.ReadNullableInt("ClientSpecimenNumber", ClientSpecimenNumber);
			propertyReader.ReadBoolean("Accepted", Accepted);
			propertyReader.ReadString("SpecialInstructions", SpecialInstructions);
			propertyReader.ReadString("ContainerId", ContainerId);
			propertyReader.ReadBoolean("Verified", Verified);
			propertyReader.ReadInt("VerifiedById", VerifiedById);
			propertyReader.ReadNullableDateTime("VerifiedDate", VerifiedDate);
			propertyReader.ReadBoolean("RequiresBlindVerification", RequiresBlindVerification);
			propertyReader.ReadString("SystemInitiatingOrder", SystemInitiatingOrder);
			propertyReader.ReadString("SpecimenSource", SpecimenSource);
			propertyReader.ReadNullableDateTime("DateReceived", DateReceived);
			propertyReader.ReadString("SpecimenAdequacy", SpecimenAdequacy);
            propertyReader.ReadString("LocationFacilityId", LocationFacilityId);
            propertyReader.ReadString("OwnerFacilityId", OwnerFacilityId);
			propertyReader.ReadString("ObjectId", ObjectId);
            propertyReader.ReadBoolean("RequiresGrossExamination", RequiresGrossExamination);
            propertyReader.ReadBoolean("ClientAccessioned", ClientAccessioned);
            propertyReader.ReadString("LocationId", LocationId);
            propertyReader.ReadString("FacilityId", FacilityId);
            propertyReader.ReadString("ProcessorRun", ProcessorRun);
            propertyReader.ReadString("ProcessorRunId", ProcessorRunId);
            propertyReader.ReadNullableDateTime("ProcessorStartTime", ProcessorStartTime);
            propertyReader.ReadNullableInt("ProcessorFixationTime", ProcessorFixationTime);            
            propertyReader.ReadBoolean("FixationStartTimeManuallyEntered", FixationStartTimeManuallyEntered);
            propertyReader.ReadNullableDateTime("FixationStartTime", FixationStartTime);
            propertyReader.ReadNullableInt("TimeToFixation", TimeToFixation);
            propertyReader.ReadNullableInt("FixationDuration", FixationDuration);
            propertyReader.ReadNullableDateTime("FixationEndTime", FixationEndTime);
            propertyReader.ReadString("TimeToFixationHourString", TimeToFixationHourString);
		}

        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
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
