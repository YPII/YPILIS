using System;
using System.Data;
using System.Xml.Linq;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class FlowGateway
	{               
		public static Flow.FlowLogList GetByLeukemiaNotFinal()
		{
            MySqlCommand cmd = new MySqlCommand("select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, " +
                "pso.PanelSetName TestName, pso.ObjectId, pso.MasterAccessionNo " +
				"from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
				"where pso.PanelSetId = 20 and pso.Final = 0 order by ao.AccessionDate desc, pso.ReportNo desc;");
			cmd.CommandType = CommandType.Text;
			return BuildFlowLogList(cmd);
		}        

		public static Flow.FlowLogList GetFlowLogListByReportNo(string reportNo)
		{
			MySqlCommand cmd = new MySqlCommand("select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, " +
                "pso.PanelSetName TestName, pso.ObjectId, pso.MasterAccessionNo " +
				"from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
                "where pso.PanelSetId = 20 and pso.CaseType = 'Flow Cytometry' and pso.ReportNo = @ReportNo ");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@ReportNo", reportNo);
			return BuildFlowLogList(cmd);
		}

		public static Flow.FlowLogList GetByAccessionMonth(DateTime date)
		{
			MySqlCommand cmd = new MySqlCommand("Select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, " +
                "pso.PanelSetName TestName, pso.ObjectId, pso.MasterAccessionNo " +
                "from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
                "where pso.PanelSetId = 20 and pso.CaseType = 'Flow Cytometry' and Month(ao.AccessionDate) = @Month " +
                "and Year(ao.AccessionDate) = @Year " +
				"order by ao.AccessionDate desc, pso.ReportNo desc;");
			cmd.Parameters.AddWithValue("@Month", date.Month);
			cmd.Parameters.AddWithValue("@Year", date.Year);
			cmd.CommandType = CommandType.Text;
			return BuildFlowLogList(cmd);
		}

		public static Flow.FlowLogList GetByPatientName(string patientName)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandType = CommandType.Text;

			string whereClause = string.Empty;
			string sql = "select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, pso.PanelSetName TestName, " +
                "pso.ObjectId, pso.MasterAccessionNo " +
                "from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
                "Where pso.PanelSetId = 20 and pso.CaseType = 'Flow Cytometry' ";

			string[] commaSplit = patientName.Split(',');
			switch (commaSplit.Length)
			{
				case 1:
					cmd.Parameters.AddWithValue("@PLastName", commaSplit[0] + "%");
					whereClause = "and ao.PLastName like @PLastName ";
					break;
				case 2:
					cmd.Parameters.AddWithValue("@PLastName", commaSplit[0] + "%");
					cmd.Parameters.AddWithValue("@PFirstName", commaSplit[1].Trim() + "%");
					whereClause = "and ao.PLastName like @PLastName and ao.PFirstName like @PFirstName ";
					break;
			}

			cmd.CommandText = sql + whereClause + " order by ao.AccessionDate desc, pso.ReportNo desc;";
			cmd.CommandType = CommandType.Text;
			return BuildFlowLogList(cmd);
		}

		public static Flow.FlowMarkerCollection GetFlowMarkerCollectionByPanelId(string reportNo, int panelId)
		{
            Flow.FlowMarkerCollection result = new Flow.FlowMarkerCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select @ReportNo as ReportNo, mp.MarkerName as Name, mp.Intensity, mp.Interpretation, 'true' as MarkerUsed " +
                "from tblFlowMarkerPanel mp left outer join tblMarkers m on mp.MarkerName = m.MarkerName where PanelId = @PanelId " +
                "order by m.OrderFlag, m.MarkerName;";
            cmd.Parameters.AddWithValue("@ReportNo", reportNo);
            cmd.Parameters.AddWithValue("@PanelId", panelId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Flow.FlowMarkerItem flowMarkerItem = new Flow.FlowMarkerItem();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(flowMarkerItem, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(flowMarkerItem);
                    }
                }
            }
            return result;
        }

        private static Flow.FlowLogList BuildFlowLogList(MySqlCommand cmd)
		{
			Flow.FlowLogList result = new Flow.FlowLogList();
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						Flow.FlowLogListItem flowLogListItem = new Flow.FlowLogListItem();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(flowLogListItem, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
						result.Add(flowLogListItem);
					}
				}
			}
			return result;
		}

        public static int PreviousFlowCasesAbnormalCLL(string masterAccessionNo, string patientId)
        {
            int result = 0;
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT count(*) " +
                "FROM tblAccessionOrder ao JOIN tblPanelSetOrder pso ON ao.MasterAccessionNo = pso.MasterAccessionNo " +
                "join tblFlowLeukemia f on pso.ReportNo = f.ReportNo " +
                "WHERE ao.MasterAccessionno <> @MasterAccessionNo and ao.PatientId = @PatientId and f.TestResult = 'Abnormal' and " +
                "instr(f.Impression, 'chronic lymphocytic leukemia') > 0 order by 1;";
            cmd.Parameters.AddWithValue("@MasterAccessionNo", masterAccessionNo);
            cmd.Parameters.AddWithValue("@PatientId", patientId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result = Convert.ToInt32(dr[0].ToString());
                    }
                }
            }
            return result;
        }


    }
}
