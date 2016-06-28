using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Deployment.Internal;

namespace YellowstonePathology.Business.Rules.Surgical
{
	public class PathologistFinalRules
	{
		private YellowstonePathology.Business.Rules.Rule m_Rule;
		private YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_PanelSetOrderSurgical;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private WordSearchList m_PapCorrelationWordList;
		private bool m_IsSigning;

		public PathologistFinalRules()
		{
			this.m_PapCorrelationWordList = new WordSearchList();
			this.m_PapCorrelationWordList.Add(new WordSearchListItem("ECC", true, string.Empty));
			this.m_PapCorrelationWordList.Add(new WordSearchListItem("CERVIX", true, string.Empty));
			this.m_PapCorrelationWordList.Add(new WordSearchListItem("CERVICAL", true, string.Empty));
			this.m_PapCorrelationWordList.Add(new WordSearchListItem("VAGINAL", true, string.Empty));
			this.m_PapCorrelationWordList.Add(new WordSearchListItem("ENDOCERVICAL", true, string.Empty));
			this.m_PapCorrelationWordList.Add(new WordSearchListItem("BLADDER", true, string.Empty));
			this.m_PapCorrelationWordList.Add(new WordSearchListItem("THYROID", true, string.Empty));
			this.m_PapCorrelationWordList.Add(new WordSearchListItem("BREAST", true, string.Empty));
			this.m_PapCorrelationWordList.Add(new WordSearchListItem("GALLBLADDER", false, string.Empty));

			this.m_Rule = new Rule();
			this.m_Rule.ActionList.Add(IsTheCaseBeingSigned);
			this.m_Rule.ActionList.Add(AreAncillaryStudiesHandled);
			this.m_Rule.ActionList.Add(IsPapCorrelationHandled);
			this.m_Rule.ActionList.Add(CaseIsAssigned);
			this.m_Rule.ActionList.Add(IsSpecialDermCaseViolation);
			this.m_Rule.ActionList.Add(IsIntraoperativeCorrelationHandled);
			this.m_Rule.ActionList.Add(CaseHasQuestionMarks);
			this.m_Rule.ActionList.Add(CurrentUserIsTheAssignedUser);
			this.m_Rule.ActionList.Add(CaseHasSvhAccount);
			this.m_Rule.ActionList.Add(CaseHasSvhMRN);
            this.m_Rule.ActionList.Add(CaseHasClientNotFound);
            this.m_Rule.ActionList.Add(CaseHasPhysicianNotFound);
            this.m_Rule.ActionList.Add(CaseHasUnfinaledPeerReview);
			this.m_Rule.ActionList.Add(GradedStainsAreHandled);
			this.m_Rule.ActionList.Add(SignCase);
			this.m_Rule.ActionList.Add(UnSignCase);
			this.m_Rule.ActionList.Add(DeleteUndistributedLogItems);
		}

		private void IsTheCaseBeingSigned()
		{
			this.m_IsSigning = this.m_PanelSetOrderSurgical.Final == false;
		}

		private void AreAncillaryStudiesHandled()
		{
			if (this.m_IsSigning == true)
			{
				foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in this.m_PanelSetOrderSurgical.TypingStainCollection)
				{
                    if (stainResultItem.ClientAccessioned == false)
                    {
                        if (string.IsNullOrEmpty(stainResultItem.Result) == true)
                        {
                            this.m_ExecutionStatus.AddMessage("One or more Ancillary study result is not set.", true);
                            break;
                        }
                        if (string.IsNullOrEmpty(stainResultItem.ProcedureComment) == true && (string.IsNullOrEmpty(stainResultItem.Result) || stainResultItem.Result != "Pending"))
                        {
                            this.m_ExecutionStatus.AddMessage("One or more Ancillary study comment is not set or is pending.", true);
                            break;
                        }
                    }
				}
			}
		}

		private void IsPapCorrelationHandled()
		{
			if (this.m_IsSigning == true)
			{
				if (this.IsPapCorrelationRequired == true)
				{
					this.m_PanelSetOrderSurgical.PapCorrelationRequired = true;
					if (this.m_PanelSetOrderSurgical.PapCorrelation == 0)
					{
						this.m_ExecutionStatus.AddMessage("Pap correlation is not handled.", true);
					}
				}
				else
				{
					this.m_PanelSetOrderSurgical.PapCorrelationRequired = false;
					this.m_PanelSetOrderSurgical.PapCorrelation = 0;
				}
			}
		}

		private void CaseIsAssigned()
		{
			if (this.m_IsSigning == true)
			{
				if (this.m_PanelSetOrderSurgical.AssignedToId == 0)
				{
					this.m_ExecutionStatus.AddMessage("The case is not assigned to a pathologist.", true);
				}
			}
		}

		private void IsSpecialDermCaseViolation()
		{
            /*
			if (this.m_IsSigning == true)
			{
				string msg = string.Empty;
				switch (this.m_AccessionOrder.PhysicianId)
				{
					case 2835:// Dr West
						//If it's Dr Emmerick or Dr Gallagher					
						if (this.m_SystemIdentity.User.UserId != 5088 && this.m_SystemIdentity.User.UserId != 5087)
						{
							msg = "Dr. West has requested that only Dr. Emerick or Dr. Gallagher sign his cases.";
						}
						break;
					case 2747:// Dr Walton
						//If it's Dr Emmerick or Dr Gallagher					
						if (this.m_SystemIdentity.User.UserId != 5088 && this.m_SystemIdentity.User.UserId != 5087)
						{
							msg = "Dr. Walton has requested that only Dr. Emerick or Dr. Gallagher sign his cases.";
						}
						break;
					case 58:// Dr Hawk
						//If it's Dr Emmerick					
						if (this.m_SystemIdentity.User.UserId != 5088)
						{
							msg = "Dr Hawk has requested that only Dr. Emerick sign her cases.";
						}
						break;
					default:
						break;
				}

				if (msg.Length > 0)
				{
					System.Windows.MessageBoxResult dialogResult = System.Windows.MessageBox.Show(msg + "\r\n Are you sure you want to sign this case out?", "Sign Case?", System.Windows.MessageBoxButton.YesNo);
					if (dialogResult == System.Windows.MessageBoxResult.Yes)
					{
						this.m_ExecutionStatus.Halted = true;
					}
				}
			}
            */
		}

		private void IsIntraoperativeCorrelationHandled()
		{
			if (this.m_IsSigning == true)
			{
				foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in this.m_PanelSetOrderSurgical.SurgicalSpecimenCollection)
				{
					foreach (YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult in surgicalSpecimen.IntraoperativeConsultationResultCollection)
					{
						if (intraoperativeConsultationResult.Correlation == "Not Correlated")
						{
							this.m_ExecutionStatus.AddMessage("The case has an intraoperative consultation that is not correlated.", true);
							break;
						}
					}
				}
			}
		}

		private void CaseHasQuestionMarks()
		{
			if (this.m_IsSigning == true)
			{
				StringBuilder result = new StringBuilder();
				result.Append("There are ??? in ");
				if (!string.IsNullOrEmpty(this.m_PanelSetOrderSurgical.GrossX) && this.m_PanelSetOrderSurgical.GrossX.Contains("???") == true) result.Append("Gross Description, ");
				if (!string.IsNullOrEmpty(this.m_PanelSetOrderSurgical.MicroscopicX) && this.m_PanelSetOrderSurgical.MicroscopicX.Contains("???") == true) result.Append("Microscopic Description, ");
				if (!string.IsNullOrEmpty(this.m_PanelSetOrderSurgical.Comment) && this.m_PanelSetOrderSurgical.Comment.Contains("???") == true) result.Append("Comment, ");

				if (!string.IsNullOrEmpty(this.m_AccessionOrder.ClinicalHistory) && this.m_AccessionOrder.ClinicalHistory.Contains("???") == true) result.Append("Clinical History, ");

				foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen specimen in this.m_PanelSetOrderSurgical.SurgicalSpecimenCollection)
				{
					if (!string.IsNullOrEmpty(specimen.Diagnosis) && specimen.Diagnosis.Contains("???") == true) result.Append("Diagnosis for " + specimen.DiagnosisId + ", ");
					if (!string.IsNullOrEmpty(specimen.SpecimenOrder.Description) && specimen.SpecimenOrder.Description.Contains("???") == true) result.Append("Description for " + specimen.DiagnosisId + ", ");

					foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResultItem in specimen.StainResultItemCollection)
					{
						if (!string.IsNullOrEmpty(stainResultItem.ControlComment) && stainResultItem.ControlComment.Contains("???") == true) result.Append(stainResultItem.ProcedureName + " control comment, ");
						if (!string.IsNullOrEmpty(stainResultItem.ReportComment) && stainResultItem.ReportComment.Contains("???") == true) result.Append(stainResultItem.ProcedureName + " report comment, ");
						if (!string.IsNullOrEmpty(stainResultItem.Result) && stainResultItem.Result.Contains("???") == true) result.Append(stainResultItem.ProcedureName + " result, ");
					}
					foreach (YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultation in specimen.IntraoperativeConsultationResultCollection)
					{
						if (!string.IsNullOrEmpty(intraoperativeConsultation.Result) && intraoperativeConsultation.Result.Contains("???") == true) result.Append("intraoperative consultation result, ");
					}

                    foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in this.m_PanelSetOrderSurgical.AmendmentCollection)
					{
						if (!string.IsNullOrEmpty(amendment.Text) && amendment.Text.Contains("???") == true) result.Append("amendment, ");
					}
				}

				if (result.Length > 17)
				{
					string msg = result.ToString().Substring(0, result.Length - 2);
					this.m_ExecutionStatus.AddMessage(msg, true);
				}
			}
		}

		private void CurrentUserIsTheAssignedUser()
		{
			if (this.m_IsSigning == true)
			{
				if (this.m_SystemIdentity.User.UserId != this.m_PanelSetOrderSurgical.AssignedToId)
				{
					this.m_ExecutionStatus.AddMessage("The case is assigned to another pathologist.", true);
				}
			}
		}

		private void CaseHasSvhAccount()
		{
			if (this.m_IsSigning == true)
			{
				if (this.CaseIsSVH == true)
				{
					if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhAccount) == true)
					{
						this.m_ExecutionStatus.AddMessage("The case is an SVH case but has no SVH account.", true);
					}
				}
			}
		}

		private void CaseHasSvhMRN()
		{
			if (this.m_IsSigning == true)
			{
				if (this.CaseIsSVH == true)
				{
					if (string.IsNullOrEmpty(this.m_AccessionOrder.SvhMedicalRecord) == true)
					{
						this.m_ExecutionStatus.AddMessage("The case is an SVH case but has no MRN.", true);
					}
				}
			}
		}

        private void CaseHasClientNotFound()
        {
            if (this.m_IsSigning == true)
            {
                if (this.m_AccessionOrder.ClientId == 1007)
                {                    
                    this.m_ExecutionStatus.AddMessage("The client for this case is not set.", true);                    
                }
            }
        }

        private void CaseHasPhysicianNotFound()
        {
            if (this.m_IsSigning == true)
            {
                if (this.m_AccessionOrder.PhysicianId == 2371)
                {
                    this.m_ExecutionStatus.AddMessage("The physician for this case is not set.", true);
                }
            }
        }

        private void CaseHasUnfinaledPeerReview()
        {
            if (this.m_IsSigning == true)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
                {
                    YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
                    if (surgicalTestOrder.HoldForPeerReview == true)
                    {
                        if (this.m_AccessionOrder.PanelSetOrderCollection.HasUnfinaledPeerReview() == true)
                        {
                            this.m_ExecutionStatus.AddMessage("There is one or more unfinaled prospective peer reviews.", true);
                        }
                    }                    
                }
            }
        }

		private void GradedStainsAreHandled()
		{
			YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection = this.m_PanelSetOrderSurgical.GetTestOrders();
			YellowstonePathology.Business.SpecialStain.StainResultItemCollection allStaints = this.m_PanelSetOrderSurgical.GetAllStains();
			YellowstonePathology.Business.SpecialStain.StainResultItemCollection gradedStains = allStaints.GetGradedStains(testOrderCollection);

			foreach (YellowstonePathology.Business.SpecialStain.StainResultItem stainResult in gradedStains)
			{
				if (stainResult.IsResultPositive() == true)
				{
					if (stainResult.ReportCommentContainsNumber() == false)
					{
						this.m_ExecutionStatus.AddMessage("Graded stain " + stainResult.ProcedureName + " is not graded.", true);
						break;
					}
				}
			}
		}

		private void SignCase()
		{
			if (this.m_IsSigning == true)
			{
				this.m_PanelSetOrderSurgical.Finish(this.m_AccessionOrder);                				
				if (this.m_PanelSetOrderSurgical.Accepted == false)
				{
					this.m_PanelSetOrderSurgical.Accept();
				}
			}
		}

		private void UnSignCase()
		{
			if (this.m_IsSigning == false)
			{
				if (this.CaseIsDistributed == false)
				{
					this.m_PanelSetOrderSurgical.Unfinalize();
				}
			}
		}

		private void DeleteUndistributedLogItems()
		{
			if (this.m_IsSigning == false)
			{
                this.m_PanelSetOrderSurgical.TestOrderReportDistributionCollection.UnscheduleDistributions();
			}
		}

		private bool CaseIsDistributed
		{
            get { return this.m_PanelSetOrderSurgical.TestOrderReportDistributionCollection.HasDistributedItems(); }
		}

		private bool IsPapCorrelationRequired
		{
			get
			{
				bool result = this.m_AccessionOrder.SpecimenOrderCollection.FindWordsInDescription(this.m_PapCorrelationWordList);
				return result;
			}
		}

		private bool CaseIsSVH
		{
			get
			{
				bool result = false;
				if (this.m_AccessionOrder.ClientId == 558) result = true;
				return result;
			}
		}

		public void Execute(Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder panelSetOrderSurgical, Business.User.SystemIdentity systemIdentity, ExecutionStatus executionStatus)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderSurgical = panelSetOrderSurgical;
			this.m_SystemIdentity = systemIdentity;
			this.m_ExecutionStatus = executionStatus;
			this.m_Rule.Execute(this.m_ExecutionStatus);
		}
	}
}
