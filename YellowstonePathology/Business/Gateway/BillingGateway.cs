using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class BillingGateway
	{
        public static void UpdateMRNACCT()
        {
            MySqlCommand cmd = new MySqlCommand("prcUpdateMRNACCT");
            cmd.CommandType = CommandType.StoredProcedure;            

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateAccessionBillingInformationFromSVHBillingData(DateTime fileDate)
        {
            MySqlCommand cmd = new MySqlCommand("pUpdateAccessionBillingInformationFromSVHBillingData");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("FileDate", fileDate);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }			        
	}
}
