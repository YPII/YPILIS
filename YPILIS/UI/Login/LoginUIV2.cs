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
        private Business.SvhAuditList m_SvhAuditList;
        private Business.ClientOrder.Model.ClientOrderCollection m_ClientOrderCollection;

        public LoginUIV2(object writer)
		{
            this.m_Writer = writer;
			this.m_LogUsers = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Log, true);
            this.m_CaseTypeList = Business.PanelSet.Model.PanelSetCollection.GetCaseTypes();
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

        public Business.SvhAuditList SvhAuditList
        {
            get { return this.m_SvhAuditList; }
            set { this.m_SvhAuditList = value; }
        }

        public Business.ClientOrder.Model.ClientOrderCollection ClientOrderCollection
        {
            get { return this.m_ClientOrderCollection; }
            set { this.m_ClientOrderCollection = value; }
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
            set { this.m_ReportSearchList = value; }
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
            if(this.m_CurrentCaseType == "NEO")
            {
                this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByNeo(this.m_AccessionOrderDate);
            }
            else
            {
                List<int> panelSetIdList = Business.PanelSet.Model.PanelSetCollection.GetPanelSetIdList(this.m_CurrentCaseType);
                panelSetIdList.Add(131);
                this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByAccessionDate(this.m_AccessionOrderDate, panelSetIdList);
            }            
			this.NotifyPropertyChanged("ReportSearchList");
		}

        public void GetReportSearchListByReportNo(string reportNo)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByReportNo(reportNo);
            this.NotifyPropertyChanged("ReportSearchList");
        }        

        public void GetReportSearchListBySpecimenKeyword(string specimenDesription, DateTime startDate, DateTime endDate)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListBySpecimenKeyword(specimenDesription, startDate, endDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByPanelSetFinalDate(DateTime panelSetFinalDate)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByPanelSetFinalDate(panelSetFinalDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByNotPosted()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByNotPosted();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListBySVHFinalNotPosted()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListBySVHFinalNotPosted();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListBySVHPosted(DateTime postDate)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListBySVHPosted(postDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByCOVIDCases()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByCOVIDCases();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByYPICOVIDCases()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByYPICOVIDCases();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListBySCLCOVIDCases()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListBySCLCOVIDCases();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByCOVIDPatientDistributionCases()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByCOVIDPatientDistributionCases();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByDurdenCOVIDCases()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByDurdenCOVIDCases();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByQICases(string year)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByCasesWithNotes(year);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByASCCPCases()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByASCCPCases();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByChangesNotPosted()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByChangesNotPosted();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByPostDate(DateTime postDate)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByPostDate(postDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByPositiveHPylori(DateTime startDate, DateTime endDate)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByPositiveHPylori(startDate, endDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByAutopsies()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByAutopsies();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByClientAccessioned()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByClientAccessioned();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByDrKurtzman()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByDrKurtzman();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByMasterAccessionNo(string masterAccessionNo)
		{
			this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByMasterAccessionNo(masterAccessionNo);
			this.NotifyPropertyChanged("ReportSearchList");
		}

        public void GetReportSearchListByAliquotOrderId(string aliquotOrderId)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByAliquotOrderId(aliquotOrderId);
            this.NotifyPropertyChanged("ReportSearchList");
        }        

        public void GetReportSearchListByITAudit(YellowstonePathology.Business.Test.ITAuditPriorityEnum itAuditPriority)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByITAudit(itAuditPriority);
            this.NotifyPropertyChanged("ReportSearchList");
        }

		public void GetReportSearchListByPatientName(YellowstonePathology.Business.PatientName patientName)
		{
			this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByPatientName(new List<object>(){patientName.LastName, patientName.FirstName});
			this.NotifyPropertyChanged("ReportSearchList");
		}

        public void GetReportSearchListByTest(int panelSetId, DateTime startDate, DateTime endDate)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByTest(panelSetId, startDate, endDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByInvalidFinal()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByInvalidFinal();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByPendingTests()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByPendingTests();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByClientId(string clientId, DateTime startDate, DateTime endDate)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByClient(clientId, startDate, endDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByBillingDelayed()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByBillingDelayed();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByDistributionDelayed()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByDistributionDelayed();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListBySVHNotFinalMultipleOrders()
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListBySVHNotFinalMultipleOrders();
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByProvationFinal(DateTime finalDate)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByProvationFinal(finalDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetReportSearchListByNMHFinal(DateTime finalDate)
        {
            this.m_ReportSearchList = Business.Gateway.ReportSearchGateway.GetReportSearchListByNMHFinal(finalDate);
            this.NotifyPropertyChanged("ReportSearchList");
        }

        public void GetAccessionOrder(string masterAccessionNo, string reportNo)
		{
			this.AccessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);
			this.ReportNo = reportNo;
            this.m_CaseDocumentCollection = new YellowstonePathology.Business.Document.CaseDocumentCollection(this.AccessionOrder, reportNo);            
		}

		public bool GetAccessionOrderByContainerId(string containerId)
		{
			bool result = false;

            string masterAccessionNo = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromContainerId(containerId);
            if(string.IsNullOrEmpty(masterAccessionNo) == false)
            {
                this.m_AccessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);

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

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
