using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
	public class PathologistSearch : INotifyPropertyChanged
	{
		protected delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.User.SystemUserCollection m_Pathologists;
        private YellowstonePathology.Business.PanelSet.Model.PanelSetCollection m_PanelSets;
        private YellowstonePathology.Business.Search.PathologistSearchResultCollection m_Results;
        private List<string> m_PathologistCaseTypes;
        private List<string> m_FinalDates;

        private string m_SelectedCaseType;
        private int m_SelectedPanelSetId;
        private int m_SelectedPathologistId;
        private string m_FinalDateValue;
        private string m_SearchValue;
        
        object m_Writer;

		public PathologistSearch(object writer)
        {
            this.m_Writer = writer;

			this.m_FinalDates = new List<string>();
			this.m_FinalDates.Add("Not Final");
			this.m_FinalDates.Add("Final Today");
			this.m_FinalDates.Add("Final Yesterday");
			this.m_FinalDates.Add("Final Last 7 Days");
			this.m_FinalDates.Add("Final Last 30 Days");

            this.m_Pathologists = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetPathologistUsers();
			YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.AddAllToUserList(this.m_Pathologists, true);
			this.m_Pathologists[0].UserId = -1;
			YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.AddUnassignedToUserList(this.m_Pathologists, true);
			
			this.m_PanelSets = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();

			this.m_PathologistCaseTypes = this.m_PanelSets.GetPathologistsCaseTypes();
            this.m_SelectedCaseType = "All Case Types";

            this.m_Results = new YellowstonePathology.Business.Search.PathologistSearchResultCollection();

			this.m_SelectedPanelSetId = 0;
			this.m_SelectedPathologistId = -1;
            if (YellowstonePathology.Business.User.SystemIdentity.Instance.User.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist))
			{				
                this.m_SelectedPathologistId = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserId;
			}

			m_SearchValue = string.Empty;
		}		

		public void ExecuteGenericSearch()
		{
			string dateLimit = string.Empty;
			bool final = true;
			if (this.m_FinalDateValue.Contains("Not"))
			{				
                final = false;
			}
			if(this.m_FinalDateValue.Contains("30"))
			{
				dateLimit = "30";
			}
			if (this.m_FinalDateValue.Contains("7"))
			{
				dateLimit = "7";
			}
			if (this.m_FinalDateValue.Contains("Yesterday"))
			{
				dateLimit = "Yesterday";
			}
			if (this.m_FinalDateValue.Contains("Today"))
			{
				dateLimit = "Today";
			}

			YellowstonePathology.Business.Gateway.SearchGateway gateway = new Gateway.SearchGateway();
			this.m_Results = gateway.PathologistGenericSearch(this.m_SelectedCaseType, this.m_SelectedPathologistId, final, dateLimit);
			this.NotifyPropertyChanged("Results");   
		}

		public void ExecutePatientIdSearch(string patientId)
		{
			YellowstonePathology.Business.Gateway.SearchGateway gateway = new Gateway.SearchGateway();
			this.m_Results = gateway.PathologistPatientIdSearch(patientId);
			this.NotifyPropertyChanged("Results");
		}

        public YellowstonePathology.Business.Search.PathologistSearchResultCollection ExecuteSlideOrderIdSearch(string slideOrderId)
		{
			YellowstonePathology.Business.Gateway.SearchGateway gateway = new Gateway.SearchGateway();
            YellowstonePathology.Business.Search.PathologistSearchResultCollection pathologistSearchResultCollection = gateway.PathologistSlideOrderIdSearch(slideOrderId);
            foreach (YellowstonePathology.Business.Search.PathologistSearchResult psr in pathologistSearchResultCollection)
            {
                if (this.m_Results.ReportNoExists(psr.ReportNo) == false)
                {
                    this.m_Results.Add(psr);
                }
            }
			this.NotifyPropertyChanged("Results");
            return pathologistSearchResultCollection;
		}

        public YellowstonePathology.Business.Search.PathologistSearchResult ExecuteAliquotOrderIdSearch(string slideOrderId, int panelSetIdHint)
        {
            YellowstonePathology.Business.Gateway.SearchGateway gateway = new Gateway.SearchGateway();
            YellowstonePathology.Business.Search.PathologistSearchResult pathologistSearchResult = gateway.PathologistAliquotOrderIdSearch(slideOrderId, panelSetIdHint);

            if(pathologistSearchResult != null)
            {
                if (this.m_Results.ReportNoExists(pathologistSearchResult.ReportNo) == false)
                {
                    this.m_Results.Add(pathologistSearchResult);
                }
            }            
                        
            this.NotifyPropertyChanged("Results");
            return pathologistSearchResult;
        }              

		public void ExecuteNameSearch(string firstName, string lastName)
		{
			YellowstonePathology.Business.Gateway.SearchGateway gateway = new Gateway.SearchGateway();
			this.m_Results = gateway.PathologistNameSearch(lastName, firstName);
			this.NotifyPropertyChanged("Results");
		}

		public void ExecuteReportNoSearch(string reportNo)
		{
			YellowstonePathology.Business.Gateway.SearchGateway gateway = new Gateway.SearchGateway();
			this.m_Results = gateway.GetPathologistSearchListByReportNo(reportNo);
			this.NotifyPropertyChanged("Results");   
		}

        public void ExecuteMasterAccessionNoSearch(string masterAccessionNo)
        {
            YellowstonePathology.Business.Gateway.SearchGateway gateway = new Gateway.SearchGateway();
            this.m_Results = gateway.GetPathologistSearchListByMasterAccessionNoNo(masterAccessionNo);
            this.NotifyPropertyChanged("Results");
        }

		public YellowstonePathology.Business.Search.PathologistSearchResultCollection Results
		{
			get { return this.m_Results; }
		}

        public YellowstonePathology.Business.User.SystemUserCollection Pathologists
		{
			get { return this.m_Pathologists; }
		}

		public YellowstonePathology.Business.PanelSet.Model.PanelSetCollection PanelSets
		{
			get { return this.m_PanelSets; }
		}

        public List<string> PathologistCaseTypes
        {
            get { return this.m_PathologistCaseTypes; }
        }

        public string SelectedCaseType
        {
            get { return this.m_SelectedCaseType; }
            set
            {
                this.m_SelectedCaseType = value;
                this.NotifyPropertyChanged("SelectedCaseType");
            }
        }

		public int SelectedPanelSetId
		{
			get { return this.m_SelectedPanelSetId; }
			set
			{
				this.m_SelectedPanelSetId = value;
				this.NotifyPropertyChanged("SelectedPanelSetId");
			}
		}

		public int SelectedPathologistId
		{
			get { return this.m_SelectedPathologistId; }
			set
			{
				this.m_SelectedPathologistId = value;
				this.NotifyPropertyChanged("SelectedPathologistId");
			}
		}

		public string FinalDateValue
		{
			get { return this.m_FinalDateValue; }
			set
			{
				this.m_FinalDateValue = value;
				this.NotifyPropertyChanged("FinalDateValue");
			}
		}

		public string SearchValue
		{
			get { return this.m_SearchValue; }
			set
			{
				this.m_SearchValue = value;
				this.NotifyPropertyChanged("SearchValue");
			}
		}

		public List<string> FinalDates
		{
			get { return this.m_FinalDates; }
		}

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
        
		public YellowstonePathology.Business.Rules.RuleExecutionStatus AssignCurrentUser()
		{
			YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = new YellowstonePathology.Business.Rules.RuleExecutionStatus();
			foreach (YellowstonePathology.Business.Search.PathologistSearchResult item in this.m_Results)
			{
				if (item.Assign && item.GroupType != "Flow" && item.GroupType != "Cytology")
				{
					YellowstonePathology.Business.Rules.Surgical.RulesAssignPathologistId rule = YellowstonePathology.Business.Rules.Surgical.RulesAssignPathologistId.Instance;
					YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(item.MasterAccessionNo, this);					
					YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(item.ReportNo);
					rule.AccessionOrder = accessionOrder;
					rule.PanelSetOrder = panelSetOrder;                    
					rule.Run(ruleExecutionStatus);

					if (ruleExecutionStatus.ExecutionHalted == false)
					{
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
					}
				}
			}
			return ruleExecutionStatus;
		}
	}
}
