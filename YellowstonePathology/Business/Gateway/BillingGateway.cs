using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class BillingGateway
	{
        public static void UpdateAccessionBillingInformationFromSVHBillingData(DateTime fileDate)
        {
            MySqlCommand cmd = new MySqlCommand("pUpdateAccessionBillingInformationFromSVHBillingData");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FileDate", SqlDbType.DateTime).Value = fileDate;

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }			        
	}
}
