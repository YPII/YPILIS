using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Cytology
{
	public class SetSpecimenAdequacy
    {
        private YellowstonePathology.Business.Rules.Rule m_Rule;
        private YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

		private YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy m_SpecimenAdequacy;
		private List<YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment> m_SpecimenAdequacyComments;
        private YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology m_PanelOrder;
        private string m_ResultCode;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		public SetSpecimenAdequacy()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
            this.m_ExecutionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();

            this.m_Rule.ActionList.Add(IsOkToSetSpecimenAdequacy);
            this.m_Rule.ActionList.Add(SetSpecimenAdequacyResult);
			this.m_Rule.ActionList.Add(RemoveTZoneReportComment);
			this.m_Rule.ActionList.Add(RemoveUnsatResultComment);
			this.m_Rule.ActionList.Add(SetSpecimenAdequacyCode);
            this.m_Rule.ActionList.Add(HandleAbsentTZoneReportComment);
            this.m_Rule.ActionList.Add(HandleUnsatResult);
        }

        public void IsOkToSetSpecimenAdequacy()
        {
            if (this.m_PanelOrder.Accepted == true)
            {
                this.m_Rule.ExecutionStatus.AddMessage("Unable to set Specimen Adequacy because screening is final", true);
            }
        }

        public void SetSpecimenAdequacyResult()
        {
            StringBuilder result = new StringBuilder();
            if (this.m_SpecimenAdequacy != null)
            {
                result.Append(this.m_SpecimenAdequacy.Description);
            }
            else
            {
                result.Append(this.m_PanelOrder.SpecimenAdequacy);
            }

            if (this.m_SpecimenAdequacyComments.Count != 0)
            {
				foreach (YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment specimenAdequacyComment in this.m_SpecimenAdequacyComments)
                {
                    result.Append(" " + specimenAdequacyComment.Comment);
                }
            }
            this.m_PanelOrder.SpecimenAdequacy = result.ToString();
        }

		private void RemoveTZoneReportComment()
		{
			if (String.IsNullOrEmpty(m_PanelOrder.ReportComment) == false)
			{
				string reportComment = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCytologyReportCommentById(48).Comment.Trim();
				this.m_PanelOrder.ReportComment = this.m_PanelOrder.ReportComment.Replace(reportComment, string.Empty).Trim();
			}
		}

		private void RemoveUnsatResultComment()
		{
			if (String.IsNullOrEmpty(m_PanelOrder.ReportComment) == false)
			{
				string reportComment = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCytologyReportCommentById(17).Comment;
				this.m_PanelOrder.ReportComment = this.m_PanelOrder.ReportComment.Replace(reportComment, string.Empty).Trim();
			}
		}

        public void SetSpecimenAdequacyCode()
        {
            this.m_ResultCode = this.m_PanelOrder.ResultCode;
			string changedResultCode = YellowstonePathology.Business.Cytology.Model.CytologyResultCode.ChangeResultCode(this.m_ResultCode, this.m_SpecimenAdequacy);
            this.m_PanelOrder.ResultCode = changedResultCode;
            this.m_ResultCode = changedResultCode;
        }

        public void HandleAbsentTZoneReportComment()
        {
			bool tZoneIsAbsent = YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeTZoneAbsent(this.m_PanelOrder.ResultCode);
            if (tZoneIsAbsent == true)
            {
				string reportComment = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCytologyReportCommentById(48).Comment;
                if (string.IsNullOrEmpty(this.m_PanelOrder.ReportComment) == true || this.m_PanelOrder.ReportComment.Contains(reportComment) == false)
                {
                    if (string.IsNullOrEmpty(this.m_PanelOrder.ReportComment) == false)
                    {
                        this.m_PanelOrder.ReportComment += "  ";
                    }
                    this.m_PanelOrder.ReportComment += reportComment;
                }
            }
        }

        public void HandleUnsatResult()
        {
			if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsResultCodeUnsat(this.m_PanelOrder.ResultCode) == true)
            {
				YellowstonePathology.Business.Cytology.Model.ScreeningImpression screeningImpression = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetScreeningImpressionByResultCode("00");
                YellowstonePathology.Business.Rules.Cytology.SetScreeningImpression setScreeningImpression = new SetScreeningImpression();
                setScreeningImpression.Execute(screeningImpression, this.m_PanelOrder, this.m_AccessionOrder, this.m_ExecutionStatus);

				string reportComment = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetCytologyReportCommentById(17).Comment;
                YellowstonePathology.Business.Rules.Cytology.SetReportComment setReportComment = new SetReportComment();
                setReportComment.Execute(reportComment, this.m_PanelOrder, this.m_ExecutionStatus);
            }
        }

		public void Execute(YellowstonePathology.Business.Cytology.Model.SpecimenAdequacy specimenAdequacy, List<YellowstonePathology.Business.Cytology.Model.SpecimenAdequacyComment> specimenAdequacyComments, YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_SpecimenAdequacy = specimenAdequacy;
            this.m_SpecimenAdequacyComments = specimenAdequacyComments;
            this.m_PanelOrder = panelOrder;
			this.m_AccessionOrder = accessionOrder;
            this.m_ExecutionStatus = executionStatus;
            this.m_Rule.Execute(this.m_ExecutionStatus);
        }        
	}
}
