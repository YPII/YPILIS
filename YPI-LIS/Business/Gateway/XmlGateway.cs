using System;
using System.Xml.Linq;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class XmlGateway
    {
		public static XElement GetOrderComments(string masterAccessionNo)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "Select * from tblOrderCommentLog where MasterAccessionNo = @MasterAccessionNo;";
			cmd.CommandType = CommandType.Text;
			cmd.Parameters.AddWithValue("@MasterAccessionNo", masterAccessionNo);

			XElement result = new XElement("OrderComments");
			using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
			{
				cn.Open();
				cmd.Connection = cn;
				using (MySqlDataReader dr = cmd.ExecuteReader())
				{
					while (dr.Read())
					{
						YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog = new Domain.OrderCommentLog();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(orderCommentLog, dr);
						propertyWriter.WriteProperties();
                        XElement comment = new XElement("OrderComment");
                        YellowstonePathology.Business.Persistence.XmlPropertyReader xmlPropertyReader = new Persistence.XmlPropertyReader(orderCommentLog, comment);

						xmlPropertyReader.Write();

						result.Add(comment);
					}
				}
			}

			return result;
		}

        public static YellowstonePathology.Business.XPSDocument.Result.Data.PlacentalPathologyQuestionnaireDataV2 GetPlacentalPathologyQuestionnaire1(string clientOrderId, object writer)
        {
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = Business.Persistence.DocumentGateway.Instance.PullClientOrder(clientOrderId, writer);
            YellowstonePathology.Business.XPSDocument.Result.Data.PlacentalPathologyQuestionnaireDataV2 result = new YellowstonePathology.Business.XPSDocument.Result.Data.PlacentalPathologyQuestionnaireDataV2(clientOrder);
            return result;
        }

        public static YellowstonePathology.Business.Reports.LabOrderSheetData GetOrdersToAcknowledge(string panelOrderIds)
        {
            Reports.LabOrderSheetData result = new Reports.LabOrderSheetData();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "pGetXmlOrdersToAcknowledge_A2";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("PanelOrderIdString", panelOrderIds);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while(dr.Read())
                    {
                        Reports.LabOrderSheetDataReport labOrderSheetDataReport = new Reports.LabOrderSheetDataReport();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(labOrderSheetDataReport, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(labOrderSheetDataReport);
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        Reports.LabOrderSheetDataPanelOrder labOrderSheetDataPanelOrder = new Reports.LabOrderSheetDataPanelOrder();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(labOrderSheetDataPanelOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach(Reports.LabOrderSheetDataReport labOrderSheetDataReport in result)
                        {
                            if (labOrderSheetDataReport.ReportNo == labOrderSheetDataPanelOrder.ReportNo)
                            {
                                labOrderSheetDataReport.LabOrderSheetDataPanelOrders.Add(labOrderSheetDataPanelOrder);
                                break;
                            }
                        }
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        Reports.LabOrderSheetDataTestOrder labOrderSheetDataTestOrder = new Reports.LabOrderSheetDataTestOrder();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(labOrderSheetDataTestOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach (Reports.LabOrderSheetDataReport labOrderSheetDataReport in result)
                        {
                            bool added = false;
                            foreach(Reports.LabOrderSheetDataPanelOrder labOrderSheetDataPanelOrder in labOrderSheetDataReport.LabOrderSheetDataPanelOrders)
                            {
                                if (labOrderSheetDataPanelOrder.PanelOrderId == labOrderSheetDataTestOrder.PanelOrderId)
                                {
                                    labOrderSheetDataPanelOrder.LabOrderSheetDataTestOrders.Add(labOrderSheetDataTestOrder);
                                    added = true;
                                    break;
                                }
                            }
                            if (added == true) break;
                        }
                    }
                }
            }
            return result;
        }

        public static Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataV2 GetAccessionOrderDataSheetData(string masterAccessionNo)
        {
            Test.AccessionOrder accessionOrder = GetAccessionOrder(masterAccessionNo);
            ClientOrder.Model.ClientOrderCollection clientOrderCollection = Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(masterAccessionNo);
            Domain.OrderCommentLogCollection orderCommentLogCollection = Gateway.OrderCommentGateway.GetOrderCommentLogCollectionByMasterAccessionNo(masterAccessionNo);
            Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataV2 accessionOrderDataSheetData = new Business.XPSDocument.Result.Data.AccessionOrderDataSheetDataV2(accessionOrder, clientOrderCollection, orderCommentLogCollection);
            return accessionOrderDataSheetData;
        }

        public static Test.AccessionOrder GetAccessionOrder(string masterAccessionNo)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "Select * from tblAccessionOrder where MasterAccessionno = @MasterAccessionNo; " +
                "Select * from tblSpecimenOrder where MasterAccessionNo = @MasterAccessionNo order by SpecimenNumber;";
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MasterAccessionNo", masterAccessionNo);

            YellowstonePathology.Business.Test.AccessionOrder result = new Test.AccessionOrder();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(result, dr);
                        propertyWriter.WriteProperties();
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        Specimen.Model.SpecimenOrder specimenOrder = new Specimen.Model.SpecimenOrder();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter propertyWriter = new YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter(specimenOrder, dr);
                        propertyWriter.WriteProperties();
                        result.SpecimenOrderCollection.Add(specimenOrder);
                    }
                }
            }

            return result;
        }

        public static XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData GetClientBillingDetailReport(DateTime postDateStart, DateTime postDateEnd, string clientGroupId)
        {
            XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData result = new XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prcGetClientBillingDetailReport_2";
            cmd.Parameters.AddWithValue("StartDate", postDateStart);
            cmd.Parameters.AddWithValue("EndDate", postDateEnd);
            cmd.Parameters.AddWithValue("ClientGroupId", clientGroupId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder = new XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientBillingDetailReportDataAccessionOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(clientBillingDetailReportDataAccessionOrder);
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport = new XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientBillingDetailReportDataReport, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach(XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder in result)
                        {
                            if (clientBillingDetailReportDataReport.MasterAccessionNo == clientBillingDetailReportDataAccessionOrder.MasterAccessionNo)
                            {
                                clientBillingDetailReportDataAccessionOrder.ClientBillingDetailReportDataReports.Add(clientBillingDetailReportDataReport);
                                break;
                            }

                        }
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        Test.PanelSetOrderCPTCode panelSetOrderCPTCode = new Test.PanelSetOrderCPTCode();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrderCPTCode, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach (XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder in result)
                        {
                            bool added = false;
                            foreach(XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport in clientBillingDetailReportDataAccessionOrder.ClientBillingDetailReportDataReports)
                            if (panelSetOrderCPTCode.ReportNo == clientBillingDetailReportDataReport.ReportNo)
                            {
                                clientBillingDetailReportDataReport.PanelSetOrderCPTCodes.Add(panelSetOrderCPTCode);
                                added = true;
                                break;
                            }
                            if (added) break;
                        }
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = new Test.PanelSetOrderCPTCodeBill();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrderCPTCodeBill, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach (XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder in result)
                        {
                            bool added = false;
                            foreach (XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport in clientBillingDetailReportDataAccessionOrder.ClientBillingDetailReportDataReports)
                                if (panelSetOrderCPTCodeBill.ReportNo == clientBillingDetailReportDataReport.ReportNo)
                                {
                                    clientBillingDetailReportDataReport.PanelSetOrderCPTCodeBills.Add(panelSetOrderCPTCodeBill);
                                    added = true;
                                    break;
                                }
                            if (added) break;
                        }
                    }
                }
            }

            return result;
        }

        public static XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData GetClientBillingDetailReportSVHANumbers(DateTime postDateStart, DateTime postDateEnd, string clientGroupId)
        {
            XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData result = new XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportData();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "prcGetClientBillingDetailReportSVHANumbers_2";
            cmd.Parameters.AddWithValue("StartDate", postDateStart);
            cmd.Parameters.AddWithValue("EndDate", postDateEnd);
            cmd.Parameters.AddWithValue("ClientGroupId", clientGroupId);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder = new XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientBillingDetailReportDataAccessionOrder, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(clientBillingDetailReportDataAccessionOrder);
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport = new XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(clientBillingDetailReportDataReport, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach (XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder in result)
                        {
                            if (clientBillingDetailReportDataReport.MasterAccessionNo == clientBillingDetailReportDataAccessionOrder.MasterAccessionNo)
                            {
                                clientBillingDetailReportDataAccessionOrder.ClientBillingDetailReportDataReports.Add(clientBillingDetailReportDataReport);
                                break;
                            }

                        }
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        Test.PanelSetOrderCPTCode panelSetOrderCPTCode = new Test.PanelSetOrderCPTCode();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrderCPTCode, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach (XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder in result)
                        {
                            bool added = false;
                            foreach (XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport in clientBillingDetailReportDataAccessionOrder.ClientBillingDetailReportDataReports)
                                if (panelSetOrderCPTCode.ReportNo == clientBillingDetailReportDataReport.ReportNo)
                                {
                                    clientBillingDetailReportDataReport.PanelSetOrderCPTCodes.Add(panelSetOrderCPTCode);
                                    added = true;
                                    break;
                                }
                            if (added) break;
                        }
                    }
                    dr.NextResult();

                    while (dr.Read())
                    {
                        Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill = new Test.PanelSetOrderCPTCodeBill();
                        Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(panelSetOrderCPTCodeBill, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        foreach (XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataAccessionOrder clientBillingDetailReportDataAccessionOrder in result)
                        {
                            bool added = false;
                            foreach (XPSDocument.Result.ClientBillingDetailReportResult.ClientBillingDetailReportDataReport clientBillingDetailReportDataReport in clientBillingDetailReportDataAccessionOrder.ClientBillingDetailReportDataReports)
                                if (panelSetOrderCPTCodeBill.ReportNo == clientBillingDetailReportDataReport.ReportNo)
                                {
                                    clientBillingDetailReportDataReport.PanelSetOrderCPTCodeBills.Add(panelSetOrderCPTCodeBill);
                                    added = true;
                                    break;
                                }
                            if (added) break;
                        }
                    }
                }
            }

            return result;
        }

    }
}
