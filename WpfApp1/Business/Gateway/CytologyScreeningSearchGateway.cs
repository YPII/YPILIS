using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class CytologyScreeningSearchGateway
	{        
		public static List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> GetCytologyScreeningSearchResults(string sqlStatement)
		{
			List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> result = new List<Search.CytologyScreeningSearchResult>();
			MySqlCommand cmd = new MySqlCommand(sqlStatement);

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Search.CytologyScreeningSearchResult cytologyScreeningSearchResult = new Search.CytologyScreeningSearchResult();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(cytologyScreeningSearchResult, dr);
                        propertyWriter.WriteProperties();
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
                "concat(ao.PFirstName, ' ', ao.PLastName) PatientName, " +
                "ao.AccessionTime, " +
                "cpo.ScreeningType, " +
                "su.DisplayName OrderedByName, " +
                "cpo.ScreenedByName, " +
                "su1.DisplayName AssignedToName, " +
                "po.AcceptedTime ScreeningFinalTime, " +
                "pso.FinalTime CaseFinalTime, " +
                "cpo.Reconciled " +
                "from tblAccessionOrder ao " +
                "join tblPanelSetOrder pso on ao.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblPanelOrder po on pso.ReportNo = po.ReportNo " +
                "join tblPanelOrderCytology cpo on po.PanelOrderId = cpo.PanelORderId " +
                "left outer join tblSystemUser su on po.OrderedById = su.UserId " +
                "left outer join tblSystemUser su1 on po.AssignedToId = su1.UserId " +
                "where ao.AccessionDate >= date_add(curdate(), Interval -30 Day) " +
                    "and po.AssignedToId = @AssignedToId " +
                    "and pso.ResultCode <> po.ResultCode;";

            MySqlCommand cmd = new MySqlCommand(sqlStatement);
            cmd.Parameters.AddWithValue("@AssignedToId", assignedToId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Search.CytologyScreeningSearchResult cytologyScreeningSearchResult = new Search.CytologyScreeningSearchResult();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Persistence.SqlDataReaderPropertyWriter(cytologyScreeningSearchResult, dr);
                        propertyWriter.WriteProperties();
                        result.Add(cytologyScreeningSearchResult);
                    }
                }
            }
            return result;
        }
    }
}
