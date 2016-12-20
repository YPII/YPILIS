using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Persistence
{
    public class MaterialTrackingBatchDocumentBuilder : DocumentBuilder
    {
        private MySqlCommand m_SQLCommand;

        public MaterialTrackingBatchDocumentBuilder(string materialTrackingBatchId)
        {
            this.m_SQLCommand = new MySqlCommand();
            this.m_SQLCommand.CommandText = "Select * from tblMaterialTrackingBatch where MaterialTrackingBatchId = @MaterialTrackingBatchId";
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@MaterialTrackingBatchId", SqlDbType.VarChar).Value = materialTrackingBatchId;
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch = new MaterialTracking.Model.MaterialTrackingBatch();
            this.Build(materialTrackingBatch);
            return materialTrackingBatch;
        }

        private void Build(YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingBatch materialTrackingBatch)
        {
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                m_SQLCommand.Connection = cn;

                using (MySqlDataReader dr = m_SQLCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(materialTrackingBatch, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
        }
    }
}
