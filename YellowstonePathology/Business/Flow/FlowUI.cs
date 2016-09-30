using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace YellowstonePathology.Business.Flow
{        
    public class FlowUI : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Flow.FlowLogSearch m_FlowLogSearch;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma m_PanelSetOrderLeukemiaLymphoma;

        private YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;
        private YellowstonePathology.Business.User.SystemUserCollection m_MedTechUsers;
        private YellowstonePathology.Business.PanelSet.Model.PanelSetCollection m_FlowPanelSetCollection;
        private Flow.FlowComment m_FlowComment;
        private Flow.Marker m_Marker;
        
        private Flow.FlowCaseValidation m_FlowCaseValidation;
        private Billing.Model.ICDCodeList m_ICDCodeList;
        private Flow.FlowPanelList m_FlowPanelList;
		private bool m_IsEnabled = true;
		private string m_ReportNo;

        
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;
		private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
		private YellowstonePathology.Business.Patient.Model.PatientHistoryList m_PatientHistoryList;
		private YellowstonePathology.Business.PanelSet.Model.PanelSetCollection m_PanelSets;
        private YellowstonePathology.Business.Billing.Model.ICD9BillingCodeCollection m_ICD9BillingCodeCollection;
        private object m_Writer;

        public FlowUI(object writer)
        {
            this.m_Writer = writer;
            this.m_FlowLogSearch = new FlowLogSearch();

            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
            if (this.m_SystemIdentity.User.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist))
			{
				this.m_FlowLogSearch.SetByLeukemiaNotFinal();
            }
            else
            {
                this.m_FlowLogSearch.SetByAccessionMonth(DateTime.Now);
			}			

			this.m_PathologistUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist, true);
			this.m_MedTechUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.MedTech, true);

            this.m_FlowPanelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetFlowPanelSets(false);            

            this.m_FlowComment = new FlowComment();
            this.m_FlowComment.FlowCommentCollection.SetFillCommandAll();
            this.m_FlowComment.FlowCommentCollection.Fill();

            this.m_Marker = new Marker();
            this.m_Marker.MarkerCollection.SetFillCommandAll();
            this.m_Marker.MarkerCollection.Fill();                       

            this.m_FlowCaseValidation = new FlowCaseValidation();

            this.m_ICDCodeList = new YellowstonePathology.Business.Billing.Model.ICDCodeList();
            this.m_ICDCodeList.SetFillCommandByFlowCodes();
            this.m_ICDCodeList.Fill();           

            this.m_FlowPanelList = new FlowPanelList();
            this.m_FlowPanelList.SetFillCommandByAll();
            this.m_FlowPanelList.Fill();

            this.m_FacilityCollection = YellowstonePathology.Business.Facility.Model.FacilityCollection.GetAllFacilities();

			this.m_CaseDocumentCollection = new Document.CaseDocumentCollection();
			this.m_PatientHistoryList = new YellowstonePathology.Business.Patient.Model.PatientHistoryList();
			this.m_PanelSets = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();

			this.Search();
        }

        public void Search()
        {
			this.m_FlowLogSearch.Search();
        }		

        public YellowstonePathology.Business.Billing.Model.ICDCodeList ICDCodeList
        {
            get { return this.m_ICDCodeList; }
        }

		public Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
            set
            {
                if(this.m_AccessionOrder != value)
                {
                    this.m_AccessionOrder = value;
                    this.NotifyPropertyChanged("AccessionOrder");                    
                }                
            }
		}

		public YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma PanelSetOrderLeukemiaLymphoma
		{
			get	{ return this.m_PanelSetOrderLeukemiaLymphoma;	}
            set
            {
                if(this.m_PanelSetOrderLeukemiaLymphoma != value)
                {
                    this.m_PanelSetOrderLeukemiaLymphoma = value;
                    this.NotifyPropertyChanged("PanelSetOrderLeukemiaLymphoma");
                }
            }
		}

		public Document.CaseDocumentCollection CaseDocumentCollection
		{
			get { return this.m_CaseDocumentCollection; }
			set
			{
                if(this.m_CaseDocumentCollection != value)
                {
                    this.m_CaseDocumentCollection = value;
                    this.NotifyPropertyChanged("CaseDocumentCollection");
                }				
			}
		}

		public void RefreshCaseDocumentCollection(string reportNo)
		{
			this.m_CaseDocumentCollection = new Document.CaseDocumentCollection(reportNo);
			NotifyPropertyChanged("CaseDocumentCollection");
		}

		public Patient.Model.PatientHistoryList PatientHistoryList
		{
			get { return this.m_PatientHistoryList; }
		}

		public YellowstonePathology.Business.Billing.Model.ICD9BillingCodeCollection ICD9BillingCodeCollection
		{
			get	{ return this.m_ICD9BillingCodeCollection; }
            set 
            {
                if (this.m_ICD9BillingCodeCollection != value)
                {
                    this.m_ICD9BillingCodeCollection = value;
                    this.NotifyPropertyChanged("ICD9BillingCodeCollection");
                }
            }
		}

		public FlowLogSearch FlowLogSearch
        {
			get { return this.m_FlowLogSearch; }
        }

        public FlowCaseValidation FlowCaseValidation
        {
            get { return this.m_FlowCaseValidation; }
        }

        public FlowPanelList FlowPanelList
        {
            get { return this.m_FlowPanelList; }
        }

		public void GetAccessionOrder(string reportNo, string masterAccessionNo)
		{			
			this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);			

            this.m_PanelSetOrderLeukemiaLymphoma = (YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            this.m_ReportNo = reportNo;

			this.RefreshCaseDocumentCollection(reportNo);
			this.m_PatientHistoryList.SetFillCommandByAccessionNo(reportNo);
			this.m_PatientHistoryList.Fill();
			this.m_PatientHistoryList.SetCaseDocumentCollection(reportNo);
			
			this.NotifyPropertyChanged("AccessionOrder");
			this.NotifyPropertyChanged("PanelSetOrderLeukemiaLymphoma");
			this.NotifyPropertyChanged("PatientHistoryList");
			this.NotifyPropertyChanged("Icd9BillingCodes");
			this.NotifyPropertyChanged("CaseHeader");
			this.NotifyPropertyChanged("SignReportButtonContent");
			this.NotifyPropertyChanged("SignReportButtonEnabled");
			this.SetAccess();            
        }

        public bool IsWorkspaceEnabled
		{
			get { return this.m_IsEnabled; }
			set
			{
				if (value != this.m_IsEnabled)
				{
					this.m_IsEnabled = value;
					this.NotifyPropertyChanged("IsWorkspaceEnabled");
				}
			}
		}

		public string CaseHeader
		{
			get
			{
				string result = string.Empty;
				if (this.AccessionOrder != null)
				{
                    if (this.PanelSetOrderLeukemiaLymphoma != null)
                    {
                        result = this.PanelSetOrderLeukemiaLymphoma.ReportNo + "  " + this.AccessionOrder.PatientName;
                    }
				}
				return result;
			}
		}

		public void SetAccess()
		{
			if (this.m_AccessionOrder.AccessionLock.IsLockAquired == true)
			{
				if (this.PanelSetOrderLeukemiaLymphoma.Final == true)
				{
					if (this.PanelSetOrderLeukemiaLymphoma.AmendmentCollection.HasOpenAmendment() == true)
					{
						this.IsWorkspaceEnabled = true;
					}
					else
					{
						this.IsWorkspaceEnabled = false;
					}
				}
				else
				{
					this.IsWorkspaceEnabled = true;
				}
			}
			else
			{
				this.IsWorkspaceEnabled = false;
			}
		}		

        public void MedTechUnfinal()
        {
			if (this.AccessionOrder != null)
            {                
                if (this.m_SystemIdentity.User.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.MedTech)
                    || this.m_SystemIdentity.User.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Administrator) == true)
				{
					if (this.PanelSetOrderLeukemiaLymphoma.TechFinal == true && this.PanelSetOrderLeukemiaLymphoma.Final == false)
                    {
						this.PanelSetOrderLeukemiaLymphoma.TechFinaledById = 0;
						this.PanelSetOrderLeukemiaLymphoma.TechFinal = false;
						this.PanelSetOrderLeukemiaLymphoma.TechFinalDate = null;
						this.PanelSetOrderLeukemiaLymphoma.TechFinalTime = null;

						if (this.PanelSetOrderLeukemiaLymphoma.PanelSetId != 20)
                        {
							this.PanelSetOrderLeukemiaLymphoma.Final = false;
							this.PanelSetOrderLeukemiaLymphoma.FinalDate = null;
							this.PanelSetOrderLeukemiaLymphoma.FinalTime = null;
                        }
                    }
                }
            }
        }

        public YellowstonePathology.Business.Rules.MethodResult IsOkToMedTechFinal()
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            methodResult.Success = true;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.PanelSetOrderLeukemiaLymphoma.OrderedOn, this.PanelSetOrderLeukemiaLymphoma.OrderedOnId);
            if (specimenOrder == null)
            {
                methodResult.Success = false;
                methodResult.Message = "The specimen for this case has not been selected.";
            }

            return methodResult;
        }

        public void MedTechFinal()
        {
			if (this.AccessionOrder != null)
            {                
                if (this.m_SystemIdentity.User.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.MedTech)
                    || this.m_SystemIdentity.User.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Administrator) == true)
				{
                    bool caseIsValid = FlowCaseValidation.Validate(this.PanelSetOrderLeukemiaLymphoma, this.AccessionOrder);
                    if (caseIsValid == false)
                    {
                        MessageBox.Show("There are broken rules that need to be fixed.");
                        return;
                    }

					this.PanelSetOrderLeukemiaLymphoma.TechFinal = true;
					this.PanelSetOrderLeukemiaLymphoma.TechFinalDate = DateTime.Today;
					this.PanelSetOrderLeukemiaLymphoma.TechFinalTime = DateTime.Now;
				}
                else
                {
                    MessageBox.Show("You do not have permission to perform this action.");
                }
            }
        }        

        public Flow.Marker Marker
        {
            get { return this.m_Marker; }
        }

        public FlowComment FlowComment
        {
            get { return this.m_FlowComment; }
        }

        public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
        {
            get { return this.m_PathologistUsers; }
        }

        public YellowstonePathology.Business.User.SystemUserCollection MedTechUsers
        {
            get { return this.m_MedTechUsers; }
        }

        public YellowstonePathology.Business.PanelSet.Model.PanelSetCollection FlowPanelSetCollection
        {
            get { return this.m_FlowPanelSetCollection; }
        }

		public YellowstonePathology.Business.User.SystemUser CurrentUser
		{
            get { return this.m_SystemIdentity.User; }
		}

		public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
		{
			get { return this.m_FacilityCollection; }
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		public void SetMarkerPanel(int panelId)
		{
			this.PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.Clear();

			Flow.FlowMarkerCollection panelCollection = Gateway.FlowGateway.GetFlowMarkerCollectionByPanelId(this.PanelSetOrderLeukemiaLymphoma.ReportNo, panelId);
			this.PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.Insert(panelCollection, this.PanelSetOrderLeukemiaLymphoma.ReportNo);
		}		

		public string SignReportButtonContent
		{
			get
			{
				string result = "Sign Report";
				if (this.PanelSetOrderLeukemiaLymphoma != null && this.PanelSetOrderLeukemiaLymphoma.Final == true)
				{
					result = "Unsign Report";
				}
				return result;
			}
		}

		public bool SignReportButtonEnabled
		{
			get
			{
				bool result = false;
				if (this.m_AccessionOrder != null && this.m_AccessionOrder.AccessionLock.IsLockAquired == true)
				{
                    result = true;
				}
				return result;
			}
		}

		public void ChangePanelSetIdentification(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
		{
			if (this.PanelSetOrderLeukemiaLymphoma != null)
			{				
                if (this.PanelSetOrderLeukemiaLymphoma.PanelSetId != panelSet.PanelSetId)
                {
                    this.PanelSetOrderLeukemiaLymphoma.PanelSetId = panelSet.PanelSetId;
                    this.PanelSetOrderLeukemiaLymphoma.PanelSetName = panelSet.PanelSetName;
                }
			}
		}

		public void AddICD9Code(string icd9Code, string icd10Code)
		{
            int quantity = 1;
            string specimenOrderId = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.PanelSetOrderLeukemiaLymphoma.OrderedOn, this.PanelSetOrderLeukemiaLymphoma.OrderedOnId).SpecimenOrderId;

			YellowstonePathology.Business.Billing.Model.ICD9BillingCode icd9BillingCode = this.m_AccessionOrder.ICD9BillingCodeCollection.GetNextItem(this.PanelSetOrderLeukemiaLymphoma.ReportNo,
                this.m_AccessionOrder.MasterAccessionNo, specimenOrderId, icd9Code, icd10Code, quantity);
			this.m_AccessionOrder.ICD9BillingCodeCollection.Add(icd9BillingCode);
            this.m_ICD9BillingCodeCollection = this.m_AccessionOrder.ICD9BillingCodeCollection.GetReportCollection(this.PanelSetOrderLeukemiaLymphoma.ReportNo);
			this.NotifyPropertyChanged("ICD9BillingCodeCollection");
		}

		public void RemoveICD9Code(YellowstonePathology.Business.Billing.Model.ICD9BillingCode item)
		{
			this.m_AccessionOrder.ICD9BillingCodeCollection.Remove(item);
            this.m_ICD9BillingCodeCollection = this.m_AccessionOrder.ICD9BillingCodeCollection.GetReportCollection(this.PanelSetOrderLeukemiaLymphoma.ReportNo);
			this.NotifyPropertyChanged("ICD9BillingCodeCollection");
		}        
    }
}
