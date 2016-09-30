using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
    public class CytologyScreeningSearch : INotifyPropertyChanged
    {
        protected delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;  

        Search.DateList m_AccessionDates;
        YellowstonePathology.Business.User.SystemUserCollection m_Screeners;

        List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> m_Results;        
        Search.AccessionDateField m_AccessionDate;

        List<string> m_SearchTypes;

		public CytologyScreeningSearch()
		{
			this.m_Screeners = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.CytologyScreener, true);
			YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.AddAllToUserList(this.m_Screeners, true);
            this.m_AccessionDate = new Search.AccessionDateField(DateTime.Today.AddDays(-1));
            this.NotifyPropertyChanged("AccessionDate");

            this.m_Results = new List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult>();
            this.m_AccessionDates = new Search.DateList();

            this.m_SearchTypes = new List<string>();            
            this.m_SearchTypes.Add("Final");
            this.m_SearchTypes.Add("Pending");
			this.m_SearchTypes.Add("Not Final");
            this.m_SearchTypes.Add("At Loggerheads");
        }

        public List<string> SearchTypes
        {
            get { return this.m_SearchTypes; }
        }

		public void ExecuteAcceptedBySearch(int acceptedById)
        {
            Search.CytologyScreeningSqlStatement sqlStatement = new YellowstonePathology.Business.Search.CytologyScreeningSqlStatement();
            YellowstonePathology.Business.Search.AcceptedField acceptedField = new AcceptedField(true);
			sqlStatement.SearchFields.Add(acceptedField);

			if (acceptedById != 0)
			{
				YellowstonePathology.Business.Search.AcceptedByIdField userField = new AcceptedByIdField(acceptedById);
				sqlStatement.SearchFields.Add(userField);
			}

			YellowstonePathology.Business.Search.DateLimitField dateLimitField = new DateLimitField(730, "AccessionDate");
			sqlStatement.SearchFields.Add(dateLimitField);
			this.m_Results = YellowstonePathology.Business.Gateway.CytologyScreeningSearchGateway.GetCytologyScreeningSearchResults(sqlStatement.ToString());
			this.NotifyPropertyChanged("Results");
        }

		public void ExecuteNotFinaledSearch(int assignedToId)
		{
            Search.CytologyScreeningSqlStatement sqlStatement = new YellowstonePathology.Business.Search.CytologyScreeningSqlStatement();
            YellowstonePathology.Business.Search.AcceptedField acceptedField = new AcceptedField(false);
            sqlStatement.SearchFields.Add(acceptedField);

            if (assignedToId != 0)
            {
                YellowstonePathology.Business.Search.AssignedToIdField assignedToIdField = new AssignedToIdField(assignedToId);
                sqlStatement.SearchFields.Add(assignedToIdField);
            }

			YellowstonePathology.Business.Search.DateLimitField dateLimitField = new DateLimitField(730, "AccessionDate");
			sqlStatement.SearchFields.Add(dateLimitField);

			this.m_Results = YellowstonePathology.Business.Gateway.CytologyScreeningSearchGateway.GetCytologyScreeningSearchResults(sqlStatement.ToString());
			this.NotifyPropertyChanged("Results");
		}

        public void ExecutePendingSearch(int orderedById)
        {
            Search.CytologyScreeningSqlStatement sqlStatement = new YellowstonePathology.Business.Search.CytologyScreeningSqlStatement();
            YellowstonePathology.Business.Search.AcceptedField acceptedField = new AcceptedField(false);
            sqlStatement.SearchFields.Add(acceptedField);

			if (orderedById != 0)
			{
                YellowstonePathology.Business.Search.OrderedByIdField orderedByIdField = new OrderedByIdField(orderedById);
				sqlStatement.SearchFields.Add(orderedByIdField);
			}

			YellowstonePathology.Business.Search.DateLimitField dateLimitField = new DateLimitField(730, "AccessionDate");
			sqlStatement.SearchFields.Add(dateLimitField);            

			this.m_Results = YellowstonePathology.Business.Gateway.CytologyScreeningSearchGateway.GetCytologyScreeningSearchResults(sqlStatement.ToString());
			this.NotifyPropertyChanged("Results");
        }

        public void ExecuteReportNoSearch(string reportNo)
        {
            Search.CytologyScreeningSqlStatement sqlStatement = new YellowstonePathology.Business.Search.CytologyScreeningSqlStatement();
            YellowstonePathology.Business.Search.ReportNoField reportNoField = new ReportNoField();
            reportNoField.Value = reportNo;

            sqlStatement.SearchFields.Add(reportNoField);
			this.m_Results = YellowstonePathology.Business.Gateway.CytologyScreeningSearchGateway.GetCytologyScreeningSearchResults(sqlStatement.ToString());
			this.NotifyPropertyChanged("Results");
        }

        public void ExecuteAtLoggerheadSearch(int assignedToId)
        {            
            this.m_Results = YellowstonePathology.Business.Gateway.CytologyScreeningSearchGateway.GetCytologyScreeningSearchResultsByAtLoggerheads(assignedToId);
            this.NotifyPropertyChanged("Results");
        }

        public List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> Results
        {
            get { return this.m_Results; }
        }

        public YellowstonePathology.Business.User.SystemUserCollection Screeners
        {
            get { return this.m_Screeners; }            
        }

        public DateTime AccessionDate
        {
            get { return this.m_AccessionDate.Value; }
            set 
            {
                if (this.m_AccessionDate.Value != value)
                {
                    this.m_AccessionDate.Value = value;
                    this.NotifyPropertyChanged("AccessionDate");
                }
            }
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
