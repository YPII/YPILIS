using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace YellowstonePathology.UI.Surgical
{
	public class PathologistUI : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;		
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

		private YellowstonePathology.Business.Search.PathologistSearch m_PathologistSearch;
		private YellowstonePathology.Business.Surgical.PathologistHistoryList m_PathologistHistoryList;
		private YellowstonePathology.Business.Test.PanelOrderCollection m_OrderCollection;
		private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
		private YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;
		private YellowstonePathology.Business.User.SystemUserCollection m_AmendmentUsers;
		private YellowstonePathology.Business.Common.FieldEnabler m_FieldEnabler;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        private string m_SignatureButtonText;
        private bool m_SignatureButtonIsEnabled;

		private int m_SelectedTabIndex;
        private System.Windows.Controls.TabItem m_Writer;

        private List<int> m_PanelSetIdsThatCanOrderStains;
        private string m_LastSlideOrderIdScanned;

        public PathologistUI(System.Windows.Controls.TabItem writer)
        {            
            this.m_Writer = writer;
            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;

            this.m_OrderCollection = new YellowstonePathology.Business.Test.PanelOrderCollection();			
			this.m_PathologistHistoryList = new YellowstonePathology.Business.Surgical.PathologistHistoryList();

			this.m_SelectedTabIndex = 0;
            
			this.m_PathologistSearch = new YellowstonePathology.Business.Search.PathologistSearch(this.m_Writer);
			this.m_PathologistUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetPathologistUsers();

			this.m_FieldEnabler = new YellowstonePathology.Business.Common.FieldEnabler();
			this.m_AmendmentUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.AmendmentSigner, true);

            this.m_PanelSetIdsThatCanOrderStains = new List<int>();
            YellowstonePathology.Business.Test.Surgical.SurgicalTest surgicalTest = new Business.Test.Surgical.SurgicalTest();
            this.m_PanelSetIdsThatCanOrderStains.Add(surgicalTest.PanelSetId);
            YellowstonePathology.Business.Test.TechnicalOnly.TechnicalOnlyTest technicalOnlyTest = new Business.Test.TechnicalOnly.TechnicalOnlyTest();
            this.m_PanelSetIdsThatCanOrderStains.Add(technicalOnlyTest.PanelSetId);
            YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTest reviewForAdditionalTestingTest = new Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTest();
            this.m_PanelSetIdsThatCanOrderStains.Add(reviewForAdditionalTestingTest.PanelSetId);
        }

        public Business.User.SystemIdentity SystemIdentity
        {
            get { return this.m_SystemIdentity; }
        }

        public string LastSlideOrderIdScanned
        {
            get { return this.m_LastSlideOrderIdScanned; }
            set { this.m_LastSlideOrderIdScanned = value; }
        }

        public string SignatureButtonText
        {
            get { return this.m_SignatureButtonText; }
            set
            {
                if (this.m_SignatureButtonText != value)
                {
                    this.m_SignatureButtonText = value;
                    this.NotifyPropertyChanged("SignatureButtonText");
                }
            }
        }

        public bool SignatureButtonIsEnabled
        {
            get { return this.m_SignatureButtonIsEnabled; }
            set
            {
                if (this.m_SignatureButtonIsEnabled != value)
                {
                    this.m_SignatureButtonIsEnabled = value;
                    this.NotifyPropertyChanged("SignatureButtonIsEnabled");
                }
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

		public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public YellowstonePathology.Business.Common.FieldEnabler FieldEnabler
		{
			get { return this.m_FieldEnabler; }
			set { this.m_FieldEnabler = value; }
		}

		public YellowstonePathology.Business.Search.PathologistSearch Search
		{
			get { return this.m_PathologistSearch; }
		}

		public YellowstonePathology.Business.User.SystemUser CurrentUser
		{
			get { return YellowstonePathology.Business.User.SystemIdentity.Instance.User; }
		}

        public System.Windows.Controls.TabItem Writer
        {
            get { return this.m_Writer; }
        }

		public void DoPatientIdSearch(YellowstonePathology.Business.Search.PathologistSearchResult selectedResult)
		{
			this.m_PathologistSearch.ExecutePatientIdSearch(selectedResult.PatientId);
		}

		public int SelectedTabIndex
		{
			get { return this.m_SelectedTabIndex; }
			set { this.m_SelectedTabIndex = value; }
		}

        public string CaseStatusTextColor
        {
            get
            {
                string color = string.Empty;
				if (this.m_PanelSetOrder != null && this.m_PanelSetOrder.Final == true)
                {
                    color = "Black";
                }
                else
                {
					if (this.AccessionOrder != null && this.AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
                    {
                        color = "Green";
                    }
                    else
                    {
                        color = "Red";
                    }
                }                              
                return color;
            }
        }

		public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
        {
            get { return this.m_PathologistUsers; }
        }

		public YellowstonePathology.Business.Surgical.PathologistHistoryList PathologistHistoryList
		{
			get { return this.m_PathologistHistoryList; }
		}		

		public virtual void GetAccessionOrder(string masterAccessionNo, string reportNo)
		{             
            this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);             
			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.RunWorkspaceEnableRules();
			this.m_CaseDocumentCollection = new Business.Document.CaseDocumentCollection(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
			this.m_AccessionOrder.PanelSetOrderCollection.PathologistTestOrderItemList.Build(this.m_AccessionOrder);
			this.NotifyPropertyChanged("");
		}
        
		public virtual void GetAccessionOrderByReportNo(string reportNo)
		{
            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo);
            this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);

            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_AccessionOrder.PanelSetOrderCollection.PathologistTestOrderItemList.Build(this.m_AccessionOrder);
			this.RunWorkspaceEnableRules();            
			this.NotifyPropertyChanged("");
		}

		public YellowstonePathology.Business.Rules.RuleExecutionStatus AssignCurrentUser()
		{
			return this.m_PathologistSearch.AssignCurrentUser();
		}

		public YellowstonePathology.Business.Document.CaseDocumentCollection CaseDocumentCollection
		{
			get { return this.m_CaseDocumentCollection; }
		}

		public YellowstonePathology.Business.User.SystemUserCollection AmendmentUsers
		{
		    get { return this.m_AmendmentUsers; }            
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string PatientAccessionAgeInfo
		{
			get
			{
				StringBuilder result = new StringBuilder();
				if (this.m_AccessionOrder.PBirthdate.HasValue)
				{
					result.Append(this.m_AccessionOrder.PBirthdate.Value.ToShortDateString() + "  ");
					result.Append(this.m_AccessionOrder.PatientAccessionAge + "  ");
				}
				if (string.IsNullOrEmpty(this.m_AccessionOrder.PSex) == false)
				{
					result.Append(this.m_AccessionOrder.PSex);
				}
				return result.ToString();
			}
		}       

        public void ShowAmendmentDialog()
		{
            YellowstonePathology.UI.AmendmentPageController amendmentPageController = new AmendmentPageController(this.m_AccessionOrder, this.m_PanelSetOrder);
			amendmentPageController.ShowDialog();
			this.RunPathologistEnableRules();
			this.NotifyPropertyChanged("AccessonOrder");
		}

		public void RunWorkspaceEnableRules()
		{
			YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
			YellowstonePathology.Business.Rules.WorkspaceEnableRules workspaceEnableRules = new YellowstonePathology.Business.Rules.WorkspaceEnableRules();
			workspaceEnableRules.Execute(this.m_AccessionOrder, this.m_PanelSetOrder, this.m_FieldEnabler, executionStatus);
			if (this.m_PanelSetOrder.PanelSetId == 15)
			{
				this.m_FieldEnabler.IsUnprotectedEnabled = false;
			}
		}

		public virtual void RunPathologistEnableRules()
		{
            this.SetSignatureButtonProperties();
			foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
			{
                foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrder.AmendmentCollection)
				{
					YellowstonePathology.Business.Rules.Surgical.SetAmendmentSignatureText setAmendmentSignatureText = new Business.Rules.Surgical.SetAmendmentSignatureText();
					setAmendmentSignatureText.Execute(this.m_AccessionOrder, this.m_PanelSetOrder, amendment);
				}
			}
		}

        public void SetSignatureButtonProperties()
        {
            if (this.m_PanelSetOrder.AssignedToId != 0)
            {
                if (this.m_PanelSetOrder.Final == false)
                {
                    this.m_SignatureButtonText = "Sign Case";
                    this.m_SignatureButtonIsEnabled = true;
                }
                else
                {
                    if (this.m_PanelSetOrder.TestOrderReportDistributionCollection.HasDistributedItems() == false)
                    {
                        this.m_SignatureButtonText = "Unsign Case";
                        this.m_SignatureButtonIsEnabled = true;
                    }
                    else
                    {
                        this.SignatureButtonText = "Electronic Signature";
                        this.SignatureButtonIsEnabled = false;
                    }
                }
            }
            else
            {
                this.SignatureButtonText = "Not Assigned";
                this.SignatureButtonIsEnabled = false;
            }

            this.NotifyPropertyChanged("SignatureButtonText");
            this.NotifyPropertyChanged("SignatureButtonIsEnabled");
        }

		public void DoGenericSearch()
		{
			this.m_PathologistSearch.ExecuteGenericSearch();
		}

		public void DoReportNoSearch(string reportNo)
		{
			this.m_PathologistSearch.ExecuteReportNoSearch(reportNo);
		}

        public void DoMasterAccessionNoSearch(string masterAccessionNo)
        {
            this.m_PathologistSearch.ExecuteMasterAccessionNoSearch(masterAccessionNo);
        }

        public void DoPatientNameSearch(string firstName, string lastName)
        {
            this.m_PathologistSearch.ExecuteNameSearch(firstName, lastName);
        }

		public void DoNameSearch(string firstName, string lastName)
		{
            this.m_PathologistSearch.ExecuteNameSearch(firstName, lastName);
		}

        public YellowstonePathology.Business.Search.PathologistSearchResultCollection DoSlideOrderIdSearch(string slideOrderId)
		{
			return this.m_PathologistSearch.ExecuteSlideOrderIdSearch(slideOrderId);
		}

        public YellowstonePathology.Business.Search.PathologistSearchResult DoAliquotOrderIdSearch(string aliquotOrderId, int panelSetIdHint)
        {
            return this.m_PathologistSearch.ExecuteAliquotOrderIdSearch(aliquotOrderId, panelSetIdHint);
        }

		public bool CanPlaceOrder()
		{
			if (this.m_AccessionOrder != null && this.m_AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
			{
				return true;
			}
			return false;
		}

		public void CheckEnabled()
		{
			this.RunWorkspaceEnableRules();
			this.RunPathologistEnableRules();
		}

		public virtual void SpellCheckCurrentItem()
		{
			if (this.m_PanelSetOrder.ReportNo != null)
			{
				YellowstonePathology.Business.Common.SpellChecker spellChecker = new YellowstonePathology.Business.Common.SpellChecker();
				YellowstonePathology.Business.Common.PathologistSpellCheckList pathologistSpellCheckList = new YellowstonePathology.Business.Common.PathologistSpellCheckList(this.m_AccessionOrder, this.m_PanelSetOrder);
				spellChecker.CheckSpelling(pathologistSpellCheckList);
				MessageBox.Show("Spell check is complete.");
			}
		}

		public string ReportNo
		{
			get
			{
				if (this.m_AccessionOrder != null && this.m_PanelSetOrder != null)
				{
					return this.m_PanelSetOrder.ReportNo;
				}
				return string.Empty;
			}
		}

		public string MasterAccessionNo
		{
			get
			{
				if (this.m_AccessionOrder != null)
				{
					return this.m_AccessionOrder.MasterAccessionNo;
				}
				return string.Empty;
			}
		}


        public void DeleteAmendment(YellowstonePathology.Business.Amendment.Model.Amendment amendment)
		{
			this.m_PanelSetOrder.DeleteAmendment(amendment.AmendmentId);
			this.RunPathologistEnableRules();
		}

		public void AddAmentment()
		{
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = this.PanelSetOrder.AddAmendment();
			amendment.UserId = this.m_PanelSetOrder.AssignedToId;

			this.RunWorkspaceEnableRules();
			this.RunPathologistEnableRules();
		}
        
        public bool StainOrderButtonIsEnabled
        {
            get
            {
                return this.m_FieldEnabler.IsUnprotectedEnabled &&
                    this.m_PanelSetOrder != null && this.m_PanelSetIdsThatCanOrderStains.Contains(this.m_PanelSetOrder.PanelSetId);
            }
        }      		
	}
}
