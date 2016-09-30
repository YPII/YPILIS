using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Persistence
{
    public class MaterialTrackingBatchDocumentBuilder : DocumentBuilder
    {
        private SqlCommand m_SQLCommand;

        public MaterialTrackingBatchDocumentBuilder(string materialTrackingBatchId)
        {
            this.m_SQLCommand = new SqlCommand();
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
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                m_SQLCommand.Connection = cn;

                using (SqlDataReader dr = m_SQLCommand.ExecuteReader())
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
