using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHNegativeResult : PNHResult
	{		
		public PNHNegativeResult()
		{
			this.m_Result = "No phenotypic evidence of paroxysmal nocturnal hemoglobinuria.";
            this.m_Comment = "Flow cytometric analysis does not identify any evidence of a PNH clone, based on analysis of several different GPI-linked antibodies on 3 separate cell populations (red blood cells, monocytes and granulocytes).  These findings do not support the diagnosis of PNH.";

            this.m_ResultCode = "PNHNGTV";
		}
	}
}
