using System;
using System.ComponentModel;
using System.Windows.Data;

namespace YellowstonePathology.Business
{
    public class SearchUI : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        private SearchList m_SearchList;
        private ListCollectionView m_SearchListCollectionView;
        private Client.Model.PhysicianClientCollection m_PhysicianClientCollection;
        private Client.Model.PhysicianClientCollection m_RDPhysicianClientCollection;

        private SearchResultList m_ResultList;
        private Document.CaseDocumentCollection m_CaseDocumentCollection;
        private Document.CaseDocumentCollection m_PatientHistoryCaseDocumentCollection;        

        private YellowstonePathology.Business.Patient.Model.PatientHistoryList m_PatientHistoryList;

        public SearchUI()
        {
            this.m_SearchList = new SearchList();            
            this.m_SearchListCollectionView = new ListCollectionView(this.m_SearchList);
            this.m_PhysicianClientCollection = new Client.Model.PhysicianClientCollection();
            this.m_RDPhysicianClientCollection = new Client.Model.PhysicianClientCollection();
            this.m_PatientHistoryList = new YellowstonePathology.Business.Patient.Model.PatientHistoryList();

            this.m_ResultList = new SearchResultList();			            
        }

        public void Sort(string sortBy)
        {
            this.m_SearchListCollectionView.SortDescriptions.Clear();
            switch (sortBy)
            {
                case "Patient Name":                    
                    this.m_SearchListCollectionView.SortDescriptions.Add(new SortDescription("PatientName", ListSortDirection.Ascending));                    
                    break;
                case "Accession Date":
                    this.m_SearchListCollectionView.SortDescriptions.Add(new SortDescription("AccessionDate", ListSortDirection.Descending));
                    break;
                case "SSN":                    
                    this.m_SearchListCollectionView.SortDescriptions.Add(new SortDescription("SSN", ListSortDirection.Ascending));
                    break;
            }
        }

        public void GetRDPhysicianClientCollection(string physicianLastName)
        {
            this.m_RDPhysicianClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientListByPhysicianLastNameV2(physicianLastName);
            this.NotifyPropertyChanged("RDPhysicianClientCollection");
        }

        public void GetPhysicianClientCollection(string physicianLastName)
        {
            this.m_PhysicianClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientListByPhysicianLastNameV2(physicianLastName);
            this.NotifyPropertyChanged("PhysicianClientCollection");
        }
             
        public YellowstonePathology.Business.Patient.Model.PatientHistoryList PatientHistoryList
        {
            get { return this.m_PatientHistoryList; }
        }        

        public SearchResultList ResultList
        {
            get { return this.m_ResultList; }
        }

		public Document.CaseDocumentCollection CaseDocumentCollection
        {
			get { return this.m_CaseDocumentCollection; }
        }

		public Document.CaseDocumentCollection PatientHistoryCaseDocumentCollection
        {
			get { return this.m_PatientHistoryCaseDocumentCollection; }
        }        

        public SearchList SearchList
        {
            get { return this.m_SearchList; }
        }

        public ListCollectionView SearchListCollectionView
        {
            get { return this.m_SearchListCollectionView; }
        }
        
        public Client.Model.PhysicianClientCollection PhysicianClientCollection
        {
            get { return this.m_PhysicianClientCollection; }
            set 
            {
                this.m_PhysicianClientCollection = value;
                this.NotifyPropertyChanged("PhysicianClientCollection");
            }
        }

        public Client.Model.PhysicianClientCollection RDPhysicianClientCollection
        {
            get { return this.m_RDPhysicianClientCollection; }
            set 
            {
                this.m_RDPhysicianClientCollection = value;
                this.NotifyPropertyChanged("RDPhysicianClientCollection");
            }
        }

		public void RefreshCaseDocumentCollection(string reportNo)
		{
			this.m_CaseDocumentCollection = new Document.CaseDocumentCollection(reportNo);
            this.NotifyPropertyChanged("CaseDocumentCollection");
		}

		public void RefreshPatientHistoryCaseDocumentCollection(string reportNo)
		{
			this.m_PatientHistoryCaseDocumentCollection = new Document.CaseDocumentCollection(reportNo);
            this.NotifyPropertyChanged("PatientHistoryCaseDocumentCollection");
		}

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
	}
}
