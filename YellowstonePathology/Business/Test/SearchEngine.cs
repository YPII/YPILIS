using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Data;

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
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByAccessionDate:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByAccessionDate(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByDateRangeBatchLocation:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByDateRangeBatchLocation(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByDateRange:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByDateRange(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByTodayLocation:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByDateRangeLocation(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByReportNo:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByReportNo(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByNotDistributed:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByNotDistributed(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByNotFinalLoacation:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByNotFinalLocation(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByNotFinalPanelId:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByNotFinalPanelId(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByInHouseMolecularPending:
                    this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByInHouseMolecularPending();
                    break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByUnBatchedBatchTypeId:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByUnBatchedBatchTypeId(m_Parameters);
					break;
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByBatchId:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByBatchId(m_Parameters);
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
				case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByNotVerified:
					this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByNotVerified(m_Parameters);
					break;
                case YellowstonePathology.Business.Search.ReportSearchFillEnum.ByPanelSetId:
                    this.m_ReportSearchList = Gateway.ReportSearchGateway.GetReportSearchListByPanelSetId(m_Parameters);
                    break;
                default:
					break;
			}
			if (this.m_ReportSearchList == null) this.m_ReportSearchList = new YellowstonePathology.Business.Search.ReportSearchList();
		}

		public void SetFillByAccessionDate(DateTime accessionDate, int batchTypeId, string facilityId)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(accessionDate);
			this.m_Parameters.Add(batchTypeId);
			this.m_Parameters.Add(facilityId);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByAccessionDate;
		}

        public void SetFillByAccessionDate(DateTime accessionDate, string originatingLocation)
        {
            this.m_Parameters.Clear();
            this.m_Parameters.Add(accessionDate);            
            this.m_Parameters.Add(originatingLocation);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByAccessionDate;
        }        

		public void SetFillByAccessionDateRange(DateTime startDate, DateTime endDate, int batchTypeId, string originatingLocation)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(startDate);
			this.m_Parameters.Add(endDate);
			this.m_Parameters.Add(batchTypeId);
			this.m_Parameters.Add(originatingLocation);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByDateRangeBatchLocation;
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

		public void SetFillByToday(int panelSetId, string originatingLocation)
		{
			DateTime startDate = DateTime.Today;
			DateTime endDate = DateTime.Today;
			this.m_Parameters.Clear();
			this.m_Parameters.Add(startDate);
			this.m_Parameters.Add(endDate);
			this.m_Parameters.Add(panelSetId);
			this.m_Parameters.Add(originatingLocation);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByDateRangeLocation;
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

		public void SetFillByNotFinal(string originatingLocation)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(originatingLocation);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByNotFinalLoacation;
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

		public void SetFillByUnBatchedBatchTypeId(int batchTypeId)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(batchTypeId);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByUnBatchedBatchTypeId;
		}

		public void SetFillByBatchId(int panelOrderBatchId)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(panelOrderBatchId);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByBatchId;
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

		public void SetFillByNotVerified(int panelSetId)
		{
			this.m_Parameters.Clear();
			this.m_Parameters.Add(panelSetId);
			this.m_SearchFillEnum = YellowstonePathology.Business.Search.ReportSearchFillEnum.ByNotVerified;
		}

        public YellowstonePathology.Business.AutomatedOrderList AutomatedOrderList
		{
			get { return this.m_AutomatedOrderList; }
		}		
	}
}
