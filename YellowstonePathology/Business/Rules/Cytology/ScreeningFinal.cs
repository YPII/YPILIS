using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class ScreeningFinal
	{
        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_PanelOrderToFinal;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
		YellowstonePathology.Business.User.SystemUser m_UserPerformingFinal;
		YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology m_PanelSetOrderCytology;

        ProcessingModeEnum m_ProcessingMode;

        YellowstonePathology.Business.Rules.Rule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

        bool m_UserHasPermissions;        
        bool m_PanelAlreadyAccepted;
        bool m_ResultCodeHasNines;
        bool m_ScreeningResultsAreSet;
        bool m_UnacceptedDotReviewExists;

		public ScreeningFinal(ProcessingModeEnum processingMode)
        {            
            this.m_ProcessingMode = processingMode;
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
            
            this.m_Rule.ActionList.Add(IsUserPathologistOrCytotech);
            this.m_Rule.ActionList.Add(DoesPriorUnacceptedScreeningExist);
            this.m_Rule.ActionList.Add(ArePanelResultsAlreadyAccepted);
			this.m_Rule.ActionList.Add(DoesTheResultCodeHaveAnyNines);
            this.m_Rule.ActionList.Add(HandleHistorectomyWarning);            
            this.m_Rule.ActionList.Add(AreThePanelOrderResultsSet);
            this.m_Rule.ActionList.Add(SetScreenedByUserData);            
            this.m_Rule.ActionList.Add(SetScreeningError);
            this.m_Rule.ActionList.Add(AddNoChargePeerReviewForEndoGreaterThan40Comment);            
            this.m_Rule.ActionList.Add(AddPathologistReviewIfDiagnosisIsTwoOrBetter);
            this.m_Rule.ActionList.Add(HandleUnsatPrimaryScreeningResult);            
            this.m_Rule.ActionList.Add(HandleUnsatPathologistReviewScreeningConfirmedResult);
            this.m_Rule.ActionList.Add(HandleNoChargePathologistReview);
			this.m_Rule.ActionList.Add(HandleNoChargePathologistReviewEndometrialCells);    
            this.m_Rule.ActionList.Add(AcceptPanelOrder);
            this.m_Rule.ActionList.Add(DoesDotReviewExist);
			this.m_Rule.ActionList.Add(AddQCReviewIfNecessary);
            this.m_Rule.ActionList.Add(HandleImagerError);
            this.m_Rule.ActionList.Add(IsOkToFinalPanelSetOrderResult);
			this.m_Rule.ActionList.Add(IsQCScreenerSameAsInitialScreener);
			this.m_Rule.ActionList.Add(FinalPanelSetOrder);
            this.m_Rule.ActionList.Add(HandleScreeningError);
		}

        public bool UserHasPermissions
        {
            get { return this.m_UserHasPermissions; }
            set { this.m_UserHasPermissions = value; }
        }

        public bool PanelAlreadyAccepted
        {
            get { return this.m_PanelAlreadyAccepted; }
            set { this.m_PanelAlreadyAccepted = value; }
        }

        public bool ResultCodeHasNines
        {
            get { return this.m_ResultCodeHasNines; }
            set { this.m_ResultCodeHasNines = value; }
        }

        public bool ScreeningResultsAreSet
        {
            get { return this.m_ScreeningResultsAreSet; }
            set { this.m_ScreeningResultsAreSet = value; }
        }

        public bool UnacceptedDotReviewExists
        {
            get { return this.m_UnacceptedDotReviewExists; }
            set { this.m_UnacceptedDotReviewExists = value; }
        }

        private void HandleScreeningError()
        {
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderCytology = this.m_PanelSetOrderCytology.PanelOrderCollection.GetPrimaryScreening();            
            if(YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeNormal(panelOrderCytology.ResultCode) == true)
            {
                if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisThreeOrBetter(this.m_PanelSetOrderCytology.ResultCode))
                {
                    this.m_PanelSetOrderCytology.ScreeningError = true;
                }
                else
                {
                    this.m_PanelSetOrderCytology.ScreeningError = false;
                }
            }            
        }

        private void DoesPriorUnacceptedScreeningExist()
        {            
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(15);
            bool result = panelSetOrder.PanelOrderCollection.HasPriorUnacceptedPanelOrder(this.m_PanelOrderToFinal, 38);
            if (result == true)
            {
                this.m_ExecutionStatus.AddMessage("You cannot finalize this screening until a previous screening is finalized.", true);
                this.m_ExecutionStatus.ShowMessage = true;
            }
        }

        private void IsUserPathologistOrCytotech()
        {
            this.m_UserHasPermissions = false;
			YellowstonePathology.Business.User.SystemUser user = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(this.m_UserPerformingFinal.UserId) as YellowstonePathology.Business.User.SystemUser;
			if (user.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist) || user.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Cytotech))
			{
                this.m_UserHasPermissions = true;
                this.m_PanelOrderToFinal.PhysicianInterpretation = false;
				if (user.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist))
                {
                    this.m_PanelOrderToFinal.PhysicianInterpretation = true;
                }
            }            
            if (this.m_UserHasPermissions == false)
            {
                this.m_ExecutionStatus.AddMessage("Permission Denied", true);
				this.m_ExecutionStatus.ShowMessage = true;
            }
        }

        private void ArePanelResultsAlreadyAccepted()
        {
            this.m_PanelAlreadyAccepted = false;
            if (this.m_PanelOrderToFinal.Accepted == true)
            {
                this.m_Rule.ExecutionStatus.AddMessage("Unable to final the screening because it is already final.", true);
                this.m_PanelAlreadyAccepted = true;
				this.m_ExecutionStatus.ShowMessage = true;
			}
        }
        
        private void DoesTheResultCodeHaveAnyNines()
        {
            this.m_ResultCodeHasNines = false;
			string shortResult = (Convert.ToInt32(this.m_PanelOrderToFinal.ResultCode) / 10).ToString();
			if (shortResult.Contains('9') == true)
            {
                this.m_ResultCodeHasNines = true;
                this.m_Rule.ExecutionStatus.AddMessage("Unable to final the screening because one or more Nine's exists in the Result Code.", true);
				this.m_ExecutionStatus.ShowMessage = true;
			}
        }

        private void HandleHistorectomyWarning()
        {			
			if (string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory) == false)
			{
				if (this.m_AccessionOrder.ClinicalHistory.ToUpper().Contains("HYST") == true)
				{
					if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeTZonePresent(this.m_PanelOrderToFinal.ResultCode) == true ||
						YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeTZoneAbsent(this.m_PanelOrderToFinal.ResultCode))
					{
						System.Windows.MessageBox.Show("Warning, Clinical history indicates a hysterectomy, TZone present/absent may be inappropriate.");
					}
				}
			}			
		}

        private void AreThePanelOrderResultsSet()
        {
            this.m_ScreeningResultsAreSet = true;
            if (string.IsNullOrEmpty(this.m_PanelOrderToFinal.ScreeningImpression) == true)
            {
                this.m_ScreeningResultsAreSet = false;
                this.m_ExecutionStatus.AddMessage("Screening Impression is Missing.\n", true);
				this.m_ExecutionStatus.ShowMessage = true;
			}
            if (string.IsNullOrEmpty(this.m_PanelOrderToFinal.SpecimenAdequacy) == true)
            {
                this.m_ScreeningResultsAreSet = false;
                this.m_ExecutionStatus.AddMessage("Specimen Adequacy is Missing.\n", true);
				this.m_ExecutionStatus.ShowMessage = true;
			}
        }        

        private void AddNoChargePeerReviewForEndoGreaterThan40Comment()
        {
			string endoComment = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCytologyReportCommentById(44).Comment;
            if (string.IsNullOrEmpty(this.m_PanelOrderToFinal.ReportComment) == false && this.m_PanelOrderToFinal.ReportComment.Contains(endoComment) == true)
            {
                if (this.m_PanelSetOrderCytology.DoesPathologistReviewExist() == false)
                {
                    YellowstonePathology.Business.Rules.Cytology.AddScreeningReview addPeerReview = new YellowstonePathology.Business.Rules.Cytology.AddScreeningReview();
                    YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					addPeerReview.Execute("Pathologist Review", this.m_AccessionOrder, this.m_PanelOrderToFinal, this.m_ProcessingMode, executionStatus);
                }
            }
        }

        private void SetScreenedByUserData()
        {            
            this.m_PanelOrderToFinal.ScreenedById = this.m_UserPerformingFinal.UserId;
            this.m_PanelOrderToFinal.ScreenedByName = this.m_UserPerformingFinal.DisplayName;
        }

        private void AddPathologistReviewIfDiagnosisIsTwoOrBetter()
        {
			bool isTwoOrBetter = YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisTwoOrBetter(this.m_PanelOrderToFinal.ResultCode);
            if (isTwoOrBetter == true)
            {
				if (this.m_PanelSetOrderCytology.DoesPathologistReviewExist() == false)
                {
                    YellowstonePathology.Business.Rules.Cytology.AddScreeningReview addScreeningReview = new YellowstonePathology.Business.Rules.Cytology.AddScreeningReview();
                    YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					addScreeningReview.Execute("Pathologist Review", this.m_AccessionOrder, this.m_PanelOrderToFinal, this.m_ProcessingMode, executionStatus);
                }
            }
        }

        private void HandleUnsatPrimaryScreeningResult()
        {
			bool resultIsUnsat = YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeUnsat(this.m_PanelOrderToFinal.ResultCode);
            if (resultIsUnsat == true)
            {
				if (this.m_PanelSetOrderCytology.DoesPathologistReviewExist() == false)
                {
                    YellowstonePathology.Business.Rules.Cytology.AddScreeningReview addScreeningReview = new YellowstonePathology.Business.Rules.Cytology.AddScreeningReview();
                    YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					addScreeningReview.Execute("Pathologist Review", this.m_AccessionOrder, this.m_PanelOrderToFinal, this.m_ProcessingMode, executionStatus);
                }
            }
        }        

        private void HandleUnsatPathologistReviewScreeningConfirmedResult()
        {
            if (this.m_PanelOrderToFinal.ScreeningType.ToUpper() == "PATHOLOGIST REVIEW")
            {
				bool pathologistReviewResultIsUnsat = YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeUnsat(this.m_PanelOrderToFinal.ResultCode);
                if (pathologistReviewResultIsUnsat == true)
                {
					this.m_PanelSetOrderCytology.NoCharge = true;
					foreach (YellowstonePathology.Business.Interface.IPanelOrder panelOrder in this.m_PanelSetOrderCytology.PanelOrderCollection)
                    {
                        Type objectType = panelOrder.GetType();
                        if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                        {
                            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                            cytologyPanelOrder.NoCharge = true;
                        }
                    }                                
                }
            }
        }

        private void HandleNoChargePathologistReview()
        {
            if (this.m_PanelOrderToFinal.ScreeningType.ToUpper() == "PATHOLOGIST REVIEW")
            {
				bool pathologistReviewResultIsNormal = YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeNormal(this.m_PanelOrderToFinal.ResultCode);
				if (pathologistReviewResultIsNormal == true)
				{
					this.m_PanelOrderToFinal.NoCharge = true;
				}
            }
        }

		private void HandleNoChargePathologistReviewEndometrialCells()
		{
            if (this.m_PanelOrderToFinal.ScreeningType.ToUpper() == "PATHOLOGIST REVIEW")
            {
				bool pathologistReviewResultIsNormal = YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeNormal(this.m_PanelOrderToFinal.ResultCode);
				string endoComment = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCytologyReportCommentById(44).Comment;
				
				if (pathologistReviewResultIsNormal == false && 
					string.IsNullOrEmpty(this.m_PanelOrderToFinal.ReportComment) == false && this.m_PanelOrderToFinal.ReportComment.Contains(endoComment) == true)
				{
					this.m_PanelOrderToFinal.NoCharge = false;
				}
            }
		}

        private void HandleImagerError()
        {
            if (this.m_PanelOrderToFinal.ImagerError == true)
            {
				if (this.m_PanelSetOrderCytology.DoesScreeningReviewExist() == false)
                {
                    YellowstonePathology.Business.Rules.Cytology.AddScreeningReview addScreeningReview = new YellowstonePathology.Business.Rules.Cytology.AddScreeningReview();
                    YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					addScreeningReview.Execute("Cytotech Review", this.m_AccessionOrder, this.m_PanelOrderToFinal, this.m_ProcessingMode, executionStatus);
                }
            }
        }

        private void AcceptPanelOrder()
        {
            this.m_PanelOrderToFinal.Accepted = true;            
            this.m_PanelOrderToFinal.AcceptedDate = DateTime.Today;
            this.m_PanelOrderToFinal.AcceptedTime = DateTime.Now;
            this.m_PanelOrderToFinal.AcceptedById = this.m_UserPerformingFinal.UserId;
            this.m_PanelOrderToFinal.AssignedToId = this.m_UserPerformingFinal.UserId;
            this.m_PanelOrderToFinal.NotifyPropertyChanged("");            
        }

        private void DoesDotReviewExist()
        {
            this.m_UnacceptedDotReviewExists = false;
			for (int i = 0; i < this.m_PanelSetOrderCytology.PanelOrderCollection.Count; i++)
            {
				Type objectType = this.m_PanelSetOrderCytology.PanelOrderCollection[i].GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.m_PanelSetOrderCytology.PanelOrderCollection[i];
                    if (cytologyPanelOrder.ScreeningType.ToUpper() == "DOT REVIEW")
                    {
                        if (cytologyPanelOrder.Accepted == false)
                        {
                            this.m_UnacceptedDotReviewExists = true;
                            break;
                        }
                    }
                }
            }
            if (this.m_UnacceptedDotReviewExists == true)
            {
                this.m_ExecutionStatus.AddMessage("Dot Review Exists", true);
                this.m_ExecutionStatus.ShowMessage = false;
            }
        }

		private void AddQCReviewIfNecessary()
		{
            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
            YellowstonePathology.Business.Rules.Cytology.AddCytologyQCPanelOrder addQC = new AddCytologyQCPanelOrder();
			addQC.Execute(this.m_AccessionOrder, this.m_PanelOrderToFinal, executionStatus);
		}

        private void IsOkToFinalPanelSetOrderResult()
        {
			foreach (YellowstonePathology.Business.Interface.IPanelOrder panelOrder in this.m_PanelSetOrderCytology.PanelOrderCollection)
            {                
                Type objectType = panelOrder.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {				
                    if (panelOrder.Accepted == false)
                    {
                        this.m_ExecutionStatus.AddMessage("Not OK To Final PanelSetOrder", true);
                        this.m_ExecutionStatus.ShowMessage = false;
                        break;
                    }
                }
            }
        }

		private void IsQCScreenerSameAsInitialScreener()
		{            
			if (this.m_PanelOrderToFinal.ScreeningType.ToUpper() == "CYTOTECH REVIEW")
			{
                YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology primaryScreening = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.m_PanelSetOrderCytology.PanelOrderCollection.GetPrimaryScreening();
				if (primaryScreening.ScreenedById == this.m_PanelOrderToFinal.ScreenedById)
				{
					this.m_ExecutionStatus.AddMessage("When the reviewer is the same as the primary screener the reviewer may not final the PanelSetOrder.", true);
					this.m_ExecutionStatus.ShowMessage = true;
				}
			}         
		}        

        private void SetScreeningError()
        {
			if (this.m_PanelSetOrderCytology.PanelOrderCollection.Count > 1)
            {
                if (this.m_PanelOrderToFinal.ScreeningType.ToUpper() != "PRIMARY SCREENING")
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology primaryScreening = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.m_PanelSetOrderCytology.PanelOrderCollection.GetPrimaryScreening();
					bool primaryScreeningIsNormal = YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeNormal(primaryScreening.ResultCode);
                    if (primaryScreeningIsNormal == true)
                    {
                        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytotechReview = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.m_PanelOrderToFinal;
						if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisThreeOrBetter(cytotechReview.ResultCode) == true)
                        {
                            primaryScreening.ScreeningError = true;
                        }
                    }
                }
            }
        }        
        
        private void FinalPanelSetOrder()
        {
			this.m_PanelSetOrderCytology.Finish(this.m_AccessionOrder);
			this.m_PanelSetOrderCytology.AssignedToId = this.m_UserPerformingFinal.UserId;
			this.m_PanelSetOrderCytology.Audited = true;            

            if (this.m_PanelOrderToFinal.ScreeningType.ToUpper() == "PATHOLOGIST REVIEW")
            {
				this.m_PanelSetOrderCytology.HasProfessionalComponent = true;
				this.m_PanelSetOrderCytology.ProfessionalComponentFacilityId = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId;
				this.m_PanelSetOrderCytology.ProfessionalComponentBillingFacilityId = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId;
			}
            else
            {
				this.m_PanelSetOrderCytology.HasProfessionalComponent = false;
				this.m_PanelSetOrderCytology.ProfessionalComponentFacilityId = null;
				this.m_PanelSetOrderCytology.ProfessionalComponentBillingFacilityId = null;
			}            
		}

        public void Execute(YellowstonePathology.Business.User.SystemUser userPerformingFinal, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderToFinal, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
            this.m_UserPerformingFinal = userPerformingFinal;
            this.m_ExecutionStatus = executionStatus;
			this.m_AccessionOrder = accessionOrder;
            this.m_PanelOrderToFinal = panelOrderToFinal;
			this.m_PanelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelOrderToFinal.ReportNo);
			this.m_Rule.Execute(executionStatus);            
        }
	}
}
