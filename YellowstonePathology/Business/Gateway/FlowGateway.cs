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
            MySqlCommand cmd = new MySqlCommand("select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, pso.PanelSetName [TestName], pso.ObjectId, pso.MasterAccessionNo " +
				"from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
				"where pso.PanelSetId = 20 and pso.Final = 0 order by ao.AccessionDate desc, pso.ReportNo desc");
			cmd.CommandType = CommandType.Text;
			return BuildFlowLogList(cmd);
		}        

		public static Flow.FlowLogList GetByTestType(int panelSetId)
		{
			MySqlCommand cmd = new MySqlCommand("select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, pso.PanelSetName [TestName], pso.ObjectId, pso.MasterAccessionNo " +
                "from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
				"Where PanelSetId = @PanelSetId order by ao.AccessionDate desc, pso.ReportNo desc");
			cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@PanelSetId", SqlDbType.Int).Value = panelSetId;
			return BuildFlowLogList(cmd);
		}

		public static Flow.FlowLogList GetFlowLogListByReportNo(string reportNo)
		{
			MySqlCommand cmd = new MySqlCommand("select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, pso.PanelSetName [TestName], pso.ObjectId, pso.MasterAccessionNo " +
				"from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
                "where pso.PanelSetId not in (19,143,211,222,223) and pso.CaseType = 'Flow Cytometry' and pso.ReportNo = @ReportNo ");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
			return BuildFlowLogList(cmd);
		}

		public static Flow.FlowLogList GetByAccessionMonth(DateTime date)
		{
			MySqlCommand cmd = new MySqlCommand("Select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, pso.PanelSetName [TestName], pso.ObjectId, pso.MasterAccessionNo " +
                "from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
				"where pso.PanelSetId not in (19,143,211,222,223) and pso.CaseType = 'Flow Cytometry' and month(ao.AccessionDate) = @Month and Year(ao.AccessionDate) = @Year " +
				"order by ao.AccessionDate desc, pso.ReportNo desc");
			cmd.Parameters.Add("@Month", SqlDbType.Int).Value = date.Month;
			cmd.Parameters.Add("@Year", SqlDbType.Int).Value = date.Year;
			cmd.CommandType = CommandType.Text;
			return BuildFlowLogList(cmd);
		}

		public static Flow.FlowLogList GetByPatientName(string patientName)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandType = CommandType.Text;

			string whereClause = string.Empty;
			string sql = "select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, pso.PanelSetName [TestName], pso.ObjectId, pso.MasterAccessionNo " +
                "from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
                "Where pso.PanelSetId not in (19,143,211,222,223) and pso.CaseType = 'Flow Cytometry' ";

			string[] commaSplit = patientName.Split(',');
			switch (commaSplit.Length)
			{
				case 1:
					cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value=commaSplit[0] + "%";
					whereClause = "and ao.PLastName like @PLastName ";
					break;
				case 2:
					cmd.Parameters.Add("@PLastName", SqlDbType.VarChar).Value = commaSplit[0] + "%";
					cmd.Parameters.Add("@PFirstName", SqlDbType.VarChar).Value = commaSplit[1].Trim() + "%";
					whereClause = "and ao.PLastName like @PLastName and ao.PFirstName like @PFirstName ";
					break;
			}

			cmd.CommandText = sql + whereClause + " order by ao.AccessionDate desc, pso.ReportNo desc";
			cmd.CommandType = CommandType.Text;
			return BuildFlowLogList(cmd);
		}

		public static Flow.FlowLogList GetByPathologistId(int pathologistId)
		{
			MySqlCommand cmd = new MySqlCommand("select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, pso.PanelSetName [TestName], pso.ObjectId, pso.MasterAccessionNo " +
                "from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
                "where dateadd(yy, +1, ao.AccessionDate) > getDate() and pso.AssignedToId = @PathologistId " +
				"order by ao.AccessionDate desc, pso.ReportNo desc");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PathologistId", SqlDbType.Int).Value = pathologistId;
			return BuildFlowLogList(cmd);
		}

		public static Flow.FlowMarkerCollection GetFlowMarkerCollectionByPanelId(string reportNo, int panelId)
		{
            Flow.FlowMarkerCollection result = new Flow.FlowMarkerCollection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select @ReportNo as ReportNo, mp.MarkerName as Name, mp.Intensity, mp.Interpretation, 'true' as MarkerUsed " +
                "from tblFlowMarkerPanel mp left outer join tblMarkers m on mp.MarkerName = m.MarkerName where PanelId = @PanelId " +
                "order by m.OrderFlag, m.MarkerName";
            cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
            cmd.Parameters.Add("@PanelId", SqlDbType.Int).Value = panelId;

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

		/*private static Flow.FlowMarkerCollection BuildFlowMarkerCollection(XElement sourceElement)
		{
			Flow.FlowMarkerCollection flowMarkerCollection = new Flow.FlowMarkerCollection();
			if (sourceElement != null)
			{
				foreach (XElement flowMarkerItemElement in sourceElement.Elements("FlowMarkerItem"))
				{
					Flow.FlowMarkerItem flowMarkerItem = BuildFlowMarkerItem(flowMarkerItemElement);
					flowMarkerCollection.Add(flowMarkerItem);
				}
			}
			return flowMarkerCollection;
		}*/

		private static Flow.FlowMarkerItem BuildFlowMarkerItem(XElement sourceElement)
		{
			Flow.FlowMarkerItem flowMarkerItem = new Flow.FlowMarkerItem();
			if (sourceElement != null)
			{
				YellowstonePathology.Business.Persistence.XmlPropertyWriter xmlPropertyWriter = new YellowstonePathology.Business.Persistence.XmlPropertyWriter(sourceElement, flowMarkerItem);
				xmlPropertyWriter.Write();
			}
			return flowMarkerItem;
		}
	}
}
