﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Flow
{
	public enum FlowLogListSearchTypeEnum
	{
		GetByLeukemiaNotFinal,
		GetByCommonTestAccessionMonth,
		GetByReportNo,
		GetByAccessionMonth,
		GetByPatientName
	}
}
