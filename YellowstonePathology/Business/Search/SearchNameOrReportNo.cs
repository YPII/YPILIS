using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
	public class SearchNameOrReportNo
	{
		YellowstonePathology.Business.Rules.Rule m_Rule;
		YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

		string m_SourceValue;
		string m_FirstName;
		string m_LastName;
		string m_ReportNo;
		bool m_IsReportNo;
		bool m_IsValid;

		public SearchNameOrReportNo()
		{
			this.m_ExecutionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();

			this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
			this.m_Rule.ActionList.Add(this.HasContent);
			this.m_Rule.ActionList.Add(this.GetReportNo);
			this.m_Rule.ActionList.Add(this.GetName);
		}

		private void HasContent()
		{
			this.m_SourceValue = this.m_SourceValue.Trim();
			if (this.m_SourceValue.Length == 1 && !Char.IsPunctuation(this.m_SourceValue[0]))
			{
				this.m_IsValid = true;
			}
			else if(this.m_SourceValue.Length > 1)
			{
				this.m_IsValid = true;
			}
		}

		private void GetReportNo()
		{
			if (this.m_IsValid)
			{
				YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_SourceValue);
				if (orderIdParser.ReportNo != null)
				{
					this.m_IsValid = true;
					this.m_ReportNo = this.m_SourceValue;
					this.m_IsReportNo = true;
				}
				else if (orderIdParser.MasterAccessionNo != null)
				{
					this.m_IsValid = true;
					this.m_ReportNo = orderIdParser.SurgicalReportNoFromMasterAccessionNo;
					this.m_IsReportNo = true;
				}
				else if (this.m_SourceValue.Length > 0 && this.CheckDigits(0))
				{
					this.m_IsValid = true;
					this.m_ReportNo = YellowstonePathology.Business.OrderIdParser.SurgicalReportNoFromNumber(this.m_SourceValue);
					this.m_IsReportNo = true;
				}
			}
		}

		private void GetName()
		{
			if (this.m_IsValid && !this.m_IsReportNo)
			{

				string[] names = this.m_SourceValue.Split(new char[] { ',' });
				this.m_LastName = names[0].Trim();
				if (names.Length > 1)
				{
					this.m_FirstName = names[1].Trim();
				}
			}
		}

		private bool CheckDigits(int startPos)
		{
			for (int idx = startPos; idx < this.m_SourceValue.Length; idx++)
			{
				if(!Char.IsDigit(this.m_SourceValue[idx]))
				{
					return false;
				}
			}
			return true;
		}

		public string FirstName
		{
			get { return this.m_FirstName; }
		}

		public string LastName
		{
			get { return this.m_LastName; }
		}

		public string ReportNo
		{
			get { return this.m_ReportNo; }
		}

		public bool IsReportNo
		{
			get { return this.m_IsReportNo; }
		}

		public bool IsValid
		{
			get { return this.m_IsValid; }
		}

		public void Execute(string sourceValue)
		{
			this.m_SourceValue = sourceValue;
			this.m_FirstName = string.Empty;
			this.m_LastName = string.Empty;
			this.m_ReportNo = string.Empty;
			this.m_IsReportNo = false;
			this.m_IsValid = false;

			this.m_Rule.Execute(m_ExecutionStatus);
		}
	}
}
