using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace YellowstonePathology.UI.Cytology
{
    public class CytologyUI : INotifyPropertyChanged
    {
		public delegate void AccessionChangedEventHandler(object sender, EventArgs e);
		public event AccessionChangedEventHandler AccessionChanged;

        public delegate void WHPOpenedEventHandler(object sender, EventArgs e);
        public event WHPOpenedEventHandler WHPOpened;

        public delegate void WHPClosedEventHandler(object sender, EventArgs e);
        public event WHPClosedEventHandler WHPClosed;

        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

		YellowstonePathology.Business.Cytology.Model.ScreeningImpressionCollection m_ScreeningImpressionCollection;
		YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCollection m_SpecimenAdequacyCollection;

        YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
        YellowstonePathology.Business.Search.CytologyScreeningSearch m_Search;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
		YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology m_PanelSetOrderCytology;

        YellowstonePathology.Business.Domain.Cytology.OtherConditionCollection m_OtherConditionCollection;
        YellowstonePathology.Business.Domain.HpvRequisitionInstructionCollection m_HpvRequisitionInstructions;
		protected YellowstonePathology.Business.Domain.PatientHistory m_PatientHistory;

		bool m_UserIsPathologist;
		bool m_UserIsCytotech;
		YellowstonePathology.Business.Domain.DataLoadResult m_DataLoadResult;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.UI.PageNavigationWindow m_PageNavigationWindow;
        private System.Windows.Controls.TabItem m_Writer;

        private YellowstonePathology.Business.Facility.Model.Facility m_Facility;
        private string m_Location;

        public CytologyUI(System.Windows.Controls.TabItem writer)
        {
            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
            this.m_Writer = writer;

			this.m_UserIsPathologist = this.m_SystemIdentity.User.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist);
			this.m_UserIsCytotech = this.m_SystemIdentity.User.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.CytologyScreener);

			this.m_Search = new YellowstonePathology.Business.Search.CytologyScreeningSearch();

			this.m_ScreeningImpressionCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetScreeningImpressions();
			this.m_SpecimenAdequacyCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSpecimenAdequacy();            

			this.m_OtherConditionCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetOtherConditions();
            this.m_HpvRequisitionInstructions = new Business.Domain.HpvRequisitionInstructionCollection();
			this.m_DataLoadResult = new Business.Domain.DataLoadResult();

            this.m_Facility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
            this.m_Location = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName;
        }

        public void AssignScreenings(List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> cytologyScreeningSearchResults, YellowstonePathology.Business.User.SystemUser systemUser)
		{			
			YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new Business.Rules.ExecutionStatus();            
            foreach (YellowstonePathology.Business.Search.CytologyScreeningSearchResult cytologyScreeningSearchResult in cytologyScreeningSearchResults)
            {
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(cytologyScreeningSearchResult.MasterAccessionNo, this.m_Writer);

                if (accessionOrder.AccessionLock.IsLockAquiredByMe == true)
                {
                    YellowstonePathology.Business.Rules.Cytology.AssignScreening assignScreening = new YellowstonePathology.Business.Rules.Cytology.AssignScreening(this.m_Writer);
                    assignScreening.Execute(cytologyScreeningSearchResult.MasterAccessionNo, systemUser.UserId, executionStatus);
                }
                else
                {
                    YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPAP();
                    executionStatus.AddMessage(panelSetOrder.ReportNo + " was not assigned as it is locked.", false);
                }
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(accessionOrder, this.m_Writer);
            }

			if (executionStatus.ExecutionMessages.Count > 0)
			{
				YellowstonePathology.Business.Rules.ExecutionStatusDialog executionStatusDialog = new Business.Rules.ExecutionStatusDialog(executionStatus);
				executionStatusDialog.ShowDialog();
			}

			this.LoadDataByReportNo(this.m_PanelSetOrderCytology.ReportNo);
        }

        public YellowstonePathology.Business.Domain.Cytology.OtherConditionCollection OtherConditionCollection
        {
            get { return this.m_OtherConditionCollection; }
        }

        public bool IsUserACytotech()
        {
            bool result = false;            
            return result;
        }		

        public void MoveFocus()
        {			
			FrameworkElement element = Keyboard.FocusedElement as FrameworkElement;
			if (element != null && element.Name != null && element.Name != "TextBoxReportNoSearch")
			{
				element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
			}			
        }

        public void ShowCaseDocument()
        {			
			YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument report = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument(this.m_AccessionOrder, this.m_PanelSetOrderCytology, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(report.SaveFileName);
        }        

        public YellowstonePathology.Business.Search.CytologyScreeningSearch Search
        {
            get { return this.m_Search; }
        }

		public YellowstonePathology.Business.Domain.PatientHistory PatientHistory
		{
			get { return this.m_PatientHistory; }
		}

		public void AddPeerReview(string screeningType, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology initiatingPanelOrder)
        {
            YellowstonePathology.Business.Rules.Cytology.AddScreeningReview addScreeningReview = new YellowstonePathology.Business.Rules.Cytology.AddScreeningReview();
            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
            addScreeningReview.Execute(screeningType, this.m_AccessionOrder, initiatingPanelOrder, YellowstonePathology.Business.ProcessingModeEnum.Production, executionStatus);            
            this.NotifyPropertyChanged("");        
        }

		public void StartWomensHealthProfilePath()
        {
			if (this.m_AccessionOrder.PanelSetOrderCollection.HasWomensHealthProfileOrder() == true)
            {
				YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);

                YellowstonePathology.Business.Audit.Model.IsWHPAllDoneAuditCollection isWHPAllDoneAuditCollection = new Business.Audit.Model.IsWHPAllDoneAuditCollection(this.m_AccessionOrder);
				isWHPAllDoneAuditCollection.Run();

				if (isWHPAllDoneAuditCollection.ActionRequired == true)
                {
                    this.HandleWHPOpened();
                    YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = null;
                    YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrders = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(this.m_AccessionOrder.MasterAccessionNo);

                    if (clientOrders.Count > 0)
                    {
                        clientOrder = clientOrders[0];
                    }

                    this.m_PageNavigationWindow = new PageNavigationWindow(this.m_SystemIdentity);
                    YellowstonePathology.UI.Login.WomensHealthProfilePath womensHealthProfilePath = new YellowstonePathology.UI.Login.WomensHealthProfilePath(this.m_AccessionOrder, clientOrder, this.m_PageNavigationWindow.PageNavigator, m_PageNavigationWindow, Visibility.Collapsed);
                    womensHealthProfilePath.Back += new Login.WomensHealthProfilePath.BackEventHandler(WomensHealthProfilePath_Finished);
                    womensHealthProfilePath.Finish += new Login.WomensHealthProfilePath.FinishEventHandler(WomensHealthProfilePath_Finished);
                    womensHealthProfilePath.Start();
                    this.m_PageNavigationWindow.ShowDialog();                    
                }                                
            }            
        }

        private void WomensHealthProfilePath_Finished(object sender, EventArgs e)
        {
            this.m_PageNavigationWindow.Close();
            this.HandleWHPClosed();
        }

		public void ScreeningFinal(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderToFinal, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrderCytology.IsOkToFinalize(this.m_AccessionOrder, panelOrderToFinal, this.m_SystemIdentity, executionStatus);
            if (this.m_PanelSetOrderCytology.Final == true)
            {
                Business.Audit.Model.AuditResult result = this.m_PanelSetOrderCytology.CheckFinaledUnsatResultWithAssociatedCases(this.m_AccessionOrder);
                if(result.Status == Business.Audit.Model.AuditStatusEnum.Failure)
                {
                    MessageBox.Show(result.Message);
                }
                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

                if (this.m_AccessionOrder.PanelSetOrderCollection.WomensHealthProfileExists() == true)
                {
                    this.m_AccessionOrder.PanelSetOrderCollection.GetWomensHealthProfile().SetExptectedFinalTime(this.m_AccessionOrder);
                }                
                
				this.StartWomensHealthProfilePath();
            }						
        }        

		public void SetResultToAgree(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderToSet, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            YellowstonePathology.Business.Rules.Cytology.SetResultToAgree setResultToAgree = new YellowstonePathology.Business.Rules.Cytology.SetResultToAgree();
			setResultToAgree.Execute(panelOrderToSet, this.m_AccessionOrder, executionStatus);
		}

		public void SetResult(string resultCode, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            YellowstonePathology.Business.Rules.Cytology.SetResult setResult = new YellowstonePathology.Business.Rules.Cytology.SetResult();
			setResult.Execute(resultCode, selectedPanelOrder, this.m_AccessionOrder, executionStatus);
		}

		public void SetSpecimenAdequacy(YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy specimenAdequacy, List<YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment> specimenAdequacyComments,
										YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder,
										YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
            YellowstonePathology.Business.Rules.Cytology.SetSpecimenAdequacy setSpecimenAdequacy = new YellowstonePathology.Business.Rules.Cytology.SetSpecimenAdequacy();
			setSpecimenAdequacy.Execute(specimenAdequacy, specimenAdequacyComments, selectedPanelOrder, this.m_AccessionOrder, executionStatus);
		}

		public void SetScreeningImpression(YellowstonePathology.Business.Cytology.Model.ScreeningImpression screeningImpression,
											YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder,
											YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
            YellowstonePathology.Business.Rules.Cytology.SetScreeningImpression setScreeningImpression = new YellowstonePathology.Business.Rules.Cytology.SetScreeningImpression();
			setScreeningImpression.Execute(screeningImpression, selectedPanelOrder, this.m_AccessionOrder, executionStatus);
		}

		public void SetOtherCondition(string otherCondition, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
			YellowstonePathology.Business.Rules.Cytology.SetOtherCondition setOtherCondition = new YellowstonePathology.Business.Rules.Cytology.SetOtherCondition();
			setOtherCondition.Execute(otherCondition, selectedPanelOrder, executionStatus);
		}

        public void SetReportComment(string comment, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
			YellowstonePathology.Business.Rules.Cytology.SetReportComment setReportComment = new YellowstonePathology.Business.Rules.Cytology.SetReportComment();
            setReportComment.Execute(comment, selectedPanelOrder, executionStatus);
        }

        public void SetAccessionOrder(YellowstonePathology.Business.Search.CytologyScreeningSearchResult searchResult)
        {						
            this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(searchResult.MasterAccessionNo, this.m_Writer);
			this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(searchResult.ReportNo);
            this.m_SpecimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrderCytology.OrderedOnId);
             
			this.DataLoaded();
        }

        public bool SetAccessionOrderByReportNo(string reportNo)
        {            
            this.LoadDataByReportNo(reportNo);			
			return this.DataLoadResult.Successful;
        }

        public bool SetAccessionOrderByAliquotOrderId(string aliquotOrderId)
        {            
            this.LoadDataByAliquotOrderId(aliquotOrderId);
            return this.DataLoadResult.Successful;
        }

		public void SetAccessionOrder(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{            
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
             
        }

        public void ScreeningUnfinal(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrderToUnfinal, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
			YellowstonePathology.Business.Rules.Cytology.ScreeningUnfinal screeningUnfinal = new YellowstonePathology.Business.Rules.Cytology.ScreeningUnfinal();
			screeningUnfinal.Execute(cytologyPanelOrderToUnfinal, this.m_AccessionOrder, executionStatus);			
		}

        public void ClearCase(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrderToClear, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            YellowstonePathology.Business.Rules.Cytology.ClearCase clearCase = new YellowstonePathology.Business.Rules.Cytology.ClearCase();
            clearCase.Execute(cytologyPanelOrderToClear, this.m_PanelSetOrderCytology, executionStatus);
        }

		public void DeletePanelOrder(YellowstonePathology.Business.Interface.IPanelOrder panelOrderToDelete, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
			YellowstonePathology.Business.Rules.Cytology.DeletePanelOrder deletePanelOrder = new YellowstonePathology.Business.Rules.Cytology.DeletePanelOrder();
			deletePanelOrder.Execute(panelOrderToDelete, this.m_AccessionOrder, executionStatus, this.m_SystemIdentity);			
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

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
            set { this.m_SpecimenOrder = value; }
        }

		public YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology PanelSetOrderCytology
		{
			get { return this.m_PanelSetOrderCytology; }
            set
            {
                this.m_PanelSetOrderCytology = value;
                this.NotifyPropertyChanged("PanelSetOrderCytology");
            }
        }

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
        {
            get { return this.m_ClientOrder; }
            set { this.m_ClientOrder = value; }
        }

		public YellowstonePathology.Business.Cytology.Model.ScreeningImpressionCollection ScreeningImpressionList
        {
			get { return this.m_ScreeningImpressionCollection; }
			set { this.m_ScreeningImpressionCollection = value; }
        }

		public YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyCollection SpecimenAdequacyCollection
        {
            get { return this.m_SpecimenAdequacyCollection; }
            set { this.m_SpecimenAdequacyCollection = value; }
        }

        public void DoListSearch(string selectedSearch, int screenedById)
        {
            switch (selectedSearch)
            {
                case "Final":
                    this.Search.ExecuteAcceptedBySearch(screenedById);
                    break;
                case "Pending":
					this.Search.ExecutePendingSearch(screenedById);
                    break;
				case "Not Final":
					this.Search.ExecuteNotFinaledSearch(screenedById);
					break;
                case "At Loggerheads":
                    this.Search.ExecuteAtLoggerheadSearch(screenedById);
                    break;
            }
        }

        public void ShowHistoryReport(string reportNo)
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(reportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameDoc(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
        }

        public void AddAcidWashPanelOrder(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology orderingPanelOrder, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {                     			
            string panelOrderId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapAcidWashPanel thinPrepPapAcidWashPanel = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapAcidWashPanel();
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderAcidWash panelOrderCytologyAcidWash = new YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderAcidWash(this.m_PanelSetOrderCytology.ReportNo, panelOrderId, panelOrderId, thinPrepPapAcidWashPanel, this.m_SystemIdentity.User.UserId, this.m_SystemIdentity.User.Initials);
            this.m_PanelSetOrderCytology.PanelOrderCollection.Add(panelOrderCytologyAcidWash);
            orderingPanelOrder.AppendReportComment(thinPrepPapAcidWashPanel.ReportComment);
		}

		public void OrderDotReviewPanelOrder(string comment, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology orderingPanelOrder)
        {                        			
            string panelOrderId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapDotReviewPanel thinPrepPapDotReviewPanel = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapDotReviewPanel();
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderCytologyDotReview = new YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology(this.m_PanelSetOrderCytology.ReportNo, panelOrderId, panelOrderId, thinPrepPapDotReviewPanel, this.m_SystemIdentity.User.UserId, this.m_SystemIdentity.User.Initials);
			panelOrderCytologyDotReview.OrderComment = comment;            
            panelOrderCytologyDotReview.OrderedById = orderingPanelOrder.OrderedById;
            this.m_PanelSetOrderCytology.PanelOrderCollection.Add(panelOrderCytologyDotReview);                            
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }		

		public bool RequisitionValidated
		{
			get { return Convert.ToBoolean(this.m_PanelSetOrderCytology.TemplateId); }
			set
			{
				this.m_PanelSetOrderCytology.TemplateId = Convert.ToInt32(value);
				NotifyPropertyChanged("RequisitionValidated");
			}
		}

		public int GetScreenerIndex()
		{						
			for (int idx = 0; idx < this.Search.Screeners.Count; idx++)
			{
				if ((this.Search.Screeners[idx].UserId == this.m_SystemIdentity.User.UserId))
				{
					return idx;
				}
			}
			return 0;
		}

		public void ShowAmendmentDialog(object sender, EventArgs e)
		{
            if (this.m_AccessionOrder != null)
            {
                YellowstonePathology.UI.AmendmentPageController amendmentPageController = new AmendmentPageController(this.m_AccessionOrder, this.m_PanelSetOrderCytology);
                amendmentPageController.ShowDialog();
            }
		}

        public YellowstonePathology.Business.Domain.HpvRequisitionInstructionCollection HpvRequisitionInstructions
        {
            get { return this.m_HpvRequisitionInstructions; }
        }

		public System.Windows.Visibility OptionalButtonVisibility
		{
			get
			{
				return this.m_UserIsPathologist ? System.Windows.Visibility.Collapsed : System.Windows.Visibility.Visible;
			}
		}

        public bool ButtonEnabled
        {
            get
            {
                if (this.m_AccessionOrder != null && this.m_AccessionOrder.AccessionLock.IsLockAquiredByMe == true && (this.m_UserIsCytotech || this.m_UserIsPathologist))
                {
                    return true;
                }
                return false;
            }
        }

        public YellowstonePathology.Business.Domain.DataLoadResult DataLoadResult
		{
			get { return this.m_DataLoadResult; }
			set { this.m_DataLoadResult = value; }
		}

        public void LoadDataByReportNo(string reportNo)
        {
            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromReportNo(reportNo);
            if(string.IsNullOrEmpty(masterAccessionNo) == false)
            {
                this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);
                if (this.m_AccessionOrder != null)
                {
                    this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
                }
                this.LoadData();
            }
            else
            {
                MessageBox.Show("Case not found.");
            }			
        }

        private void LoadDataByAliquotOrderId(string aliquotOrderId)
        {
            string masterAccessionNo = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromAliquotOrderId(aliquotOrderId);
			this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(aliquotOrderId);
			this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(15, specimenOrder.SpecimenOrderId, true);
            this.LoadData();
        }

        private void LoadData()
        {
            if (this.m_AccessionOrder != null)
            {
				this.SpecimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrderCytology.OrderedOnId);

				this.m_DataLoadResult.DataLoadStatusEnum = YellowstonePathology.Business.Domain.DataLoadStatusEnum.NotFound;
                YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(this.m_AccessionOrder.MasterAccessionNo);
                if (clientOrderCollection.Count == 0)
                {
                    this.m_ClientOrder = null;
					this.m_DataLoadResult.DataLoadStatusEnum = YellowstonePathology.Business.Domain.DataLoadStatusEnum.Successful;
                    this.DataLoaded();
                }
                else
                {
                    this.m_ClientOrder = clientOrderCollection[0];
					this.m_DataLoadResult.DataLoadStatusEnum = YellowstonePathology.Business.Domain.DataLoadStatusEnum.Successful;
                    this.DataLoaded();
                }

                this.NotifyPropertyChanged("ClientOrder");
            }
            else
            {
                MessageBox.Show("The ReportNo entered cannot be found.");
            }
        }		

		public void DataLoaded()
		{
            if (string.IsNullOrEmpty(this.m_AccessionOrder.PatientId) == false)
            {
				this.m_PatientHistory = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPatientHistory(this.m_AccessionOrder.PatientId);
            }
			this.NotifyPropertyChanged("");
			this.OnAccessionChanged(new EventArgs());
		}

		public YellowstonePathology.Business.User.SystemUser CurrentUser
		{
			get { return this.m_SystemIdentity.User; }
		}

		protected virtual void OnAccessionChanged(EventArgs e)
		{
			if (AccessionChanged != null)
			{
				AccessionChanged(this, e);
			}
		}

        public void HandleWHPOpened()
        {
            if(this.WHPOpened != null)
            {
                this.WHPOpened(this, new EventArgs());
            }
        }

        public void HandleWHPClosed()
        {
            if (this.WHPClosed != null)
            {
                this.WHPClosed(this, new EventArgs());
            }
        }

        public void UpdateAliquotLocation(string aliquotOrderId)
        {
            if (this.m_AccessionOrder.AccessionLock.IsLockAquiredByMe == true)
            {
                YellowstonePathology.Business.Test.AliquotOrder aliiquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(aliquotOrderId);
                if (aliiquotOrder != null)
                {
                    aliiquotOrder.SetLocation(this.m_Facility, this.m_Location);
                }
            }
        }
    }
}
