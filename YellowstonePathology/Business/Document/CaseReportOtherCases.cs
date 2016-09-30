using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;

namespace YellowstonePathology.Business.Document
{
	public class CaseReportOtherCases
	{
		YellowstonePathology.Business.Rules.Rule m_Rule;
		YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
        YellowstonePathology.Business.Patient.Model.PatientHistoryList m_PatientHistoryList;
		Paragraph m_OtherCases;
		string m_ReportNo;

		public CaseReportOtherCases()
		{
			m_Rule = new YellowstonePathology.Business.Rules.Rule();

            m_Rule.ActionList.Add(this.MakePatientHistoryList);
            m_Rule.ActionList.Add(this.SetOtherCases);
		}

        private void MakePatientHistoryList()
        {
            this.m_PatientHistoryList = new Patient.Model.PatientHistoryList();
            m_PatientHistoryList.SetFillCommandByAccessionNo(this.m_ReportNo);
            m_PatientHistoryList.Fill();
        }

		private void SetOtherCases()
		{
			if (this.m_PatientHistoryList.Count > 1)
			{
				StringBuilder caseHistory = new StringBuilder();
                foreach (Business.Patient.Model.PatientHistoryListItem item in this.m_PatientHistoryList)
				{
					if (item.ReportNo != this.m_ReportNo)
					{
						caseHistory.Append(item.ReportNo + ", ");
					}
				}
				if (caseHistory.Length > 2)
				{
					caseHistory.Remove(caseHistory.Length - 2, 2);
				}
				this.m_OtherCases.Inlines.Add(new Run(caseHistory.ToString()));
			}
		}

        public void SetData(Paragraph otherCases, string reportNo, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
			m_OtherCases = otherCases;
			m_ReportNo = reportNo;
			m_ExecutionStatus = executionStatus;
			this.m_Rule.Execute(m_ExecutionStatus);
		}
	}
}
