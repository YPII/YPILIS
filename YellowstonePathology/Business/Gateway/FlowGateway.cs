using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Xml;

namespace YellowstonePathology.Business.Gateway
{
	public class FlowGateway
	{               
		public static Flow.FlowLogList GetByLeukemiaNotFinal()
		{
#if MONGO
            return FlowGatewayMongo.GetByLeukemiaNotFinal();
#else
            SqlCommand cmd = new SqlCommand("select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, pso.PanelSetName [TestName], pso.ObjectId, pso.MasterAccessionNo " +
				"from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
				"where pso.PanelSetId = 20 and pso.Final = 0 order by ao.AccessionDate desc, pso.ReportNo desc");
			cmd.CommandType = CommandType.Text;
			return BuildFlowLogList(cmd);
#endif
		}        

		public static Flow.FlowLogList GetByTestType(int panelSetId)
		{
#if MONGO
            return FlowGatewayMongo.GetByTestType(panelSetId);
#else
			SqlCommand cmd = new SqlCommand("select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, pso.PanelSetName [TestName], pso.ObjectId, pso.MasterAccessionNo " +
                "from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
				"Where PanelSetId = @PanelSetId order by ao.AccessionDate desc, pso.ReportNo desc");
			cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@PanelSetId", SqlDbType.Int).Value = panelSetId;
			return BuildFlowLogList(cmd);
#endif
		}

		public static Flow.FlowLogList GetFlowLogListByReportNo(string reportNo)
		{
#if MONGO
            return FlowGatewayMongo.GetFlowLogListByReportNo(reportNo);
#else
			SqlCommand cmd = new SqlCommand("select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, pso.PanelSetName [TestName], pso.ObjectId, pso.MasterAccessionNo " +
				"from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
                "where pso.PanelSetId not in (19,143,211,222,223) and pso.CaseType = 'Flow Cytometry' and pso.ReportNo = @ReportNo ");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
			return BuildFlowLogList(cmd);
#endif
		}

		public static Flow.FlowLogList GetByAccessionMonth(DateTime date)
		{
#if MONGO
            return FlowGatewayMongo.GetByAccessionMonth(date);
#else
			SqlCommand cmd = new SqlCommand("Select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, pso.PanelSetName [TestName], pso.ObjectId, pso.MasterAccessionNo " +
                "from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
				"where pso.PanelSetId not in (19,143,211,222,223) and pso.CaseType = 'Flow Cytometry' and month(ao.AccessionDate) = @Month and Year(ao.AccessionDate) = @Year " +
				"order by ao.AccessionDate desc, pso.ReportNo desc");
			cmd.Parameters.Add("@Month", SqlDbType.Int).Value = date.Month;
			cmd.Parameters.Add("@Year", SqlDbType.Int).Value = date.Year;
			cmd.CommandType = CommandType.Text;
			return BuildFlowLogList(cmd);
#endif
		}

		public static Flow.FlowLogList GetByPatientName(string patientName)
		{
#if MONGO
            return FlowGatewayMongo.GetByPatientName(patientName);
#else
			SqlCommand cmd = new SqlCommand();
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
#endif
		}

		public static Flow.FlowLogList GetByPathologistId(int pathologistId)
		{
#if MONGO
            return FlowGatewayMongo.GetByPathologistId(pathologistId);
#else
			SqlCommand cmd = new SqlCommand("select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.AccessionDate, pso.FinalDate, pso.PanelSetName [TestName], pso.ObjectId, pso.MasterAccessionNo " +
                "from tblPanelSetOrder pso join tblAccessionOrder ao on pso.MasterAccessionNo = ao.MasterAccessionNo " +
                "where dateadd(yy, +1, ao.AccessionDate) > getDate() and pso.AssignedToId = @PathologistId " +
				"order by ao.AccessionDate desc, pso.ReportNo desc");
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.Add("@PathologistId", SqlDbType.Int).Value = pathologistId;
			return BuildFlowLogList(cmd);
#endif
		}

		public static Flow.FlowMarkerCollection GetFlowMarkerCollectionByPanelId(string reportNo, int panelId)
		{
#if MONGO
            return FlowGatewayMongo.GetFlowMarkerCollectionByPanelId(reportNo, panelId);
#else
            Flow.FlowMarkerCollection result = new Flow.FlowMarkerCollection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select @ReportNo as ReportNo, mp.MarkerName as Name, mp.Intensity, mp.Interpretation, 'true' as MarkerUsed " +
                "from tblFlowMarkerPanel mp left outer join tblMarkers m on mp.MarkerName = m.MarkerName where PanelId = @PanelId " +
                "order by m.OrderFlag, m.MarkerName";
            cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
            cmd.Parameters.Add("@PanelId", SqlDbType.Int).Value = panelId;

            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
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
#endif
        }

        private static Flow.FlowLogList BuildFlowLogList(SqlCommand cmd)
		{
			Flow.FlowLogList result = new Flow.FlowLogList();
			using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (SqlDataReader dr = cmd.ExecuteReader())
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

		private static Flow.FlowMarkerCollection BuildFlowMarkerCollection(XElement sourceElement)
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
		}

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
