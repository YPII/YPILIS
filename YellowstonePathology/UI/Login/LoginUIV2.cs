using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class LoginUIV2 : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
				
		private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;		
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemUserCollection m_LogUsers;

		private DateTime m_AccessionOrderDate;
        private string m_SpecimenDescriptionSearchString;

        private List<string> m_CaseTypeList;

		private YellowstonePathology.Business.Search.ReportSearchList m_ReportSearchList;
        private string m_CurrentCaseType;
		private string m_ReportNo;
        private string m_SelectedItemCount;
        private object m_Writer;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        public LoginUIV2(object writer)
		{
            this.m_Writer = writer;
			this.m_LogUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Log, true);
            this.m_CaseTypeList = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetCaseTypes();
			this.m_AccessionOrderDate = DateTime.Today;
            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;
		}

        public YellowstonePathology.Business.User.SystemIdentity SystemIdentity
        {
            get { return Business.User.SystemIdentity.Instance; }
        }            

        public string SelectedItemCount
        {
            get { return this.m_SelectedItemCount; }
            set
            {
                if (this.m_SelectedItemCount != value)
                {
                    this.m_SelectedItemCount = value;
                    this.NotifyPropertyChanged("SelectedItemCount");
                }
            }
        }

        public string CurrentCaseType
        {
            get { return this.m_CurrentCaseType; }
            set { this.m_CurrentCaseType = value; }
        }

        public List<string> CaseTypeList
        {
            get { return this.m_CaseTypeList; }
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
			set
			{
				this.m_AccessionOrder = value;
				this.NotifyPropertyChanged("AccessionOrder");
			}
		}

		public string ReportNo
		{
			get { return this.m_ReportNo; }
			private set
			{
				this.m_ReportNo = value;
				this.NotifyPropertyChanged("ReportNo");
			}
		}

		public YellowstonePathology.Business.Document.CaseDocumentCollection CaseDocumentCollection
		{
			get { return this.m_CaseDocumentCollection; }
		}

		public YellowstonePathology.Business.Search.ReportSearchList ReportSearchList
		{
			get { return this.m_ReportSearchList; }
		}

		public YellowstonePathology.Business.User.SystemUserCollection LogUsers
		{
			get { return this.m_LogUsers; }
		}

        public string SpecimenDescriptionSearchString
        {
            get { return this.m_SpecimenDescriptionSearchString; }
            set
            {
                this.m_SpecimenDescriptionSearchString = value;
                NotifyPropertyChanged("SpeicmenDescriptionSearchString");
            }
        }

		public DateTime AccessionOrderDate
		{
			get { return this.m_AccessionOrderDate; }
			set
			{
				this.m_AccessionOrderDate = value;
				NotifyPropertyChanged("AccessionOrderDate");
			}
		}

		public void GetReportSearchList()
		{
            List<int> panelSetIdList = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetPanelSetIdList(this.m_CurrentCaseType);

			panelSetIdList.Add(131);

            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByAccessionDate(this.m_AccessionOrderDate, panelSetIdList);
			this.NotifyPropertyChanged("ReportSearchList");
		}

        public void GetReportSearchListByReportNo(string reportNo)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByReportNo(reportNo);
            this.NotifyPropertyChanged("ReportSearchList");
        }        

        public void GetReportSearchListBySpecimenKeyword(string specimenDesription, DateTime startDate, DateTime endDate)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListBySpecimenKeyword(specimenDesription, startDate, endDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByPanelSetFinalDate(DateTime panelSetFinalDate)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByPanelSetFinalDate(panelSetFinalDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByNotPosted()
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByNotPosted();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByPostDate(DateTime postDate)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByPostDate(postDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByPositiveHPylori(DateTime startDate, DateTime endDate)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByPositiveHPylori(startDate, endDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByAutopsies()
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByAutopsies();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByClientAccessioned()
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByClientAccessioned();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByDrKurtzman()
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByDrKurtzman();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByMasterAccessionNo(string masterAccessionNo)
		{
			this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByMasterAccessionNo(masterAccessionNo);
			this.NotifyPropertyChanged("ReportSearchList");
		}

        public void GetReportSearchListByAliquotOrderId(string aliquotOrderId)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByAliquotOrderId(aliquotOrderId);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByITAudit(YellowstonePathology.Business.Test.ITAuditPriorityEnum itAuditPriority)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByITAudit(itAuditPriority);
            this.NotifyPropertyChanged("ReportSearchList");
        }

		public void GetReportSearchListByPatientName(YellowstonePathology.Business.PatientName patientName)
		{
			this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByPatientName(new List<object>(){patientName.LastName, patientName.FirstName});
			this.NotifyPropertyChanged("ReportSearchList");
		}

        public void GetReportSearchListByTest(int panelSetId, DateTime startDate, DateTime endDate)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByTest(panelSetId, startDate, endDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetAccessionOrder(string masterAccessionNo, string reportNo)
		{
			this.AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);
			this.ReportNo = reportNo;
            this.m_CaseDocumentCollection = new YellowstonePathology.Business.Document.CaseDocumentCollection(this.AccessionOrder, reportNo);            
		}

		public bool GetAccessionOrderByContainerId(string containerId)
		{
			bool result = false;

            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromContainerId(containerId);
            if(string.IsNullOrEmpty(masterAccessionNo) == false)
            {
                this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);

                if (this.m_AccessionOrder != null)
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Count != 0)
                    {
                        result = true;
                        string reportNo = this.m_AccessionOrder.PanelSetOrderCollection[0].ReportNo;
                        this.GetReportSearchListByReportNo(reportNo);
                        this.NotifyPropertyChanged("ReportSearchList");
                    }
                }
            }            
            
			return result;
		}		

        /*public void GetAccessionOrderBySlideOrderId(string slideOrderId)
        {
            this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderBySlideOrderId(slideOrderId);
            if (this.m_AccessionOrder != null)
            {
                string reportNo = this.m_AccessionOrder.PanelSetOrderCollection[0].ReportNo;
                this.GetReportSearchListByReportNo(reportNo);
                this.NotifyPropertyChanged("ReportSearchList");
            }
        }*/

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
