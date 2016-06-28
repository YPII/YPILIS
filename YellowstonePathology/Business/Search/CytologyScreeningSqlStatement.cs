using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Search
{
    public class CytologyScreeningSqlStatement : SqlSearchStatement
    {
        public CytologyScreeningSqlStatement()
        {			
			this.m_SelectClause = "Select ao.MasterAccessionNo, " +
				"pso.ReportNo, " +
				"ao.PFirstName + ' ' + ao.PLastName [PatientName], " + 
				"ao.AccessionTime, " +
				"cpo.ScreeningType, " +
				"su.DisplayName [OrderedByName], " +
				"cpo.ScreenedByName, " +
				"su1.DisplayName [AssignedToName], " +
				"po.AcceptedTime [ScreeningFinalTime], " +
				"pso.FinalTime [CaseFinalTime] ";

			this.m_FromClause = "from tblAccessionOrder ao join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
				"join tblPanelOrder po on pso.ReportNo = po.ReportNo " +
				"join tblPanelOrderCytology cpo on po.PanelOrderId = cpo.PanelORderId " +
				"left outer join tblSystemUser su on po.OrderedById = su.UserId " +
				"left outer join tblSystemUser su1 on po.AssignedToId = su1.UserId";

			this.m_OrderByClause = "Order By MasterAccessionNo desc";

            this.SearchFields.Add(new Search.HasCytologyResultField());
        }
    }
}
