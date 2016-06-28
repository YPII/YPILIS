using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.UI.Test
{
	public class LabUI : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection m_ClientOrderCollection;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_CurrentClientOrder;
		private YellowstonePathology.Business.Test.SearchEngine m_SearchEngine;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
		YellowstonePathology.Business.Search.PathologistSearch m_PathologistSearch;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
		YellowstonePathology.Business.User.SystemUserCollection m_MedTechUsers;
		YellowstonePathology.Business.User.SystemUserCollection m_LogUsers;

		YellowstonePathology.Business.PanelSet.Model.PanelSetCaseTypeCollection m_PanelSetCaseTypeCollection;

		YellowstonePathology.Business.FileList m_DigeneImportFileList;
		YellowstonePathology.Business.PanelSet.Model.PanelSetCollection m_PanelSetCollection;

		private YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;				

		private bool m_HasDataError;

		YellowstonePathology.Business.Domain.XElementFromSql m_AcknowledgeOrders;
		private string m_PanelOrderIds;
		YellowstonePathology.Business.Common.FieldEnabler m_FieldEnabler;
		YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        System.Windows.Controls.TabItem m_Writer;


        public LabUI(YellowstonePathology.Business.User.SystemIdentity systemIdentity, System.Windows.Controls.TabItem writer)
		{            
            this.m_SystemIdentity = systemIdentity;
            this.m_Writer = writer;
			
			this.m_SearchEngine = new Business.Test.SearchEngine();
			this.m_MedTechUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.MedTech, true);
			this.m_LogUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Log, true);

            this.m_PanelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetMolecularLabPanelSets();
			
			this.m_DigeneImportFileList = new YellowstonePathology.Business.FileList();
            			
			this.m_AcknowledgeOrders = new YellowstonePathology.Business.Domain.XElementFromSql();
			this.m_PanelOrderIds = string.Empty;
			this.m_HasDataError = false;

			this.m_PathologistSearch = new YellowstonePathology.Business.Search.PathologistSearch(this.m_Writer);

			this.m_PathologistUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist, true);			

			this.m_FieldEnabler = new YellowstonePathology.Business.Common.FieldEnabler();

			this.m_PanelSetCaseTypeCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPanelSetCaseTypeCollection();            
		}

        public YellowstonePathology.Business.User.SystemIdentity SystemIdentity
        {
            get { return Business.User.SystemIdentity.Instance; }
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection ClientOrderCollection
        {
            get { return this.m_ClientOrderCollection; }
            set
            {
                this.m_ClientOrderCollection = value;
                this.NotifyPropertyChanged("ClientOrderCollection");
            }
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder CurrentClientOrder
        {
            get { return this.m_CurrentClientOrder; }
            set
            {
                this.m_CurrentClientOrder = value;
                this.NotifyPropertyChanged("CurrentClientOrder");
            }
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

		public YellowstonePathology.Business.Common.FieldEnabler FieldEnabler
		{
			get { return this.m_FieldEnabler; }
			set { this.m_FieldEnabler = value; }
		}

		public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
            set
            {
                this.m_PanelSetOrder = value;
                this.NotifyPropertyChanged("PanelSetOrder");
            }
		}

		public YellowstonePathology.Business.Search.ReportSearchList CaseList
        {
			get { return this.m_SearchEngine.ReportSearchList; }
        }

		public YellowstonePathology.Business.User.SystemUserCollection MedTechUsers
        {
            get { return this.m_MedTechUsers; }
        }

		public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
        {
            get { return this.m_PathologistUsers; }
        }

		public YellowstonePathology.Business.User.SystemUserCollection LogUsers
		{
			get { return this.m_LogUsers; }
		}

		public YellowstonePathology.Business.PanelSet.Model.PanelSetCollection PanelSetCollection
        {
			get { return this.m_PanelSetCollection; }
		}

        public YellowstonePathology.Business.AutomatedOrderList AutomatedOrderList
		{
			get { return this.m_SearchEngine.AutomatedOrderList; }
		}

		public YellowstonePathology.Business.FileList DigeneImportFileList
		{
			get { return m_DigeneImportFileList; }
		}

		public YellowstonePathology.Business.Domain.XElementFromSql AcknowledgeOrders
		{
			get { return this.m_AcknowledgeOrders; }
		}

        public System.Windows.Controls.TabItem Writer
        {
            get { return this.m_Writer; }
        }

        public void GetAccessionOrder(string masterAccessionNo, string reportNo)
		{                         
            this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);
			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);             

			this.m_CaseDocumentCollection = new YellowstonePathology.Business.Document.CaseDocumentCollection(this.AccessionOrder, reportNo);
			this.RunWorkspaceEnableRules();
			this.NotifyPropertyChanged("");
		}

		public void PrintCaseList(string description, DateTime printDate, YellowstonePathology.Business.Search.ReportSearchList selectedItemList)
		{
			YellowstonePathology.Business.MolecularTesting.CaseList report = new YellowstonePathology.Business.MolecularTesting.CaseList();
			report.Print(selectedItemList, description, printDate);
		}

		public void ViewLabOrderLog(DateTime orderDate)
		{
			string rptpath = @"\\CFileServer\documents\Reports\Lab\LabOrdersLog\YEAR\MONTH\LabOrdersLog.FILEDATE.v1.xml";

			string rptName = rptpath.Replace("YEAR", orderDate.ToString("yyyy"));
			rptName = rptName.Replace("MONTH", orderDate.ToString("MMMM"));
			rptName = rptName.Replace("FILEDATE", orderDate.ToString("MM.dd.yy"));

			YellowstonePathology.Business.Reports.LabOrdersLog labOrdersLog = new YellowstonePathology.Business.Reports.LabOrdersLog();
			labOrdersLog.CreateReport(orderDate);

			string holdRptName = string.Empty;
			do
			{
				holdRptName = rptName; 
				int vLocation = rptName.IndexOf(".v");
				int originalVersion = Convert.ToInt32(rptName.Substring(vLocation + 2, 1));
				int newVersion = originalVersion + 1;
				rptName = rptName.Replace(".v" + originalVersion.ToString(), ".v" + newVersion.ToString());
			} while (File.Exists(rptName));

			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(holdRptName);
		}

		public bool HasDataError
		{
			get { return this.m_HasDataError; }
			set	{ m_HasDataError = value; }
		}

		public void RunWorkspaceEnableRules()
		{
			YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
			YellowstonePathology.Business.Rules.WorkspaceEnableRules workspaceEnableRules = new YellowstonePathology.Business.Rules.WorkspaceEnableRules();
			workspaceEnableRules.Execute(this.m_AccessionOrder, this.m_PanelSetOrder, this.m_FieldEnabler, executionStatus);
		}

		public string OrderedBy
		{
			get
			{
				string name = string.Empty;
				if (AccessionOrder != null && m_PanelSetOrder != null)
				{
					Nullable<int> id = m_PanelSetOrder.OrderedById;
					if (id.HasValue)
					{
						YellowstonePathology.Business.User.SystemUser orderedBy = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(id.Value);
						name = orderedBy.UserName;
					}
				}
				return name;
			}
		}

		public YellowstonePathology.Business.PanelSet.Model.PanelSetCaseTypeCollection PanelSetCaseTypeCollection
		{
			get { return this.m_PanelSetCaseTypeCollection; }
		}             		

		public YellowstonePathology.Business.Document.CaseDocumentCollection CaseDocumentCollection
		{
			get { return this.m_CaseDocumentCollection; }
		}

		public YellowstonePathology.Business.Test.SearchEngine SearchEngine
		{
			get { return this.m_SearchEngine; }
		}

		public void FillCaseList()
		{
			this.m_SearchEngine.FillSearchList();
			this.NotifyPropertyChanged("CaseList");
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public void RefreshCaseDocumentCollection()
		{
			this.m_CaseDocumentCollection = new YellowstonePathology.Business.Document.CaseDocumentCollection(this.m_PanelSetOrder.ReportNo);
			NotifyPropertyChanged("CaseDocumentCollection");
		}

		public YellowstonePathology.Business.Test.Model.TestOrderCollection TestOrders
		{
			get
			{
				if(this.m_AccessionOrder != null) return this.m_PanelSetOrder.GetTestOrders();
				return null;
			}
		}

		public string ResultString
		{
			get
			{
				string result = string.Empty;
				if (this.m_AccessionOrder != null) result = this.m_AccessionOrder.ToResultString(this.m_PanelSetOrder.ReportNo);
				return result;
			}
		}

		public void SetPanelSetOrder(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
		{
			this.m_PanelSetOrder = panelSetOrder;
		}
	}
}
