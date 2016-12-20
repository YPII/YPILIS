using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
    public class ReportDistributionGateway
    {
        public static YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection GetReportDistributionCollectionByDateRangeTumorRegistry(DateTime startDate, DateTime endDate, string distributionType)
        {
            YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection result = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection();
            string sql = "Select * from tblTestOrderReportDistribution where TimeOfLastDistribution between @StartDate and @EndDate and DistributionType = @DistributionType";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
            cmd.Parameters.Add("@DistributionType", SqlDbType.VarChar).Value = distributionType;
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(testOrderReportDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(testOrderReportDistribution);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList GetPhysicianClientDistributionCollection(int physicianId, int clientId)
        {
            YellowstonePathology.Business.Client.Model.PhysicianClientDistributionList result = new Client.Model.PhysicianClientDistributionList();
            string sql = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
                "from tblPhysicianClient pc " +
	            "join tblPhysicianClientDistribution pcd on pc.PhysicianClientId = pcd.PhysicianClientId " +
	            "join tblPhysicianClient pc2 on pcd.DistributionId = pc2.PhysicianClientId " +
	            "join tblClient c on pc2.ClientId = c.ClientId " +
                "join tblPhysician ph on pc2.Physicianid = ph.PhysicianId " +
	            "where pc.ClientId = @ClientId and pc.PhysicianId = @PhysicianId";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physicianId;
            cmd.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;            
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string distributionType = dr["DistributionType"].ToString();
                        YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistribution = YellowstonePathology.Business.Client.Model.PhysicianClientDistributionFactory.GetPhysicianClientDistribution(distributionType);
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physicianClientDistribution, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(physicianClientDistribution);
                    }
                }
            }
            return result;
        }

        public static YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem GetPhysicianClientDistributionCollection(string physicianClientId)
        {
            YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem result = null;
            string sql = "Select c.ClientId, c.ClientName, ph.PhysicianId, ph.DisplayName [PhysicianName], c.DistributionType, c.Fax [FaxNumber], c.LongDistance " +
                "from tblPhysicianClient pc " +
                "join tblPhysicianClientDistribution pcd on pc.PhysicianClientId = pcd.PhysicianClientId " +
                "join tblPhysicianClient pc2 on pcd.DistributionId = pc2.PhysicianClientId " +
                "join tblClient c on pc2.ClientId = c.ClientId " +
                "join tblPhysician ph on pc2.Physicianid = ph.PhysicianId " +
                "where pc.PhysicianClientId = @PhysicianClientId";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = sql;
            cmd.Parameters.Add("@PhysicianClientId", SqlDbType.VarChar).Value = physicianClientId;            
            cmd.CommandType = CommandType.Text;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string distributionType = dr["DistributionType"].ToString();
                        result = YellowstonePathology.Business.Client.Model.PhysicianClientDistributionFactory.GetPhysicianClientDistribution(distributionType);
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(result, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();                        
                    }
                }
            }
            return result;
        }
    }
}
