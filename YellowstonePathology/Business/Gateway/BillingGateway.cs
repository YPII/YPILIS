using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class BillingGateway
	{
        public static void UpdateAccessionBillingInformationFromSVHBillingData(DateTime fileDate)
        {
            SqlCommand cmd = new SqlCommand("pUpdateAccessionBillingInformationFromSVHBillingData");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FileDate", SqlDbType.DateTime).Value = fileDate;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }			        
	}
}
