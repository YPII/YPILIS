using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Persistence
{
    public class ProviderDocumentBuilder : DocumentBuilder
    {
        private SqlCommand m_SQLCommand;

        public ProviderDocumentBuilder(int physicianId)
        {
            this.m_SQLCommand = new SqlCommand();
            this.m_SQLCommand.CommandText = "select * From tblPhysician where PhysicianId = @PhysicianId";
            this.m_SQLCommand.CommandType = CommandType.Text;
            this.m_SQLCommand.Parameters.Add("@PhysicianId", SqlDbType.Int).Value = physicianId;
        }

        public override object BuildNew()
        {
            YellowstonePathology.Business.Domain.Physician physician = new Domain.Physician();
            this.Build(physician);
            return physician;
        }

        private void Build(YellowstonePathology.Business.Domain.Physician physician)
        {
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.ProductionConnectionString))
            {
                cn.Open();
                m_SQLCommand.Connection = cn;

                using (SqlDataReader dr = m_SQLCommand.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Persistence.SqlDataReaderPropertyWriter(physician, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                    }
                }
            }
        }
    }
}
