using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Patient.Model
{
    public class PatientHistoryList : ObservableCollection<PatientHistoryListItem>
    {
		private SqlCommand m_SqlCommand;
		Document.CaseDocumentCollection m_CaseDocumentCollection;

		public PatientHistoryList()
		{
			this.m_SqlCommand = new SqlCommand();
		}

		public Document.CaseDocumentCollection CaseDocumentCollection
		{
			get { return this.m_CaseDocumentCollection; }
		}

		public void SetCaseDocumentCollection(string reportNo)
		{
			this.m_CaseDocumentCollection = new Document.CaseDocumentCollection(reportNo);
		}

		public void SetFillCommandByAccessionNo(string reportNo)
		{
			this.m_SqlCommand.Parameters.Clear();
			this.m_SqlCommand.CommandType = CommandType.StoredProcedure;
			this.m_SqlCommand.CommandText = "pGetCaseHistoryFromReportNo";
			this.m_SqlCommand.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
		}

		public void Fill()
		{
			this.Clear();
			using (SqlConnection cn = new SqlConnection(BaseData.SqlConnectionString))
			{
				cn.Open();
				this.m_SqlCommand.Connection = cn;
				using (SqlDataReader dr = this.m_SqlCommand.ExecuteReader())
				{
					while (dr.Read())
					{
						PatientHistoryListItem item = new PatientHistoryListItem();
						item.Fill(dr);
						this.Add(item);
					}
				}
			}
		}

		public string ToReportString(string excludeReportNo)
		{
			string result = string.Empty;
			bool hashistory = false;
			if (this.Count > 1)
			{
                foreach (Business.Patient.Model.PatientHistoryListItem item in this)
				{
					if (item.ReportNo != excludeReportNo)
					{
						result += item.ReportNo + ", ";
					}
				}
				if (result.Length > 2)
				{
					hashistory = true;
					result = result.Substring(0, result.Length - 2);
				}
			}

			if (!hashistory)
			{
				result = "None.";
			}
			return result;
		}
	}

	public class PatientHistoryListItem
	{
		string m_ReportNo;
		Nullable<DateTime> m_AccessionDate;

		public PatientHistoryListItem()
		{

		}

		public string ReportNo
		{
			get { return this.m_ReportNo; }
		}

		public Nullable<DateTime> AccessionDate
		{
			get { return this.m_AccessionDate; }
		}

		public void Fill(SqlDataReader dr)
		{
			this.m_ReportNo = BaseData.GetStringValue("ReportNo", dr);
			this.m_AccessionDate = BaseData.GetDateTimeValue("AccessionDate", dr);
		}
	}
}
