using System;
using System.Collections.Generic;

namespace YellowstonePathology.Business.Test
{
	public class SearchEngine
	{
		private List<object> m_Parameters;
		private YellowstonePathology.Business.Search.ReportSearchFillEnum m_SearchFillEnum;
		private YellowstonePathology.Business.Search.ReportSearchList m_ReportSearchList;

        private YellowstonePathology.Business.AutomatedOrderList m_AutomatedOrderList;

		public SearchEngine()
		{
			m_Parameters = new List<object>();
			m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.None;
			this.m_ReportSearchList = new YellowstonePathology.Business.Search.ReportSearchList();
            this.m_AutomatedOrderList = new YellowstonePathology.Business.AutomatedOrderList();
		}

		public YellowstonePathology.Business.Search.ReportSearchList ReportSearchList
		{
			get { return this.m_ReportSearchList; }
		}

		public void FillSearchList()
		{
			switch (this.m_SearchFillEnum)
			{
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByDateRange:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByDateRange(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByReportNo:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByReportNo(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByNotDistributed:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByNotDistributed(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByNotFinalPanelId:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByNotFinalPanelId(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByInHouseMolecularPending:
                    this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByInHouseMolecularPending();
                    break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByPatientName:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByPatientName(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByMasterAccessionNo:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByMasterAccessionNo(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByNotAudited:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByNotAudited(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByPatientId:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByPatientId(m_Parameters);
					break;
                case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByPanelSetId:
                    this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByPanelSetId(m_Parameters);
                    break;
                default:
					break;
			}
			if (this.m_ReportSearchList == null) this.m_ReportSearchList = new YellowstonePathology.Business.Search.ReportSearchList();
		}

        public void SetFillByThisMonth(int panelSetId)
		{
			DateTime startDate = DateTime.Today;
			startDate = startDate.AddDays(-startDate.Day + 1);
			DateTime endDate = startDate.AddMonths(1);
			endDate = endDate.AddDays(-1);
			this.m_Parameters.Clear();
			this.m_Parameters.Add(startDate);
			this.m_Parameters.Add(endDate);
			this.m_Parameters.Add(panelSetId);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByDateRange;
		}

		public void SetFillByLastMonth(int panelSetId)
		{
			DateTime startDate = DateTime.Parse(DateTime.Today.AddMonths(-1).Month.ToString() + "/1/" + DateTime.Today.Year.ToString());
			DateTime endDate = startDate.AddDays(DateTime.DaysInMonth(startDate.Year, startDate.Month) - 1);
			this.m_Parameters.Clear();
			this.m_Parameters.Add(startDate);
			this.m_Parameters.Add(endDate);
			this.m_Parameters.Add(panelSetId);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByDateRange;
		}

		public void SetFillByToday(int panelSetId)
		{
			DateTime startDate = DateTime.Today;
			DateTime endDate = DateTime.Today;
			this.m_Parameters.Clear();
			this.m_Parameters.Add(startDate);
			this.m_Parameters.Add(endDate);
			this.m_Parameters.Add(panelSetId);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByDateRange;
		}

		public void SetFillByYesterday(int panelSetId)
		{
			DateTime startDate = DateTime.Today.AddDays(-1);
			DateTime endDate = DateTime.Today.AddDays(-1);
			this.m_Parameters.Clear();
			this.m_Parameters.Add(startDate);
			this.m_Parameters.Add(endDate);
			this.m_Parameters.Add(panelSetId);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByDateRange;
		}

		public void SetFillByReportNo(string reportNo)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(reportNo);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByReportNo;
		}

		public void SetFillByNotDistributed(int panelId)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(panelId);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByNotDistributed;
		}

		public void SetFillByNotFinal(int panelId)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(panelId);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByNotFinalPanelId;
		}

        public void SetFillByInHouseMolecularPending()
        {
            this.m_Parameters.Clear();
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByInHouseMolecularPending;
        }

		public void SetFillByPatientName(YellowstonePathology.Business.PatientName patientName)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(patientName.LastName);
			this.m_Parameters.Add(patientName.FirstName);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByPatientName;
		}

		public void SetFillByMasterAccessionNo(string masterAccessionNo)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(masterAccessionNo);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByMasterAccessionNo;
		}

		public void SetFillByNotAudited(string caseType)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(caseType);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByNotAudited;
		}

        public void SetFillByPanelSetId(int panelSetId)
        {
            this.m_Parameters.Clear();
            this.m_Parameters.Add(panelSetId);
            this.m_Parameters.Add(DateTime.Today.AddMonths(-3));
            this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByPanelSetId;
        }

        public void SetFillByPatientId(string patientId)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(patientId);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByPatientId;
		}

        public YellowstonePathology.Business.AutomatedOrderList AutomatedOrderList
		{
			get { return this.m_AutomatedOrderList; }
		}		
	}
}
