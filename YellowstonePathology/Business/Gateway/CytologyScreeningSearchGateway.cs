using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class CytologyScreeningSearchGateway
	{        
		public static List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> GetCytologyScreeningSearchResults(string sqlStatement)
		{
			List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> result = new List<Search.CytologyScreeningSearchResult>();
			SqlCommand cmd = new SqlCommand(sqlStatement);

			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Search.CytologyScreeningSearchResult cytologyScreeningSearchResult = new Search.CytologyScreeningSearchResult();
						YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Domain.Persistence.DataReaderPropertyWriter(dr);
						cytologyScreeningSearchResult.WriteProperties(propertyWriter);
						result.Add(cytologyScreeningSearchResult);
					}
				}
			}
			return result;
		}

        public static List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> GetCytologyScreeningSearchResultsByAtLoggerheads(int assignedToId)
        {
            List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> result = new List<Search.CytologyScreeningSearchResult>();
            string sqlStatement = "Select ao.MasterAccessionNo, " +
                "pso.ReportNo, " +
                "ao.PFirstName + ' ' + ao.PLastName [PatientName], " +
                "ao.AccessionTime, " +
                "cpo.ScreeningType, " +
                "su.DisplayName [OrderedByName], " +
                "cpo.ScreenedByName, " +
                "su1.DisplayName [AssignedToName], " +
                "po.AcceptedTime [ScreeningFinalTime], " +
                "pso.FinalTime [CaseFinalTime], " +
                "cpo.Reconciled " +
                "from tblAccessionOrder ao " +
                "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPanelOrder po on pso.ReportNo = po.ReportNo " +
                "join tblPanelOrderCytology cpo on po.PanelOrderId = cpo.PanelORderId " +
                "left outer join tblSystemUser su on po.OrderedById = su.UserId " +
                "left outer join tblSystemUser su1 on po.AssignedToId = su1.UserId " +
                "where ao.AccessionDate >= dateadd(dd, -30, getdate()) " +
                    "and po.AssignedToId = @AssignedToId " +
                    "and pso.ResultCode <> po.ResultCode ";

            SqlCommand cmd = new SqlCommand(sqlStatement);
            cmd.Parameters.Add("@AssignedToId", System.Data.SqlDbType.Int).Value = assignedToId;

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Search.CytologyScreeningSearchResult cytologyScreeningSearchResult = new Search.CytologyScreeningSearchResult();
                        YellowstonePathology.Business.Domain.Persistence.DataReaderPropertyWriter propertyWriter = new Domain.Persistence.DataReaderPropertyWriter(dr);
                        cytologyScreeningSearchResult.WriteProperties(propertyWriter);
                        result.Add(cytologyScreeningSearchResult);
                    }
                }
            }
            return result;
        }
    }
}
