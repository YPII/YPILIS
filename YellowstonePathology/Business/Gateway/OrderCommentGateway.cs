using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class OrderCommentGateway
    {
		public static Domain.OrderCommentLogCollection GetOrderCommentLogCollectionByClientOrderId(string clientOrderId)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "gwOrderCommentsByClientOrderId_1;";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ClientOrderId", clientOrderId);
            Domain.OrderCommentLogCollection result = BuildOrderCommentLogCollection(cmd);
            return result;
		}

		public static Domain.OrderCommentLogCollection GetOrderCommentLogCollectionByMasterAccessionNo(string masterAccessionNo)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "gwOrderCommentsByMasterAccessionNo_1";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("MasterAccessionNo", masterAccessionNo);
            Domain.OrderCommentLogCollection result = BuildOrderCommentLogCollection(cmd);
            return result;
        }

        public static Domain.OrderCommentLogCollection GetOrderCommentsForSpecimenLogId(int specimenLogId)
		{
			MySqlCommand cmd = new MySqlCommand();
			cmd.CommandText = "SELECT * from tblOrderCommentLog where SpecimenLogId = @SpecimenLogId order by LogDate desc;";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("@SpecimenLogId", specimenLogId);
            Domain.OrderCommentLogCollection result = BuildOrderCommentLogCollection(cmd);
            return result;
        }

        private static YellowstonePathology.Business.Domain.OrderCommentLogCollection BuildOrderCommentLogCollection(MySqlCommand cmd)
        {
            Domain.OrderCommentLogCollection result = new Domain.OrderCommentLogCollection();
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Domain.OrderCommentLog orderCommentLog = new Domain.OrderCommentLog();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(orderCommentLog, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(orderCommentLog);
                    }
                }
            }
            return result;
        }

    }
}
