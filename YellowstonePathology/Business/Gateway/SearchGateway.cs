using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class SearchGateway
	{		
		public YellowstonePathology.Business.Search.PathologistSearchResultCollection PathologistGenericSearch(string caseType, int pathologistId, bool final, string finalDateLimit)
		{
			MySqlCommand cmd = new MySqlCommand("pPathologistGenericSearch_3");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("CaseType", caseType);
			cmd.Parameters.AddWithValue("PathologistId", pathologistId);
			cmd.Parameters.AddWithValue("Final", final);
			cmd.Parameters.AddWithValue("FinalDateLimit", finalDateLimit);
			return this.BuildResultList(cmd);
		}

        public YellowstonePathology.Business.Search.PathologistSearchResultCollection PathologistNameSearch(string pLastName, string pFirstName)
		{
			MySqlCommand cmd = new MySqlCommand("pPathologistNameSearch_3");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("PLastName", pLastName);
			cmd.Parameters.AddWithValue("PFirstName", pFirstName);
			if (string.IsNullOrEmpty(pFirstName) == true)
			{
				cmd.Parameters["PFirstName"].Value = DBNull.Value;
			}
			return this.BuildResultList(cmd);
		}

        public YellowstonePathology.Business.Search.PathologistSearchResultCollection PathologistPatientIdSearch(string patientId)
		{
			MySqlCommand cmd = new MySqlCommand("pPathologistPatientIdSearch_3");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("PatientId",  patientId);
			return this.BuildResultList(cmd);
		}

        public YellowstonePathology.Business.Search.PathologistSearchResultCollection PathologistSlideOrderIdSearch(string slideOrderId)
		{
            MySqlCommand cmd = new MySqlCommand("pPathologistHistologySlideSearch_3");
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("SlideOrderId", slideOrderId);
			return this.BuildResultList(cmd);
		}

        public YellowstonePathology.Business.Search.PathologistSearchResult PathologistAliquotOrderIdSearch(string aliquotOrderId, int panelSetIdHint)
        {

            MySqlCommand cmd = new MySqlCommand("pPathologistAliquotOrderIdSearch_5");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("AliquotOrderId", aliquotOrderId);

            List<YellowstonePathology.Business.Search.PathologistSearchResult> resultList = new List<Search.PathologistSearchResult>();

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Search.PathologistSearchResult result = new Search.PathologistSearchResult();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderProperyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderProperyWriter.WriteProperties();
                        resultList.Add(result);        
                    }
                }
            }

            if(resultList.Count == 0)
            {
                return null;
            }
            else if(resultList.Count == 1)
            {
                return resultList[0];
            }
            else
            {
                foreach (YellowstonePathology.Business.Search.PathologistSearchResult item in resultList)
                {
                    if (item.PanelSetId == panelSetIdHint) return item;
                }
                return resultList[0];
            }                       
        }

        public YellowstonePathology.Business.Search.PathologistSearchResultCollection GetPathologistSearchListByReportNo(string reportNo)
		{
			MySqlCommand cmd = new MySqlCommand("pPathologistReportNoSearch_3");
			cmd.Parameters.AddWithValue("ReportNo", reportNo);
			cmd.CommandType = CommandType.StoredProcedure;

			return this.BuildResultList(cmd);
		}

        public YellowstonePathology.Business.Search.PathologistSearchResultCollection GetPathologistSearchListByMasterAccessionNoNo(string masterAccessionNo)
        {
            MySqlCommand cmd = new MySqlCommand("pPathologistMasterAccessionNoSearch_3");
            cmd.Parameters.AddWithValue("MasterAccessionNo", masterAccessionNo);
            cmd.CommandType = CommandType.StoredProcedure;

            return this.BuildResultList(cmd);
        }

		private YellowstonePathology.Business.Search.PathologistSearchResultCollection BuildResultList(MySqlCommand cmd)
		{
            YellowstonePathology.Business.Search.PathologistSearchResultCollection result = new Search.PathologistSearchResultCollection();

			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Search.PathologistSearchResult pathologistSearchResult = new Search.PathologistSearchResult();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderProperyWriter = new Persistence.SqlDataReaderPropertyWriter(pathologistSearchResult, dr);
                        sqlDataReaderProperyWriter.WriteProperties();
						result.Add(pathologistSearchResult);
					}
				}
			}
			return result;
		}
	}
}
