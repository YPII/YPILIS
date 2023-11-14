using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
	public enum ReportSearchFillEnum
	{
		None,
		ByAccessionDate,
		ByDateRangeBatchLocation,
		ByDateRangeLocation,
		ByDateRange,
		ByTodayLocation,
		ByReportNo,
		ByNotDistributed,
		ByNotFinalLoacation,
		ByNotFinalPanelId,
		ByUnBatchedBatchTypeId,
		ByBatchId,
		ByPatientName,
		ByMasterAccessionNo,
		ByNotAudited,
		ByPatientId,        
		ByNotVerified,
        ByInHouseMolecularPending,
        ByPanelSetId
	}
}
