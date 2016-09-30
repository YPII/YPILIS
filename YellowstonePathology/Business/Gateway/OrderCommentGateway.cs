using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Gateway
{
	public class OrderCommentGateway
    {
		public static Domain.OrderCommentLogCollection GetOrderCommentLogCollectionByClientOrderId(string clientOrderId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "gwOrderCommentsByClientOrderId_1";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClientOrderId", System.Data.SqlDbType.VarChar).Value = clientOrderId;
            Domain.OrderCommentLogCollection result = BuildOrderCommentLogCollection(cmd);
            return result;
		}

		public static Domain.OrderCommentLogCollection GetOrderCommentLogCollectionByMasterAccessionNo(string masterAccessionNo)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "gwOrderCommentsByMasterAccessionNo_1";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@MasterAccessionNo", System.Data.SqlDbType.VarChar).Value = masterAccessionNo;
            Domain.OrderCommentLogCollection result = BuildOrderCommentLogCollection(cmd);
            return result;
        }

        public static Domain.OrderCommentLogCollection GetOrderCommentsForSpecimenLogId(int specimenLogId)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * from tblOrderCommentLog where SpecimenLogId = @SpecimenLogId order by LogDate desc";
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.Add("@SpecimenLogId", System.Data.SqlDbType.Int).Value = specimenLogId;
            Domain.OrderCommentLogCollection result = BuildOrderCommentLogCollection(cmd);
            return result;
        }

        public static Domain.OrderCommentLogCollection OrderCommentLogCollectionFromCaseNotesKeys(Domain.CaseNotesKeyCollection caseNotesKeyCollection)
        {
            Domain.OrderCommentLogCollection result = new Domain.OrderCommentLogCollection();
            foreach (Domain.CaseNotesKey caseNotesKey in caseNotesKeyCollection)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                switch (caseNotesKey.CaseNotesKeyName)
                {
                    case Domain.CaseNotesKeyNameEnum.MasterAccessionNo:
                        cmd.CommandText = "SELECT * from tblOrderCommentLog where MasterAccessionNo = '" + caseNotesKey.Key + "' order by LogDate";
                        break;
                    case Domain.CaseNotesKeyNameEnum.ClientOrderId:
                        cmd.CommandText = "SELECT * from tblOrderCommentLog where ClientOrderId = '" + caseNotesKey.Key + "' order by LogDate";
                        break;
                    case Domain.CaseNotesKeyNameEnum.ContainerId:
                        cmd.CommandText = "SELECT * from tblOrderCommentLog where ContainerId = '" + caseNotesKey.Key + "' order by LogDate";
                        break;
                    case Domain.CaseNotesKeyNameEnum.SpecimenLogId:
                        cmd.CommandText = "SELECT * from tblOrderCommentLog where SpecimenLogId = " + caseNotesKey.Key + " order by LogDate";
                        break;
                }

                Domain.OrderCommentLogCollection midCollection1 = BuildOrderCommentLogCollection(cmd);
                foreach (Domain.OrderCommentLog orderCommentLog in midCollection1)
                {
                    result.AddUnique(orderCommentLog);
                }
            }
            return result;
        }

        private static YellowstonePathology.Business.Domain.OrderCommentLogCollection BuildOrderCommentLogCollection(SqlCommand cmd)
        {
            Domain.OrderCommentLogCollection result = new Domain.OrderCommentLogCollection();
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
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
