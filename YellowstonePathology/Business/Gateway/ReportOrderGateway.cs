using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class ReportOrderGateway
    {
        public static YellowstonePathology.Business.Test.PanelSetOrder GetReport(string reportNo, int panelSetId)
        {
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = null;
            switch (panelSetId)
            {
                case 51: //FISH Analysis
                    panelSetOrder = BuildReportOrderFishAnalysis(reportNo);
                    break;
                case 52: //Molecular Analysis
                    panelSetOrder = BuildReportOrderMolecularAnalysis(reportNo);
                    break;
                case 53: //Absolute CD4 Count
                    panelSetOrder = BuildReportOrderMolecularAnalysis(reportNo);
                    break;
            }
            return panelSetOrder;
        }

		public static void UpdateReportOrderAbsoluteCD4Count(YellowstonePathology.Business.Test.AbsoluteCD4Count.AbsoluteCD4CountTestOrder reportOrderAbsoluteCD4Count)
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
				cmd.CommandText = "Update tblMolecularAnalysisTestOrder set ReportNo = @ReportNo, CD3Result = @CD3Result, CD4Result = @CD4Result, CD8Result = @CD8Result, CD4CD8Ratio = @CD4CD8Ratio, Interpretation = @Interpretation where ReportOrderAbsoluteCD4CountId = @ReportOrderAbsoluteCD4CountId";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportOrderAbsoluteCD4Count.ReportNo;
                cmd.Parameters.Add("@CD3Result", SqlDbType.VarChar).Value = reportOrderAbsoluteCD4Count.CD3Result;
                cmd.Parameters.Add("@CD4Result", SqlDbType.VarChar).Value = reportOrderAbsoluteCD4Count.CD4Result;
                cmd.Parameters.Add("@CD8Result", SqlDbType.VarChar).Value = reportOrderAbsoluteCD4Count.CD8Result;
                cmd.Parameters.Add("@CD4CD8Ratio", SqlDbType.VarChar).Value = reportOrderAbsoluteCD4Count.CD4CD8Ratio;
                cmd.Parameters.Add("@Interpretation", SqlDbType.VarChar).Value = reportOrderAbsoluteCD4Count.Interpretation;
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

		private static YellowstonePathology.Business.Test.MolecularAnalysis.MolecularAnalysisTestOrder BuildReportOrderMolecularAnalysis(string reportNo)
        {
			YellowstonePathology.Business.Test.MolecularAnalysis.MolecularAnalysisTestOrder reportMolecularAnalysis = null;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Select * " +
                	"from tblMolecularAnalysisTestOrder ro " +
                    "join tblPanelSetOrder pso on rm.ReportNo = pso.ReportNo " +
	                "where ro.ReportNo = @ReportNo";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
                cmd.Connection = cn;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
						reportMolecularAnalysis = new YellowstonePathology.Business.Test.MolecularAnalysis.MolecularAnalysisTestOrder();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(reportMolecularAnalysis, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
            return reportMolecularAnalysis;
        }

		private static YellowstonePathology.Business.Test.FishAnalysis.FishAnalysisTestOrder BuildReportOrderFishAnalysis(string reportNo)
        {
			YellowstonePathology.Business.Test.FishAnalysis.FishAnalysisTestOrder reportFishAnalysis = null;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Select * " +
                    "from tblFishAnalysisTestOrder ro " +
                    "join tblPanelSetOrder pso on ro.ReportNo = pso.ReportNo " +
                    "where ro.ReportNo = @ReportNo";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
                cmd.Connection = cn;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
						reportFishAnalysis = new YellowstonePathology.Business.Test.FishAnalysis.FishAnalysisTestOrder();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(reportFishAnalysis, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
            return reportFishAnalysis;
        }

		private static YellowstonePathology.Business.Test.AbsoluteCD4Count.AbsoluteCD4CountTestOrder BuildReportOrderAbsoluteCD4Count(string reportNo)
        {
			YellowstonePathology.Business.Test.AbsoluteCD4Count.AbsoluteCD4CountTestOrder reportAbsoluteCD4Count = null;
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "Select * " +
                    "from tblAbsoluteCD4CountTestOrder ro " +
                    "join tblPanelSetOrder pso on ro.ReportNo = pso.ReportNo " +
                    "where ro.ReportNo = @ReportNo";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = reportNo;
                cmd.Connection = cn;

                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
						reportAbsoluteCD4Count = new YellowstonePathology.Business.Test.AbsoluteCD4Count.AbsoluteCD4CountTestOrder();
						YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(reportAbsoluteCD4Count, dr);
						sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
            return reportAbsoluteCD4Count;
        }
    }
}
