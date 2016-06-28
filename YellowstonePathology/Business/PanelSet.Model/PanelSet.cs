using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.PanelSet.Model
{
	public class PanelSet : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

        protected string m_ObjectId;
		protected int m_PanelSetId;
        protected string m_PanelSetOrderClassName;
        protected string m_PanelSetOrderTableName;
		protected string m_PanelSetName;
        protected string m_Abbreviation;
		protected bool m_Active;
		protected bool m_SurgicalAmendmentRequired;
		protected string m_CaseType;
		protected bool m_HasTechnicalComponent;
		protected bool m_HasProfessionalComponent;
		protected ResultDocumentSourceEnum m_ResultDocumentSource;
		protected YellowstonePathology.Business.ReportNoLetter m_ReportNoLetter;		
		protected string m_ReflexTestingComment;		
		protected bool m_EnforceOrderTarget;
		protected bool m_RequiresPathologistSignature;
		protected bool m_AcceptOnFinal;
		protected bool m_IsReflexPanel;
		protected bool m_AllowMultiplePerAccession;
        protected bool m_IsBillable;
        protected bool m_NeverDistribute;        
        protected TimeSpan m_ExpectedDuration;
        protected bool m_ShowResultPageOnOrder;                
        protected bool m_CanHaveMultipleOrderTargets;
        protected bool m_HasNoOrderTarget;
        protected bool m_RequireAssignmentOnOrder;
        protected bool m_AttemptOrderTargetLookup;
        protected bool m_IsClientAccessioned;
        protected bool m_AddAliquotOnOrder;
        protected bool m_SendOrderToPanther;
        
        protected YellowstonePathology.Business.Specimen.Model.Aliquot m_AliquotToAddOnOrder;        

        protected YellowstonePathology.Business.OrderTargetTypeCollection m_OrderTargetTypeCollectionExclusions;
        protected YellowstonePathology.Business.OrderTargetTypeCollection m_OrderTargetTypeCollectionRestrictions;

        protected YellowstonePathology.Business.Panel.Model.PanelCollection m_PanelCollection;
        protected YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection m_UniversalServiceIdCollection;

        protected YellowstonePathology.Business.Facility.Model.Facility m_TechnicalComponentFacility;
        protected YellowstonePathology.Business.Facility.Model.Facility m_ProfessionalComponentFacility;

        protected YellowstonePathology.Business.Facility.Model.Facility m_TechnicalComponentBillingFacility;
        protected YellowstonePathology.Business.Facility.Model.Facility m_ProfessionalComponentBillingFacility;

		protected YellowstonePathology.Business.Task.Model.TaskCollection m_TaskCollection;
        protected YellowstonePathology.Business.Billing.Model.PanelSetCptCodeCollection m_PanelSetCptCodeCollection;

        protected bool m_EpicDistributionIsImplemented;
        protected bool m_CMMCDistributionIsImplemented;

        public PanelSet()
        {
            this.m_IsBillable = true;
            this.m_NeverDistribute = false;            
            this.m_Active = true;
            this.m_ExpectedDuration = TimeSpan.FromDays(7);

            this.m_PanelCollection = new Business.Panel.Model.PanelCollection();
            this.m_UniversalServiceIdCollection = new Business.ClientOrder.Model.UniversalServiceCollection();
            this.m_PanelSetCptCodeCollection = new Business.Billing.Model.PanelSetCptCodeCollection();
			this.m_TaskCollection = new YellowstonePathology.Business.Task.Model.TaskCollection();

            this.m_ShowResultPageOnOrder = false;                        
            this.m_CanHaveMultipleOrderTargets = false;
            this.m_HasNoOrderTarget = false;
            this.m_AttemptOrderTargetLookup = false;
            this.m_RequireAssignmentOnOrder = true;
            this.m_IsClientAccessioned = false;

            this.m_EpicDistributionIsImplemented = false;
            this.m_CMMCDistributionIsImplemented = false;

            this.m_OrderTargetTypeCollectionExclusions = new YellowstonePathology.Business.OrderTargetTypeCollection();
            this.m_OrderTargetTypeCollectionRestrictions = new YellowstonePathology.Business.OrderTargetTypeCollection();

			this.m_PanelSetOrderClassName = typeof(YellowstonePathology.Business.Test.PanelSetOrder).AssemblyQualifiedName;
		}        

        public YellowstonePathology.Business.OrderTargetTypeCollection OrderTargetTypeCollectionExclusions
        {
            get { return this.m_OrderTargetTypeCollectionExclusions; }
        }

        public YellowstonePathology.Business.OrderTargetTypeCollection OrderTargetTypeCollectionRestrictions
        {
            get { return this.m_OrderTargetTypeCollectionRestrictions; }
        }

		public YellowstonePathology.Business.Task.Model.TaskCollection TaskCollection
        {
            get { return this.m_TaskCollection; }
        }

        public YellowstonePathology.Business.Panel.Model.PanelCollection PanelCollection
        {
            get { return this.m_PanelCollection; }
        }
        
        public YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection UniversalServiceIdCollection
        {
            get { return this.m_UniversalServiceIdCollection; }            
        }

        public YellowstonePathology.Business.Billing.Model.PanelSetCptCodeCollection PanelSetCptCodeCollection
        {
            get { return this.m_PanelSetCptCodeCollection; }
        }        
        
        public YellowstonePathology.Business.Facility.Model.Facility TechnicalComponentFacility
        {
            get { return this.m_TechnicalComponentFacility; }
        }

        public YellowstonePathology.Business.Facility.Model.Facility ProfessionalComponentFacility
        {
            get { return this.m_ProfessionalComponentFacility; }
        }

        public YellowstonePathology.Business.Facility.Model.Facility TechnicalComponentBillingFacility
        {
            get { return this.m_TechnicalComponentBillingFacility; }
        }

        public YellowstonePathology.Business.Facility.Model.Facility ProfessionalComponentBillingFacility
        {
            get { return this.m_ProfessionalComponentBillingFacility; }
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

        [PersistentProperty()]
		public int PanelSetId
		{
			get { return this.m_PanelSetId; }
			set
			{
				if (this.m_PanelSetId != value)
				{
					this.m_PanelSetId = value;
					this.NotifyPropertyChanged("PanelSetId");
				}
			}
		}

        [PersistentProperty()]
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
        public string PanelSetOrderClassName
        {
            get { return this.m_PanelSetOrderClassName; }
            set
            {
                if (this.m_PanelSetOrderClassName != value)
                {
                    this.m_PanelSetOrderClassName = value;
                    this.NotifyPropertyChanged("PanelSetOrderClassName");
                }
            }
        }

        [PersistentProperty()]
        public string PanelSetOrderTableName
        {
            get { return this.m_PanelSetOrderTableName; }
            set
            {
                if (this.m_PanelSetOrderTableName != value)
                {
                    this.m_PanelSetOrderTableName = value;
                    this.NotifyPropertyChanged("PanelSetOrderTableName");
                }
            }
        }

        [PersistentProperty()]
		public bool Active
		{
			get { return this.m_Active; }
			set
			{
				if (this.m_Active != value)
				{
					this.m_Active = value;
					this.NotifyPropertyChanged("Active");
				}
			}
		}

        [PersistentProperty()]
        public bool AttemptOrderTargetLookup
        {
            get { return this.m_AttemptOrderTargetLookup; }
            set
            {
                if (this.m_AttemptOrderTargetLookup != value)
                {
                    this.m_AttemptOrderTargetLookup = value;
                    this.NotifyPropertyChanged("AttemptOrderTargetLookup");
                }
            }
        }


        [PersistentProperty()]
		public bool RequiresPathologistSignature
		{
			get { return this.m_RequiresPathologistSignature; }
			set
			{
				if (this.m_RequiresPathologistSignature != value)
				{
					this.m_RequiresPathologistSignature = value;
					this.NotifyPropertyChanged("RequiresPathologistSignature");
				}
			}
		}

        [PersistentProperty()]
		public bool SurgicalAmendmentRequired
		{
			get { return this.m_SurgicalAmendmentRequired; }
			set
			{
				if (this.m_SurgicalAmendmentRequired != value)
				{
					this.m_SurgicalAmendmentRequired = value;
					this.NotifyPropertyChanged("SurgicalAmendmentRequired");
				}
			}
		}

        [PersistentProperty()]
		public string CaseType
		{
			get { return this.m_CaseType; }
			set
			{
				if (this.m_CaseType != value)
				{
					this.m_CaseType = value;
					this.NotifyPropertyChanged("CaseType");
				}
			}
		}

        [PersistentProperty()]
		public bool HasTechnicalComponent
		{
			get { return this.m_HasTechnicalComponent; }
			set
			{
				if (this.m_HasTechnicalComponent != value)
				{
					this.m_HasTechnicalComponent = value;
					this.NotifyPropertyChanged("HasTechnicalComponent");
				}
			}
		}

        [PersistentProperty()]
		public bool HasProfessionalComponent
		{
			get { return this.m_HasProfessionalComponent; }
			set
			{
				if (this.m_HasProfessionalComponent != value)
				{
					this.m_HasProfessionalComponent = value;
					this.NotifyPropertyChanged("HasProfessionalComponent");
				}
			}
		}

        [PersistentProperty()]
		public ResultDocumentSourceEnum ResultDocumentSource
		{
			get { return this.m_ResultDocumentSource; }
			set
			{
				if (this.m_ResultDocumentSource != value)
				{
					this.m_ResultDocumentSource = value;
					this.NotifyPropertyChanged("ResultDocumentSource");
				}
			}
		}
        
        public YellowstonePathology.Business.ReportNoLetter ReportNoLetter
		{
            get { return this.m_ReportNoLetter; }
			set
			{
                if (this.m_ReportNoLetter != value)
				{
                    this.m_ReportNoLetter = value;
                    this.NotifyPropertyChanged("ReportNoLetter");
				}
			}
		}                  

        [PersistentProperty()]
		public bool EnforceOrderTarget
		{
			get { return this.m_EnforceOrderTarget; }
			set
			{
				if (this.m_EnforceOrderTarget != value)
				{
					this.m_EnforceOrderTarget = value;
					this.NotifyPropertyChanged("EnforceOrderTarget");
				}
			}
		}

        [PersistentProperty()]
		public bool AcceptOnFinal
		{
			get { return this.m_AcceptOnFinal; }
			set
			{
				if (this.m_AcceptOnFinal != value)
				{
					this.m_AcceptOnFinal = value;
					this.NotifyPropertyChanged("AcceptOnFinal");
				}
			}
		}

        [PersistentProperty()]
		public bool IsReflexPanel
		{
			get { return this.m_IsReflexPanel; }
			set
			{
				if (this.m_IsReflexPanel != value)
				{
					this.m_IsReflexPanel = value;
					this.NotifyPropertyChanged("IsReflexPanel");
				}
			}
		}

        [PersistentProperty()]
		public bool AllowMultiplePerAccession
		{
			get { return this.m_AllowMultiplePerAccession; }
			set
			{
				if (this.m_AllowMultiplePerAccession != value)
				{
					this.m_AllowMultiplePerAccession = value;
					this.NotifyPropertyChanged("AllowMultiplePerAccession");
				}
			}
		}

        [PersistentProperty()]
        public bool IsBillable
        {
            get { return this.m_IsBillable; }
            set
            {
                if (this.m_IsBillable != value)
                {
                    this.m_IsBillable = value;
                    this.NotifyPropertyChanged("IsBillable");
                }
            }
        }

        [PersistentProperty()]
        public bool NeverDistribute
        {
			get { return this.m_NeverDistribute; }
            set
            {
				if (this.m_NeverDistribute != value)
                {
					this.m_NeverDistribute = value;
					this.NotifyPropertyChanged("NeverDistribute");
                }
            }
        }        
        
        public TimeSpan ExpectedDuration
        {
            get { return this.m_ExpectedDuration; }
            set
            {
                if (this.m_ExpectedDuration != value)
                {
                    this.m_ExpectedDuration = value;
                    this.NotifyPropertyChanged("ExpectedDuration");
                }
            }
        }

        [PersistentProperty()]
        public string Abbreviation
        {
            get { return this.m_Abbreviation; }
            set
            {
                if (this.m_Abbreviation != value)
                {
                    this.m_Abbreviation = value;
                    this.NotifyPropertyChanged("Abbreviation");
                }
            }
        }
        
        [PersistentProperty()]
        public bool ShowResultPageOnOrder
        {
            get { return this.m_ShowResultPageOnOrder; }
            set
            {
                if (this.m_ShowResultPageOnOrder != value)
                {
                    this.m_ShowResultPageOnOrder = value;
                    this.NotifyPropertyChanged("ShowResultPageOnOrder");
                }
            }
        }        

        [PersistentProperty()]
        public bool CanHaveMultipleOrderTargets
        {
            get { return this.m_CanHaveMultipleOrderTargets; }
            set
            {
                if (this.m_CanHaveMultipleOrderTargets != value)
                {
                    this.m_CanHaveMultipleOrderTargets = value;
                    this.NotifyPropertyChanged("CanHaveMultipleOrderTargets");
                }
            }
        }

        [PersistentProperty()]
        public bool HasNoOrderTarget
        {
            get { return this.m_HasNoOrderTarget; }
            set
            {
                if (this.m_HasNoOrderTarget != value)
                {
                    this.m_HasNoOrderTarget = value;
                    this.NotifyPropertyChanged("HasNoOrderTarget");
                }
            }
        }

        [PersistentProperty()]
        public bool RequireAssignmentOnOrder
        {
            get { return this.m_RequireAssignmentOnOrder; }
            set
            {
                if (this.m_RequireAssignmentOnOrder != value)
                {
                    this.m_RequireAssignmentOnOrder = value;
                    this.NotifyPropertyChanged("RequireAssignmentOnOrder");
                }
            }
        }

        [PersistentProperty()]
        public bool IsClientAccessioned
        {
            get { return this.m_IsClientAccessioned; }
            set
            {
                if (this.m_IsClientAccessioned != value)
                {
                    this.m_IsClientAccessioned = value;
                    this.NotifyPropertyChanged("IsClientAccessioned");
                }
            }
        }

        [PersistentProperty()]
        public bool AddAliquotOnOrder
        {
            get { return this.m_AddAliquotOnOrder; }
            set
            {
                if (this.m_AddAliquotOnOrder != value)
                {
                    this.m_AddAliquotOnOrder = value;
                    this.NotifyPropertyChanged("AddAliquotOnOrder");
                }
            }
        }

        [PersistentProperty()]
        public YellowstonePathology.Business.Specimen.Model.Aliquot AliquotToAddOnOrder
        {
            get { return this.m_AliquotToAddOnOrder; }
            set
            {
                if (this.m_AliquotToAddOnOrder != value)
                {
                    this.m_AliquotToAddOnOrder = value;
                    this.NotifyPropertyChanged("AliquotToAddOnOrder");
                }
            }
        }

        [PersistentProperty()]
        public bool SendOrderToPanther
        {
            get { return this.m_SendOrderToPanther; }
            set
            {
                if (this.m_SendOrderToPanther != value)
                {
                    this.m_SendOrderToPanther = value;
                    this.NotifyPropertyChanged("SendOrderToPanther");
                }
            }
        }

        public static int CompareByPanelSetName(PanelSet x, PanelSet y)
        {
            if (x == null)
            {
                if (y == null)
                {                    
                    return 0;
                }
                else
                {                 
                    return -1;
                }
            }
            else
            {                
                if (y == null)             
                {
                    return 1;
                }
                else
                {                    
                    int retval = x.m_PanelSetName.CompareTo(y.m_PanelSetName);
                    if (retval != 0)
                    {                 
                        return retval;
                    }
                    else
                    {                     
                        return x.m_PanelSetName.CompareTo(y.m_PanelSetName);
                    }
                }
            }
        }

        public virtual YellowstonePathology.Business.Rules.MethodResult OrderTargetIsOk(YellowstonePathology.Business.Interface.IOrderTarget orderTarget)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            methodResult.Success = true;
            return methodResult;
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
