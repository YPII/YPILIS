using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace YellowstonePathology.UI.Surgical
{
	public class PathologistUI : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected YellowstonePathology.Business.Domain.Lock m_Lock;
		protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		protected YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

		private YellowstonePathology.Business.Search.PathologistSearch m_PathologistSearch;
		private YellowstonePathology.Business.Surgical.PathologistHistoryList m_PathologistHistoryList;
		private YellowstonePathology.Business.Test.PanelOrderCollection m_OrderCollection;
		private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
		private YellowstonePathology.Business.User.SystemUserCollection m_PathologistUsers;
		private YellowstonePathology.Business.User.SystemUserCollection m_AmendmentUsers;
		private YellowstonePathology.Business.Test.PanelSetOrderCollection m_PathologistOrderCollection;
		private YellowstonePathology.Business.Common.FieldEnabler m_FieldEnabler;
		private YellowstonePathology.UI.Test.ResultDialog m_ResultDialog;

        private string m_SignatureButtonText;
        private bool m_SignatureButtonIsEnabled;

		private int m_SelectedTabIndex;
		
		private AmendmentPageController m_AmendmentPageController;

		public PathologistUI(YellowstonePathology.Business.User.SystemIdentity systemidentity)
        {
			this.m_ObjectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();

            this.m_SystemIdentity = systemidentity;
			this.m_Lock = new YellowstonePathology.Business.Domain.Lock(this.m_SystemIdentity);            
			this.m_Lock.LockStatusChanged += new YellowstonePathology.Business.Domain.LockStatusChangedEventHandler(AccessionLock_LockStatusChanged);
            this.m_Lock.SetLockingMode(Business.Domain.LockModeEnum.AlwaysAttemptLock);
			this.m_OrderCollection = new YellowstonePathology.Business.Test.PanelOrderCollection();			
			this.m_PathologistHistoryList = new YellowstonePathology.Business.Surgical.PathologistHistoryList();

			this.m_SelectedTabIndex = 0;
            
			this.m_PathologistSearch = new YellowstonePathology.Business.Search.PathologistSearch(this.m_SystemIdentity);
			this.m_PathologistUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetPathologistUsers();

			this.m_FieldEnabler = new YellowstonePathology.Business.Common.FieldEnabler();
			this.m_AmendmentUsers = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.AmendmentSigner, true);
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

		public YellowstonePathology.Business.Persistence.ObjectTracker ObjectTracker
		{
			get { return this.m_ObjectTracker; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public YellowstonePathology.Business.Domain.Lock Lock
		{
			get { return this.m_Lock; }
			set { this.m_Lock = value; }
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
			get { return this.m_SystemIdentity.User; }
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

		public void ClearLock()
		{
			this.Lock.ReleaseLock();
			NotifyPropertyChanged("CaseStatusTextColor");
		}

		void AccessionLock_LockStatusChanged(object sender, EventArgs e)
		{
			((MainWindow)Application.Current.MainWindow).SetLockObject(this.Lock);
			NotifyPropertyChanged("CaseStatusTextColor");
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
					if (this.Lock.LockAquired == true)
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
			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(reportNo);
			this.m_ObjectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			this.m_ObjectTracker.RegisterObject(this.m_AccessionOrder);
			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_Lock.SetLockable(m_AccessionOrder);
			this.RunWorkspaceEnableRules();
			this.m_CaseDocumentCollection = new Business.Document.CaseDocumentCollection(this.m_AccessionOrder, this.m_PanelSetOrder.ReportNo);
			this.m_AccessionOrder.PanelSetOrderCollection.PathologistTestOrderItemList.Build(this.m_AccessionOrder);
			this.PathologistOrderCollection = this.m_AccessionOrder.PanelSetOrderCollection;
			this.NotifyPropertyChanged("");
		}
        
		public virtual void GetAccessionOrderByReportNo(string reportNo)
		{
			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(reportNo);
			this.m_ObjectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			this.m_ObjectTracker.RegisterObject(this.m_AccessionOrder);
			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_AccessionOrder.PanelSetOrderCollection.PathologistTestOrderItemList.Build(this.m_AccessionOrder);
			this.PathologistOrderCollection = this.m_AccessionOrder.PanelSetOrderCollection;
			this.m_Lock.SetLockable(m_AccessionOrder);
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

		public YellowstonePathology.Business.Test.PanelSetOrderCollection PathologistOrderCollection
		{
			get { return this.m_PathologistOrderCollection; }
			set
			{
				this.m_PathologistOrderCollection = value;
				NotifyPropertyChanged("PathologistOrderCollection");
			}
		}

		public void Save()
		{            
            if (this.m_AccessionOrder != null)
            {
                MainWindow.MoveKeyboardFocusNextThenBack();
                this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
            }
		}

		public void ShowAmendmentDialog()
		{
			this.Save();
			//YellowstonePathology.UI.AmendmentPageController amendmentPageController = new AmendmentPageController(this.m_AccessionOrder, this.m_ObjectTracker,
			//	this.m_PanelSetOrder, this.m_SystemIdentity);
			//amendmentPageController.ShowDialog();
			this.m_AmendmentPageController = new AmendmentPageController();
			AmendmentPath amendmentPath = new AmendmentPath(this.m_AccessionOrder, this.m_ObjectTracker, this.m_PanelSetOrder, this.m_SystemIdentity, this.m_AmendmentPageController.PageNavigator);
			amendmentPath.Finish += AmendmentPath_Finish;
			amendmentPath.Start(this.m_SystemIdentity);
			this.m_AmendmentPageController.ShowDialog();
			
			this.RunPathologistEnableRules();
			this.NotifyPropertyChanged("AccessonOrder");
		}
		
		private void AmendmentPath_Finish(object sender, EventArgs e)
		{
			this.m_AmendmentPageController.Close();
		}

		public void RunWorkspaceEnableRules()
		{
			YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
			YellowstonePathology.Business.Rules.WorkspaceEnableRules workspaceEnableRules = new YellowstonePathology.Business.Rules.WorkspaceEnableRules();
			workspaceEnableRules.Execute(this.m_AccessionOrder, this.m_PanelSetOrder, this.m_FieldEnabler, this.m_Lock, executionStatus, this.m_SystemIdentity);
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
					setAmendmentSignatureText.Execute(this.m_PanelSetOrder, amendment, this.Lock);
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

        public YellowstonePathology.Business.Search.PathologistSearchResult DoAliquotOrderIdSearch(string aliquotOrderId, int panelSetId)
        {
            return this.m_PathologistSearch.ExecuteAliquotOrderIdSearch(aliquotOrderId, panelSetId);
        }

		public bool CanPlaceOrder()
		{
			if (this.Lock.LockAquired)
			{
				return true;
			}
			return false;
		}

		public void AlterAccessionLock()
		{
			this.Lock.ToggleLockingMode();
			this.NotifyPropertyChanged("");
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
			this.Save();
			this.RunPathologistEnableRules();
		}

		public void AddAmentment()
		{
            YellowstonePathology.Business.Amendment.Model.Amendment amendment = this.PanelSetOrder.AddAmendment();
			amendment.UserId = this.m_PanelSetOrder.AssignedToId;

			this.Save();
			this.RunWorkspaceEnableRules();
			this.RunPathologistEnableRules();
		}        

		public void SetNonSurgicalPanelSetSignature()
		{
			YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = new YellowstonePathology.Business.Rules.RuleExecutionStatus();
			this.m_PanelSetOrder.Finalize(this.m_AccessionOrder, ruleExecutionStatus, this.m_SystemIdentity);

			if (ruleExecutionStatus.ExecutionHalted)
			{
				YellowstonePathology.UI.RuleExecutionStatusDialog ruleExecutionStatusDialog = new RuleExecutionStatusDialog(ruleExecutionStatus);
				ruleExecutionStatusDialog.ShowDialog();
			}

			this.Save();
			this.CheckEnabled();
			this.HandleAdditionalFinalRequirements();
		}

		private void HandleAdditionalFinalRequirements()
		{
			if (this.m_PanelSetOrder.Final == true)
			{
				if (this.m_PanelSetOrder is YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder)
				{
					this.HandleBraf();
				}

				if (this.m_PanelSetOrder is YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder ||
					this.m_PanelSetOrder is YellowstonePathology.Business.Test.ErPrSemiQuantitative.ErPrSemiQuantitativeTestOrder)
				{
					this.HandleHer2orErPr();
				}
			}
		}

		private void HandleBraf()
		{
			YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest panelSetLse = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest();
			YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest panelSetcccp = new YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfileTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.DoesPanelSetExist(panelSetLse.PanelSetId) == true)
			{
				string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLse.PanelSetId).ReportNo;
				this.m_ResultDialog = new Test.ResultDialog();
				YellowstonePathology.UI.Test.LynchSyndromeEvaluationResultPath resultPath = new YellowstonePathology.UI.Test.LynchSyndromeEvaluationResultPath(reportNo,
					this.m_AccessionOrder, this.m_ObjectTracker, this.m_ResultDialog.PageNavigator, Visibility.Visible);

				resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
				resultPath.Start();
				this.m_ResultDialog.ShowDialog();
			}
			else if (this.m_AccessionOrder.PanelSetOrderCollection.DoesPanelSetExist(panelSetcccp.PanelSetId) == true)
			{
				string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetcccp.PanelSetId).ReportNo;
				this.m_ResultDialog = new Test.ResultDialog();
				YellowstonePathology.UI.Test.ComprehensiveColonCancerProfilePath resultPath = new YellowstonePathology.UI.Test.ComprehensiveColonCancerProfilePath(reportNo,
					this.m_AccessionOrder, this.m_ObjectTracker, this.m_ResultDialog.PageNavigator, Visibility.Collapsed);

				resultPath.Finish += new Test.ResultPath.FinishEventHandler(ResultPath_Finish);
				resultPath.Start();
				this.m_ResultDialog.ShowDialog();
			}
		}

		private void HandleHer2orErPr()
		{
			YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelTest panelSetInvasiveBreastPanel = new YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetInvasiveBreastPanel.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
			{
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetInvasiveBreastPanel.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
				this.m_ResultDialog = new Test.ResultDialog();
				YellowstonePathology.UI.Test.InvasiveBreastPanelPath resultPath = new Test.InvasiveBreastPanelPath(panelSetOrder.ReportNo, this.m_AccessionOrder, this.m_ObjectTracker, this.m_ResultDialog.PageNavigator);
				resultPath.Finish += new Test.InvasiveBreastPanelPath.FinishEventHandler(ResultPath_Finish);
				resultPath.Start();
				this.m_ResultDialog.ShowDialog();
			}
		}

        private void ResultPath_Finish(object sender, EventArgs e)
        {
            this.m_ResultDialog.Close();
        }
	}
}
