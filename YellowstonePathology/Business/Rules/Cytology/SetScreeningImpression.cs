using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class SetScreeningImpression
	{
        YellowstonePathology.Business.Rules.Rule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

		YellowstonePathology.Business.Cytology.Model.ScreeningImpression m_ScreeningImpression;
        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_PanelOrder;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		string m_ResultCode;

		public SetScreeningImpression()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
            this.m_ExecutionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();

            this.m_Rule.ActionList.Add(IsOkToSetScreeningImpression);
            this.m_Rule.ActionList.Add(SetScreeningImpressionResult);
			this.m_Rule.ActionList.Add(RemoveBiopsyPerformedInAnotherFacilityComment);			
			this.m_Rule.ActionList.Add(SetScreeningImpressionCode);
			this.m_Rule.ActionList.Add(AddBiopsyPerformedInAnotherFacilityComment);			
		}

		public void Execute(YellowstonePathology.Business.Cytology.Model.ScreeningImpression screeningImpression, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_ScreeningImpression = screeningImpression;
            this.m_PanelOrder = panelOrder;
			this.m_AccessionOrder = accessionOrder;
            this.m_ExecutionStatus = executionStatus;
            this.m_Rule.Execute(this.m_ExecutionStatus);
        }       

        private void IsOkToSetScreeningImpression()
        {
            if (this.m_PanelOrder.Accepted == true)
            {
                this.m_Rule.ExecutionStatus.AddMessage("Unable to set the Screening Impression because screening is final.", true);
            }
        }

        private void SetScreeningImpressionResult()
        {
            this.m_PanelOrder.ScreeningImpression = this.m_ScreeningImpression.Description;
        }

		private void RemoveBiopsyPerformedInAnotherFacilityComment()
		{
			if (String.IsNullOrEmpty(m_PanelOrder.ReportComment) == false)
			{
				string otherFacilityComment = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCytologyReportCommentById(54).Comment;
				this.m_PanelOrder.ReportComment = this.m_PanelOrder.ReportComment.Replace(otherFacilityComment, string.Empty).Trim();
			}
		}
				
        public void SetScreeningImpressionCode()
        {
            this.m_ResultCode = this.m_PanelOrder.ResultCode.ToString();
			string changedResultCode = YellowstonePathology.Business.Cytology.Model.CytologyResultCode.ChangeResultCode(this.m_ResultCode, this.m_ScreeningImpression);
            this.m_PanelOrder.ResultCode = changedResultCode;
            this.m_ResultCode = changedResultCode;
        }  

		private void AddBiopsyPerformedInAnotherFacilityComment()
		{
			bool diagnosisIsGreaterThanThree = YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisGreaterThanThree(this.m_PanelOrder.ResultCode);
			if (diagnosisIsGreaterThanThree == true)
			{
				string otherFacilityComment = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCytologyReportCommentById(54).Comment;
				if (string.IsNullOrEmpty(this.m_PanelOrder.ReportComment) == true || this.m_PanelOrder.ReportComment.Contains(otherFacilityComment) == false)
				{
					if (string.IsNullOrEmpty(this.m_PanelOrder.ReportComment) == false)
					{
						this.m_PanelOrder.ReportComment += " ";
					}
					this.m_PanelOrder.ReportComment += otherFacilityComment;
				}
			}
		}		
	}
}
