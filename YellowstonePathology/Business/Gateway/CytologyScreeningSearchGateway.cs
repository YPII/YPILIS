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
	}
}
