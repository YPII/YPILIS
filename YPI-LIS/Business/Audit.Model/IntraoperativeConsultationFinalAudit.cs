using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
	public class IntraoperativeConsultationFinalAudit : Audit
	{
		private YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult m_IntraoperativeConsultationResult;

		public IntraoperativeConsultationFinalAudit(YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult)
		{
			this.m_IntraoperativeConsultationResult = intraoperativeConsultationResult;
		}

		public override void Run()
		{
			this.StartTimeIsSet();
			this.EndTimeIsSet();
			this.ResultIsSet();
			this.CallbackNumberIsSet();
			this.FinaledByIsSet();
		}

		private void StartTimeIsSet()
		{
			if (this.m_IntraoperativeConsultationResult.StartDate.HasValue == false)
			{
				this.Message.AppendLine("The Start Time must be entered before the intraoperative consultation can be finalled.");
				this.m_Status = AuditStatusEnum.Failure;
				this.m_ActionRequired = true;
			}
		}

		private void EndTimeIsSet()
		{
			if (this.m_IntraoperativeConsultationResult.EndDate.HasValue == false)
			{
				this.Message.AppendLine("The End Time must be entered before the intraoperative consultation can be finalled.");
				this.m_Status = AuditStatusEnum.Failure;
				this.m_ActionRequired = true;
			}
		}

		private void ResultIsSet()
		{
			if (string.IsNullOrEmpty(this.m_IntraoperativeConsultationResult.Result))
			{
				this.Message.AppendLine("The result must be entered before the intraoperative consultation can be finalled.");
				this.m_Status = AuditStatusEnum.Failure;
				this.m_ActionRequired = true;
			}
		}

		private void CallbackNumberIsSet()
		{
			if (string.IsNullOrEmpty(this.m_IntraoperativeConsultationResult.CallbackContact))
			{
				this.Message.AppendLine("The callback contact for the intraoperative consultation has not been entered.");
				this.m_Status = AuditStatusEnum.Failure;
				this.m_ActionRequired = true;
			}
		}

		private void FinaledByIsSet()
		{
			if (this.m_IntraoperativeConsultationResult.FinaledById == null)
			{
				this.Message.AppendLine("The pathologist performing the intraoperative consultation has not been selected.");
				this.m_Status = AuditStatusEnum.Failure;
				this.m_ActionRequired = true;
			}
		}
	}
}
