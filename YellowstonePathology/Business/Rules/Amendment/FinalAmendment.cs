using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Rules.Amendment
{
	public class FinalAmendment
	{
		YellowstonePathology.Business.Rules.Rule m_Rule;
		YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
        YellowstonePathology.Business.Amendment.Model.Amendment m_Amendment;
        YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

		public FinalAmendment()
        {			
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
            this.m_Rule.ActionList.Add(this.IsFinal);
			this.m_Rule.ActionList.Add(this.DoesAmendmentHaveQuestionMarks);
			this.m_Rule.ActionList.Add(this.SetFinalValues);
		}

		private void IsFinal()
		{
			if(this.m_Amendment.Final == true)
			{
				this.m_ExecutionStatus.AddMessage("The amendment is already finaled.", true);
			}
		}

		public void DoesAmendmentHaveQuestionMarks()
		{
			if (this.m_Amendment.Text.Contains("???"))
			{
				this.m_ExecutionStatus.AddMessage("The amendment text contains ???.", true);
			}
		}        

		private void SetFinalValues()
		{			
			string signature = this.m_SystemIdentity.User.Signature;
			if (signature == null)
			{
				signature = string.Empty;
			}

			this.m_Amendment.PathologistSignature = signature;
			this.m_Amendment.UserId = this.m_SystemIdentity.User.UserId;
			this.m_Amendment.Final = true;
			this.m_Amendment.FinalDate = DateTime.Today;
			this.m_Amendment.FinalTime = DateTime.Now;
		}

        public void Execute(YellowstonePathology.Business.Amendment.Model.Amendment amendment, 
            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_Amendment = amendment;
			this.m_ExecutionStatus = executionStatus;
            this.m_SystemIdentity = systemIdentity;
			this.m_Rule.Execute(this.m_ExecutionStatus);
		}
	}
}
